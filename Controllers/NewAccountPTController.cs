using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Web;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.IO;
using Microsoft.Extensions.Configuration;
using System.Net.Mime;
using Newtonsoft.Json;

namespace Farmerbrothers.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class NewAccountPTController : ControllerBase
    {

        private IConfiguration Configuration;

        public NewAccountPTController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        public class Result
        {
            public string errorDesc;
            public int code;
        }

        public class Product
        {
            public List<ProductSearchResponse> items { get; set; }
            public List<Coffee_Eqp_Charges> coffeeEquipChg { get; set; }

        }        

        public class ProductSearchRequest
        {
            public string ItemNumber;            
        }

        public class ProductSearchResponse
        {
            public string Category { get; set; }
            public string SubCategory { get; set; }
            public string PriceGroup { get; set; }
            public string CatCode { get; set; }
            public string Brand { get; set; }
            public string ItemNumber { get; set; }
            public string Description { get; set; }
            public string UOM { get; set; }
            public string FBFLOOR { get; set; }
            public string FBDSDLISTPRICE { get; set; }
            public string ListPriceLevel { get; set; }            
            public string Useup { get; set; }
            public string COGS { get; set; }
            public string CHECKGPMARGIN { get; set; }
            public string GPMarginper { get; set; }
            
        }

        public class Coffee_Eqp_Charges
        {
            public string Code { get; set; }
            public string Description { get; set; }
            public string PerLb { get; set; }
        }

        public class getPricingToolLookupResponse
        {
            public string result { get; set; }
            public Product data { get; set; }
            public string error { get; set; }
        }       

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // POST api/values
        [HttpPost]
        [Route("getNewAccountPTMaster")]
        public string getPricingToolLookup(ProductSearchRequest productSearchRequest)
        {
            string connString = this.Configuration.GetConnectionString("DefaultConnection");
            getPricingToolLookupResponse gplr = new getPricingToolLookupResponse();                
            Product p = new Product();

            SqlConnection cnn = new SqlConnection(connString);

            try
            {
                string getCategoryDetails = "SELECT * FROM ALLIEDPRICE; ";

                using (SqlCommand cmd = new SqlCommand(getCategoryDetails, cnn))
                {
                    cnn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();
                    List<ProductSearchResponse> psr = new List<ProductSearchResponse>();

                    while (reader.Read())
                    {
                        ProductSearchResponse ps = new ProductSearchResponse();
                        if (reader["Category"] != DBNull.Value)
                        {
                            if ((string)reader["Category"] != "")
                                ps.Category = (string)reader["Category"];
                            else
                                ps.Category = "";
                        }
                        else
                            ps.Category = "";
                        if (reader["SubCategory"] != DBNull.Value)
                        {
                            if ((string)reader["SubCategory"] != "")
                                ps.SubCategory = (string)reader["SubCategory"];
                            else
                                ps.SubCategory = "";
                        }
                        else
                            ps.SubCategory = "";
                        if (reader["PriceGroup"] != DBNull.Value)
                        {
                            if ((string)reader["PriceGroup"] != "")
                                ps.PriceGroup = (string)reader["PriceGroup"];
                            else
                                ps.PriceGroup = "";
                        }
                        else
                            ps.PriceGroup = "";
                        if (reader["CatCode"] != DBNull.Value)
                        {
                            if ((string)reader["CatCode"] != "")
                                ps.CatCode = (string)reader["CatCode"];
                            else
                                ps.CatCode = "";
                        }
                        else
                            ps.CatCode = "";
                        if (reader["Brand"] != DBNull.Value)
                        {
                            if ((string)reader["Brand"] != "")
                                ps.Brand = (string)reader["Brand"];
                            else
                                ps.Brand = "";
                        }
                        else
                            ps.Brand = "";
                        
                        if (reader["ItemNumber"] != DBNull.Value)
                        {
                            if ((string)reader["ItemNumber"] != "")
                                ps.ItemNumber = (string)reader["ItemNumber"];
                            else
                                ps.ItemNumber = "";
                        }
                        else
                            ps.ItemNumber = "";
                        if (reader["Description"] != DBNull.Value)
                        {
                            if ((string)reader["Description"] != "")
                                ps.Description = (string)reader["Description"];
                            else
                                ps.Description = "";
                        }
                        else
                            ps.Description = "";
                        if (reader["UOM"] != DBNull.Value)
                        {
                            if ((string)reader["UOM"] != "")
                                ps.UOM = (string)reader["UOM"];
                            else
                                ps.UOM = "";
                        }
                        else
                            ps.UOM = "";
                        if (reader["FBFLOOR"] != DBNull.Value)
                        {
                            //if ((string)reader["FBFLOOR"] != "")
                                ps.FBFLOOR = Convert.ToString((decimal)reader["FBFLOOR"]);
                        }
                        else
                            ps.FBFLOOR = "";
                        if (reader["FBDSDLISTPRICE"] != DBNull.Value)
                        {
                            //if ((string)reader["FBDSDLISTPRICE"] != "")
                                ps.FBDSDLISTPRICE = Math.Round((decimal)reader["FBDSDLISTPRICE"], 2).ToString();
                        }
                        else
                            ps.FBDSDLISTPRICE = "";
                        if (reader["ListPriceLevel"] != DBNull.Value)
                        {
                            if ((string)reader["ListPriceLevel"] != "")
                                ps.ListPriceLevel = (string)reader["ListPriceLevel"];
                            else
                                ps.ListPriceLevel = "";
                        }
                        else
                            ps.ListPriceLevel = "";
                        if (reader["Useup"] != DBNull.Value)
                        {
                            if ((string)reader["Useup"] != "")
                                ps.Useup = (string)reader["Useup"];
                            else
                                ps.Useup = "";
                        }
                        else
                            ps.Useup = "";
                        if (reader["COGS"] != DBNull.Value)
                        {
                            //if ((string)reader["COGS"] != "")
                                ps.COGS = Convert.ToString((decimal)reader["COGS"]);
                        }
                        else
                            ps.COGS = "";
                        if (reader["CHECKGPMARGIN"] != DBNull.Value)
                        {
                            //if ((string)reader["CHECKGPMARGIN"] != "")
                                ps.CHECKGPMARGIN = Convert.ToString((decimal)reader["CHECKGPMARGIN"]);
                        }
                        else
                            ps.CHECKGPMARGIN = "";
                        if (reader["GPMarginper"] != DBNull.Value)
                        {
                            //if ((string)reader["CHECKGPMARGIN"] != "")
                            ps.GPMarginper = reader["GPMarginper"].ToString();
                        }
                        else
                            ps.GPMarginper = "";
                        psr.Add(ps);
                    }
                    p.items = psr;
                    cmd.Dispose();
                    reader.Close();
                    cnn.Close();
                    string getCoffeeEqpChrs = "SELECT * FROM Coffee_Eqp_Charges;";
                    SqlCommand cmd1 = new SqlCommand(getCoffeeEqpChrs, cnn);
                    cnn.Open();
                    SqlDataReader reader1 = cmd1.ExecuteReader();
                    List<Coffee_Eqp_Charges> ceclt = new List<Coffee_Eqp_Charges>();
                        
                    while (reader1.Read())
                    {
                        Coffee_Eqp_Charges c = new Coffee_Eqp_Charges();
                        c.Code = (string)reader1["Code"];
                        c.Description = (string)reader1["Description"];
                        c.PerLb = reader1["PerLb"].ToString();
                        ceclt.Add(c);
                    }
                    p.coffeeEquipChg = ceclt;
                    reader1.Close();
                    cmd1.Dispose();
                    

                    if (cnn.State == System.Data.ConnectionState.Open)
                        cnn.Close();
                    gplr.result = "Success";                    
                    gplr.data = p;
                    gplr.error = "";
                }                
            }
            catch (SqlException ex)
            {
                cnn.Close();
                gplr.result = "Failed";
                gplr.data = null;                
                gplr.error = ex.ToString();
            }

            String json = JsonConvert.SerializeObject(gplr, Formatting.Indented);
            //MessageBox.Show(json);
            return json;
        }
    }
}
