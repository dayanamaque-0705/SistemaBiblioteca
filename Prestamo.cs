using System;

namespace SistemaBiblioteca
{
    public record Prestamo(string CodigoLibro, string IdUsuario, DateTime FechaPrestamo);
}