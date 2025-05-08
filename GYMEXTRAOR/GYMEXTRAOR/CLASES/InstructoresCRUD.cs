using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace GYMEXTRAOR.CLASES
{
    internal class InstructoresCRUD
    {
        private static string connectionString = "Server=DESKTOP-P5L5BPG\\SQLEXPRESS01;Database=GYM;Integrated Security=True;TrustServerCertificate=True;";

        public static void Menu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("==============================================");
                Console.WriteLine("          Gestión de Instructores");
                Console.WriteLine("==============================================");
                Console.WriteLine("Seleccione una opción:");
                Console.WriteLine("1. Agregar Instructor");
                Console.WriteLine("2. Listar Instructores");
                Console.WriteLine("3. Actualizar Instructor");
                Console.WriteLine("4. Eliminar Instructor");
                Console.WriteLine("0. Volver al Menú Principal");

                Console.Write("Opción: ");
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        AgregarInstructor();
                        break;
                    case "2":
                        ListarInstructores();
                        break;
                    case "3":
                        ActualizarInstructor();
                        break;
                    case "4":
                        EliminarInstructor();
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

        private static void AgregarInstructor()
        {
            Console.WriteLine("\nIngrese el nombre del instructor:");
            string nombre = Console.ReadLine();
            Console.WriteLine("Ingrese el apellido del instructor:");
            string apellido = Console.ReadLine();
            Console.WriteLine("Ingrese el email del instructor:");
            string email = Console.ReadLine();
            Console.WriteLine("Ingrese el teléfono del instructor:");
            string telefono = Console.ReadLine();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Instructores (Nombre, Apellido, Email, Telefono) VALUES (@Nombre, @Apellido, @Email, @Telefono)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Nombre", nombre);
                command.Parameters.AddWithValue("@Apellido", apellido);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Telefono", telefono);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                    Console.WriteLine("\nInstructor agregado exitosamente.");
                else
                    Console.WriteLine("\nError al agregar el instructor.");
            }
        }

        private static void ListarInstructores()
        {
            Console.WriteLine("\nLista de Instructores:");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Id, Nombre, Apellido, Email, Telefono FROM Instructores";
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"ID: {reader["Id"]}, Nombre: {reader["Nombre"]} {reader["Apellido"]}, Email: {reader["Email"]}, Teléfono: {reader["Telefono"]}");
                    }
                }
                else
                {
                    Console.WriteLine("No hay instructores registrados.");
                }

                reader.Close();
            }
        }

        private static void ActualizarInstructor()
        {
            Console.WriteLine("\nIngrese el ID del instructor a actualizar:");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Ingrese el nuevo nombre:");
                string nuevoNombre = Console.ReadLine();
                while (string.IsNullOrWhiteSpace(nuevoNombre))
                {
                    Console.WriteLine("El nombre no puede estar vacío. Por favor, ingrese un nombre válido:");
                    nuevoNombre = Console.ReadLine();
                }

                Console.WriteLine("Ingrese el nuevo apellido:");
                string nuevoApellido = Console.ReadLine();
                while (string.IsNullOrWhiteSpace(nuevoApellido))
                {
                    Console.WriteLine("El apellido no puede estar vacío. Por favor, ingrese un apellido válido:");
                    nuevoApellido = Console.ReadLine();
                }

                Console.WriteLine("Ingrese el nuevo email:");
                string nuevoEmail = Console.ReadLine();
                while (string.IsNullOrWhiteSpace(nuevoEmail))
                {
                    Console.WriteLine("El email no puede estar vacío. Por favor, ingrese un email válido:");
                    nuevoEmail = Console.ReadLine();
                }

                Console.WriteLine("Ingrese el nuevo teléfono:");
                string nuevoTelefono = Console.ReadLine();
                while (string.IsNullOrWhiteSpace(nuevoTelefono))
                {
                    Console.WriteLine("El teléfono no puede estar vacío. Por favor, ingrese un teléfono válido:");
                    nuevoTelefono = Console.ReadLine();
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Instructores SET Nombre = @Nombre, Apellido = @Apellido, Email = @Email, Telefono = @Telefono WHERE Id = @Id";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@Nombre", nuevoNombre);
                    command.Parameters.AddWithValue("@Apellido", nuevoApellido);
                    command.Parameters.AddWithValue("@Email", nuevoEmail);
                    command.Parameters.AddWithValue("@Telefono", nuevoTelefono);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                        Console.WriteLine("\nInstructor actualizado exitosamente.");
                    else
                        Console.WriteLine("\nInstructor no encontrado.");
                }
            }
            else
            {
                Console.WriteLine("\nID inválido.");
            }
        }

        private static void EliminarInstructor()
        {
            Console.WriteLine("\nIngrese el ID del instructor a eliminar:");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Instructores WHERE Id = @Id";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", id);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                        Console.WriteLine("\nInstructor eliminado exitosamente.");
                    else
                        Console.WriteLine("\nInstructor no encontrado.");
                }
            }
            else
            {
                Console.WriteLine("\nID inválido.");
            }
        }
    }
}
