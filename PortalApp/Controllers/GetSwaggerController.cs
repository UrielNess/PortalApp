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

// password obfuscatio on connection string
// take all sql connection


namespace PortalApp.Controllers
{
    public class GetSwaggerController : ApiController
    {
        public IHttpActionResult getswaggerdetails()
        {
            try
            { 
                List<swaggerClass> ec = new List<swaggerClass>();
                string mainconn = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
                MySqlConnection sqlconn = new MySqlConnection(mainconn);
                // extracting API uuid from query param in URL
                string uuidParam = HttpContext.Current.Request.QueryString.Get("uuid");
                // performing regex validation on uuid param to make sure there is no sql injection inside
                if (guidClass.isGuid(uuidParam) == false)
                {
                    throw new Exception("Failed due to non valid uuid param");
                }
                
                string sqlQuery = "SELECT convert(content using utf8mb4) as content_converted FROM API_ASSET where API_ASSET.API_UUID = '" + uuidParam + "' ";
                sqlconn.Open();
                MySqlCommand sqlcomm = new MySqlCommand(sqlQuery, sqlconn);
                MySqlDataReader sdr = sqlcomm.ExecuteReader();
                while (sdr.Read())
                {
                    ec.Add(new swaggerClass()
                    {
                        swagger = sdr.GetValue(0).ToString(),
                        //string st1 = System.Text.Encoding.UTF8.GetString()

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

