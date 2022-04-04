using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
//using System.

namespace Farmerbrothers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public class Customer
        {
            public string contact_name;
            public string company_name;
            public string email;
            public string phoneno;
            //public TradeReferences bkreferences { get; set; }
            public List<TradeReference> TradeReferences { get; set; }
        }

        public class TradeReference
        {
            public string CompanyName { get; set; }
            public string AccountID { get; set; }
            public string Phone { get; set; }            
        }
       
        public class NewAccount
        {
            public int ApplicationId;
            public string Name;
            public string Phone;
            public string Email;
            public string DBAName;
            public string BillingAddress;
            public string BillingState;
            public string BillingCity;
            public string BillingZip;
            public string DeliveryAddress;
            public string DeliveryState;
            public string DeliveryCity;
            public string DeliveryZip;
            public string CompanyType;
            public string PrincipalOfficer;
            public string PrincipalOfficerTitle;
            public string NatureOfBusiness;
            public string EstablishedYear;
            public string StateIncorporated;
            public string FederalTaxID;
            public string ResaleCertificateNumber;
            public int TaxExempt;
            public int PORequired;
            public int AlreadyHasFBAccount;
            public string FBAccount;
            public string AccountPayableContact;
            public string AccountPayableTitle;
            public string AccountPayablePhone;
            public string AccountPayableEmail;
            public float CreditRequested;
            public int CreatedUserID;
            public string CreatedUserName;
            public DateTime CreatedDate;
            public int ModifiedUserID;
            public string ModifiedUserName;
            public DateTime ModifiedDate;
            public string BankName;
            public string AccountNo;
            public string ContactName;
            public string BankAddress;
            public string BankCity;
            public string BankState;
            public string BankZip;
            public string BankPhone;
            public List<TradeReference> TradeReferences { get; set; }
        }        

        public int insertIntoMainTable(NewAccount newAccount)
        {
            string connString = "Data Source=localhost\\SQLEXPRESS;Database=Test;Integrated Security=SSPI";

            SqlConnection cnn = new SqlConnection(connString);
            try
            {
                string saveStaff = "INSERT into NARF_NewAccount (Name,Phone,Email,DBAName,BillingAddress," +
                    "BillingState,BillingCity,BillingZip,DeliveryAddress,DeliveryState,DeliveryCity,DeliveryZip,CompanyType," +
                    "PrincipalOfficer,PrincipalOfficerTitle,NatureOfBusiness,EstablishedYear,StateIncorporated,FederalTaxID," +
                    "ResaleCertificateNumber,TaxExempt,PORequired,AlreadyHasFBAccount,FBAccount,AccountPayableContact," +
                    "AccountPayableTitle,AccountPayablePhone,AccountPayableEmail,CreditRequested,CreatedUserID,CreatedUserName," +
                    "CreatedDate,ModifiedUserID,ModifiedUserName,ModifiedDate,BankName,AccountNo,ContactName,BankAddress,BankCity, " +
                    "BankState,BankZip,BankPhone) output INSERTED.APPLICATIONID VALUES(@Name,@Phone," +
                    "@Email,@DBAName,@BillingAddress,@BillingState,@BillingCity,@BillingZip,@DeliveryAddress," +
                    "@DeliveryState,@DeliveryCity,@DeliveryZip,@CompanyType,@PrincipalOfficer,@PrincipalOfficerTitle," +
                    "@NatureOfBusiness,@EstablishedYear,@StateIncorporated,@FederalTaxID,@ResaleCertificateNumber," +
                    "@TaxExempt,@PORequired,@AlreadyHasFBAccount,@FBAccount,@AccountPayableContact,@AccountPayableTitle," +
                    "@AccountPayablePhone,@AccountPayableEmail,@CreditRequested,@CreatedUserID,@CreatedUserName," +
                    "@CreatedDate,@ModifiedUserID,@ModifiedUserName,@ModifiedDate,@BankName,@AccountNo,@ContactName,@BankAddress,@BankCity, " +
                    "@BankState,@BankZip,@BankPhone);";


                /*using (SqlCommand cmd = new SqlCommand("INSERT into NARF_NewAccount(Name, Phone, Email, DBAName, BilllingAddress, " +
                    "BilllingState,BilllingCity,BilllingZip,DeliveryAddress,DeliveryState,DeliveryCity,DeliveryZip,CompanyType," +
                    "PrincipalOfficer,PrincipalOfficerTitle,NatureOfBusiness,EstablishedYear,StateIncorporated,FederalTaxID," +
                    "ResaleCertificateNumber,TaxExempt,PORequired,AlreadyHasFBAccount,FBAccount,AccountPayableContact," +
                    "AccountPayableTitle,AccountPayablePhone,AccountPayableEmail,CreditRequested,CreatedUserID,CreatedUserName," +
                    "CreatedDate,ModifiedUserID,ModifiedUserName,ModifiedDate)  output INSERTED.APPLICATIONID VALUES(@Name,@Phone," +
                    "@Email,@DBAName,@BilllingAddress,@BilllingState,@BilllingCity,@BilllingZip,@DeliveryAddress," +
                    "@DeliveryState,@DeliveryCity,@DeliveryZip,@CompanyType,@PrincipalOfficer,@PrincipalOfficerTitle," +
                    "@NatureOfBusiness,@EstablishedYear,@StateIncorporated,@FederalTaxID,@ResaleCertificateNumber," +
                    "@TaxExempt,@PORequired,@AlreadyHasFBAccount,@FBAccount,@AccountPayableContact,@AccountPayableTitle," +
                    "@AccountPayablePhone,@AccountPayableEmail,@CreditRequested,@CreatedUserID,@CreatedUserName," +
                    "@CreatedDate,@ModifiedUserID,@ModifiedUserName,@ModifiedDate,@BankName,@AccountNo,@ContactName,@BankAddress,@BankCity, " +
                    "@BankState,@BankZip,@BankPhone)", cnn))*/
                using (SqlCommand cmd = new SqlCommand(saveStaff, cnn))
                {
                    string sourceDate = newAccount.CreatedDate.ToString();
                    DateTime Date = DateTime.Parse(sourceDate);
                    string createdDate = Date.ToString("yyyy-MM-dd HH:mm:ss");

                    sourceDate = newAccount.ModifiedDate.ToString();
                    Date = DateTime.Parse(sourceDate);
                    string modifiedDate = Date.ToString("yyyy-MM-dd HH:mm:ss");

                    cmd.Parameters.AddWithValue("@Name", newAccount.Name);
                    cmd.Parameters.AddWithValue("@Phone", newAccount.Phone);
                    cmd.Parameters.AddWithValue("@Email", newAccount.Email);
                    cmd.Parameters.AddWithValue("@DBAName", newAccount.DBAName);
                    cmd.Parameters.AddWithValue("@BillingAddress", newAccount.BillingAddress);
                    cmd.Parameters.AddWithValue("@BillingState", newAccount.BillingState);
                    cmd.Parameters.AddWithValue("@BillingCity", newAccount.BillingCity);
                    cmd.Parameters.AddWithValue("@BillingZip", newAccount.BillingZip);
                    cmd.Parameters.AddWithValue("@DeliveryAddress", newAccount.DeliveryAddress);
                    cmd.Parameters.AddWithValue("@DeliveryState", newAccount.DeliveryState);
                    cmd.Parameters.AddWithValue("@DeliveryCity", newAccount.DeliveryCity);
                    cmd.Parameters.AddWithValue("@DeliveryZip", newAccount.DeliveryZip);
                    cmd.Parameters.AddWithValue("@CompanyType", newAccount.CompanyType);
                    cmd.Parameters.AddWithValue("@PrincipalOfficer", newAccount.PrincipalOfficer);
                    cmd.Parameters.AddWithValue("@PrincipalOfficerTitle", newAccount.PrincipalOfficerTitle);
                    cmd.Parameters.AddWithValue("@NatureOfBusiness", newAccount.NatureOfBusiness);
                    cmd.Parameters.AddWithValue("@EstablishedYear", newAccount.EstablishedYear);
                    cmd.Parameters.AddWithValue("@StateIncorporated", newAccount.StateIncorporated);
                    cmd.Parameters.AddWithValue("@FederalTaxID", newAccount.FederalTaxID);
                    cmd.Parameters.AddWithValue("@ResaleCertificateNumber", newAccount.ResaleCertificateNumber);
                    cmd.Parameters.AddWithValue("@TaxExempt", newAccount.TaxExempt);
                    cmd.Parameters.AddWithValue("@PORequired", newAccount.PORequired);
                    cmd.Parameters.AddWithValue("@AlreadyHasFBAccount", newAccount.AlreadyHasFBAccount);
                    cmd.Parameters.AddWithValue("@FBAccount", newAccount.FBAccount);
                    cmd.Parameters.AddWithValue("@AccountPayableContact", newAccount.AccountPayableContact);
                    cmd.Parameters.AddWithValue("@AccountPayableTitle", newAccount.AccountPayableTitle);
                    cmd.Parameters.AddWithValue("@AccountPayablePhone", newAccount.AccountPayablePhone);
                    cmd.Parameters.AddWithValue("@AccountPayableEmail", newAccount.AccountPayableEmail);
                    cmd.Parameters.AddWithValue("@CreditRequested", newAccount.CreditRequested);
                    cmd.Parameters.AddWithValue("@CreatedUserID", newAccount.CreatedUserID);
                    cmd.Parameters.AddWithValue("@CreatedUserName", newAccount.CreatedUserName);
                    cmd.Parameters.AddWithValue("@CreatedDate", createdDate);
                    cmd.Parameters.AddWithValue("@ModifiedUserID", newAccount.ModifiedUserID);
                    cmd.Parameters.AddWithValue("@ModifiedUserName", newAccount.ModifiedUserName);
                    cmd.Parameters.AddWithValue("@ModifiedDate", modifiedDate);
                    cmd.Parameters.AddWithValue("@BankName", newAccount.BankName);
                    cmd.Parameters.AddWithValue("@AccountNo", newAccount.AccountNo);
                    cmd.Parameters.AddWithValue("@ContactName", newAccount.ContactName);
                    cmd.Parameters.AddWithValue("@BankAddress", newAccount.BankAddress);
                    cmd.Parameters.AddWithValue("@BankCity", newAccount.BankCity);
                    cmd.Parameters.AddWithValue("@BankState", newAccount.BankState);
                    cmd.Parameters.AddWithValue("@BankZip", newAccount.BankZip);
                    cmd.Parameters.AddWithValue("@BankPhone", newAccount.BankPhone);
                    cnn.Open();
                    //MessageBox.Show(cmd.CommandText);
                    int modified = (int)cmd.ExecuteScalar();

                    if (cnn.State == System.Data.ConnectionState.Open)
                        cnn.Close();

                    return modified;

                }
            }
            catch (SqlException e)
            {
                cnn.Close();
                return 0;
            }

        }

        public void insertIntoChildTable(int applicationID,NewAccount newAccount)
        {
            string connString = "Data Source=localhost\\SQLEXPRESS;Database=Test;Integrated Security=SSPI";
            SqlConnection cnn = new SqlConnection(connString);

            List<TradeReference> tr = newAccount.TradeReferences;
            foreach (TradeReference t in tr)
            {
                try
                {
                    string saveStaff = "INSERT into NARF_TradeReference (ApplicationId,CompanyName,AccountId,Phone) VALUES(@ApplicationId,@CompanyName," +
                        "@AccountId,@Phone);";

                    using (SqlCommand cmd = new SqlCommand(saveStaff, cnn))
                    {
                        cmd.Parameters.AddWithValue("@ApplicationId", applicationID);
                        cmd.Parameters.AddWithValue("@CompanyName", t.CompanyName);
                        cmd.Parameters.AddWithValue("@AccountId", t.AccountID);
                        cmd.Parameters.AddWithValue("@Phone", t.Phone);
                        cnn.Open();

                        cmd.ExecuteNonQuery();
                        //int modified = (int)cmd.ExecuteScalar();

                        if (cnn.State == System.Data.ConnectionState.Open)
                            cnn.Close();                        

                    }
                }
                catch (SqlException ex)
                {
                    cnn.Close();                    
                }
            }

        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        [Route("addCustomerAccount")]
        //public ActionResult<string> Post([FromBody] string value)
        public string TestMethod(NewAccount newAccount)
        {
            /*int recordID= insertIntoMainTable(newAccount);
            insertIntoChildTable(recordID, newAccount);*/
            return "Successfull ";//+ recordID;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
