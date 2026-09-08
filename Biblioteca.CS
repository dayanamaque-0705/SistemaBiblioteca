using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemaBiblioteca
{
    public class Biblioteca
    {
        private List<Libro> libros = new List<Libro>();
        private List<Usuario> usuarios = new List<Usuario>();
        private List<Prestamo> prestamos = new List<Prestamo>();

 
        // LIBROS
        public void RegistrarLibro(string titulo, string autor, string categoria, string codigo)
        {
            // LINQ: Any para verificar duplicados
            if (libros.Any(l => l.Codigo == codigo))
            {
                throw new InvalidOperationException($"el libro con codigo '{codigo}' ya esta registrado");
            }

            Libro nuevoLibro = new Libro(titulo, autor, categoria, codigo, true);
            libros.Add(nuevoLibro);
        }

        public IEnumerable<Libro> obtenerLibrosDisponibles()
        {
            // LINQ: Where para filtrar solo los disponibles
            return libros.Where(lib => lib.Disponible);
        }

        public IEnumerable<Libro> buscarLibros(string filtro)
        {
            // LINQ: Where para filtrar por Autor o Categoría
            return libros.Where(lib => lib.Autor.Contains(filtro, StringComparison.OrdinalIgnoreCase) || 
                                       lib.Categoria.Contains(filtro, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<Libro> OrdenarLibrosPorTitulo()
        {
            // LINQ: OrderBy para ordenar alfabéticamente
            return libros.OrderBy(l => l.Titulo);
        }

        public Libro BuscarLibroCodigo(string codigo)
        {
            // LINQ: FirstOrDefault para buscar por código único
            Libro? libroencontrado = libros.FirstOrDefault(lib => lib.Codigo == codigo);

            if (libroencontrado == null)
            {
                throw new KeyNotFoundException($"el libro con codigo '{codigo}' no esta registrado");
            }

            return libroencontrado;
        }

        public void EliminarLibro(string codigo)
        {
            Libro libroEliminar = BuscarLibroCodigo(codigo);
            libros.Remove(libroEliminar);
        }

        // USUARIOS
        
        public void RegistrarUsuario(string id, string nombre, string correo)
        {
            // LINQ: Any para verificar ID duplicado
            if (usuarios.Any(usu => usu.Id == id))
            {
                throw new InvalidOperationException($"el usuario con id '{id}' ya esta registrado");
            }

            Usuario nuevoUsuario = new Usuario(id, nombre, correo);
            usuarios.Add(nuevoUsuario);
        }

        public IEnumerable<Usuario> ObtenerUsuarios()
        {
            return usuarios;
        }

        public Usuario BuscarUsuario(string id)
        {
            // LINQ: FirstOrDefault para buscar usuario por ID
            Usuario? usuarioEncontrado = usuarios.FirstOrDefault(usu => usu.Id == id);

            if (usuarioEncontrado == null)
            {
                throw new KeyNotFoundException($"el usuario con id '{id}' no esta registrado");
            }

            return usuarioEncontrado;
        }

        // PRÉSTAMOS
        public void RegistrarPrestamo(string codigoLibro, string idUsuario)
        {
            Libro libro = BuscarLibroCodigo(codigoLibro);
            Usuario usuario = BuscarUsuario(idUsuario);

            libro.Prestar();

            prestamos.Add(new Prestamo(codigoLibro, idUsuario, DateTime.Now));
        }

        public void RegistrarDevolucion(string codigoLibro)
        {
            // LINQ: FirstOrDefault para encontrar el préstamo activo
            Prestamo? prestamoActivo = prestamos.FirstOrDefault(p => p.CodigoLibro == codigoLibro);

            if (prestamoActivo == null)
            {
                throw new InvalidOperationException($"No existe un registro de préstamo activo para el código '{codigoLibro}'.");
            }

            Libro libro = BuscarLibroCodigo(codigoLibro);
            libro.Devolver();

            prestamos.Remove(prestamoActivo);
        }

        public IEnumerable<object> ObtenerPrestamosActivos()
        {
            // LINQ: Select para mostrar lista de préstamos activos con detalles
            return prestamos.Select(p => new 
            { 
                Codigo = p.CodigoLibro, 
                Usuario = p.IdUsuario, 
                Fecha = p.FechaPrestamo.ToString("dd/MM/yyyy HH:mm") 
            });
        }
    }
}