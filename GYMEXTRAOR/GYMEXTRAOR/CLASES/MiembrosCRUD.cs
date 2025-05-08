using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace GYMEXTRAOR.CLASES
{
    internal class MiembrosCRUD
    {

        private static string connectionString = "Server=DESKTOP-P5L5BPG\\SQLEXPRESS01;Database=GYM;Integrated Security=True; TrustServerCertificate=True; ";

        public static void Menu()
        {
            Console.WriteLine("Gestión de Miembros:");
            Console.WriteLine("1. Crear Miembro");
            Console.WriteLine("2. Leer Miembros");
            Console.WriteLine("3. Actualizar Miembro");
            Console.WriteLine("4. Eliminar Miembro");
            Console.WriteLine("0. Volver al Menú Principal");

            string opcion = Console.ReadLine();
            switch (opcion)
            {
                case "1":
                    CrearMiembro();
                    break;
                case "2":
                    LeerMiembros();
                    break;
                case "3":
                    ActualizarMiembro();
                    break;
                case "4":
                    EliminarMiembro();
                    break;
                case "0":
                    Console.WriteLine("Volviendo al menú principal...");
                    break;
                default:
                    Console.WriteLine("Opción no válida.");
                    break;
            }
        }

        public static void CrearMiembro()
        {
            Console.WriteLine("Ingrese el nombre del miembro:");
            string nombre = Console.ReadLine();

            Console.WriteLine("Ingrese el apellido del miembro:");
            string apellido = Console.ReadLine();

            Console.WriteLine("Ingrese la fecha de nacimiento (YYYY-MM-DD):");
            string fechaNacimiento = Console.ReadLine();

            Console.WriteLine("Ingrese el email:");
            string email = Console.ReadLine();

            Console.WriteLine("Ingrese el teléfono:");
            string telefono = Console.ReadLine();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Miembros (Nombre, Apellido, FechaNacimiento, Email, Telefono) VALUES (@Nombre, @Apellido, @FechaNacimiento, @Email, @Telefono)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Nombre", nombre);
                command.Parameters.AddWithValue("@Apellido", apellido);
                command.Parameters.AddWithValue("@FechaNacimiento", fechaNacimiento);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Telefono", telefono);

                connection.Open();
                int result = command.ExecuteNonQuery();

                connection.Open();
                command.ExecuteNonQuery();
                Console.WriteLine("Miembro creado exitosamente.");
            }
        }

        public static void LeerMiembros()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Miembros";
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine($"ID: {reader["ID_Miembro"]}, Nombre: {reader["Nombre"]}, Apellido: {reader["Apellido"]}, Fecha Nacimiento: {reader["FechaNacimiento"]}, Email: {reader["Email"]}, Teléfono: {reader["Telefono"]}");
                }
            }
        }

        public static void ActualizarMiembro()
        {
            Console.WriteLine("Ingrese el ID del miembro a actualizar:");
            int id = int.Parse(Console.ReadLine());

            Console.WriteLine("Ingrese el nuevo nombre del miembro:");
            string nombre = Console.ReadLine();

            Console.WriteLine("Ingrese el nuevo apellido del miembro:");
            string apellido = Console.ReadLine();

            Console.WriteLine("Ingrese el nuevo email:");
            string email = Console.ReadLine();

            Console.WriteLine("Ingrese el nuevo teléfono:");
            string telefono = Console.ReadLine();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Miembros SET Nombre = @Nombre, Apellido = @Apellido, Email = @Email, Telefono = @Telefono WHERE ID_Miembro = @ID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ID", id);
                command.Parameters.AddWithValue("@Nombre", nombre);
                command.Parameters.AddWithValue("@Apellido", apellido);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Telefono", telefono);

                connection.Open();
                command.ExecuteNonQuery();
                Console.WriteLine("Miembro actualizado exitosamente.");
            }
        }

        public static void EliminarMiembro()
        {
            Console.WriteLine("Ingrese el ID del miembro a eliminar:");
            int id = int.Parse(Console.ReadLine());

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Miembros WHERE ID_Miembro = @ID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ID", id);

                connection.Open();
                command.ExecuteNonQuery();
                Console.WriteLine("Miembro eliminado exitosamente.");
            }
        }

    }
}
