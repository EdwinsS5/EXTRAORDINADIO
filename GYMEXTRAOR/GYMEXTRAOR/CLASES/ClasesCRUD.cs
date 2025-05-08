using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;


namespace GYMEXTRAOR.CLASES
{
    internal class ClasesCRUD
    {
        private static string connectionString = "Server=DESKTOP-P5L5BPG\\SQLEXPRESS01;Database=GYM;Integrated Security=True;TrustServerCertificate=True;";

        public static void Menu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("==============================================");
                Console.WriteLine("          Gestión de Clases");
                Console.WriteLine("==============================================");
                Console.WriteLine("Seleccione una opción:");
                Console.WriteLine("1. Agregar Clase");
                Console.WriteLine("2. Listar Clases");
                Console.WriteLine("3. Actualizar Clase");
                Console.WriteLine("4. Eliminar Clase");
                Console.WriteLine("0. Volver al Menú Principal");

                Console.Write("Opción: ");
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        AgregarClase();
                        break;
                    case "2":
                        ListarClases();
                        break;
                    case "3":
                        ActualizarClase();
                        break;
                    case "4":
                        EliminarClase();
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

        private static void AgregarClase()
        {
            Console.WriteLine("\nIngrese el nombre de la clase:");
            string nombre = Console.ReadLine();
            Console.WriteLine("Ingrese la descripción de la clase:");
            string descripcion = Console.ReadLine();
            Console.WriteLine("Ingrese la duración de la clase (en minutos):");
            if (int.TryParse(Console.ReadLine(), out int duracion))
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Clases (Nombre, Descripcion, Duracion) VALUES (@Nombre, @Descripcion, @Duracion)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Nombre", nombre);
                    command.Parameters.AddWithValue("@Descripcion", descripcion);
                    command.Parameters.AddWithValue("@Duracion", duracion);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                        Console.WriteLine("\nClase agregada exitosamente.");
                    else
                        Console.WriteLine("\nError al agregar la clase.");
                }
            }
            else
            {
                Console.WriteLine("\nDuración inválida. No se pudo agregar la clase.");
            }
        }

        private static void ListarClases()
        {
            Console.WriteLine("\nLista de Clases:");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Id, Nombre, Descripcion, Duracion FROM Clases";
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"ID: {reader["Id"]}, Nombre: {reader["Nombre"]}, Descripción: {reader["Descripcion"]}, Duración: {reader["Duracion"]} minutos");
                    }
                }
                else
                {
                    Console.WriteLine("No hay clases registradas.");
                }

                reader.Close();
            }
        }

        private static void ActualizarClase()
        {
            Console.WriteLine("\nIngrese el ID de la clase a actualizar:");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Ingrese el nuevo nombre:");
                string nuevoNombre = Console.ReadLine();
                while (string.IsNullOrWhiteSpace(nuevoNombre))
                {
                    Console.WriteLine("El nombre no puede estar vacío. Por favor, ingrese un nombre válido:");
                    nuevoNombre = Console.ReadLine();
                }

                Console.WriteLine("Ingrese la nueva descripción:");
                string nuevaDescripcion = Console.ReadLine();
                while (string.IsNullOrWhiteSpace(nuevaDescripcion))
                {
                    Console.WriteLine("La descripción no puede estar vacía. Por favor, ingrese una descripción válida:");
                    nuevaDescripcion = Console.ReadLine();
                }

                Console.WriteLine("Ingrese la nueva duración (en minutos):");
                string nuevaDuracionInput = Console.ReadLine();
                int nuevaDuracion;
                while (!int.TryParse(nuevaDuracionInput, out nuevaDuracion) || nuevaDuracion <= 0)
                {
                    Console.WriteLine("La duración debe ser un número válido y mayor a 0. Por favor, ingrese una duración válida:");
                    nuevaDuracionInput = Console.ReadLine();
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Clases SET Nombre = @Nombre, Descripcion = @Descripcion, Duracion = @Duracion WHERE Id = @Id";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@Nombre", nuevoNombre);
                    command.Parameters.AddWithValue("@Descripcion", nuevaDescripcion);
                    command.Parameters.AddWithValue("@Duracion", nuevaDuracion);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                        Console.WriteLine("\nClase actualizada exitosamente.");
                    else
                        Console.WriteLine("\nClase no encontrada.");
                }
            }
            else
            {
                Console.WriteLine("\nID inválido.");
            }
        }

        private static void EliminarClase()
        {
            Console.WriteLine("\nIngrese el ID de la clase a eliminar:");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Clases WHERE Id = @Id";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", id);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                        Console.WriteLine("\nClase eliminada exitosamente.");
                    else
                        Console.WriteLine("\nClase no encontrada.");
                }
            }
            else
            {
                Console.WriteLine("\nID inválido.");
            }
        }
    }
}
