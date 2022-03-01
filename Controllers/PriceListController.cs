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
    public class PriceListController : ControllerBase
    {

        private IConfiguration Configuration;

        public PriceListController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        public class Result
        {
            public string errorDesc;
            public int code;
        }

        public class GetProductListResponse
        {
            public string result { get; set; }            
            public Dictionary<string, Product> data { get; set; }
            public string error { get; set; }
        }

        public class Product
        {
            public List<string> SubCategory { get; set; }
            public List<string> PriceGroup { get; set; }

            public List<string> CatCode { get; set; }
            public List<string> Brand { get; set; }
            public List<string> ItemNumber { get; set; }
        }

        /*public class ProductSearchResponse
        {
            public List<string> Category { get; set; }
            public List<decimal> FBDSDLISTPRICE { get; set; }
            public List<string> Description { get; set; }
            public List<string> ItemNumber { get; set; }
            public List<string> SubCategory { get; set; }
            public List<string> Brand { get; set; }
        }*/

        public class ProductSearchRequest
        {
            public string Category;
            public string SubCategory;
            public string PriceGroup;
            public string categoryCode;
            public string Brand;
            public string ItemNumber;
            public string priceLevelStart;
            public string priceLevelEnd;
        }

        public class ProductSearchResponse
        {
            public string Category { get; set; }
            public decimal FBDSDLISTPRICE { get; set; }
            public string Description { get; set; }
            public string ItemNumber { get; set; }
            public string SubCategory { get; set; }
            public string Brand { get; set; }
            public Dictionary<string, string> levels { get; set; }
        }

        public class getPriceListLookupResponse
        {
            public string result { get; set; }
            public List<ProductSearchResponse> data { get; set; }
            public string error { get; set; }
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpPost]
        [Route("getPriceListMaster")]
        public string getPriceListMaster()
        {
            string connString = this.Configuration.GetConnectionString("DefaultConnection");
            GetProductListResponse gplr = new GetProductListResponse();
            List<string> categories = new List<string>();
            Dictionary<string, Product> sp = new Dictionary<string, Product>();
            SqlConnection cnn = new SqlConnection(connString);
            //Result r = new Result();
            try
            {
                string getCategories = "SELECT DISTINCT(Category) FROM ALLIEDPRICE; ";

                using (SqlCommand cmd = new SqlCommand(getCategories, cnn))
                {
                    cnn.Open();
                    //cmd.ExecuteNonQuery();

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        categories.Add((string)reader["Category"]);
                        //customer.contact_name = (string)reader["contact_name"];                                               
                    }
                    reader.Close();

                    foreach (var category in categories)
                    {
                        Product p = new Product();
                        List<string> SubCategory = new List<string>();
                        List<string> PriceGroup = new List<string>();
                        List<string> CatCode = new List<string>();
                        List<string> Brand = new List<string>();
                        List<string> ItemNumber = new List<string>();

                        string getCategoryList = "SELECT SubCategory,PriceGroup,CatCode,Brand,ItemNumber FROM ALLIEDPRICE" +
                            " where Category='" + category + "';";
                        SqlCommand cmd1 = new SqlCommand(getCategoryList, cnn);
                        reader = cmd1.ExecuteReader();

                        while (reader.Read())
                        {
                            if (reader["SubCategory"] != DBNull.Value)
                            {
                                if ((string)reader["SubCategory"] != "")
                                    SubCategory.Add((string)reader["SubCategory"]);
                            }
                            if (reader["PriceGroup"] != DBNull.Value)
                            {
                                if ((string)reader["PriceGroup"] != "")
                                    PriceGroup.Add((string)reader["PriceGroup"]);
                            }
                            if (reader["CatCode"] != DBNull.Value)
                            {
                                if ((string)reader["CatCode"] != "")
                                    CatCode.Add((string)reader["CatCode"]);
                            }
                            if (reader["Brand"] != DBNull.Value)
                            {
                                if ((string)reader["Brand"] != "")
                                    Brand.Add((string)reader["Brand"]);
                            }
                            if (reader["ItemNumber"] != DBNull.Value)
                            {
                                if ((string)reader["ItemNumber"] != "")
                                    ItemNumber.Add((string)reader["ItemNumber"]);
                            }
                            //customer.contact_name = (string)reader["contact_name"];                                               
                        }
                        cmd1.Dispose();
                        reader.Close();
                        //List<string> liIDs = SubCategory.Distinct().ToList<string>();
                        p.SubCategory = SubCategory.Distinct().ToList<string>();
                        p.PriceGroup = PriceGroup.Distinct().ToList<string>();
                        p.CatCode = CatCode.Distinct().ToList<string>();
                        p.Brand = Brand.Distinct().ToList<string>();
                        p.ItemNumber = ItemNumber.Distinct().ToList<string>();
                        sp.Add(category, p);
                    }
                    //products
                    if (cnn.State == System.Data.ConnectionState.Open)
                        cnn.Close();
                    gplr.result = "Success";
                    gplr.data = sp;
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

        /*public string checkDBNull(string value)
        {
            string val = value;

            return val;
        }*/

        // POST api/values
        [HttpPost]
        [Route("getPriceListLookup")]
        public string getPriceListLookup(ProductSearchRequest productSearchRequest)
        {
            string connString = this.Configuration.GetConnectionString("DefaultConnection");
            getPriceListLookupResponse gplr = new getPriceListLookupResponse();
            //ProductSearchResponse psr = new ProductSearchResponse();
            List<ProductSearchResponse> list = new List<ProductSearchResponse>();
            //Dictionary<string, ProductSearchResponse> sp = new Dictionary<string, ProductSearchResponse>();

            /*GetProductListResponse gplr = new GetProductListResponse();

            Dictionary<string, Product> sp = new Dictionary<string, Product>();*/

            SqlConnection cnn = new SqlConnection(connString);

            try
            {
                string whereClause = "";
                if (productSearchRequest.SubCategory != "")
                    whereClause = whereClause + " and SubCategory='" + productSearchRequest.SubCategory + "'";
                if (productSearchRequest.PriceGroup != "")
                    whereClause = whereClause + " and PriceGroup='" + productSearchRequest.PriceGroup + "'";
                if (productSearchRequest.categoryCode != "")
                    whereClause = whereClause + " and CatCode='" + productSearchRequest.categoryCode + "'";
                if (productSearchRequest.Brand != "")
                    whereClause = whereClause + " and Brand='" + productSearchRequest.Brand + "'";
                if (productSearchRequest.ItemNumber != "")
                    whereClause = whereClause + " and ItemNumber='" + productSearchRequest.ItemNumber + "'";

                if (productSearchRequest.Category != "")
                {
                    string getCategoryDetails = "SELECT Category,SubCategory,Description,FBFLOOR,FBDSDLISTPRICE,Brand,ItemNumber " +
                    "FROM ALLIEDPRICE where Category='" + productSearchRequest.Category + "'" + whereClause + ";";

                    using (SqlCommand cmd = new SqlCommand(getCategoryDetails, cnn))
                    {
                        cnn.Open();

                        SqlDataReader reader = cmd.ExecuteReader();

                        int start = Int32.Parse(productSearchRequest.priceLevelStart);
                        int stop = Int32.Parse(productSearchRequest.priceLevelEnd);
                        if (stop == 0)
                            stop = start;

                        while (reader.Read())
                        {
                            Dictionary<string, string> ld = new Dictionary<string, string>();
                            ProductSearchResponse p = new ProductSearchResponse();

                            double floor, calc;
                            int j = 1;
                            for (int i = start; i <= stop; i++)
                            {
                                floor = (double)(decimal)reader["FBFLOOR"];
                                calc = Math.Round(floor * (1 + 0.05 * i), 2);
                                //calc = Math.Round(calc, 2);
                                if (i <= 9)
                                    ld.Add("L0" + i, calc.ToString());
                                else
                                    ld.Add("L" + i, calc.ToString());

                                j++;
                            }
                            p.Category = (string)reader["Category"];
                            if (reader["FBDSDLISTPRICE"] != DBNull.Value)
                            {
                                //if ((decimal)reader["FBDSDLISTPRICE"] != "")
                                p.FBDSDLISTPRICE = Math.Round((decimal)reader["FBDSDLISTPRICE"], 2);
                            }
                            if (reader["Description"] != DBNull.Value)
                            {
                                if ((string)reader["Description"] != "")
                                    p.Description = (string)reader["Description"];
                            }
                            if (reader["ItemNumber"] != DBNull.Value)
                            {
                                if ((string)reader["ItemNumber"] != "")
                                    p.ItemNumber = (string)reader["ItemNumber"];
                            }
                            if (reader["SubCategory"] != DBNull.Value)
                            {
                                if ((string)reader["SubCategory"] != "")
                                    p.SubCategory = (string)reader["SubCategory"];
                            }
                            if (reader["Brand"] != DBNull.Value)
                            {
                                if ((string)reader["Brand"] != "")
                                    p.Brand = (string)reader["Brand"];
                            }
                            p.levels = ld;
                            //psr = p;
                            //sp.Add(p.Category, p);
                            list.Add(p);
                            /*if (start == stop)
                                break;*/
                        }
                        cmd.Dispose();
                        reader.Close();
                        /*p.Category = Category.Distinct().ToList<string>(); ;
                        p.FBDSDLISTPRICE = FBDSDLISTPRICE;
                        p.Description = Description;
                        p.ItemNumber = ItemNumber;
                        p.SubCategory = SubCategory;
                        p.Brand = Brand;

                        psr.Add(p);*/
                    }

                    if (cnn.State == System.Data.ConnectionState.Open)
                        cnn.Close();
                    gplr.result = "Success";
                    gplr.data = list;
                    gplr.error = "";
                }
                else
                {
                    gplr.result = "Failed";
                    gplr.data = null;
                    gplr.error = "Missing Category";
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
