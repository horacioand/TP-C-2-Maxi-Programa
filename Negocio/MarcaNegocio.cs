using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
namespace Negocio
{
    public class MarcaNegocio
    {
        public List<Marca> listar()
        {
            AccesoDatos datos = new AccesoDatos();
            List<Marca> listaMarca = new List<Marca>();
            try
            {
                datos.setearConsulta("SELECT Id, Descripcion FROM CATEGORIAS");
                datos.ejecutarLectura();
                while (datos.Reader.Read())
                {
                    Marca auxiliar = new Marca();
                    auxiliar.Id = (int)datos.Reader["Id"];
                    auxiliar.Descripcion = (string)datos.Reader["Descripcion"];
                    listaMarca.Add(auxiliar);
                }
                return listaMarca;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
