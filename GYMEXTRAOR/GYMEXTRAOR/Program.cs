
namespace GYMEXTRAOR.CLASES
{
    internal static class Program
    {
       
        static void Main()
        {
            Console.WriteLine("Inicio del programa"); // Depuración
            bool salir = false;

            while (!salir)
            {
                

            
            Console.WriteLine("==============================================");
            Console.WriteLine("          Sistema de Gestión de Gimnasio");
            Console.WriteLine("==============================================");
            Console.WriteLine("Seleccione una opción:");
            Console.WriteLine("1. Gestionar Miembros");
            Console.WriteLine("2. Gestionar Membresías");
            Console.WriteLine("3. Gestionar Clases");
            Console.WriteLine("4. Gestionar Instructores");
            Console.WriteLine("5. Gestionar Reservas");
            Console.WriteLine("0. Salir");


            Console.Write("Opcion: ");
            string opcion = Console.ReadLine();
                switch (opcion)
                {
                    case "1":
                        MiembrosCRUD.Menu();
                        break;
                    case "2":
                        MembresiasCRUD.Menu();
                        break;
                    case "3":
                        ClasesCRUD.Menu();
                        break;
                    case "4":
                        InstructoresCRUD.Menu();
                        break;
                    case "5":
                        ReservasCRUD.Menu();
                        break;
                    case "0":
                        Console.WriteLine("Saliendo del sistema...");
                        break;
                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }
                if (!salir)
                {
                    Console.WriteLine("\nPresiona cualquier tecla para volver al menú principal...");
                    Console.ReadLine();
                }

               

            }
        }
    }
}