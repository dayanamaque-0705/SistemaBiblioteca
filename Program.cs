using System;
using System.Collections.Generic;
using System.Linq; // Agregado para usar .ToList()
using SistemaBiblioteca;

Biblioteca biblioteca = new Biblioteca();
bool salir = false;

while (!salir)
{
    Console.Clear();
    Console.WriteLine("========================================");
    Console.WriteLine("  Gestión de la  Biblioteca        ");
    Console.WriteLine("========================================");
    Console.WriteLine("1. Registrar un nuevo libro");
    Console.WriteLine("2. Registrar un nuevo usuario");
    Console.WriteLine("3. Ver libros (ordenados por título)");
    Console.WriteLine("4. Buscar libros (por autor o categoría)");
    Console.WriteLine("5. Ver qué libros están disponibles");
    Console.WriteLine("6. Realizar un préstamo");
    Console.WriteLine("7. Registrar la devolución de un libro");
    Console.WriteLine("8. Ver préstamos activos en este momento");
    Console.WriteLine("9. Eliminar un libro");
    Console.WriteLine("10. Salir del sistema");
    Console.WriteLine("========================================");
    Console.Write("¿Ingrese la opción? (1-10): ");

    string opcion = Console.ReadLine() ?? "";

    try
    {
        switch (opcion)
        {
            case "1":
                Console.WriteLine("\n--- Agregar un nuevo libro ---");
                Console.Write("¿Cuál es el título?: ");
                string titulo = Console.ReadLine() ?? "";
                Console.Write("¿Quién es el autor?: ");
                string autor = Console.ReadLine() ?? "";
                Console.Write("¿A qué categoría pertenece?: ");
                string categoria = Console.ReadLine() ?? "";
                Console.Write("Ingresa su código único: ");
                string codigo = Console.ReadLine() ?? "";

                biblioteca.RegistrarLibro(titulo, autor, categoria, codigo);
                Console.WriteLine("\nEl libro se guardó correctamente.");
                break;

            case "2":
                Console.WriteLine("\n--- Registrar nuevo usuario ---");
                Console.Write("Ingresa su número de ID: ");
                string id = Console.ReadLine() ?? "";
                Console.Write("Nombre completo: ");
                string nombre = Console.ReadLine() ?? "";
                Console.Write("Correo electrónico: ");
                string correo = Console.ReadLine() ?? "";

                biblioteca.RegistrarUsuario(id, nombre, correo);
                Console.WriteLine("\n¡Genial! El usuario ya quedó registrado.");
                break;

            case "3":
                // LINQ: .ToList() 
                var ordenados = biblioteca.OrdenarLibrosPorTitulo().ToList();
                Console.WriteLine("\n--- Catálogo de libros ---");
                
                if (ordenados.Count == 0)
                {
                    Console.WriteLine("Aún no tenemos libros registrados en el catálogo.");
                }
                else
                {
                    foreach (var l in ordenados)
                    {
                        string estado = l.Disponible ? "Disponible" : "Prestado";
                        Console.WriteLine($"- [{l.Codigo}] \"{l.Titulo}\" de {l.Autor} | Estado: {estado}");
                    }
                }
                break;

            case "4":
                Console.Write("\n¿Qué autor o categoría estás buscando?: ");
                string filtro = Console.ReadLine() ?? "";
                
                // LINQ: .ToList() directo
                var resultados = biblioteca.buscarLibros(filtro).ToList();
                Console.WriteLine($"\n--- Resultados para '{filtro}' ---");
                
                if (resultados.Count == 0)
                {
                    Console.WriteLine("No encontramos ningún libro que coincida con tu búsqueda.");
                }
                else
                {
                    foreach (var l in resultados)
                    {
                        Console.WriteLine($"- [{l.Codigo}] \"{l.Titulo}\" ({l.Autor}) - Categoría: {l.Categoria}");
                    }
                }
                break;

            case "5":
                // LINQ: .ToList()
                var disponibles = biblioteca.obtenerLibrosDisponibles().ToList();
                Console.WriteLine("\n--- Libros listos para prestar ---");
                
                if (disponibles.Count == 0)
                {
                    Console.WriteLine("En este momento no hay ningún libro disponible.");
                }
                else
                {
                    foreach (var l in disponibles)
                    {
                        Console.WriteLine($"- [{l.Codigo}] {l.Titulo}");
                    }
                }
                break;

            case "6":
                Console.WriteLine("\n--- Generar préstamo ---");
                Console.Write("Código del libro a prestar: ");
                string codLibro = Console.ReadLine() ?? "";
                Console.Write("ID del usuario que lo solicita: ");
                string idUser = Console.ReadLine() ?? "";

                biblioteca.RegistrarPrestamo(codLibro, idUser);
                Console.WriteLine("\n¡Perfecto! El préstamo fue registrado sin problemas.");
                break;

            case "7":
                Console.WriteLine("\n--- Devolución de libro ---");
                Console.Write("Ingresa el código del libro que están devolviendo: ");
                string codDev = Console.ReadLine() ?? "";

                biblioteca.RegistrarDevolucion(codDev);
                Console.WriteLine("\n¡Excelente! Se ha registrado la devolución.");
                break;

            case "8":
                // LINQ: .ToList() 
                var activos = biblioteca.ObtenerPrestamosActivos().ToList();
                Console.WriteLine("\n--- Préstamos que están en curso ---");
                
                if (activos.Count == 0)
                {
                    Console.WriteLine("No hay préstamos pendientes en este momento.");
                }
                else
                {
                    foreach (var p in activos)
                    {
                        Console.WriteLine(p);
                    }
                }
                break;

            case "9":
                Console.WriteLine("\n--- Eliminar libro ---");
                Console.Write("Ingresa el código del libro que deseas retirar: ");
                string codDel = Console.ReadLine() ?? "";

                biblioteca.EliminarLibro(codDel);
                Console.WriteLine("\nEl libro fue retirado del sistema correctamente.");
                break;

            case "10":
                salir = true;
                Console.WriteLine("\nSaliendo del sistema...");
                break;

            default:
                Console.WriteLine("\nEsa opción no existe en el menú. Por favor, elige un número del 1 al 10.");
                break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("\nOcurrió un detalle al procesar la solicitud: " + ex.Message);
    }

    if (!salir)
    {
        Console.WriteLine("\nPresiona cualquier tecla para regresar al menú...");
        Console.ReadKey();
    }
}