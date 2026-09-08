using System;
using System.Collections;
using System.Collections.Generic;
using SistemaBiblioteca;

Biblioteca biblioteca = new Biblioteca();
bool salir = false;

while (!salir)
{
    Console.Clear();
    Console.WriteLine("========================================");
    Console.WriteLine("    SISTEMA DE GESTIÓN DE BIBLIOTECA    ");
    Console.WriteLine("========================================");
    Console.WriteLine("1. Registrar libro");
    Console.WriteLine("2. Registrar usuario");
    Console.WriteLine("3. Listar libros ordenados por título");
    Console.WriteLine("4. Buscar libros por autor o categoría");
    Console.WriteLine("5. Consultar libros disponibles");
    Console.WriteLine("6. Registrar préstamo");
    Console.WriteLine("7. Registrar devolución");
    Console.WriteLine("8. Consultar préstamos activos");
    Console.WriteLine("9. Eliminar libro");
    Console.WriteLine("10. Salir");
    Console.WriteLine("========================================");
    Console.Write("Seleccione una opción: ");

    string opcion = Console.ReadLine();
    if (opcion == null)
    {
        opcion = "";
    }

    try
    {
        switch (opcion)
        {
            case "1":
                Console.Write("Título: ");
                string titulo = Console.ReadLine();
                Console.Write("Autor: ");
                string autor = Console.ReadLine();
                Console.Write("Categoría: ");
                string categoria = Console.ReadLine();
                Console.Write("Código único: ");
                string codigo = Console.ReadLine();
                biblioteca.RegistrarLibro(titulo, autor, categoria, codigo);
                Console.WriteLine("\n[Éxito] Libro registrado correctamente.");
                break;

            case "2":
                Console.Write("ID de usuario: ");
                string id = Console.ReadLine();
                Console.Write("Nombre: ");
                string nombre = Console.ReadLine();
                Console.Write("Correo: ");
                string correo = Console.ReadLine();
                biblioteca.RegistrarUsuario(id, nombre, correo);
                Console.WriteLine("\n[Éxito] Usuario registrado correctamente.");
                break;

            case "3":
                List<Libro> ordenados = new List<Libro>();
                foreach (Libro l in biblioteca.OrdenarLibrosPorTitulo())
                {
                    ordenados.Add(l);
                }
                Console.WriteLine("\n--- LIBROS ORDENADOS POR TÍTULO ---");
                if (ordenados.Count == 0)
                {
                    Console.WriteLine("No hay libros registrados.");
                }
                foreach (Libro l in ordenados)
                {
                    Console.WriteLine("- [" + l.Codigo + "] " + l.Titulo + " | Autor: " + l.Autor + " | Disponible: " + l.Disponible);
                }
                break;

            case "4":
                Console.Write("Ingrese autor o categoría a buscar: ");
                string filtro = Console.ReadLine();
                List<Libro> resultados = new List<Libro>();
                foreach (Libro l in biblioteca.buscarLibros(filtro))
                {
                    resultados.Add(l);
                }
                Console.WriteLine("\n--- RESULTADOS DE BÚSQUEDA ---");
                if (resultados.Count == 0)
                {
                    Console.WriteLine("No se encontraron resultados.");
                }
                foreach (Libro l in resultados)
                {
                    Console.WriteLine("- [" + l.Codigo + "] " + l.Titulo + " (" + l.Autor + ") - Categoría: " + l.Categoria);
                }
                break;

            case "5":
                List<Libro> disponibles = new List<Libro>();
                foreach (Libro l in biblioteca.obtenerLibrosDisponibles())
                {
                    disponibles.Add(l);
                }
                Console.WriteLine("\n--- LIBROS DISPONIBLES ---");
                if (disponibles.Count == 0)
                {
                    Console.WriteLine("No hay libros disponibles.");
                }
                foreach (Libro l in disponibles)
                {
                    Console.WriteLine("- [" + l.Codigo + "] " + l.Titulo);
                }
                break;

            case "6":
                Console.Write("Código del libro: ");
                string codLibro = Console.ReadLine();
                Console.Write("ID del usuario: ");
                string idUser = Console.ReadLine();
                biblioteca.RegistrarPrestamo(codLibro, idUser);
                Console.WriteLine("\n[Éxito] Préstamo registrado correctamente.");
                break;

            case "7":
                Console.Write("Código del libro a devolver: ");
                string codDev = Console.ReadLine();
                biblioteca.RegistrarDevolucion(codDev);
                Console.WriteLine("\n[Éxito] Devolución registrada correctamente.");
                break;

            case "8":
                List<object> activos = new List<object>();
                foreach (object p in biblioteca.ObtenerPrestamosActivos())
                {
                    activos.Add(p);
                }
                Console.WriteLine("\n--- PRÉSTAMOS ACTIVOS ---");
                if (activos.Count == 0)
                {
                    Console.WriteLine("No hay préstamos activos.");
                }
                foreach (object p in activos)
                {
                    Console.WriteLine(p);
                }
                break;

            case "9":
                Console.Write("Código del libro a eliminar: ");
                string codDel = Console.ReadLine();
                biblioteca.EliminarLibro(codDel);
                Console.WriteLine("\n[Éxito] Libro eliminado correctamente.");
                break;

            case "10":
                salir = true;
                Console.WriteLine("\nSaliendo del programa...");
                break;

            default:
                Console.WriteLine("\n[Opción inválida] Por favor, ingrese un número del 1 al 10.");
                break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("\n[Error Controlado]: " + ex.Message);
    }

    if (!salir)
    {
        Console.WriteLine("\nPresione cualquier tecla para continuar...");
        Console.ReadKey();
    }
}