using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
namespace Negocio
{
    public class CategoriaNegocio
    {
        public List<Categoria> listar()
        {
            AccesoDatos datos = new AccesoDatos();
            List<Categoria> listaCat = new List<Categoria>();
            try
            {
                datos.setearConsulta("SELECT Id, Descripcion FROM CATEGORIAS");
                datos.ejecutarLectura();
                while (datos.Reader.Read())
                {
                    Categoria auxiliar = new Categoria();
                    auxiliar.Id = (int)datos.Reader["Id"];
                    auxiliar.Descripcion = (string)datos.Reader["Descripcion"];
                    listaCat.Add(auxiliar);
                }
                return listaCat;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
