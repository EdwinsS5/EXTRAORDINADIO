using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace GYMEXTRAOR.CLASES
{
    internal class MembresiasCRUD
    {
        private static string connectionString = "Server=DESKTOP-P5L5BPG\\SQLEXPRESS01;Database=GYM;Integrated Security=True; TrustServerCertificate=True; ";

        public static void Menu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("==============================================");
                Console.WriteLine("          Gestión de Membresías");
                Console.WriteLine("==============================================");
                Console.WriteLine("Seleccione una opción:");
                Console.WriteLine("1. Agregar Membresía");
                Console.WriteLine("2. Listar Membresías");
                Console.WriteLine("3. Actualizar Membresía");
                Console.WriteLine("4. Eliminar Membresía");
                Console.WriteLine("0. Volver al Menú Principal");

                Console.Write("Opción: ");
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        AgregarMembresia();
                        break;
                    case "2":
                        ListarMembresias();
                        break;
                    case "3":
                        ActualizarMembresia();
                        break;
                    case "4":
                        EliminarMembresia();
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

        private static void AgregarMembresia()
        {
            Console.WriteLine("\nIngrese el nombre de la membresía:");
            string nombre = Console.ReadLine();
            Console.WriteLine("Ingrese la descripción de la membresía:");
            string descripcion = Console.ReadLine();
            Console.WriteLine("Ingrese el precio de la membresía:");
            if (decimal.TryParse(Console.ReadLine(), out decimal precio))
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Membresias (Nombre, Descripcion, Precio) VALUES (@Nombre, @Descripcion, @Precio)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Nombre", nombre);
                    command.Parameters.AddWithValue("@Descripcion", descripcion);
                    command.Parameters.AddWithValue("@Precio", precio);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                        Console.WriteLine("\nMembresía agregada exitosamente.");
                    else
                        Console.WriteLine("\nError al agregar la membresía.");
                }
            }
            else
            {
                Console.WriteLine("\nPrecio inválido. No se pudo agregar la membresía.");
            }
        }

        private static void ListarMembresias()
        {
            Console.WriteLine("\nLista de Membresías:");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Id, Nombre, Descripcion, Precio FROM Membresias";
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"ID: {reader["Id"]}, Nombre: {reader["Nombre"]}, Descripción: {reader["Descripcion"]}, Precio: {reader["Precio"]:C}");
                    }
                }
                else
                {
                    Console.WriteLine("No hay membresías registradas.");
                }

                reader.Close();
            }
        }

        private static void ActualizarMembresia()
        {
            Console.WriteLine("\nIngrese el ID de la membresía a actualizar:");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Ingrese el nuevo nombre:");
                string nuevoNombre = Console.ReadLine();
                Console.WriteLine("Ingrese la nueva descripción:");
                string nuevaDescripcion = Console.ReadLine();
                Console.WriteLine("Ingrese el nuevo precio:");
                string nuevaEntradaPrecio = Console.ReadLine();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Membresias SET Nombre = ISNULL(NULLIF(@Nombre, ''), Nombre), " +
                                   "Descripcion = ISNULL(NULLIF(@Descripcion, ''), Descripcion), " +
                                   "Precio = ISNULL(NULLIF(@Precio, ''), Precio) WHERE Id = @Id";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@Nombre", string.IsNullOrWhiteSpace(nuevoNombre) ? DBNull.Value : nuevoNombre);
                    command.Parameters.AddWithValue("@Descripcion", string.IsNullOrWhiteSpace(nuevaDescripcion) ? DBNull.Value : nuevaDescripcion);
                    command.Parameters.AddWithValue("@Precio", string.IsNullOrWhiteSpace(nuevaEntradaPrecio) ? DBNull.Value : (object)decimal.Parse(nuevaEntradaPrecio));

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                        Console.WriteLine("\nMembresía actualizada exitosamente.");
                    else
                        Console.WriteLine("\nMembresía no encontrada.");
                }
            }
            else
            {
                Console.WriteLine("\nID inválido.");
            }
        }
        private static void EliminarMembresia()
        {
            Console.WriteLine("\nIngrese el ID de la membresía a eliminar:");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Membresias WHERE Id = @Id";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", id);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                        Console.WriteLine("\nMembresía eliminada exitosamente.");
                    else
                        Console.WriteLine("\nMembresía no encontrada.");
                }
            }
            else
            {
                Console.WriteLine("\nID inválido.");
            }
        }




    }
}
