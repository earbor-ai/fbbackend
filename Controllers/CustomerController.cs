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
using System.Security.Cryptography;

namespace Farmerbrothers.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private IConfiguration Configuration;

        public CustomerController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        //private string appID;
        //private string emailContent;
        public class Result
        {
            public string errorDesc;
            public int code;            
        }

        public class TradeReference
        {
            public string CompanyName { get; set; }
            public string AccountID { get; set; }
            public string Phone { get; set; }
            public string EmailID { get; set; }
        }

        public class NewAccount
        {
            public CompanyInfo companyInfo;
            public TradeAndBankRef tradeAndBankRef;
            public PersonalGuarantee personalGuarantee;
            public ConsentAndSignature consentAndSignature;
            public UserInfo userInfo;
        }
        public class CompanyInfo
        {
            public string CustomerID;
            public string CustomerJDE;
            public string Name;
            public string Phone;
            public string Email;
            public string DBAName;
            public string BillingAddress;
            public string BillingState;
            public string BillingCity;
            public string BillingZip;
            public string DeliveryAddress1;
            public string DeliveryAddress2;
            public string DeliveryAddress3;
            public string DeliveryAddress4;
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
            public string TaxExemptFile;
            public string ResaleCertificateNoFile;
            public string SecondaryAccountPayableContact;
            public string SecondaryAccountPayableTitle;
            public string SecondaryAccountPayablePhone;
            public string SecondaryAccountPayableEmail;
            public string DUNSNumber;
            public string BillingPhone;
            public string BankRefDocumentFile;
            public string PersonalGuaranteeAuthSignFile;
            public string ConsentAuthSignFile;
            public string fb1Status;
            public string baidtda;

            // to be saved in table upon impementation in front end
            public string OwnerName;
            public string Fax;
            public string County;
            public string TaxGroup;
            public string PrincipalCell;
            public string PrincipalEmail;
        }

        public class TradeAndBankRef
        {
            public List<TradeReference> TradeReferences { get; set; }

            public string BankName;
            public string BankAccountNo;            
            public string BankAddress;
            public string BankCity;
            public string BankState;
            public string BankZip;
            public string BankPhone;
            public string BankEmailID;            
        }

        public class PersonalGuarantee
        {         
            public string FirstName;
            public string MiddleName;
            public string LastName;
            public string Title;
            public string PresentHomeAddress;
            public string City;
            public string State;
            public string Zip;            
            public string DOB;
            public string SSN;
            public string DriverLicenceNoAndState;
            public string Date;    
        }

        public class ConsentAndSignature
        {
            public bool TermsAndConditions;
            public string PrintName;
            public string Title;
            public string Date;
        }

        public class UserInfo
        {
            public int CreatedUserID;
            public string CreatedUserName;
            public DateTime CreatedDate;
            public int ModifiedUserID;
            public string ModifiedUserName;
            public DateTime ModifiedDate;            
        }

        public Result insertIntoMainTable(NewAccount newAccount)
        {
            //string connString = "Data Source=localhost\\SQLEXPRESS;Database=FarmerBrosDB;Integrated Security=SSPI";

            string connString = this.Configuration.GetConnectionString("DefaultConnection");
            string saveStaff = "";
            int modified = 0;

            SqlConnection cnn = new SqlConnection(connString);
            try
            {
                if (newAccount.companyInfo.CustomerID == "")
                {
                    saveStaff = "INSERT into NARF_Header (CustomerName,Phone,Email,DBAName,BillingAddress," +
                    "BillingState,BillingCity,BillingZip,DeliveryAddress1,DeliveryAddress2,DeliveryAddress3,DeliveryAddress4,DeliveryState," +
                    "DeliveryCity,DeliveryZip,CompanyType,PrincipalOfficer,PrincipalOfficerTitle,NatureOfBusiness,EstablishedYear," +
                    "StateIncorporated,FederalTaxID,ResaleCertificateNumber,TaxExempt,PORequired,AlreadyHasFBAccount,FBAccount," +
                    "DUNSNumber,CustomerJDE,fb1Status,fb1UpdatedDate,baidtda) output INSERTED.CustomerID VALUES" +
                    "(@Name,@Phone,@Email,@DBAName,@BillingAddress,@BillingState,@BillingCity,@BillingZip,@DeliveryAddress1,@DeliveryAddress2," +
                    "@DeliveryAddress3,@DeliveryAddress4,@DeliveryState,@DeliveryCity,@DeliveryZip,@CompanyType,@PrincipalOfficer,@PrincipalOfficerTitle," +
                    "@NatureOfBusiness,@EstablishedYear,@StateIncorporated,@FederalTaxID,@ResaleCertificateNumber," +
                    "@TaxExempt,@PORequired,@AlreadyHasFBAccount,@FBAccount,@DUNSNumber,@CustomerJDE,@fb1Status,@fb1UpdatedDate,@baidtda);";
                }
                else
                {
                    saveStaff = "UPDATE NARF_Header SET " +
                        "CustomerName=@Name,Phone=@Phone,Email=@Email,DBAName=@DBAName,BillingAddress=@BillingAddress," +
                        "BillingState=@BillingState,BillingCity=@BillingCity,BillingZip=@BillingZip,DeliveryAddress1=@DeliveryAddress1," +
                        "DeliveryAddress2=@DeliveryAddress2,DeliveryAddress3=@DeliveryAddress3,DeliveryAddress4=@DeliveryAddress4," +
                        "DeliveryState=@DeliveryState,DeliveryCity=@DeliveryCity,DeliveryZip=@DeliveryZip,CompanyType=@CompanyType," +
                        "PrincipalOfficer=@PrincipalOfficer,PrincipalOfficerTitle=@PrincipalOfficerTitle,StateIncorporated=@StateIncorporated" +
                        "FederalTaxID=@FederalTaxID,NatureOfBusiness=@NatureOfBusiness,EstablishedYear=@EstablishedYear,ResaleCertificateNumber=@ResaleCertificateNumber," +
                        "TaxExempt=@TaxExempt,PORequired=@PORequired,AlreadyHasFBAccount=@AlreadyHasFBAccount,FBAccount=@FBAccount" +
                        "CustomerJDE=@CustomerJDE,fb1Status=@fb1Status,fb1UpdatedDate=@fb1UpdatedDate,DUNSNumber=@DUNSNumber," +
                        "baidtda=@baidtda" +
                        " WHERE CustomerID = " + newAccount.companyInfo.CustomerID + ";";
                }


                using (SqlCommand cmd = new SqlCommand(saveStaff, cnn))
                {
                    string sourceDate = newAccount.userInfo.CreatedDate.ToString();
                    DateTime Date = DateTime.Parse(sourceDate);
                    string createdDate = Date.ToString("yyyy-MM-dd HH:mm:ss");

                    sourceDate = newAccount.userInfo.ModifiedDate.ToString();
                    Date = DateTime.Parse(sourceDate);
                    string modifiedDate = Date.ToString("yyyy-MM-dd HH:mm:ss");

                    /* Note: Empty date from front-end is 1900-01-01 
                     All such values to be treated as NULL */

                    string ssn = Encrypt(newAccount.personalGuarantee.SSN);
                    
                    string dlnas = Encrypt(newAccount.personalGuarantee.DriverLicenceNoAndState);

                    cmd.Parameters.AddWithValue("@Name", newAccount.companyInfo.Name);
                    cmd.Parameters.AddWithValue("@Phone", newAccount.companyInfo.Phone);
                    cmd.Parameters.AddWithValue("@Email", newAccount.companyInfo.Email);
                    cmd.Parameters.AddWithValue("@DBAName", newAccount.companyInfo.DBAName);
                    cmd.Parameters.AddWithValue("@BillingAddress", newAccount.companyInfo.BillingAddress);
                    cmd.Parameters.AddWithValue("@BillingState", newAccount.companyInfo.BillingState);
                    cmd.Parameters.AddWithValue("@BillingCity", newAccount.companyInfo.BillingCity);
                    cmd.Parameters.AddWithValue("@BillingZip", newAccount.companyInfo.BillingZip);                                      
                    cmd.Parameters.AddWithValue("@DeliveryAddress1", newAccount.companyInfo.DeliveryAddress1);
                    cmd.Parameters.AddWithValue("@DeliveryAddress2", newAccount.companyInfo.DeliveryAddress2);
                    cmd.Parameters.AddWithValue("@DeliveryAddress3", newAccount.companyInfo.DeliveryAddress3);
                    cmd.Parameters.AddWithValue("@DeliveryAddress4", newAccount.companyInfo.DeliveryAddress4);
                    cmd.Parameters.AddWithValue("@DeliveryState", newAccount.companyInfo.DeliveryState);
                    cmd.Parameters.AddWithValue("@DeliveryCity", newAccount.companyInfo.DeliveryCity);
                    cmd.Parameters.AddWithValue("@DeliveryZip", newAccount.companyInfo.DeliveryZip);
                    cmd.Parameters.AddWithValue("@CompanyType", newAccount.companyInfo.CompanyType);
                    cmd.Parameters.AddWithValue("@PrincipalOfficer", newAccount.companyInfo.PrincipalOfficer);
                    cmd.Parameters.AddWithValue("@PrincipalOfficerTitle", newAccount.companyInfo.PrincipalOfficerTitle);
                    cmd.Parameters.AddWithValue("@NatureOfBusiness", newAccount.companyInfo.NatureOfBusiness);
                    cmd.Parameters.AddWithValue("@EstablishedYear", newAccount.companyInfo.EstablishedYear);
                    cmd.Parameters.AddWithValue("@StateIncorporated", newAccount.companyInfo.StateIncorporated);
                    cmd.Parameters.AddWithValue("@FederalTaxID", newAccount.companyInfo.FederalTaxID);
                    cmd.Parameters.AddWithValue("@ResaleCertificateNumber", newAccount.companyInfo.ResaleCertificateNumber);
                    cmd.Parameters.AddWithValue("@TaxExempt", newAccount.companyInfo.TaxExempt);
                    cmd.Parameters.AddWithValue("@PORequired", newAccount.companyInfo.PORequired);
                    cmd.Parameters.AddWithValue("@AlreadyHasFBAccount", newAccount.companyInfo.AlreadyHasFBAccount);
                    cmd.Parameters.AddWithValue("@FBAccount", newAccount.companyInfo.FBAccount);
                    cmd.Parameters.AddWithValue("@DUNSNumber", newAccount.companyInfo.DUNSNumber);
                    cmd.Parameters.AddWithValue("@CustomerJDE", newAccount.companyInfo.CustomerJDE); 
                    cmd.Parameters.AddWithValue("@fb1Status", newAccount.companyInfo.fb1Status);  

                    cmd.Parameters.AddWithValue("@baidtda", newAccount.companyInfo.baidtda);
                    //cmd.Parameters.AddWithValue("@CustomerJDE", "");
                    //cmd.Parameters.AddWithValue("@fb1Status", "Open");
                    cmd.Parameters.AddWithValue("@fb1UpdatedDate", DateTime.Now.ToString("yyyy-MM-dd")); 

                    cnn.Open();
                    //MessageBox.Show(cmd.CommandText);
                    if (newAccount.companyInfo.CustomerID == "")
                        modified = (int)cmd.ExecuteScalar();
                    else
                    {
                        modified = Int32.Parse(newAccount.companyInfo.CustomerID);
                        cmd.ExecuteNonQuery();
                    }

                    if (cnn.State == System.Data.ConnectionState.Open)
                        cnn.Close();

                    Result r = new Result();
                    r.code = modified;
                    r.errorDesc = "";
                    return r;

                }
            }
            catch (SqlException e)
            {
                //MessageBox.Show();
                cnn.Close();
                Result r = new Result();
                r.code = 0;
                r.errorDesc = e.ToString();
                return r;
            }

        }

        
        public Result insertIntoNarfFB1(int applicationID, NewAccount newAccount)
        {
            string connString = this.Configuration.GetConnectionString("DefaultConnection");

            SqlConnection cnn = new SqlConnection(connString);
            try
            {
                string saveStaff = "INSERT into NARF_FB1 (CustomerID,AccountPayableContact," +
                    "AccountPayableTitle,AccountPayablePhone,AccountPayableEmail,CreatedUserID,CreatedUserName," +
                    "CreatedDate,ModifiedUserID,ModifiedUserName,ModifiedDate,BankName,AccountNo,BankAddress,BankCity, " +
                    "BankState,BankZip,BankPhone,BankEmailID,SecondaryAccountPayableContact,SecondaryAccountPayableTitle," +
                    "SecondaryAccountPayablePhone,SecondaryAccountPayableEmail,BillingPhone,PGFirstName,PGMiddleName,PGLastName," +
                    "PGTitle,PGPresentHomeAddress,PGCity,PGState,PGZip,PGDOB,PGSSN,PGDriverLicenceNoAndState,PGDate,CASTermsAndConditions," +
                    "CASPrintName,CASTitle,CASDate) VALUES" +
                    "(@CustomerID,@AccountPayableContact,@AccountPayableTitle," +
                    "@AccountPayablePhone,@AccountPayableEmail,@CreatedUserID,@CreatedUserName," +
                    "@CreatedDate,@ModifiedUserID,@ModifiedUserName,@ModifiedDate,@BankName,@AccountNo,@BankAddress,@BankCity, " +
                    "@BankState,@BankZip,@BankPhone,@BankEmailID,@SecondaryAccountPayableContact,@SecondaryAccountPayableTitle,@SecondaryAccountPayablePhone," +
                    "@SecondaryAccountPayableEmail,@BillingPhone,@PGFirstName,@PGMiddleName,@PGLastName,@PGTitle,@PGPresentHomeAddress,@PGCity," +
                    "@PGState,@PGZip,@PGDOB,@PGSSN,@PGDriverLicenceNoAndState,@PGDate,@CASTermsAndConditions,@CASPrintName,@CASTitle,@CASDate);";


                using (SqlCommand cmd = new SqlCommand(saveStaff, cnn))
                {
                    string sourceDate = newAccount.userInfo.CreatedDate.ToString();
                    DateTime Date = DateTime.Parse(sourceDate);
                    string createdDate = Date.ToString("yyyy-MM-dd HH:mm:ss");

                    sourceDate = newAccount.userInfo.ModifiedDate.ToString();
                    Date = DateTime.Parse(sourceDate);
                    string modifiedDate = Date.ToString("yyyy-MM-dd HH:mm:ss");

                    /* Note: Empty date from front-end is 1900-01-01 
                     All such values to be treated as NULL */

                    string ssn = Encrypt(newAccount.personalGuarantee.SSN);
                    string dlnas = Encrypt(newAccount.personalGuarantee.DriverLicenceNoAndState);

                    cmd.Parameters.AddWithValue("@CustomerID", applicationID);
                    cmd.Parameters.AddWithValue("@AccountPayableContact", newAccount.companyInfo.AccountPayableContact);
                    cmd.Parameters.AddWithValue("@AccountPayableTitle", newAccount.companyInfo.AccountPayableTitle);
                    cmd.Parameters.AddWithValue("@AccountPayablePhone", newAccount.companyInfo.AccountPayablePhone);
                    cmd.Parameters.AddWithValue("@AccountPayableEmail", newAccount.companyInfo.AccountPayableEmail);
                    //cmd.Parameters.AddWithValue("@CreditRequested", newAccount.companyInfo.CreditRequested);
                    
                    cmd.Parameters.AddWithValue("@CreatedUserID", newAccount.userInfo.CreatedUserID);
                    cmd.Parameters.AddWithValue("@CreatedUserName", newAccount.userInfo.CreatedUserName);
                    cmd.Parameters.AddWithValue("@CreatedDate", createdDate);
                    cmd.Parameters.AddWithValue("@ModifiedUserID", newAccount.userInfo.ModifiedUserID);
                    cmd.Parameters.AddWithValue("@ModifiedUserName", newAccount.userInfo.ModifiedUserName);
                    cmd.Parameters.AddWithValue("@ModifiedDate", modifiedDate);                    
                    cmd.Parameters.AddWithValue("@SecondaryAccountPayableContact", newAccount.companyInfo.SecondaryAccountPayableContact);
                    cmd.Parameters.AddWithValue("@SecondaryAccountPayableTitle", newAccount.companyInfo.SecondaryAccountPayableTitle);
                    cmd.Parameters.AddWithValue("@SecondaryAccountPayablePhone", newAccount.companyInfo.SecondaryAccountPayablePhone);
                    cmd.Parameters.AddWithValue("@SecondaryAccountPayableEmail", newAccount.companyInfo.SecondaryAccountPayableEmail);
                    cmd.Parameters.AddWithValue("@BillingPhone", newAccount.companyInfo.BillingPhone);  

                    /* Bank Details */
                    cmd.Parameters.AddWithValue("@BankName", newAccount.tradeAndBankRef.BankName);
                    cmd.Parameters.AddWithValue("@AccountNo", newAccount.tradeAndBankRef.BankAccountNo);                    
                    cmd.Parameters.AddWithValue("@BankAddress", newAccount.tradeAndBankRef.BankAddress);
                    cmd.Parameters.AddWithValue("@BankCity", newAccount.tradeAndBankRef.BankCity);
                    cmd.Parameters.AddWithValue("@BankState", newAccount.tradeAndBankRef.BankState);
                    cmd.Parameters.AddWithValue("@BankZip", newAccount.tradeAndBankRef.BankZip);
                    cmd.Parameters.AddWithValue("@BankPhone", newAccount.tradeAndBankRef.BankPhone);
                    cmd.Parameters.AddWithValue("@BankEmailID", newAccount.tradeAndBankRef.BankEmailID);

                    /* Personal Guarantee Tab */
                    cmd.Parameters.AddWithValue("@PGFirstName", newAccount.personalGuarantee.FirstName);
                    cmd.Parameters.AddWithValue("@PGMiddleName", newAccount.personalGuarantee.MiddleName);
                    cmd.Parameters.AddWithValue("@PGLastName", newAccount.personalGuarantee.LastName);                    
                    cmd.Parameters.AddWithValue("@PGTitle", newAccount.personalGuarantee.Title);
                    cmd.Parameters.AddWithValue("@PGPresentHomeAddress", newAccount.personalGuarantee.PresentHomeAddress);
                    cmd.Parameters.AddWithValue("@PGCity", newAccount.personalGuarantee.City);
                    cmd.Parameters.AddWithValue("@PGState", newAccount.personalGuarantee.State);
                    cmd.Parameters.AddWithValue("@PGZip", newAccount.personalGuarantee.Zip);
                    cmd.Parameters.AddWithValue("@PGDOB", newAccount.personalGuarantee.DOB);
                    cmd.Parameters.AddWithValue("@PGSSN", ssn);
                    cmd.Parameters.AddWithValue("@PGDriverLicenceNoAndState", dlnas);
                    cmd.Parameters.AddWithValue("@PGDate", newAccount.personalGuarantee.Date);

                    /* Consent and Signature Tab */
                    if (newAccount.consentAndSignature.TermsAndConditions == true)                    
                        cmd.Parameters.AddWithValue("@CASTermsAndConditions", 1);
                    else
                        cmd.Parameters.AddWithValue("@CASTermsAndConditions", 0);                   

                    cmd.Parameters.AddWithValue("@CASPrintName", newAccount.consentAndSignature.PrintName);
                    cmd.Parameters.AddWithValue("@CASTitle", newAccount.consentAndSignature.Title);
                    cmd.Parameters.AddWithValue("@CASDate", newAccount.consentAndSignature.Date);

                    cnn.Open();
                    //MessageBox.Show(cmd.CommandText);
                    cmd.ExecuteNonQuery();
                    
                    if (cnn.State == System.Data.ConnectionState.Open)
                        cnn.Close();

                    Result r = new Result();
                    r.code = 1;
                    r.errorDesc = "";
                    return r;

                }
            }
            catch (SqlException e)
            {
                //MessageBox.Show();
                cnn.Close();
                Result r = new Result();
                r.code = 0;
                r.errorDesc = e.ToString();
                return r;
            }
        }

        public Result insertIntoChildTable(int applicationID, NewAccount newAccount)
        {
            //string connString = "Data Source=localhost\\SQLEXPRESS;Database=FarmerBrosDB;Integrated Security=SSPI";
            string connString = this.Configuration.GetConnectionString("DefaultConnection");

            SqlConnection cnn = new SqlConnection(connString);

            List<TradeReference> tr = newAccount.tradeAndBankRef.TradeReferences;
            foreach (TradeReference t in tr)
            {
                try
                {
                    string saveStaff = "INSERT into NARF_TradeReference (CustomerID,CompanyName,AccountId,Phone,EmailID) VALUES(@CustomerID,@CompanyName," +
                        "@AccountId,@Phone,@EmailID);";

                    using (SqlCommand cmd = new SqlCommand(saveStaff, cnn))
                    {
                        cmd.Parameters.AddWithValue("@CustomerID", applicationID);
                        cmd.Parameters.AddWithValue("@CompanyName", t.CompanyName);
                        cmd.Parameters.AddWithValue("@AccountId", t.AccountID);
                        cmd.Parameters.AddWithValue("@EmailID", t.EmailID);
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
                    Result r = new Result();
                    r.code = -1;
                    r.errorDesc = ex.ToString();
                    return r;
                }
            }
            Result r1 = new Result();
            r1.code = 0;
            r1.errorDesc = "";
            return r1;
        }

        public void imageUploadIntoTable(string applicationID, string fieldName, string fileName)
        {
            //string connString = "Data Source=localhost\\SQLEXPRESS;Database=FarmerBrosDB;Integrated Security=SSPI";

            string connString = this.Configuration.GetConnectionString("DefaultConnection");

            SqlConnection cnn = new SqlConnection(connString);

            try
            {
                //string saveStaff = "UPDATE NARF_NewAccount ("+fieldName+ ") VALUES(@"+fieldName+ ") WHERE ApplicationId = "+ applicationID+";";
                string saveStaff = "UPDATE NARF_FB1 SET " + fieldName + "='" + fileName + "' WHERE CustomerID = " + applicationID + ";";

                using (SqlCommand cmd = new SqlCommand(saveStaff, cnn))
                {
                    cnn.Open();
                    cmd.ExecuteNonQuery();

                    if (cnn.State == System.Data.ConnectionState.Open)
                        cnn.Close();
                }               
            }
            catch (SqlException ex)
            {
                cnn.Close();
            }
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value11", "value12" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        [Route("add")]
        
        public string TestMethod(NewAccount newAccount)
        {
            //int recordID = insertIntoMainTable(newAccount);
            Result r= insertIntoMainTable(newAccount);
            int recordID = r.code;
            if (recordID != 0)
            {
                Result riinfb1 = insertIntoNarfFB1(recordID, newAccount);
                if (riinfb1.code != 0)
                {
                    Result riict = insertIntoChildTable(recordID, newAccount);
                    if (riict.code != -1)
                    {
                        if (newAccount.companyInfo.TaxExempt == 0 && newAccount.companyInfo.ResaleCertificateNumber == "")
                        {
                            string message = buildBody(newAccount);
                            sendEmail(message, newAccount, false);
                        }
                        return "{'result':'Success','data':{'ApplicationId':" + recordID + "}}";//+ recordID;
                    }
                    else
                        return "{'result':'Success','data':{'ApplicationId':" + recordID + "'},'error':{'" + riict.errorDesc + "'}}";
                }
                else
                    return "{'result':'Success','data':{'ApplicationId':" + recordID + "'},'error':{'" + riinfb1.errorDesc + "'}}";
            }
            else
                return "{'result':'Failed','data':{'ApplicationId': 'NA'},'error':{'" + r.errorDesc+"'}}"; 
            //return "abc";

        }

        [HttpPost]
        [Route("uploadDocs")]
        public string MyFileUpload()
        {
            try
            {
                var request = HttpContext.Request;
                var appID = request.Form["ApplicationId"];

                if (!Directory.Exists(".\\images\\"))
                {
                    DirectoryInfo di = Directory.CreateDirectory(".\\images\\");
                }

                foreach (var f in HttpContext.Request.Form.Files)
                {
                    var now = DateTime.Now.ToString("MMddyyyyHHmmssfff");  //fff is milliseconds

                    //var filePath = ".\\images\\" + f.FileName;

                    var filePath = ".\\images\\" + now + "_" + f.FileName;

                    //var fieldName = f.Name;
                    using (var fs = new System.IO.FileStream(filePath, System.IO.FileMode.Create))
                    {
                        f.CopyTo(fs);
                        //request.Body.CopyTo(fs);
                    }

                    imageUploadIntoTable(appID, f.Name, now + "_" + f.FileName);
                }

                /* send Email After Images Upload*/

                NewAccount newAccount = getCreatedAccount(appID);
                string message = buildBody(newAccount);
                sendEmail(message, newAccount, true);

                return "{'result':'Success','data':{'uploaded':'uploaded'}}";
            }
            catch(Exception e)
            {
                return "{'result':'Failure','data':{'uploaded':'Failed'},'error':{'" + e.ToString() + "'}}";
            }
        }

        public string buildBody(NewAccount newAccount)
        {
            try
            {
                String taxEx = "No";
                if (newAccount.companyInfo.TaxExempt == 1)
                    taxEx = "Yes";

                String poreq = "No";
                if (newAccount.companyInfo.PORequired == 1)
                    poreq = "Yes";

                String ahfba = "No";
                if (newAccount.companyInfo.AlreadyHasFBAccount == 1)
                    ahfba = "Yes";

                string tab1 = "<img src='http://courage.co.in/fb/images/logo.jpeg' width='180' height='80'/>" +
                   " <h3>Company Information</h3></br>" +
                    "<table>" +
                    "<tr><td>Company Type:</td><td>" + newAccount.companyInfo.CompanyType + "</td></tr>" +
                    "<tr><td>DUNS Number:</td><td>" + newAccount.companyInfo.DUNSNumber + "</td></tr>" +
                    "<tr><td>Full Legal Name/Business Entity:</td><td>" + newAccount.companyInfo.Name + "</td></tr>" +
                    "<tr><td>DBA Name(s):</td><td>" + newAccount.companyInfo.DBAName + "</td><tr>" +
                    "<tr><td>Phone:</td><td>" + newAccount.companyInfo.Phone + "</td></tr>" +
                    "<tr><td>Email:</td><td>" + newAccount.companyInfo.Email + "</td></tr>" +
                    "<tr><td>Delivery Address1:</td><td>" + newAccount.companyInfo.DeliveryAddress1 + "</td></tr>" +
                    "<tr><td>Delivery Address2:</td><td>" + newAccount.companyInfo.DeliveryAddress2 + "</td></tr>" +
                    "<tr><td>Delivery Address3:</td><td>" + newAccount.companyInfo.DeliveryAddress3 + "</td></tr>" +
                    "<tr><td>Delivery Address4:</td><td>" + newAccount.companyInfo.DeliveryAddress4 + "</td></tr>" +
                    "<tr><td>Delivery City:</td><td>" + newAccount.companyInfo.DeliveryCity + "</td></tr>" +
                    "<tr><td>Delivery State:</td><td>" + newAccount.companyInfo.DeliveryState + "</td></tr>" +
                    "<tr><td>Delivery Zip:</td><td>" + newAccount.companyInfo.DeliveryZip + "</td></tr>" +
                    "<tr><td>Billing Address:</td><td>" + newAccount.companyInfo.BillingAddress + "</td></tr>" +
                    "<tr><td>Billing City:</td><td>" + newAccount.companyInfo.BillingCity + "</td></tr>" +
                    "<tr><td>Billing State:</td><td>" + newAccount.companyInfo.BillingState + "</td></tr>" +
                    "<tr><td>Billing Zip:</td><td>" + newAccount.companyInfo.BillingZip + "</td></tr>" +
                    "<tr><td>Billing Phone:</td><td>" + newAccount.companyInfo.BillingPhone + "</td></tr>" +
                    "<tr><td>Nature of business:</td><td>" + newAccount.companyInfo.NatureOfBusiness + "</td></tr>" +
                    "<tr><td>Principal Officer:</td><td>" + newAccount.companyInfo.PrincipalOfficer + "</td></tr>" +
                    "<tr><td>Title:</td><td>" + newAccount.companyInfo.PrincipalOfficerTitle + "</td></tr>" +
                    "<tr><td>Year business established:</td><td>" + newAccount.companyInfo.EstablishedYear + "</td></tr>" +
                    "<tr><td>State Incorporated:</td><td>" + newAccount.companyInfo.StateIncorporated + "</td></tr>" +
                    "<tr><td>Federal Tax ID #:</td><td>" + newAccount.companyInfo.FederalTaxID + "</td></tr>" +
                    "<tr><td>Resale Certificate No.:</td><td>" + newAccount.companyInfo.ResaleCertificateNumber + "</td></tr>" +
                    "<tr><td>Resale Certificate File:</td><td>" + newAccount.companyInfo.ResaleCertificateNoFile + "</td></tr>" +
                    "<tr><td>Tax exempt:</td><td>" + taxEx + "</td></tr>" +
                    "<tr><td>Tax exempt File:</td><td>" + newAccount.companyInfo.TaxExemptFile + "</td></tr>" +
                    "<tr><td>Previous account with Farmer Brothers? :</td><td>" + ahfba + "</td></tr>" +
                    "<tr><td>FB Account Name and Location:</td><td>" + newAccount.companyInfo.FBAccount + "</td></tr>" +
                    "<tr><td>PO Required?:</td><td>" + poreq + "</td></tr>" +
                    "<tr><td>Accounts Payable Contact:</td><td>" + newAccount.companyInfo.AccountPayableContact + "</td></tr>" +
                    "<tr><td>Title:</td><td>" + newAccount.companyInfo.AccountPayableTitle + "</td></tr>" +
                    "<tr><td>Phone Number:</td><td>" + newAccount.companyInfo.AccountPayablePhone + "</td></tr>" +
                    "<tr><td>Email:</td><td>" + newAccount.companyInfo.AccountPayableEmail + "</td></tr>" +                    
                    "<tr><td>Secondary Account Payable Contact:</td><td>" + newAccount.companyInfo.SecondaryAccountPayableContact + "</td></tr>" +
                    "<tr><td>Secondary Account Payable Title:</td><td>" + newAccount.companyInfo.SecondaryAccountPayableTitle + "</td></tr>" +
                    "<tr><td>Secondary Account Payable Phone:</td><td>" + newAccount.companyInfo.SecondaryAccountPayablePhone + "</td></tr>" +
                    "<tr><td>Secondary Account Payable Email:</td><td>" + newAccount.companyInfo.SecondaryAccountPayableEmail + "</td></tr>" +
                    "</table>";

                string tab2 = "</br><h3>Trade References</h3></br><table>";
                List<TradeReference> tr = newAccount.tradeAndBankRef.TradeReferences;
                foreach (TradeReference t in tr)
                {
                    tab2 = tab2 + "<tr><td>Company Name:</td><td>" + t.CompanyName + "</td><tr>" +
                        "<tr><td>Account #:</td><td>" + t.AccountID + "</td><tr>" +
                        "<tr><td>EmailID #:</td><td>" + t.EmailID + "</td><tr>" +
                        "<tr><td>Phone:</td><td>" + t.Phone + "</td><tr>";
                }
                tab2 = tab2 + "</table>";

                string tab3 = "</br><h3>Bank References</h3></br><table>" +
                     "<tr><td>Bank Name:</td><td>" + newAccount.tradeAndBankRef.BankName + "</td></tr>" +
                     "<tr><td>Account#:</td><td>" + newAccount.tradeAndBankRef.BankAccountNo + "</td><tr>" +
                     "<tr><td>Phone#:</td><td>" + newAccount.tradeAndBankRef.BankPhone + "</td></tr>" +
                     "<tr><td>Email#:</td><td>" + newAccount.tradeAndBankRef.BankEmailID + "</td></tr>" +
                     "<tr><td>Bank Reference File: </td><td>" + newAccount.companyInfo.BankRefDocumentFile + "</td></tr>" +
                     "</table>";

            string tab4 = "</br><h3>Personal Guarantee</h3></br><table>" +
                 "<tr><td>First Name:</td><td>" + newAccount.personalGuarantee.FirstName + "</td></tr>" +
                 "<tr><td>Middle Name:</td><td>" + newAccount.personalGuarantee.MiddleName + "</td></tr>" +
                 "<tr><td>Last Name:</td><td>" + newAccount.personalGuarantee.LastName + "</td></tr>" +
                 "<tr><td>Title:</td><td>" + newAccount.personalGuarantee.Title + "</td></tr>" +
                 "<tr><td>Present Home Address:</td><td>" + newAccount.personalGuarantee.PresentHomeAddress + "</td></tr>" +
                 "<tr><td>City:</td><td>" + newAccount.personalGuarantee.City + "</td></tr>" +
                 "<tr><td>State:</td><td>" + newAccount.personalGuarantee.State + "</td></tr>" +
                 "<tr><td>Zip:</td><td>" + newAccount.personalGuarantee.Zip + "</td></tr>" +
                 "<tr><td>Date of Birth:</td><td>" + ((newAccount.personalGuarantee.DOB== "01-01-1900")?"": newAccount.personalGuarantee.DOB) + "</td></tr>" +
                 "<tr><td>Auth Sign File #:</td><td>" + newAccount.companyInfo.PersonalGuaranteeAuthSignFile + "</td></tr>" +
                /*"<tr><td>SSN:</td><td>" + newAccount.personalGuarantee.SSN + "</td></tr>" +
                "<tr><td>Driver's Licence Number & State:</td><td>" + newAccount.personalGuarantee.DriverLicenceNoAndState + "</td></tr>" +*/
                "<tr><td>Date:</td><td>" + ((newAccount.personalGuarantee.Date == "01-01-1900") ? "" : newAccount.personalGuarantee.Date) + "</td></tr>" +
                "</table>";

            string tab5 = "</br><h3>Consent and Signature</h3></br><table>" +                
                 "<tr><td>Print Name:</td><td>" + newAccount.consentAndSignature.PrintName + "</td></tr>" +
                 "<tr><td>Title:</td><td>" + newAccount.consentAndSignature.Title + "</td></tr>" +
                 "<tr><td>Date:</td><td>" + newAccount.consentAndSignature.Date + "</td></tr>" +                 
                "</table>";

            if(newAccount.personalGuarantee.FirstName=="" && newAccount.personalGuarantee.MiddleName == "" && newAccount.personalGuarantee.LastName == "")
                    return tab1 + tab2 + tab3 + tab5;
            else
                return tab1 + tab2 + tab3 + tab4 + tab5;
            }
            catch(Exception e)
            {
                return e.ToString();
            }
            //return "abc";
        }

    
        public NewAccount getCreatedAccount(string applicationID)
        {            
            string connString = this.Configuration.GetConnectionString("DefaultConnection");
            NewAccount newAccount =new NewAccount();
            newAccount.companyInfo = new CompanyInfo();
            newAccount.tradeAndBankRef = new TradeAndBankRef();
            newAccount.personalGuarantee = new PersonalGuarantee();
            newAccount.consentAndSignature = new ConsentAndSignature();
            newAccount.userInfo = new UserInfo();
            

            SqlConnection cnn = new SqlConnection(connString);
          
            try
            {
                string getimgNames = "select hd.*,fb.* from NARF_FB1 as fb,NARF_Header as hd where fb.CustomerID = hd.CustomerID " +
                                     "and hd.CustomerID = " + applicationID + ";";

                using (SqlCommand cmd = new SqlCommand(getimgNames, cnn))
                {
                    cnn.Open();
                    //cmd.ExecuteNonQuery();

                    SqlDataReader reader = cmd.ExecuteReader();
                    
                    while (reader.Read())
                    {                          
                        newAccount.companyInfo.Name = (string)reader["CustomerName"];
                        newAccount.companyInfo.Phone = (string)reader["Phone"];
                        newAccount.companyInfo.Email = (string)reader["Email"];
                        newAccount.companyInfo.DBAName = (string)reader["DBAName"];
                        newAccount.companyInfo.BillingAddress = (string)reader["BillingAddress"];
                        newAccount.companyInfo.BillingState = (string)reader["BillingState"];                        
                        newAccount.companyInfo.BillingCity = (string)reader["BillingCity"];
                        newAccount.companyInfo.BillingZip = (string)reader["BillingZip"];
                        newAccount.companyInfo.BillingPhone = (string)reader["BillingPhone"];
                        newAccount.companyInfo.DeliveryAddress1 = (string)reader["DeliveryAddress1"];
                        newAccount.companyInfo.DeliveryAddress2 = (string)reader["DeliveryAddress2"];
                        newAccount.companyInfo.DeliveryAddress3 = (string)reader["DeliveryAddress3"];
                        newAccount.companyInfo.DeliveryAddress4 = (string)reader["DeliveryAddress4"];
                        newAccount.companyInfo.DeliveryState = (string)reader["DeliveryState"];
                        newAccount.companyInfo.DeliveryCity = (string)reader["DeliveryCity"];
                        newAccount.companyInfo.DeliveryZip = (string)reader["DeliveryZip"];
                        newAccount.companyInfo.CompanyType = (string)reader["CompanyType"];
                        newAccount.companyInfo.PrincipalOfficer = (string)reader["PrincipalOfficer"];
                        newAccount.companyInfo.PrincipalOfficerTitle = (string)reader["PrincipalOfficerTitle"];
                        newAccount.companyInfo.NatureOfBusiness = (string)reader["NatureOfBusiness"];
                        newAccount.companyInfo.NatureOfBusiness = (string)reader["NatureOfBusiness"];

                        newAccount.companyInfo.EstablishedYear = (string)reader["EstablishedYear"];
                        newAccount.companyInfo.StateIncorporated = (string)reader["StateIncorporated"];
                        newAccount.companyInfo.FederalTaxID = (string)reader["FederalTaxID"];
                        newAccount.companyInfo.ResaleCertificateNumber = (string)reader["ResaleCertificateNumber"];
                        
                        newAccount.companyInfo.TaxExempt = Convert.ToInt32((Boolean)reader["TaxExempt"]);
                        newAccount.companyInfo.PORequired = Convert.ToInt32((Boolean)reader["PORequired"]);
                        newAccount.companyInfo.AlreadyHasFBAccount = Convert.ToInt32((Boolean)reader["AlreadyHasFBAccount"]);
                       
                        newAccount.companyInfo.FBAccount = (string)reader["FBAccount"];
                        newAccount.companyInfo.AccountPayableContact = (string)reader["AccountPayableContact"];
                        newAccount.companyInfo.AccountPayableTitle = (string)reader["AccountPayableTitle"];
                        newAccount.companyInfo.AccountPayablePhone = (string)reader["AccountPayablePhone"];
                        newAccount.companyInfo.AccountPayableEmail = (string)reader["AccountPayableEmail"];

                        newAccount.companyInfo.SecondaryAccountPayableContact = (string)reader["SecondaryAccountPayableContact"];
                        newAccount.companyInfo.SecondaryAccountPayableTitle = (string)reader["SecondaryAccountPayableTitle"];
                        newAccount.companyInfo.SecondaryAccountPayablePhone = (string)reader["SecondaryAccountPayablePhone"];
                        newAccount.companyInfo.SecondaryAccountPayableEmail = (string)reader["SecondaryAccountPayableEmail"];
                        newAccount.companyInfo.DUNSNumber = (string)reader["DUNSNumber"];                        

                        newAccount.userInfo.CreatedUserID = (int)reader["CreatedUserID"];                      
                        newAccount.userInfo.CreatedUserName = (string)reader["CreatedUserName"];

                        string s =(string) reader["CreatedDate"].ToString();
                        DateTime cd = DateTime.Parse(s);
						
						newAccount.userInfo.CreatedDate = cd;
                        //MessageBox.Show(newAccount.CreatedDate.ToString());
                        
                        newAccount.userInfo.ModifiedUserID = (int)reader["ModifiedUserID"];                        
                        newAccount.userInfo.ModifiedUserName = (string)reader["ModifiedUserName"];

                        string s1 = (string)reader["ModifiedDate"].ToString();
                        DateTime md = DateTime.Parse(s1);

                        newAccount.userInfo.ModifiedDate = md ;

                        newAccount.tradeAndBankRef.BankName = (string)reader["BankName"];
                        newAccount.tradeAndBankRef.BankAccountNo = (string)reader["AccountNo"];
                        newAccount.tradeAndBankRef.BankAddress = (string)reader["BankAddress"];
                        newAccount.tradeAndBankRef.BankCity = (string)reader["BankCity"];
                        newAccount.tradeAndBankRef.BankState = (string)reader["BankState"];
                        newAccount.tradeAndBankRef.BankZip = (string)reader["BankZip"];
                        newAccount.tradeAndBankRef.BankPhone = (string)reader["BankPhone"];
                        newAccount.tradeAndBankRef.BankEmailID = (string)reader["BankEmailID"];

                        if (reader["TaxExemptFile"].ToString()!="")
                            newAccount.companyInfo.TaxExemptFile= (string)reader["TaxExemptFile"];
                        if (reader["ResaleCertificateNoFile"].ToString() != "")
                            newAccount.companyInfo.ResaleCertificateNoFile= (string)reader["ResaleCertificateNoFile"];
                        if (reader["BankRefDocumentFile"].ToString() != "")
                            newAccount.companyInfo.BankRefDocumentFile = (string)reader["BankRefDocumentFile"];
                        if (reader["PersonalGuaranteeAuthSignFile"].ToString() != "")
                            newAccount.companyInfo.PersonalGuaranteeAuthSignFile = (string)reader["PersonalGuaranteeAuthSignFile"];

                        if (reader["baidtda"].ToString() != "")                        
                            newAccount.companyInfo.baidtda = reader["baidtda"].ToString();
                        else
                            newAccount.companyInfo.baidtda = "";
                        /* to be done
                         if (reader["ConsentAuthSignFile"].ToString() != "")
                             newAccount.companyInfo.ConsentAuthSignFile = (string)reader["ConsentAuthSignFile"];*/

                        newAccount.tradeAndBankRef.TradeReferences = getTradeRef(applicationID);


                        s1 = (string)reader["PGDOB"].ToString();
                        newAccount.personalGuarantee.DOB = DateTime.Parse(s1).ToString("dd-MM-yyyy");

                        s1 = (string)reader["PGDate"].ToString();
                        newAccount.personalGuarantee.Date = DateTime.Parse(s1).ToString("dd-MM-yyyy");

                        /*s1 = (string)reader["PGDOB"].ToString();
                        if (s1 == "01-01-1900 00:00:00")
                        {
                            //newAccount.personalGuarantee.DOB = "01-01-1900";
                            //DateTime dateTime1 = DateTime.ParseExact("10-02-2022", "dd-MM-yyyy hh:mm:ss", CultureInfo.InvariantCulture); // 10/22/2015 12:00:00 AM  
                            newAccount.personalGuarantee.DOB = "10-02-2022";
                        }
                           
                        else
                        {
                            DateTime dateTime1 = DateTime.ParseExact(s1, "dd-MM-yyyy hh:mm:ss", CultureInfo.InvariantCulture); // 10/22/2015 12:00:00 AM  
                            newAccount.personalGuarantee.DOB = dateTime1.ToString("dd-MM-yyyy");
                        }

                        s1 = (string)reader["PGDate"].ToString();
                        if (s1 == "01-01-1900 00:00:00")
                            newAccount.personalGuarantee.Date = "10-02-2022";
                        else
                        {
                            DateTime dateTime2 = DateTime.ParseExact(s1, "dd-MM-yyyy hh:mm:ss", CultureInfo.InvariantCulture); // 10/22/2015 12:00:00 AM  
                            newAccount.personalGuarantee.Date = dateTime2.ToString("dd-MM-yyyy");
                        }*/                        

                        newAccount.personalGuarantee.FirstName = (string)reader["PGFirstName"];
                        newAccount.personalGuarantee.MiddleName = (string)reader["PGMiddleName"];
                        newAccount.personalGuarantee.LastName = (string)reader["PGLastName"];
                        newAccount.personalGuarantee.Title = (string)reader["PGTitle"];
                        newAccount.personalGuarantee.PresentHomeAddress = (string)reader["PGPresentHomeAddress"];
                        newAccount.personalGuarantee.City = (string)reader["PGCity"];
                        newAccount.personalGuarantee.State = (string)reader["PGState"];
                        newAccount.personalGuarantee.Zip = (string)reader["PGZip"];
                        //newAccount.personalGuarantee.DOB = (string)reader["PGDOB"];
                        //newAccount.personalGuarantee.SSN = (string)reader["PGSSN"];
                        //newAccount.personalGuarantee.DriverLicenceNoAndState = (string)reader["PGDriverLicenceNoAndState"];
                        //newAccount.personalGuarantee.Date = (string)reader["PGDate"];                        

                        /* Consent and Signature */                        
                        newAccount.consentAndSignature.TermsAndConditions = (bool)reader["CASTermsAndConditions"];                        
                        newAccount.consentAndSignature.PrintName = (string)reader["CASPrintName"];
                        newAccount.consentAndSignature.Title = (string)reader["CASTitle"];
                        s1 = (string)reader["CASDate"].ToString();
                        newAccount.consentAndSignature.Date = DateTime.Parse(s1).ToString("dd-MM-yyyy");                       

                    }

                    if (cnn.State == System.Data.ConnectionState.Open)
                        cnn.Close();
                }
            }
            catch (SqlException ex)
            {
                cnn.Close();
            }

            //string[] imgNames = { TaxExemptFile, ResaleCertificateNoFile };

            return newAccount ;
        }

        private List<TradeReference> getTradeRef(string applicationID)
		{
            string connString = this.Configuration.GetConnectionString("DefaultConnection");            
            List<TradeReference> tradeRefList = new List<TradeReference>();

            SqlConnection cnn = new SqlConnection(connString);

            try
            {
                string getimgNames = "SELECT * FROM  NARF_TradeReference WHERE " +
                                    " ApplicationId = " + applicationID + ";";

                using (SqlCommand cmd = new SqlCommand(getimgNames, cnn))
                {
                    cnn.Open();
                    //cmd.ExecuteNonQuery();

                    SqlDataReader reader = cmd.ExecuteReader();                    

                    while (reader.Read())
                    {
                        TradeReference tr = new TradeReference();
                        tr.AccountID= (string)reader["AccountID"];
                        tr.CompanyName = (string)reader["CompanyName"];
                        tr.Phone = (string)reader["Phone"];
                        tr.EmailID = (string)reader["EmailID"];

                        tradeRefList.Add(tr);
                    }

                    if (cnn.State == System.Data.ConnectionState.Open)
                        cnn.Close();
                }
            }
            catch (SqlException ex)
            {
                cnn.Close();
            }            

            return tradeRefList;
        }

        //public String
        [HttpGet]
        [Route("sendEmail")]
        public void sendEmail(string msg, NewAccount newAccount,bool hasAttachments)
        {                        
            string to = "ramp@seedolabs.com , madhuri.lingam@gmail.com"; //To address  
            //string to = "kishore.penjarla@gmail.com"; //To address  

            string from = "maicsd2022@gmail.com"; //From address    
            MailMessage message = new MailMessage(from, to);

            string mailbody = msg;  //"Test Email"; 
            message.Subject = "[Farmer Brothers] Account Application";
            message.Body = mailbody;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp    
            client.UseDefaultCredentials = false;
            client.EnableSsl = true;
            System.Net.NetworkCredential basicCredential1 = new
            System.Net.NetworkCredential("maicsd2022@gmail.com", "M14MAI@2022");

            if (hasAttachments == true)
            {
                if (newAccount.companyInfo.TaxExemptFile != null)
                {
                    var attachmentPath = ".\\images\\" + newAccount.companyInfo.TaxExemptFile;
                    Attachment inline = new Attachment(attachmentPath);
                    inline.ContentDisposition.Inline = true;
                    inline.ContentDisposition.DispositionType = DispositionTypeNames.Inline;
                    //inline.ContentType.MediaType = "image/jpeg";
                    inline.ContentType.Name = Path.GetFileName(attachmentPath);
                    message.Attachments.Add(inline);
                }

                if (newAccount.companyInfo.ResaleCertificateNoFile != null)
                {
                    var attachmentPath = ".\\images\\" + newAccount.companyInfo.ResaleCertificateNoFile;
                    Attachment inline = new Attachment(attachmentPath);
                    inline.ContentDisposition.Inline = true;
                    inline.ContentDisposition.DispositionType = DispositionTypeNames.Inline;
                   // inline.ContentType.MediaType = "image/jpeg";
                    inline.ContentType.Name = Path.GetFileName(attachmentPath);
                    message.Attachments.Add(inline);
                }

                if (newAccount.companyInfo.BankRefDocumentFile != null)
                {
                    var attachmentPath = ".\\images\\" + newAccount.companyInfo.BankRefDocumentFile;
                    Attachment inline = new Attachment(attachmentPath);
                    inline.ContentDisposition.Inline = true;
                    inline.ContentDisposition.DispositionType = DispositionTypeNames.Inline;
                    // inline.ContentType.MediaType = "image/jpeg";
                    inline.ContentType.Name = Path.GetFileName(attachmentPath);
                    message.Attachments.Add(inline);
                }

                /*if (newAccount.companyInfo.PersonalGuaranteeAuthSignFile != null)
                {
                    var attachmentPath = ".\\images\\" + newAccount.companyInfo.PersonalGuaranteeAuthSignFile;
                    Attachment inline = new Attachment(attachmentPath);
                    inline.ContentDisposition.Inline = true;
                    inline.ContentDisposition.DispositionType = DispositionTypeNames.Inline;
                    // inline.ContentType.MediaType = "image/jpeg";
                    inline.ContentType.Name = Path.GetFileName(attachmentPath);
                    message.Attachments.Add(inline);
                }*/
                }

                    client.Credentials = basicCredential1;
            try
            {
                client.Send(message);
                //return "Success";
            }

            catch (Exception ex)
            {
                MessageBox.Show("Message not emailed: " + ex.ToString());

                //return ex.ToString();
            }
        }

        public static string Encrypt(string encryptString)
        {
            string EncryptionKey = "ram@1234xxfbProjectxxtttttuuuuuiiiiio";
            byte[] clearBytes = Encoding.Unicode.GetBytes(encryptString);
            using (Aes encryptor = Aes.Create())
            {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
                0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
            });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    encryptString = Convert.ToBase64String(ms.ToArray());
                }
            }
            return encryptString;
        }

        public static string Decrypt(string cipherText)
        {
            string EncryptionKey = "0ram@1234xxfbProjectxxtttttuuuuuiiiiio";  
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
        });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
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
