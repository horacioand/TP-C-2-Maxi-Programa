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
                    if (!(datos.Reader["Codigo"] is DBNull))
                        auxiliar.CodigoArticulo = (string)datos.Reader["Codigo"];
                    if (!(datos.Reader["Nombre"] is DBNull))
                        auxiliar.Nombre = (string)datos.Reader["Nombre"];
                    auxiliar.Descripcion = (string)datos.Reader["Descripcion"];
                    if (!(datos.Reader["ImagenUrl"] is DBNull))
                        auxiliar.Imagen = (string)datos.Reader["ImagenUrl"];
                    if (!(datos.Reader["Precio"] is DBNull))
                        auxiliar.Precio = (decimal)datos.Reader["Precio"];
                    auxiliar.Marca = new Marca();
                    if (!(datos.Reader["idMarca"] is DBNull))
                        auxiliar.Marca.Id = (int)datos.Reader["idMarca"];
                    if (!(datos.Reader["Marca"] is DBNull))
                        auxiliar.Marca.Descripcion = (string)datos.Reader["Marca"];
                    auxiliar.Categoria = new Categoria();
                    if (!(datos.Reader["idCategoria"] is DBNull))
                        auxiliar.Categoria.Id = (int)datos.Reader["idCategoria"];
                    if (!(datos.Reader["Categoria"] is DBNull))
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
        public void agregarArticulo(Articulo articulo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO ARTICULOS (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, ImagenUrl, Precio) VALUES (@codigo,@nombre,@descripcion,@idmarca,@idcat,@imagen,@precio)");
                datos.setearParametro("@codigo",articulo.CodigoArticulo);
                datos.setearParametro("@nombre",articulo.Nombre);
                datos.setearParametro("@descripcion",articulo.Descripcion);
                datos.setearParametro("@idmarca",articulo.Marca.Id);
                datos.setearParametro("@idcat",articulo.Categoria.Id);
                datos.setearParametro("@imagen", articulo.Imagen);
                datos.setearParametro("@precio",articulo.Precio);
                datos.ejecutarAccion();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void modificarArticulo(Articulo articulo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE ARTICULOS SET Codigo = @codigo, Nombre = @nombre, Descripcion = @descripcion, IdMarca = @marca, IdCategoria = @cat, ImagenUrl = @imagen, Precio = @precio WHERE Id = @id");
                datos.setearParametro("@codigo", articulo.CodigoArticulo);
                datos.setearParametro("@nombre", articulo.Nombre);
                datos.setearParametro("@descripcion", articulo.Descripcion);
                datos.setearParametro("@marca", articulo.Marca.Id);
                datos.setearParametro("@cat", articulo.Categoria.Id);
                datos.setearParametro("@imagen", articulo.Imagen);
                datos.setearParametro("@precio", articulo.Precio);
                datos.setearParametro("@id", articulo.Id);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally { datos.cerrarConexion(); }
        }
        public void eliminarFisico(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("DELETE FROM ARTICULOS WHERE ID = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarAccion();
            }
            catch (Exception)
            {

                throw;
            }
            finally{ datos.cerrarConexion(); };
        }
    }
}
