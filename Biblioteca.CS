using System;
using System.Collections.Generic;

namespace SistemaBiblioteca{
    public class Biblioteca{
        private List<Libro> libros = new List<Libro>();
        private List<Usuario> usuarios = new List<Usuario>();
        private List<Prestamo> prestamos = new List<Prestamo>();
        //metodos libros
        public void RegistrarLibro(string titulo, string autor, string categoria, string codigo){
            bool existe = false;
            foreach (var l in libros) {
                if (l.Codigo == codigo) {
                    existe = true;
                    break;
                }
            }

            if (existe) {
                throw new InvalidOperationException($"el libro con codigo '{codigo}' ya esta registrado");
            }
            Libro nuevoLibro = new Libro(titulo, autor, categoria, codigo, true);
            libros.Add(nuevoLibro);
        }

        public IEnumerable<Libro> obtenerLibrosDisponibles(){
            List<Libro> disponibles = new List<Libro>();
            foreach (var lib in libros) {
                if (lib.Disponible) {
                    disponibles.Add(lib);
                }
            }
            return disponibles;
        }

        public IEnumerable<Libro> buscarLibros(string filtro){
            List<Libro> resultado = new List<Libro>();
            foreach (var lib in libros) {
                if (lib.Autor.Contains(filtro, StringComparison.OrdinalIgnoreCase) || 
                    lib.Categoria.Contains(filtro, StringComparison.OrdinalIgnoreCase)) {
                    resultado.Add(lib);
                }
            }
            return resultado;
        }

        public IEnumerable<Libro> OrdenarLibrosPorTitulo(){
            List<Libro> listaOrdenada = new List<Libro>(libros);
            listaOrdenada.Sort((l1, l2) => string.Compare(l1.Titulo, l2.Titulo, StringComparison.OrdinalIgnoreCase));
            return listaOrdenada;
        }

        public Libro BuscarLibroCodigo(string codigo){
            Libro libroencontrado = null;
            foreach (var lib in libros) {
                if (lib.Codigo == codigo) {
                    libroencontrado = lib;
                    break;
                }
            }

            if(libroencontrado == null){
                throw new KeyNotFoundException($"el libro con codigo '{codigo}' no esta registrado");
            }
            return libroencontrado;
        }

        public void EliminarLibro(string codigo){
            Libro libroEliminar = BuscarLibroCodigo(codigo);
            libros.Remove(libroEliminar);
        }

        //metodos usuarios
        public void RegistrarUsuario(string id, string nombre, string correo){
            bool existe = false;
            foreach (var usu in usuarios) {
                if (usu.Id == id) {
                    existe = true;
                    break;
                }
            }

            if (existe) {
                throw new InvalidOperationException($"el usuario con id '{id}' ya esta registrado");
            }
            Usuario nuevoUsuario = new Usuario(id, nombre, correo);
            usuarios.Add(nuevoUsuario);
        }

        public IEnumerable<Usuario> ObtenerUsuarios(){
            return usuarios;
        }

        public Usuario BuscarUsuario(string id){
            Usuario usuarioEncontrado = null;
            foreach (var usu in usuarios) {
                if (usu.Id == id) {
                    usuarioEncontrado = usu;
                    break;
                }
            }

            if(usuarioEncontrado == null){
                throw new KeyNotFoundException($"el usuario con id '{id}' no esta registrado");
            }
            return usuarioEncontrado;
        }
        ///prestamos
        public void RegistrarPrestamo(string codigoLibro, string idUsuario)
        {
            Libro libro = BuscarLibroCodigo(codigoLibro);
            Usuario usuario = BuscarUsuario(idUsuario);

            libro.Prestar();

            prestamos.Add(new Prestamo(codigoLibro, idUsuario, DateTime.Now));
        }

        public void RegistrarDevolucion(string codigoLibro)
        {
            Prestamo prestamoActivo = null;
            foreach (var p in prestamos)
            {
                if (p.CodigoLibro == codigoLibro)
                {
                    prestamoActivo = p;
                    break;
                }
            }

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
            List<object> resultado = new List<object>();
            foreach (var p in prestamos)
            {
                resultado.Add(new 
                { 
                    Codigo = p.CodigoLibro, 
                    Usuario = p.IdUsuario, 
                    Fecha = p.FechaPrestamo.ToString("dd/MM/yyyy HH:mm") 
                });
            }
            return resultado;
        }
       
    }
}