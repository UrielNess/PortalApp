using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MySql.Data.MySqlClient;
using PortalApp.Models;
using System.Configuration;

namespace PortalApp.Controllers
{
    public class InitialScreenController : ApiController
    {
       
                public IHttpActionResult Getservicedetails()
        {
            try
            {
                // access portal external db and execute query from view to fetch data
                List<serviceClass> ec = new List<serviceClass>();
                string mainconn = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
                MySqlConnection sqlconn = new MySqlConnection(mainconn);
                string sqlQuery = "select * from initialScreen";
                sqlconn.Open();
                MySqlCommand sqlcomm = new MySqlCommand(sqlQuery, sqlconn);
                MySqlDataReader sdr = sqlcomm.ExecuteReader();
                while (sdr.Read())
                {

                    ec.Add(new serviceClass()
                    {
                        uuid = sdr.GetValue(0).ToString(),
                        name = sdr.GetValue(1).ToString(),
                        portal_status = sdr.GetValue(2).ToString(),
                        tenant_id = sdr.GetValue(3).ToString(),
                        ssg_service_type = sdr.GetValue(4).ToString(),
                        metadata_tags = sdr.GetValue(5).ToString(),
                        total_consumers = sdr.GetValue(6).ToString(),
                        hebrewServiceName = sdr.GetValue(7).ToString(),
                        officeName = sdr.GetValue(8).ToString(),
                        databaseName = sdr.GetValue(9).ToString(),
                        subject = sdr.GetValue(10).ToString(),
                        serviceType = sdr.GetValue(11).ToString(),
                        version = sdr.GetValue(12).ToString(),
                        publisherType = sdr.GetValue(13).ToString(),
                        description = sdr.GetValue(14).ToString(),

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


