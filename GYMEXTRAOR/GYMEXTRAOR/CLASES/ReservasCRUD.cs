using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace GYMEXTRAOR.CLASES
{
    internal class ReservasCRUD
    {
        private static string connectionString = "Server=DESKTOP-P5L5BPG\\SQLEXPRESS01;Database=GYM;Integrated Security=True;TrustServerCertificate=True;";

        public static void Menu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("==============================================");
                Console.WriteLine("          Gestión de Reservas");
                Console.WriteLine("==============================================");
                Console.WriteLine("Seleccione una opción:");
                Console.WriteLine("1. Agregar Reserva");
                Console.WriteLine("2. Listar Reservas");
                Console.WriteLine("3. Actualizar Reserva");
                Console.WriteLine("4. Eliminar Reserva");
                Console.WriteLine("0. Volver al Menú Principal");

                Console.Write("Opción: ");
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        AgregarReserva();
                        break;
                    case "2":
                        ListarReservas();
                        break;
                    case "3":
                        ActualizarReserva();
                        break;
                    case "4":
                        EliminarReserva();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }

                Console.WriteLine("\nPresiona cualquier tecla para continuar...");
                Console.ReadKey();
            }
        }

        private static void AgregarReserva()
        {
            Console.WriteLine("\nIngrese el ID del miembro:");
            if (int.TryParse(Console.ReadLine(), out int miembroId))
            {
                Console.WriteLine("Ingrese el ID de la clase:");
                if (int.TryParse(Console.ReadLine(), out int claseId))
                {
                    Console.WriteLine("Ingrese la fecha de la reserva (YYYY-MM-DD):");
                    string fechaReserva = Console.ReadLine();

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        string query = "INSERT INTO Reservas (MiembroId, ClaseId, FechaReserva) VALUES (@MiembroId, @ClaseId, @FechaReserva)";
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@MiembroId", miembroId);
                        command.Parameters.AddWithValue("@ClaseId", claseId);
                        command.Parameters.AddWithValue("@FechaReserva", fechaReserva);

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                            Console.WriteLine("\nReserva agregada exitosamente.");
                        else
                            Console.WriteLine("\nError al agregar la reserva.");
                    }
                }
                else
                {
                    Console.WriteLine("\nID de la clase inválido.");
                }
            }
            else
            {
                Console.WriteLine("\nID del miembro inválido.");
            }
        }

        private static void ListarReservas()
        {
            Console.WriteLine("\nLista de Reservas:");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT R.Id, R.MiembroId, R.ClaseId, R.FechaReserva, M.Nombre AS MiembroNombre, C.Nombre AS ClaseNombre " +
                               "FROM Reservas R " +
                               "INNER JOIN Miembros M ON R.MiembroId = M.Id " +
                               "INNER JOIN Clases C ON R.ClaseId = C.Id";
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"ID: {reader["Id"]}, Miembro: {reader["MiembroNombre"]}, Clase: {reader["ClaseNombre"]}, Fecha: {reader["FechaReserva"]}");
                    }
                }
                else
                {
                    Console.WriteLine("No hay reservas registradas.");
                }

                reader.Close();
            }
        }

        private static void ActualizarReserva()
        {
            Console.WriteLine("\nIngrese el ID de la reserva a actualizar:");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Ingrese el nuevo ID del miembro:");
                if (int.TryParse(Console.ReadLine(), out int nuevoMiembroId))
                {
                    Console.WriteLine("Ingrese el nuevo ID de la clase:");
                    if (int.TryParse(Console.ReadLine(), out int nuevaClaseId))
                    {
                        Console.WriteLine("Ingrese la nueva fecha de la reserva (YYYY-MM-DD):");
                        string nuevaFechaReserva = Console.ReadLine();

                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            string query = "UPDATE Reservas SET MiembroId = @MiembroId, ClaseId = @ClaseId, FechaReserva = @FechaReserva WHERE Id = @Id";

                            SqlCommand command = new SqlCommand(query, connection);
                            command.Parameters.AddWithValue("@Id", id);
                            command.Parameters.AddWithValue("@MiembroId", nuevoMiembroId);
                            command.Parameters.AddWithValue("@ClaseId", nuevaClaseId);
                            command.Parameters.AddWithValue("@FechaReserva", nuevaFechaReserva);

                            connection.Open();
                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                                Console.WriteLine("\nReserva actualizada exitosamente.");
                            else
                                Console.WriteLine("\nReserva no encontrada.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nID de la clase inválido.");
                    }
                }
                else
                {
                    Console.WriteLine("\nID del miembro inválido.");
                }
            }
            else
            {
                Console.WriteLine("\nID inválido.");
            }
        }

        private static void EliminarReserva()
        {
            Console.WriteLine("\nIngrese el ID de la reserva a eliminar:");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Reservas WHERE Id = @Id";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", id);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                        Console.WriteLine("\nReserva eliminada exitosamente.");
                    else
                        Console.WriteLine("\nReserva no encontrada.");
                }
            }
            else
            {
                Console.WriteLine("\nID inválido.");
            }
        }
    }
}
