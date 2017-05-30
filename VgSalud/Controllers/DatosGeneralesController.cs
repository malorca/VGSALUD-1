using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using VgSalud.Models; 
namespace VgSalud.Controllers
{
    public class DatosGeneralesController : Controller
    {


        public List<E_Datos_Generales> listadatogenerales()
        {
            List<E_Datos_Generales> Lista = new List<E_Datos_Generales>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["VG_SALUD"].ConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("USP_LISTA_GENERALES", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            E_Datos_Generales datos = new E_Datos_Generales();

                            datos.igv = dr.GetDecimal(0);
                            datos.Tipo_Cambio = dr.GetDecimal(1);
                            datos.MUESTRA_ANTECEDENTE = dr.GetBoolean(2);
                            datos.ATENCIONESPAGADAS = dr.GetBoolean(3);

                            Lista.Add(datos);
                        }
                        con.Close();
                    }

                }
                return Lista;
            }
        }


        public ActionResult ListarDatosGenerales() {
            return View(listadatogenerales()); 

        }

        public ActionResult ModificarDatos(string igv , string tipo) {
            var lista = (from x in listadatogenerales() where x.igv == Convert.ToDecimal(igv) && x.Tipo_Cambio == Convert.ToDecimal(tipo)  select x).FirstOrDefault(); 
            return View(lista); 
        }

        [HttpPost]
        public ActionResult ModificarDatos(E_Datos_Generales dat)
        {
           using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["VG_SALUD"].ConnectionString.ToString()))
            {
                con.Open();
                using (SqlCommand da = new SqlCommand("USP_DATOS_GENERALES", con))
                {
                    try
                    {
                        da.CommandType = CommandType.StoredProcedure;
                        da.Parameters.AddWithValue("@IGV", dat.igv);
                        da.Parameters.AddWithValue("@TIPO_CAMBIO",dat.Tipo_Cambio);
                        da.Parameters.AddWithValue("@MUESTRA_ANTECEDENTE", dat.MUESTRA_ANTECEDENTE);
                        da.Parameters.AddWithValue("@ATENCIONESPAGADAS", dat.ATENCIONESPAGADAS);

                        da.ExecuteNonQuery();

                    }
                    catch (Exception e)
                    {

                    }
                    finally { con.Close(); }
                }

            }
            return RedirectToAction("ListarDatosGenerales");
        }



    }
}