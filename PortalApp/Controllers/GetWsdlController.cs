using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Net.Http;
using System.Web.Http;
using MySql.Data.MySqlClient;
using PortalApp.Models;
using System.Configuration;

namespace PortalApp.Controllers
{
    public class GetWsdlController : ApiController
    {
        public IHttpActionResult getwsdldetails()
        {
            try
            { 
                List<wsdlClass> ec = new List<wsdlClass>();
                string mainconn = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
                MySqlConnection sqlconn = new MySqlConnection(mainconn);
                // extracting API uuid from query param in URL
                string uuidParam = HttpContext.Current.Request.QueryString.Get("uuid");
                // performing regex validation on uuid param to make sure there is no sql injection inside
                if (guidClass.isGuid(uuidParam) == false)
                {
                    throw new Exception("Failed due to non valid uuid param");
                }
                string sqlQuery = "SELECT convert(MARKDOWN using utf8mb4) markdown_coverted FROM MARKDOWN_ASSET where TYPE_UUID = '" + uuidParam + "' AND MARKDOWN_ASSET.TITLE = 'wsdl_doc' ";
                sqlconn.Open();
                MySqlCommand sqlcomm = new MySqlCommand(sqlQuery, sqlconn);
                MySqlDataReader sdr = sqlcomm.ExecuteReader();
                while (sdr.Read())
                {
                    ec.Add(new wsdlClass()
                    {
                        wsdl = sdr.GetValue(0).ToString(),

                    });
                }
                sqlconn.Close();
                return Ok(ec);
        }
        catch (Exception ex)
            {

                throw (ex);
            }
}
           
        }
    }
