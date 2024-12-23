using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
namespace Negocio
{
    public class ArticuloNegocio
    {
        public List<Articulo> listar()
        {
            AccesoDatos datos = new AccesoDatos();
            List<Articulo> listaArticulos = new List<Articulo>();
            try
            {
                datos.setearConsulta("SELECT A.Id, Codigo, Nombre, A.Descripcion, M.Id AS idMarca, M.Descripcion AS Marca, c.Id AS idCategoria, C.Descripcion AS Categoria,ImagenUrl, Precio FROM ARTICULOS A, MARCAS M, CATEGORIAS C WHERE IdMarca = M.Id AND IdCategoria = C.Id");
                datos.ejecutarLectura();
                while (datos.Reader.Read())
                {
                    Articulo auxiliar = new Articulo();
                    auxiliar.Id = (int)datos.Reader["Id"];
                    auxiliar.CodigoArticulo = (string)datos.Reader["Codigo"];
                    auxiliar.Nombre = (string)datos.Reader["Nombre"];
                    auxiliar.Descripcion = (string)datos.Reader["Descripcion"];
                    auxiliar.Imagen = (string)datos.Reader["ImagenUrl"];
                    auxiliar.Precio = (decimal)datos.Reader["Precio"];
                    auxiliar.Marca = new Marca();
                    auxiliar.Marca.Id = (int)datos.Reader["idMarca"];
                    auxiliar.Marca.Descripcion = (string)datos.Reader["Marca"];
                    auxiliar.Categoria = new Categoria();
                    auxiliar.Categoria.Id = (int)datos.Reader["idCategoria"];
                    auxiliar.Categoria.Descripcion = (string)datos.Reader["Categoria"];
                    listaArticulos.Add(auxiliar);
                }
                return listaArticulos;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}
