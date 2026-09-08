using System;
namespace SistemaBiblioteca
{
    public class Libro:IPrestable{
        public string Titulo {get;set;}
        public string Autor{get;set;}
        public string  Categoria{get; set;}
        public string Codigo {get; set;}
        public bool Disponible{get;set;}
        public Libro(string titulo, string autor,string categoria,string codigo ,bool disponible){
            Titulo=titulo;
             Autor=autor;
            Categoria=categoria;
           Codigo=codigo;
            Disponible=true;
        }

        public void Prestar(){
            if(!Disponible){
                throw new InvalidOperationException($"el libro '{Titulo}'no esta di8sponible");
            }
            Disponible=false;
        }
        public void Devolver(){
            Disponible=true;

        }

    }
}