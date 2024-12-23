using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    //Los datos mínimos con los que deberá contar el artículo son los siguientes:

    //Código de artículo.
    //Nombre.
    //Descripción.
    //Marca(seleccionable de una lista desplegable).
    //Categoría(seleccionable de una lista desplegable.
    //Imagen.
    //Precio.
    public class Articulo
    {
        public int Id;
        public string CodigoArticulo;
        public string Nombre;
        public string Descripcion;
        public Marca Marca;
        public Categoria Categoria;
        public string Imagen;
        public float Precio;
    }
}
