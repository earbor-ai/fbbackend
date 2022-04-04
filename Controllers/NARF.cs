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
    public class NARF : ControllerBase
    {

        private IConfiguration Configuration;

        public NARF(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        public class Result
        {
            public string errorDesc;
            public int code;
        }

        public class Account
        {
            public List<AccountSearchResponse> accounts { get; set; }
        }

        public class AccountSearchRequest
        {
            public string searchAccountNo;
            public string searchCompanyName;
            public string searchAddress;
            public string searchCity;
            public string searchState;
            public string searchStatus;
        }

        public class AccountSearchResponse
        {
            public string CustomerID { get; set; }
            public string CustomerJDE { get; set; }
            public string DUNSNumber { get; set; }
            public string CustomerName { get; set; }
            public string DBAName { get; set; }
            public string DeliveryAddress1 { get; set; }
            public string DeliveryAddress2 { get; set; }
            public string DeliveryAddress3 { get; set; }
            public string DeliveryAddress4 { get; set; }
            public string DeliveryState { get; set; }
            public string DeliveryCity { get; set; }
            public string DeliveryZip { get; set; }
            public string Phone { get; set; }
            public string Fax { get; set; }
            public string Email { get; set; }
            public string County { get; set; }
            public string PrincipalOfficer { get; set; }
            public string PrincipalOfficerTitle { get; set; }
            public string PrincipalCell { get; set; }
            public string PrincipalEmail { get; set; }
            public string OwnerName { get; set; }
            public string BillingAddress { get; set; }
            public string BillingState { get; set; }
            public string BillingCity { get; set; }
            public string BillingZip { get; set; }
            public string FederalTaxID { get; set; }
            public string ResaleCertificateNumber { get; set; }
            public string TaxGroup { get; set; }
            public string CompanyType { get; set; }
            public string NatureOfBusiness { get; set; }
            public string EstablishedYear { get; set; }
            public string StateIncorporated { get; set; }
            public string TaxExempt { get; set; }
            public string PORequired { get; set; }
            public string AlreadyHasFBAccount { get; set; }
            public string FBAccount { get; set; }
            public string fb1Status { get; set; }
            public string fb1UpdatedDate { get; set; }
            public string narfStatus { get; set; }
            public string narfUpdatedDate { get; set; }
            public string customerContactName { get; set; }
            public string baidtda { get; set; }
            public string alphaName { get; set; }
            public string legalMailingName { get; set; }
            public string eocno { get; set; }
            public string eeBilltoAcc { get; set; }
            public string cnBilltoAcc { get; set; }
            public string dailyDelvSeq { get; set; }
            public string freqDailyDelv { get; set; }
        }

        public class getNARFAccountLookupResponse
        {
            public string result { get; set; }
            public Account data { get; set; }
            public string error { get; set; }
        }

        public class NarfNewAccountReq
        {
            public CompanyInfoReq CompanyInfo;
            /*public TradeAndBankRef tradeAndBankRef;
            public PersonalGuarantee personalGuarantee;
            public ConsentAndSignature consentAndSignature;*/
            public UserInfo userInfo;
        }

        public class CompanyInfoReq
        {
            public string CustomerID;
            public string Purpose;
            public string OperatingUnit;
            public string Route;
            public string OriginatorName;
            public string OriginatorPhone;
            public string SearchType;
            public string BranchNo;
            public string DistrictNo;
            public string RegionNo;
            public string BusinessUnit;
            public string customerCell;
            public string ManagedBy;
            public string CustomerSegment;
            public string NewAcctAcquiredBySABDM;
            public string EquipServiceLevel;
            public string CustomerGroup;
            public string POSPrgram;
            public string AlliedEquipProgram;
            public string PriceProtection;
            public string AlliedDiscount;
            public string WeeklyCoffeeVolume;
            public string EquipmentProgram;
            public string CC30;
            public string EstBiWeeklySales;
            public string TermsOfSale;
            public string CreditLimit;
            public string AdjustmentSchedule;
            public string FreightHandlingCode;
            public string ParentNo;
            public string ParentChainNo;
            public string NewAcctAcquiredByRSR;
            public string EmployeeTitle;
            public string EmployeeNameNo;
            public string Notes;

            /* Header Fields */
            public string LegalName;
            public string LocationPhoneNo;
            public string CustomerEmail;
            public string AlphaDBAName;
            public string BillingAddress;
            public string BillingState;
            public string BillingCity;
            public string BillingZip;
            public string DeliveryAddressLine1;
            public string DeliveryAddressLine2;
            public string DeliveryAddressLine3;
            public string DeliveryAddressLine4;
            public string State;
            public string City;
            public string Zip;
            public string CompanyType;
            public string CustomerContactName;
            public string CustomerCellNo;
            public string FedTaxIDNo;
            public string ResaleCertNo;
            public string OwnerName;
            public string LocationFaxNo;
            public string County;
            public string TaxGroup;
            public string narfStatus;
            public string BillingAddressIfDifferentThanDeliveryAddress;
            public string AlphaName;
            public string LegalMailingName;
            public string ExistingOrCreateNewOption;
            public string EnterExistingBillToAcctNo;
            public string CreateNewBillToAcctNo;
            public string DailyDeliverySequence;
            public string FrequencyDailyDelivery;
            public List<CategoryAlliedAdjustment> categoryAlliedAdjustment { get; set; }
        }
        public class CategoryAlliedAdjustment
        {
            public string categoryCode { get; set; }
            public string categoryValue { get; set; }
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

        public class genericResponse
        {
            public string customerID { get; set; }
        }

        public class genericResponseUpload
        {
            public string uploaded { get; set; }
        }
        public class sendInsertResponse
        {
            public string result { get; set; }
            public genericResponse data { get; set; }
            public string error { get; set; }
        }
        public class sendUploadResponse
        {
            public string result { get; set; }
            public genericResponseUpload data { get; set; }
            public string error { get; set; }
        }

        public class TradeReference
        {
            public string CompanyName { get; set; }
            public string AccountID { get; set; }
            public string Phone { get; set; }
            public string EmailID { get; set; }
        }
        public class fb1TableRequest
        {
            public string CustomerID;
        }
        public class fb1TableResponse
        {
            public string CustomerID { get; set; }
            public string AccountPayableContact { get; set; }
            public string AccountPayableTitle { get; set; }
            public string AccountPayablePhone { get; set; }
            public string AccountPayableEmail { get; set; }
            public string CreatedUserID { get; set; }
            public string CreatedUserName { get; set; }
            public string CreatedDate { get; set; }
            public string ModifiedUserID { get; set; }
            public string ModifiedUserName { get; set; }
            public string ModifiedDate { get; set; }
            public string BankName { get; set; }
            public string AccountNo { get; set; }
            public string BankAddress { get; set; }
            public string BankCity { get; set; }
            public string BankState { get; set; }
            public string BankZip { get; set; }
            public string BankPhone { get; set; }
            public string TaxExemptFile { get; set; }
            public string ResaleCertificateNoFile { get; set; }
            public string SecondaryAccountPayableContact { get; set; }
            public string SecondaryAccountPayableTitle { get; set; }
            public string SecondaryAccountPayablePhone { get; set; }
            public string SecondaryAccountPayableEmail { get; set; }
            public string BankEmailID { get; set; }
            public string BillingPhone { get; set; }
            public string PGFirstName { get; set; }
            public string PGMiddleName { get; set; }
            public string PGLastName { get; set; }
            public string PGTitle { get; set; }
            public string PGPresentHomeAddress { get; set; }
            public string PGCity { get; set; }
            public string PGState { get; set; }
            public string PGZip { get; set; }
            public string PGDOB { get; set; }
            public string PGSSN { get; set; }
            public string PGDriverLicenceNoAndState { get; set; }
            public string PGDate { get; set; }
            public string BankRefDocumentFile { get; set; }
            public string PersonalGuaranteeAuthSignFile { get; set; }
            public string ConsentAuthSignFile { get; set; }
            public string CASTermsAndConditions { get; set; }
            public string CASPrintName { get; set; }
            public string CASTitle { get; set; }
            public string CASDate { get; set; }
            public List<TradeReference> tradeReferences { get; set; }
            
        }
        public class getFB1AccountLookupResponse
        {
            public string result { get; set; }
            public fb1TableResponse data { get; set; }
            public string error { get; set; }
        }

        public class NarfTableRequest
        {
            public string CustomerID;
        }
        public class NarfTableResponse
        {
            //public string NarfID { get; set; }
            public string CustomerID { get; set; }
            public string purpose { get; set; }
            public string operatingUnit { get; set; }
            public string route { get; set; }
            public string originatorName { get; set; }
            public string originatorPhone { get; set; }
            public string searchType { get; set; }
            public string branch { get; set; }
            public string district { get; set; }
            public string region { get; set; }
            public string customerCell { get; set; }
            public string managedBy { get; set; }
            public string customerSegment { get; set; }
            public string equipServiceLevel { get; set; }
            public string customerGroup { get; set; }
            public string posProgram { get; set; }
            public string alliedEquipProgram { get; set; }
            public string priceProtection { get; set; }
            public string alliedDiscount { get; set; }
            public string weeklyCoffeeVolume { get; set; }
            public string equipmentProgram { get; set; }
            public string CC30 { get; set; }
            public string estBiWeeklySales { get; set; }
            public string termsOfSale { get; set; }
            public string creditLimit { get; set; }
            public string adjustmentSchedule { get; set; }
            public string freightHandlingCode { get; set; }
            public string parentNumber { get; set; }
            public string parentChainNumber { get; set; }
            public string employeeTitle { get; set; }
            public string employeeName { get; set; }
            public string Notes { get; set; }
            public string CreatedUserID { get; set; }
            public string CreatedUserName { get; set; }
            public string CreatedDate { get; set; }
            public string ModifiedUserID { get; set; }
            public string ModifiedUserName { get; set; }
            public string ModifiedDate { get; set; }
            public string businessUnit { get; set; }
            public string acctAcquiredBySA { get; set; }
            public string acctAcquiredByRSR { get; set; }
            public string resaleCertificateNoFile { get; set; }
            public List<CategoryAlliedAdjustment> categoryAlliedAdjustment { get; set; }
        }
        public class getNarfAccountLookupResponse
        {
            public string result { get; set; }
            public NarfTableResponse data { get; set; }
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
        [Route("searchNARF")]
        public string getNARFAccountLookup(AccountSearchRequest accountSearchRequest)
        {
            string connString = this.Configuration.GetConnectionString("DefaultConnection");
            getNARFAccountLookupResponse gplr = new getNARFAccountLookupResponse();
            Account ac = new Account();

            SqlConnection cnn = new SqlConnection(connString);

            try
            {
                string whereClause = "";
                // SEARCH ACCOUNT NUMBER
                if (accountSearchRequest.searchAccountNo != "")
                    whereClause += "CustomerID = " + accountSearchRequest.searchAccountNo;
                // SEARCH COMPANY
                if (accountSearchRequest.searchCompanyName != "" && whereClause != "")
                    whereClause += " OR CustomerName like '%" + accountSearchRequest.searchCompanyName + "%'";
                else if (accountSearchRequest.searchCompanyName != "" && whereClause == "")
                    whereClause += "CustomerName like '%" + accountSearchRequest.searchCompanyName + "%'";

                // SEARCH ADDRESS
                if (accountSearchRequest.searchAddress != "" && whereClause != "")
                    whereClause += " OR DeliveryAddress1 like '%" + accountSearchRequest.searchAddress + "%'";
                else if (accountSearchRequest.searchAddress != "" && whereClause == "")
                    whereClause += "DeliveryAddress1 like '%" + accountSearchRequest.searchAddress + "%'";
                if (accountSearchRequest.searchAddress != "" && whereClause != "")
                    whereClause += " OR DeliveryAddress2 like '%" + accountSearchRequest.searchAddress + "%'";
                else if (accountSearchRequest.searchAddress != "" && whereClause == "")
                    whereClause += "DeliveryAddress2 like '%" + accountSearchRequest.searchAddress + "%'";
                if (accountSearchRequest.searchAddress != "" && whereClause != "")
                    whereClause += " OR DeliveryAddress3 like '%" + accountSearchRequest.searchAddress + "%'";
                else if (accountSearchRequest.searchAddress != "" && whereClause == "")
                    whereClause += "DeliveryAddress3 like '%" + accountSearchRequest.searchAddress + "%'";
                if (accountSearchRequest.searchAddress != "" && whereClause != "")
                    whereClause += " OR DeliveryAddress4 like '%" + accountSearchRequest.searchAddress + "%'";
                else if (accountSearchRequest.searchAddress != "" && whereClause == "")
                    whereClause += "DeliveryAddress4 like '%" + accountSearchRequest.searchAddress + "%'";
                if (accountSearchRequest.searchAddress != "" && whereClause != "")
                    whereClause += " OR BillingAddress like '%" + accountSearchRequest.searchAddress + "%'";
                else if (accountSearchRequest.searchAddress != "" && whereClause == "")
                    whereClause += "BillingAddress like '%" + accountSearchRequest.searchAddress + "%'";

                // SEARCH CITY
                if (accountSearchRequest.searchCity != "" && whereClause != "")
                    whereClause += " OR DeliveryCity like '%" + accountSearchRequest.searchCity + "%'";
                else if (accountSearchRequest.searchCity != "" && whereClause == "")
                    whereClause += "DeliveryCity like '%" + accountSearchRequest.searchCity + "%'";
                if (accountSearchRequest.searchCity != "" && whereClause != "")
                    whereClause += " OR BillingCity like '%" + accountSearchRequest.searchCity + "%'";
                else if (accountSearchRequest.searchCity != "" && whereClause == "")
                    whereClause += "BillingCity like '%" + accountSearchRequest.searchCity + "%'";

                // SEARCH STATE
                if (accountSearchRequest.searchState != "" && whereClause != "")
                    whereClause += " OR DeliveryState = '" + accountSearchRequest.searchState + "'";
                else if (accountSearchRequest.searchState != "" && whereClause == "")
                    whereClause += "DeliveryState = '" + accountSearchRequest.searchState + "'";
                if (accountSearchRequest.searchState != "" && whereClause != "")
                    whereClause += " OR BillingState = '" + accountSearchRequest.searchState + "'";
                else if (accountSearchRequest.searchState != "" && whereClause == "")
                    whereClause += "BillingState = '" + accountSearchRequest.searchState + "'";

                //SEARCH STATUS
                if (accountSearchRequest.searchStatus != "" && whereClause != "")
                    whereClause += " OR fb1Status = '" + accountSearchRequest.searchStatus + "'";
                else if (accountSearchRequest.searchStatus != "" && whereClause == "")
                    whereClause += "fb1Status = '" + accountSearchRequest.searchStatus + "'";
                if (accountSearchRequest.searchStatus != "" && whereClause != "")
                    whereClause += " OR narfStatus = '" + accountSearchRequest.searchStatus + "'";
                else if (accountSearchRequest.searchStatus != "" && whereClause == "")
                    whereClause += "narfStatus = '" + accountSearchRequest.searchStatus + "'";

                string getAccountDetails = "select * from NARF_Header WHERE " + whereClause;

                using (SqlCommand cmd = new SqlCommand(getAccountDetails, cnn))
                {
                    cnn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();
                    List<AccountSearchResponse> lasr = new List<AccountSearchResponse>();

                    while (reader.Read())
                    {
                        AccountSearchResponse asr = new AccountSearchResponse();

                        asr.CustomerID = reader["CustomerID"].ToString();

                        if (reader["CustomerJDE"] != DBNull.Value)
                        {
                            if (reader["CustomerJDE"].ToString() != "0")
                                asr.CustomerJDE = (string)reader["CustomerJDE"];
                            else
                                asr.CustomerJDE = "";
                        }
                        else
                            asr.CustomerJDE = "";

                        if (reader["DUNSNumber"] != DBNull.Value)
                        {
                            if ((string)reader["DUNSNumber"] != "")
                                asr.DUNSNumber = (string)reader["DUNSNumber"];
                            else
                                asr.DUNSNumber = "";
                        }
                        else
                            asr.DUNSNumber = "";

                        if (reader["CustomerName"] != DBNull.Value)
                        {
                            if ((string)reader["CustomerName"] != "")
                                asr.CustomerName = (string)reader["CustomerName"];
                            else
                                asr.CustomerName = "";
                        }
                        else
                            asr.CustomerName = "";

                        if (reader["DBAName"] != DBNull.Value)
                        {
                            if ((string)reader["DBAName"] != "")
                                asr.DBAName = (string)reader["DBAName"];
                            else
                                asr.DBAName = "";
                        }
                        else
                            asr.DBAName = "";

                        if (reader["DeliveryAddress1"] != DBNull.Value)
                        {
                            if ((string)reader["DeliveryAddress1"] != "")
                                asr.DeliveryAddress1 = (string)reader["DeliveryAddress1"];
                            else
                                asr.DeliveryAddress1 = "";
                        }
                        else
                            asr.DeliveryAddress1 = "";

                        if (reader["DeliveryAddress2"] != DBNull.Value)
                        {
                            if ((string)reader["DeliveryAddress2"] != "")
                                asr.DeliveryAddress2 = (string)reader["DeliveryAddress2"];
                            else
                                asr.DeliveryAddress2 = "";
                        }
                        else
                            asr.DeliveryAddress2 = "";

                        if (reader["DeliveryAddress3"] != DBNull.Value)
                        {
                            if ((string)reader["DeliveryAddress3"] != "")
                                asr.DeliveryAddress3 = (string)reader["DeliveryAddress3"];
                            else
                                asr.DeliveryAddress3 = "";
                        }
                        else
                            asr.DeliveryAddress3 = "";

                        if (reader["DeliveryAddress4"] != DBNull.Value)
                        {
                            if ((string)reader["DeliveryAddress4"] != "")
                                asr.DeliveryAddress4 = (string)reader["DeliveryAddress4"];
                            else
                                asr.DeliveryAddress4 = "";
                        }
                        else
                            asr.DeliveryAddress4 = "";


                        if (reader["DeliveryState"] != DBNull.Value)
                        {
                            if ((string)reader["DeliveryState"] != "")
                                asr.DeliveryState = (string)reader["DeliveryState"];
                            else
                                asr.DeliveryState = "";
                        }
                        else
                            asr.DeliveryState = "";

                        if (reader["DeliveryCity"] != DBNull.Value)
                        {
                            if ((string)reader["DeliveryCity"] != "")
                                asr.DeliveryCity = (string)reader["DeliveryCity"];
                            else
                                asr.DeliveryCity = "";
                        }
                        else
                            asr.DeliveryCity = "";

                        if (reader["DeliveryZip"] != DBNull.Value)
                        {
                            if ((string)reader["DeliveryZip"] != "")
                                asr.DeliveryZip = (string)reader["DeliveryZip"];
                            else
                                asr.DeliveryZip = "";
                        }
                        else
                            asr.DeliveryZip = "";

                        if (reader["Phone"] != DBNull.Value)
                        {
                            if ((string)reader["Phone"] != "")
                                asr.Phone = (string)reader["Phone"];
                            else
                                asr.Phone = "";
                        }
                        else
                            asr.Phone = "";

                        if (reader["Fax"] != DBNull.Value)
                        {
                            if ((string)reader["Fax"] != "")
                                asr.Fax = (string)reader["Fax"];
                            else
                                asr.Fax = "";
                        }
                        else
                            asr.Fax = "";

                        if (reader["Email"] != DBNull.Value)
                        {
                            if ((string)reader["Email"] != "")
                                asr.Email = (string)reader["Email"];
                            else
                                asr.Email = "";
                        }
                        else
                            asr.Email = "";

                        if (reader["County"] != DBNull.Value)
                        {
                            if ((string)reader["County"] != "")
                                asr.County = (string)reader["County"];
                            else
                                asr.County = "";
                        }
                        else
                            asr.County = "";

                        if (reader["PrincipalOfficer"] != DBNull.Value)
                        {
                            if ((string)reader["PrincipalOfficer"] != "")
                                asr.PrincipalOfficer = (string)reader["PrincipalOfficer"];
                            else
                                asr.PrincipalOfficer = "";
                        }
                        else
                            asr.PrincipalOfficer = "";

                        if (reader["PrincipalOfficerTitle"] != DBNull.Value)
                        {
                            if ((string)reader["PrincipalOfficerTitle"] != "")
                                asr.PrincipalOfficerTitle = (string)reader["PrincipalOfficerTitle"];
                            else
                                asr.PrincipalOfficerTitle = "";
                        }
                        else
                            asr.PrincipalOfficerTitle = "";

                        if (reader["PrincipalCell"] != DBNull.Value)
                        {
                            if ((string)reader["PrincipalCell"] != "")
                                asr.PrincipalCell = (string)reader["PrincipalCell"];
                            else
                                asr.PrincipalCell = "";
                        }
                        else
                            asr.PrincipalCell = "";

                        if (reader["PrincipalEmail"] != DBNull.Value)
                        {
                            if ((string)reader["PrincipalEmail"] != "")
                                asr.PrincipalEmail = (string)reader["PrincipalEmail"];
                            else
                                asr.PrincipalEmail = "";
                        }
                        else
                            asr.PrincipalEmail = "";

                        if (reader["OwnerName"] != DBNull.Value)
                        {
                            if ((string)reader["OwnerName"] != "")
                                asr.OwnerName = (string)reader["OwnerName"];
                            else
                                asr.OwnerName = "";
                        }
                        else
                            asr.OwnerName = "";

                        if (reader["BillingAddress"] != DBNull.Value)
                        {
                            if ((string)reader["BillingAddress"] != "")
                                asr.BillingAddress = (string)reader["BillingAddress"];
                            else
                                asr.BillingAddress = "";
                        }
                        else
                            asr.BillingAddress = "";

                        if (reader["BillingState"] != DBNull.Value)
                        {
                            if ((string)reader["BillingState"] != "")
                                asr.BillingState = (string)reader["BillingState"];
                            else
                                asr.BillingState = "";
                        }
                        else
                            asr.BillingState = "";

                        if (reader["BillingCity"] != DBNull.Value)
                        {
                            if ((string)reader["BillingCity"] != "")
                                asr.BillingCity = (string)reader["BillingCity"];
                            else
                                asr.BillingCity = "";
                        }
                        else
                            asr.BillingCity = "";

                        if (reader["BillingZip"] != DBNull.Value)
                        {
                            if ((string)reader["BillingZip"] != "")
                                asr.BillingZip = (string)reader["BillingZip"];
                            else
                                asr.BillingZip = "";
                        }
                        else
                            asr.BillingZip = "";


                        if (reader["FederalTaxID"] != DBNull.Value)
                        {
                            if ((string)reader["FederalTaxID"] != "")
                                asr.FederalTaxID = (string)reader["FederalTaxID"];
                            else
                                asr.FederalTaxID = "";
                        }
                        else
                            asr.FederalTaxID = "";

                        if (reader["ResaleCertificateNumber"] != DBNull.Value)
                        {
                            if ((string)reader["ResaleCertificateNumber"] != "")
                                asr.ResaleCertificateNumber = (string)reader["ResaleCertificateNumber"];
                            else
                                asr.ResaleCertificateNumber = "";
                        }
                        else
                            asr.ResaleCertificateNumber = "";

                        if (reader["TaxGroup"] != DBNull.Value)
                        {
                            if ((string)reader["TaxGroup"] != "")
                                asr.TaxGroup = (string)reader["TaxGroup"];
                            else
                                asr.TaxGroup = "";
                        }
                        else
                            asr.TaxGroup = "";

                        if (reader["CompanyType"] != DBNull.Value)
                        {
                            if ((string)reader["CompanyType"] != "")
                                asr.CompanyType = (string)reader["CompanyType"];
                            else
                                asr.CompanyType = "";
                        }
                        else
                            asr.CompanyType = "";

                        if (reader["NatureOfBusiness"] != DBNull.Value)
                        {
                            if ((string)reader["NatureOfBusiness"] != "")
                                asr.NatureOfBusiness = (string)reader["NatureOfBusiness"];
                            else
                                asr.NatureOfBusiness = "";
                        }
                        else
                            asr.NatureOfBusiness = "";

                        if (reader["EstablishedYear"] != DBNull.Value)
                        {
                            if ((string)reader["EstablishedYear"] != "")
                                asr.EstablishedYear = (string)reader["EstablishedYear"];
                            else
                                asr.EstablishedYear = "";
                        }
                        else
                            asr.EstablishedYear = "";

                        if (reader["StateIncorporated"] != DBNull.Value)
                        {
                            if ((string)reader["StateIncorporated"] != "")
                                asr.StateIncorporated = (string)reader["StateIncorporated"];
                            else
                                asr.StateIncorporated = "";
                        }
                        else
                            asr.StateIncorporated = "";

                        if (reader["TaxExempt"] != DBNull.Value)
                        {
                            if (reader["TaxExempt"].ToString() != "")
                            {
                                if (reader["TaxExempt"].ToString() == "True")
                                    asr.TaxExempt = "1";
                                if (reader["TaxExempt"].ToString() == "False")
                                    asr.TaxExempt = "0";
                            }
                            else
                                asr.TaxExempt = "";
                        }
                        else
                            asr.TaxExempt = "";

                        if (reader["PORequired"] != DBNull.Value)
                        {
                            if (reader["PORequired"].ToString() != "")
                            {
                                if (reader["PORequired"].ToString() == "True")
                                    asr.PORequired = "1";
                                if (reader["PORequired"].ToString() == "False")
                                    asr.PORequired = "0";
                            }
                            else
                                asr.PORequired = "";
                        }
                        else
                            asr.PORequired = "";

                        if (reader["AlreadyHasFBAccount"] != DBNull.Value)
                        {
                            if (reader["AlreadyHasFBAccount"].ToString() != "")
                            {
                                if (reader["AlreadyHasFBAccount"].ToString() == "True")
                                    asr.AlreadyHasFBAccount = "1";
                                if (reader["AlreadyHasFBAccount"].ToString() == "False")
                                    asr.AlreadyHasFBAccount = "0";
                            }
                            else
                                asr.AlreadyHasFBAccount = "";
                        }
                        else
                            asr.AlreadyHasFBAccount = "";

                        if (reader["FBAccount"] != DBNull.Value)
                        {
                            if ((string)reader["FBAccount"] != "")
                                asr.FBAccount = (string)reader["FBAccount"];
                            else
                                asr.FBAccount = "";
                        }
                        else
                            asr.FBAccount = "";

                        if (reader["fb1Status"] != DBNull.Value)
                        {
                            if ((string)reader["fb1Status"] != "")
                                asr.fb1Status = (string)reader["fb1Status"];
                            else
                                asr.fb1Status = "";
                        }
                        else
                            asr.fb1Status = "";

                        if (reader["fb1UpdatedDate"] != DBNull.Value)
                        {
                            string s = (string)reader["fb1UpdatedDate"].ToString();
                            DateTime cd = DateTime.Parse(s);

                            //asr.fb1UpdatedDate = cd.ToString("yyyy/MM/dd"); CSD
                            asr.fb1UpdatedDate = cd.ToString("MM/dd/yyyy");
                        }
                        else
                            asr.fb1UpdatedDate = "";

                        if (reader["narfStatus"] != DBNull.Value)
                        {
                            if ((string)reader["narfStatus"] != "")
                                asr.narfStatus = (string)reader["narfStatus"];
                            else
                                asr.narfStatus = "";
                        }
                        else
                            asr.narfStatus = "";

                        if (reader["narfUpdatedDate"] != DBNull.Value)
                        {
                            string s = (string)reader["narfUpdatedDate"].ToString();
                            DateTime cd = DateTime.Parse(s);

                            //asr.narfUpdatedDate = cd.ToString("yyyy-MM-dd"); CSD
                            asr.narfUpdatedDate = cd.ToString("MM/dd/yyyy");
                        }
                        else
                            asr.narfUpdatedDate = "";

                        if (reader["customerContactName"] != DBNull.Value)
                        {
                            if ((string)reader["customerContactName"] != "")
                                asr.customerContactName = (string)reader["customerContactName"];
                            else
                                asr.customerContactName = "";
                        }
                        else
                            asr.customerContactName = "";

                        if (reader["baidtda"] != DBNull.Value)
                        {
                            if (reader["baidtda"].ToString() != "")
                            {
                                asr.baidtda = reader["baidtda"].ToString();
                            }
                            else
                                asr.baidtda = "";
                        }
                        else
                            asr.baidtda = "";

                        if (reader["alphaName"] != DBNull.Value)
                        {
                            if ((string)reader["alphaName"] != "")
                                asr.alphaName = (string)reader["alphaName"];
                            else
                                asr.alphaName = "";
                        }
                        else
                            asr.alphaName = "";

                        if (reader["eocno"] != DBNull.Value)
                        {
                            if (reader["eocno"].ToString() != "")
                            {
                                asr.eocno = reader["eocno"].ToString();
                            }
                            else
                                asr.eocno = "";
                        }
                        else
                            asr.eocno = "";

                        if (reader["legalMailingName"] != DBNull.Value)
                        {
                            if ((string)reader["legalMailingName"] != "")
                                asr.legalMailingName = (string)reader["legalMailingName"];
                            else
                                asr.legalMailingName = "";
                        }
                        else
                            asr.legalMailingName = "";

                        if (reader["eeBilltoAcc"] != DBNull.Value)
                        {
                            if ((string)reader["eeBilltoAcc"] != "")
                                asr.eeBilltoAcc = (string)reader["eeBilltoAcc"];
                            else
                                asr.eeBilltoAcc = "";
                        }
                        else
                            asr.eeBilltoAcc = "";

                        if (reader["cnBilltoAcc"] != DBNull.Value)
                        {
                            if ((string)reader["cnBilltoAcc"] != "")
                                asr.cnBilltoAcc = (string)reader["cnBilltoAcc"];
                            else
                                asr.cnBilltoAcc = "";
                        }
                        else
                            asr.cnBilltoAcc = "";

                        if (reader["dailyDelvSeq"] != DBNull.Value)
                        {
                            if ((string)reader["dailyDelvSeq"] != "")
                                asr.dailyDelvSeq = (string)reader["dailyDelvSeq"];
                            else
                                asr.dailyDelvSeq = "";
                        }
                        else
                            asr.dailyDelvSeq = "";

                        if (reader["freqDailyDelv"] != DBNull.Value)
                        {
                            if ((string)reader["freqDailyDelv"] != "")
                                asr.freqDailyDelv = (string)reader["freqDailyDelv"];
                            else
                                asr.freqDailyDelv = "";
                        }
                        else
                            asr.freqDailyDelv = "";

                        lasr.Add(asr);
                    }
                    ac.accounts = lasr;
                    cmd.Dispose();
                    reader.Close();


                    if (cnn.State == System.Data.ConnectionState.Open)
                        cnn.Close();
                    gplr.result = "Success";
                    gplr.data = ac;
                    //gplr.accounts = lasr;
                    gplr.error = "";
                }
            }
            catch (SqlException ex)
            {
                cnn.Close();
                gplr.result = "Failed";
                gplr.data = null;
                //gplr.accounts = null;
                gplr.error = ex.ToString();
            }

            String json = JsonConvert.SerializeObject(gplr, Formatting.Indented);
            //MessageBox.Show(json);
            return json;
        }

        /* Record Insertion into Table */
        [HttpPost]
        [Route("addNARF")]
        public string addNarfAccount(NarfNewAccountReq narfNewAccount)
        {
            sendInsertResponse sir = new sendInsertResponse();
            genericResponse gr = new genericResponse();
            Result r = insertIntoHeaderTable(narfNewAccount);
            int recordID = r.code;
            if (recordID != 0)
            {
                gr.customerID = recordID.ToString();
                Result riinfac = insertIntoNarfAccount(recordID, narfNewAccount);
                if (riinfac.code != 0)
                {
                    Result riict = insertIntoChildTable(recordID, narfNewAccount);
                    if (riict.code != -1)
                    {
                        sir.result = "Success";
                        sir.data = gr;
                        sir.error = "";
                    }
                    else
                    {
                        sir.result = "Success";
                        sir.data = gr;
                        sir.error = riict.errorDesc;
                    }
                }
                else
                {
                    sir.result = "Success";
                    sir.data = gr;
                    sir.error = riinfac.errorDesc;
                }
            }
            else
            {
                gr.customerID = "NA";
                sir.result = "Failed";
                sir.data = gr;
                sir.error = r.errorDesc;
            }

            String json = JsonConvert.SerializeObject(sir, Formatting.Indented);
            return json;

        }
        public Result insertIntoHeaderTable(NarfNewAccountReq narfNewAccount)
        {
            string connString = this.Configuration.GetConnectionString("DefaultConnection");
            string saveStaff="";
            int modified = 0;

            SqlConnection cnn = new SqlConnection(connString);
            try
            {
                if (narfNewAccount.CompanyInfo.CustomerID == "")
                {
                    saveStaff = "INSERT into NARF_Header (CustomerName,Phone,Email,DBAName,BillingAddress," +
                        "BillingState,BillingCity,BillingZip,DeliveryAddress1,DeliveryAddress2,DeliveryAddress3,DeliveryAddress4,DeliveryState," +
                        "DeliveryCity,DeliveryZip,County,customerContactName,TaxGroup," +
                        "FederalTaxID,ResaleCertificateNumber," +
                        "CustomerJDE,narfStatus,narfUpdatedDate,Fax,OwnerName" +
                        ",baidtda,alphaName,legalMailingName,eocno,eeBilltoAcc,cnBilltoAcc,dailyDelvSeq,freqDailyDelv) " +
                        "output INSERTED.CustomerID VALUES" +
                        "(@Name,@Phone,@Email,@DBAName,@BillingAddress,@BillingState,@BillingCity,@BillingZip,@DeliveryAddress1,@DeliveryAddress2," +
                        "@DeliveryAddress3,@DeliveryAddress4,@DeliveryState,@DeliveryCity,@DeliveryZip,@County,@customerContactName,@TaxGroup," +
                        "@FederalTaxID,@ResaleCertificateNumber," +
                        "@CustomerJDE,@narfStatus,@narfUpdatedDate,@Fax,@OwnerName" +
                        ",@baidtda,@alphaName,@legalMailingName,@eocno,@eeBilltoAcc,@cnBilltoAcc,@dailyDelvSeq,@freqDailyDelv);";
                }
                else
                {
                    saveStaff = "UPDATE NARF_Header SET " +
                        "CustomerName=@Name,Phone=@Phone,Email=@Email,DBAName=@DBAName,BillingAddress=@BillingAddress," +
                        "BillingState=@BillingState,BillingCity=@BillingCity,BillingZip=@BillingZip,DeliveryAddress1=@DeliveryAddress1," +
                        "DeliveryAddress2=@DeliveryAddress2,DeliveryAddress3=@DeliveryAddress3,DeliveryAddress4=@DeliveryAddress4," +
                        "DeliveryState=@DeliveryState,DeliveryCity=@DeliveryCity,DeliveryZip=@DeliveryZip,County=@County," +
                        "customerContactName=@customerContactName," +
                        "TaxGroup=@TaxGroup,FederalTaxID=@FederalTaxID,ResaleCertificateNumber=@ResaleCertificateNumber," +
                        "CustomerJDE=@CustomerJDE,narfStatus=@narfStatus,narfUpdatedDate=@narfUpdatedDate,Fax=@Fax,OwnerName=@OwnerName," +
                        "baidtda=@baidtda,alphaName=@alphaName,legalMailingName=@legalMailingName,eocno=@eocno,eeBilltoAcc=@eeBilltoAcc," +
                        "cnBilltoAcc=@cnBilltoAcc,dailyDelvSeq=@dailyDelvSeq,freqDailyDelv=@freqDailyDelv" +
                        " WHERE CustomerID = " + narfNewAccount.CompanyInfo.CustomerID + ";";
                }

                using (SqlCommand cmd = new SqlCommand(saveStaff, cnn))
                {
                    cmd.Parameters.AddWithValue("@Name", narfNewAccount.CompanyInfo.LegalName);
                    cmd.Parameters.AddWithValue("@Phone", narfNewAccount.CompanyInfo.LocationPhoneNo);
                    cmd.Parameters.AddWithValue("@Email", narfNewAccount.CompanyInfo.CustomerEmail);
                    cmd.Parameters.AddWithValue("@DBAName", narfNewAccount.CompanyInfo.AlphaDBAName);
                    cmd.Parameters.AddWithValue("@BillingAddress", narfNewAccount.CompanyInfo.BillingAddress);
                    cmd.Parameters.AddWithValue("@BillingState", narfNewAccount.CompanyInfo.BillingState);
                    cmd.Parameters.AddWithValue("@BillingCity", narfNewAccount.CompanyInfo.BillingCity);
                    cmd.Parameters.AddWithValue("@BillingZip", narfNewAccount.CompanyInfo.BillingZip);
                    cmd.Parameters.AddWithValue("@DeliveryAddress1", narfNewAccount.CompanyInfo.DeliveryAddressLine1);
                    cmd.Parameters.AddWithValue("@DeliveryAddress2", narfNewAccount.CompanyInfo.DeliveryAddressLine2);
                    cmd.Parameters.AddWithValue("@DeliveryAddress3", narfNewAccount.CompanyInfo.DeliveryAddressLine3);
                    cmd.Parameters.AddWithValue("@DeliveryAddress4", narfNewAccount.CompanyInfo.DeliveryAddressLine4);
                    cmd.Parameters.AddWithValue("@DeliveryState", narfNewAccount.CompanyInfo.State);
                    cmd.Parameters.AddWithValue("@DeliveryCity", narfNewAccount.CompanyInfo.City);
                    cmd.Parameters.AddWithValue("@DeliveryZip", narfNewAccount.CompanyInfo.Zip);
                    cmd.Parameters.AddWithValue("@County", narfNewAccount.CompanyInfo.County);
                    cmd.Parameters.AddWithValue("@Fax", narfNewAccount.CompanyInfo.LocationFaxNo);
                    cmd.Parameters.AddWithValue("@customerContactName", narfNewAccount.CompanyInfo.CustomerContactName);
                    cmd.Parameters.AddWithValue("@OwnerName", narfNewAccount.CompanyInfo.OwnerName);
                    cmd.Parameters.AddWithValue("@FederalTaxID", narfNewAccount.CompanyInfo.FedTaxIDNo);
                    cmd.Parameters.AddWithValue("@ResaleCertificateNumber", narfNewAccount.CompanyInfo.ResaleCertNo);
                    cmd.Parameters.AddWithValue("@TaxGroup", narfNewAccount.CompanyInfo.TaxGroup);
                    cmd.Parameters.AddWithValue("@baidtda", narfNewAccount.CompanyInfo.BillingAddressIfDifferentThanDeliveryAddress);
                    cmd.Parameters.AddWithValue("@alphaName", narfNewAccount.CompanyInfo.AlphaName);
                    cmd.Parameters.AddWithValue("@legalMailingName", narfNewAccount.CompanyInfo.LegalMailingName);
                    cmd.Parameters.AddWithValue("@eocno", narfNewAccount.CompanyInfo.ExistingOrCreateNewOption);
                    cmd.Parameters.AddWithValue("@eeBilltoAcc", narfNewAccount.CompanyInfo.EnterExistingBillToAcctNo);
                    cmd.Parameters.AddWithValue("@cnBilltoAcc", narfNewAccount.CompanyInfo.CreateNewBillToAcctNo);
                    cmd.Parameters.AddWithValue("@dailyDelvSeq", narfNewAccount.CompanyInfo.DailyDeliverySequence);
                    cmd.Parameters.AddWithValue("@freqDailyDelv", narfNewAccount.CompanyInfo.FrequencyDailyDelivery);
                    cmd.Parameters.AddWithValue("@CustomerJDE", "");
                    cmd.Parameters.AddWithValue("@narfStatus", narfNewAccount.CompanyInfo.narfStatus);
                    cmd.Parameters.AddWithValue("@narfUpdatedDate", DateTime.Now.ToString("yyyy-MM-dd"));

                    cnn.Open();
                    //MessageBox.Show(cmd.CommandText);
                    if (narfNewAccount.CompanyInfo.CustomerID == "")
                        modified = (int)cmd.ExecuteScalar();
                    else
                    {
                        modified = Int32.Parse(narfNewAccount.CompanyInfo.CustomerID);
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
        public Result insertIntoNarfAccount(int customerID, NarfNewAccountReq narfNewAccount)
        {
            string connString = this.Configuration.GetConnectionString("DefaultConnection");

            SqlConnection cnn = new SqlConnection(connString);
            try
            {
                string saveStaff = "INSERT into NARF_Account (CustomerID,purpose," +
                    "operatingUnit,route,originatorName,CreatedUserID,CreatedUserName," +
                    "CreatedDate,ModifiedUserID,ModifiedUserName,ModifiedDate,originatorPhone,searchType,branch,district, " +
                    "region,businessUnit,customerCell,managedBy,customerSegment,acctAcquiredBySA,equipServiceLevel," +
                    "customerGroup,posProgram,alliedEquipProgram,priceProtection,alliedDiscount,weeklyCoffeeVolume," +
                    "equipmentProgram,estBiWeeklySales,termsOfSale,creditLimit,adjustmentSchedule,freightHandlingCode,parentNumber" +
                    ",parentChainNumber,acctAcquiredByRSR,employeeTitle,employeeName,Notes) " +
                    "VALUES" +
                    "(@CustomerID,@purpose,@operatingUnit,@route,@originatorName,@CreatedUserID,@CreatedUserName," +
                    "@CreatedDate,@ModifiedUserID,@ModifiedUserName,@ModifiedDate,@originatorPhone,@searchType,@branch,@district, " +
                    "@region,@businessUnit,@customerCell,@managedBy,@customerSegment,@acctAcquiredBySA,@equipServiceLevel," +
                    "@customerGroup,@posProgram,@alliedEquipProgram,@priceProtection,@alliedDiscount,@weeklyCoffeeVolume,@equipmentProgram," +
                    "@estBiWeeklySales,@termsOfSale,@creditLimit,@adjustmentSchedule,@freightHandlingCode,@parentNumber," +
                    "@parentChainNumber,@acctAcquiredByRSR,@employeeTitle,@employeeName,@Notes);";


                using (SqlCommand cmd = new SqlCommand(saveStaff, cnn))
                {
                    /*string sourceDate = narfNewAccount.userInfo.CreatedDate.ToString();
                    DateTime Date = DateTime.Parse(sourceDate);
                    string createdDate = Date.ToString("yyyy-MM-dd HH:mm:ss");

                    sourceDate = narfNewAccount.userInfo.ModifiedDate.ToString();
                    Date = DateTime.Parse(sourceDate);
                    string modifiedDate = Date.ToString("yyyy-MM-dd HH:mm:ss");  */

                    cmd.Parameters.AddWithValue("@CustomerID", customerID);
                    cmd.Parameters.AddWithValue("@purpose", narfNewAccount.CompanyInfo.Purpose);
                    cmd.Parameters.AddWithValue("@operatingUnit", narfNewAccount.CompanyInfo.OperatingUnit);
                    cmd.Parameters.AddWithValue("@route", narfNewAccount.CompanyInfo.Route);
                    cmd.Parameters.AddWithValue("@originatorName", narfNewAccount.CompanyInfo.OriginatorName);
                    cmd.Parameters.AddWithValue("@originatorPhone", narfNewAccount.CompanyInfo.OriginatorPhone);
                    cmd.Parameters.AddWithValue("@searchType", narfNewAccount.CompanyInfo.SearchType);
                    cmd.Parameters.AddWithValue("@branch", narfNewAccount.CompanyInfo.BranchNo);
                    cmd.Parameters.AddWithValue("@district", narfNewAccount.CompanyInfo.DistrictNo);
                    cmd.Parameters.AddWithValue("@region", narfNewAccount.CompanyInfo.RegionNo);
                    cmd.Parameters.AddWithValue("@businessUnit", narfNewAccount.CompanyInfo.BusinessUnit);
                    cmd.Parameters.AddWithValue("@customerCell", narfNewAccount.CompanyInfo.CustomerCellNo);
                    cmd.Parameters.AddWithValue("@managedBy", narfNewAccount.CompanyInfo.ManagedBy);
                    cmd.Parameters.AddWithValue("@customerSegment", narfNewAccount.CompanyInfo.CustomerSegment);
                    cmd.Parameters.AddWithValue("@acctAcquiredBySA", narfNewAccount.CompanyInfo.NewAcctAcquiredBySABDM);
                    cmd.Parameters.AddWithValue("@equipServiceLevel", narfNewAccount.CompanyInfo.EquipServiceLevel);
                    cmd.Parameters.AddWithValue("@customerGroup", narfNewAccount.CompanyInfo.CustomerGroup);
                    cmd.Parameters.AddWithValue("@posProgram", narfNewAccount.CompanyInfo.POSPrgram);
                    cmd.Parameters.AddWithValue("@alliedEquipProgram", narfNewAccount.CompanyInfo.AlliedEquipProgram);
                    cmd.Parameters.AddWithValue("@priceProtection", narfNewAccount.CompanyInfo.PriceProtection);
                    cmd.Parameters.AddWithValue("@alliedDiscount", narfNewAccount.CompanyInfo.AlliedDiscount);
                    cmd.Parameters.AddWithValue("@weeklyCoffeeVolume", narfNewAccount.CompanyInfo.WeeklyCoffeeVolume);
                    cmd.Parameters.AddWithValue("@equipmentProgram", narfNewAccount.CompanyInfo.EquipmentProgram);
                    //cmd.Parameters.AddWithValue("@CC30", narfNewAccount.CompanyInfo.CC30);
                    cmd.Parameters.AddWithValue("@estBiWeeklySales", narfNewAccount.CompanyInfo.EstBiWeeklySales);
                    cmd.Parameters.AddWithValue("@termsOfSale", narfNewAccount.CompanyInfo.TermsOfSale);
                    cmd.Parameters.AddWithValue("@creditLimit", narfNewAccount.CompanyInfo.CreditLimit);
                    cmd.Parameters.AddWithValue("@adjustmentSchedule", narfNewAccount.CompanyInfo.AdjustmentSchedule);
                    cmd.Parameters.AddWithValue("@freightHandlingCode", narfNewAccount.CompanyInfo.FreightHandlingCode);
                    cmd.Parameters.AddWithValue("@parentNumber", narfNewAccount.CompanyInfo.ParentNo);
                    cmd.Parameters.AddWithValue("@parentChainNumber", narfNewAccount.CompanyInfo.ParentChainNo);
                    cmd.Parameters.AddWithValue("@acctAcquiredByRSR", narfNewAccount.CompanyInfo.NewAcctAcquiredByRSR);
                    cmd.Parameters.AddWithValue("@employeeTitle", narfNewAccount.CompanyInfo.EmployeeTitle);
                    cmd.Parameters.AddWithValue("@employeeName", narfNewAccount.CompanyInfo.EmployeeNameNo);
                    cmd.Parameters.AddWithValue("@Notes", narfNewAccount.CompanyInfo.Notes);

                    cmd.Parameters.AddWithValue("@CreatedUserID", narfNewAccount.userInfo.CreatedUserID);
                    cmd.Parameters.AddWithValue("@CreatedUserName", narfNewAccount.userInfo.CreatedUserName);
                    //cmd.Parameters.AddWithValue("@CreatedDate", createdDate);
                    cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@ModifiedUserID", narfNewAccount.userInfo.ModifiedUserID);
                    cmd.Parameters.AddWithValue("@ModifiedUserName", narfNewAccount.userInfo.ModifiedUserName);
                    cmd.Parameters.AddWithValue("@ModifiedDate", DateTime.Now.ToString("yyyy-MM-dd"));
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
        public Result insertIntoChildTable(int applicationID, NarfNewAccountReq narfNewAccount)
        {
            //string connString = "Data Source=localhost\\SQLEXPRESS;Database=FarmerBrosDB;Integrated Security=SSPI";
            string connString = this.Configuration.GetConnectionString("DefaultConnection");

            SqlConnection cnn = new SqlConnection(connString);

            List<CategoryAlliedAdjustment> tr = narfNewAccount.CompanyInfo.categoryAlliedAdjustment;
            foreach (CategoryAlliedAdjustment t in tr)
            {
                try
                {
                    string saveStaff = "INSERT into Category_Allied_Adjustment (customerID,categoryCode,categoryValue,createdUserID,createdUserName," +
                    "createdDate,modifiedUserID,modifiedUserName,modifiedDate) " +
                    "VALUES(@CustomerID,@categoryCode,@categoryValue,@CreatedUserID,@CreatedUserName," +
                    "@CreatedDate,@ModifiedUserID,@ModifiedUserName,@ModifiedDate);";

                    using (SqlCommand cmd = new SqlCommand(saveStaff, cnn))
                    {
                        cmd.Parameters.AddWithValue("@CustomerID", applicationID);
                        cmd.Parameters.AddWithValue("@categoryCode", t.categoryCode);
                        cmd.Parameters.AddWithValue("@categoryValue", t.categoryValue);
                        cmd.Parameters.AddWithValue("@CreatedUserID", narfNewAccount.userInfo.CreatedUserID);
                        cmd.Parameters.AddWithValue("@CreatedUserName", narfNewAccount.userInfo.CreatedUserName);
                        cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@ModifiedUserID", narfNewAccount.userInfo.ModifiedUserID);
                        cmd.Parameters.AddWithValue("@ModifiedUserName", narfNewAccount.userInfo.ModifiedUserName);
                        cmd.Parameters.AddWithValue("@ModifiedDate", DateTime.Now.ToString("yyyy-MM-dd"));
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

        /* File Upload Implementation */
        [HttpPost]
        [Route("uploadDocsNARF")]
        public string MyFileUpload()
        {
            sendUploadResponse sur = new sendUploadResponse();
            genericResponseUpload gr = new genericResponseUpload();
            try
            {
                var request = HttpContext.Request;
                var appID = request.Form["CustomerID"];

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
                sur.result = "Success";
                gr.uploaded = "uploaded";
                sur.data = gr;
                sur.error = "";

                //return "{'result':'Success','data':{'uploaded':'uploaded'}}";
            }
            catch (Exception e)
            {
                sur.result = "Failure";
                gr.uploaded = "Failed";
                sur.data = gr;
                sur.error = e.ToString();
                //return "{'result':'Failure','data':{'uploaded':'Failed'},'error':{'" + e.ToString() + "'}}";
            }
            String json = JsonConvert.SerializeObject(sur, Formatting.Indented);
            return json;
        }

        public void imageUploadIntoTable(string applicationID, string fieldName, string fileName)
        {
            //string connString = "Data Source=localhost\\SQLEXPRESS;Database=FarmerBrosDB;Integrated Security=SSPI";

            string connString = this.Configuration.GetConnectionString("DefaultConnection");

            SqlConnection cnn = new SqlConnection(connString);

            if (fieldName == "ResaleCertificateNoFile")
                fieldName = "resaleCertificateNoFile";
            try
            {
                //string saveStaff = "UPDATE NARF_NewAccount ("+fieldName+ ") VALUES(@"+fieldName+ ") WHERE ApplicationId = "+ applicationID+";";
                string saveStaff = "UPDATE NARF_Account SET " + fieldName + "='" + fileName + "' WHERE CustomerID = " + applicationID + ";";

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

        [HttpPost]
        [Route("getFB1")]
        public string getFB1Response(fb1TableRequest fbTableRequest)
        {
            string connString = this.Configuration.GetConnectionString("DefaultConnection");
            getFB1AccountLookupResponse gfblr = new getFB1AccountLookupResponse();
            fb1TableResponse ftr = new fb1TableResponse();            

            SqlConnection cnn = new SqlConnection(connString);

            try
            {
                string whereClause = "";                
                if (fbTableRequest.CustomerID != "")
                    whereClause += "CustomerID = " + fbTableRequest.CustomerID;                

                string getAccountDetails = "select * from NARF_FB1 WHERE " + whereClause;

                using (SqlCommand cmd = new SqlCommand(getAccountDetails, cnn))
                {
                    cnn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();                    

                    while (reader.Read())
                    {
                        ftr.CustomerID = reader["CustomerID"].ToString();

                        if (reader["AccountPayableContact"] != DBNull.Value)
                        {
                            if (reader["AccountPayableContact"].ToString() != "")
                                ftr.AccountPayableContact = (string)reader["AccountPayableContact"];
                            else
                                ftr.AccountPayableContact = "";
                        }
                        else
                            ftr.AccountPayableContact = "";

                        if (reader["AccountPayableTitle"] != DBNull.Value)
                        {
                            if ((string)reader["AccountPayableTitle"] != "")
                                ftr.AccountPayableTitle = (string)reader["AccountPayableTitle"];
                            else
                                ftr.AccountPayableTitle = "";
                        }
                        else
                            ftr.AccountPayableTitle = "";

                        if (reader["AccountPayablePhone"] != DBNull.Value)
                        {
                            if ((string)reader["AccountPayablePhone"] != "")
                                ftr.AccountPayablePhone = (string)reader["AccountPayablePhone"];
                            else
                                ftr.AccountPayablePhone = "";
                        }
                        else
                            ftr.AccountPayablePhone = "";

                        if (reader["AccountPayableEmail"] != DBNull.Value)
                        {
                            if ((string)reader["AccountPayableEmail"] != "")
                                ftr.AccountPayableEmail = (string)reader["AccountPayableEmail"];
                            else
                                ftr.AccountPayableEmail = "";
                        }
                        else
                            ftr.AccountPayableEmail = "";

                        ftr.CreatedUserID = reader["CreatedUserID"].ToString();
                        ftr.CreatedUserName = (string)reader["CreatedUserName"];                        
                        ftr.ModifiedUserID = reader["ModifiedUserID"].ToString();
                        ftr.ModifiedUserName = (string)reader["ModifiedUserName"];
                        
                        if (reader["CreatedDate"] != DBNull.Value)
                        {
                            string s = (string)reader["CreatedDate"].ToString();
                            DateTime cd = DateTime.Parse(s);

                            ftr.CreatedDate = cd.ToString("yyyy-MM-dd");
                        }
                        else
                            ftr.CreatedDate = "";

                        if (reader["ModifiedDate"] != DBNull.Value)
                        {
                            string s = (string)reader["ModifiedDate"].ToString();
                            DateTime cd = DateTime.Parse(s);

                            ftr.ModifiedDate = cd.ToString("yyyy-MM-dd");
                        }
                        else
                            ftr.ModifiedDate = "";

                        if (reader["BankName"] != DBNull.Value)
                        {
                            if ((string)reader["BankName"] != "")
                                ftr.BankName = (string)reader["BankName"];
                            else
                                ftr.BankName = "";
                        }
                        else
                            ftr.BankName = "";

                        if (reader["AccountNo"] != DBNull.Value)
                        {
                            if ((string)reader["AccountNo"] != "")
                                ftr.AccountNo = (string)reader["AccountNo"];
                            else
                                ftr.AccountNo = "";
                        }
                        else
                            ftr.AccountNo = "";

                        if (reader["BankAddress"] != DBNull.Value)
                        {
                            if ((string)reader["BankAddress"] != "")
                                ftr.BankAddress = (string)reader["BankAddress"];
                            else
                                ftr.BankAddress = "";
                        }
                        else
                            ftr.BankAddress = "";

                        if (reader["BankCity"] != DBNull.Value)
                        {
                            if ((string)reader["BankCity"] != "")
                                ftr.BankCity = (string)reader["BankCity"];
                            else
                                ftr.BankCity = "";
                        }
                        else
                            ftr.BankCity = "";


                        if (reader["BankState"] != DBNull.Value)
                        {
                            if ((string)reader["BankState"] != "")
                                ftr.BankState = (string)reader["BankState"];
                            else
                                ftr.BankState = "";
                        }
                        else
                            ftr.BankState = "";

                        if (reader["BankZip"] != DBNull.Value)
                        {
                            if ((string)reader["BankZip"] != "")
                                ftr.BankZip = (string)reader["BankZip"];
                            else
                                ftr.BankZip = "";
                        }
                        else
                            ftr.BankZip = "";

                        if (reader["BankPhone"] != DBNull.Value)
                        {
                            if ((string)reader["BankPhone"] != "")
                                ftr.BankPhone = (string)reader["BankPhone"];
                            else
                                ftr.BankPhone = "";
                        }
                        else
                            ftr.BankPhone = "";

                        if (reader["TaxExemptFile"] != DBNull.Value)
                        {
                            if ((string)reader["TaxExemptFile"] != "")
                                ftr.TaxExemptFile = (string)reader["TaxExemptFile"];
                            else
                                ftr.TaxExemptFile = "";
                        }
                        else
                            ftr.TaxExemptFile = "";

                        if (reader["ResaleCertificateNoFile"] != DBNull.Value)
                        {
                            if ((string)reader["ResaleCertificateNoFile"] != "")
                                ftr.ResaleCertificateNoFile = (string)reader["ResaleCertificateNoFile"];
                            else
                                ftr.ResaleCertificateNoFile = "";
                        }
                        else
                            ftr.ResaleCertificateNoFile = "";

                        if (reader["SecondaryAccountPayableContact"] != DBNull.Value)
                        {
                            if ((string)reader["SecondaryAccountPayableContact"] != "")
                                ftr.SecondaryAccountPayableContact = (string)reader["SecondaryAccountPayableContact"];
                            else
                                ftr.SecondaryAccountPayableContact = "";
                        }
                        else
                            ftr.SecondaryAccountPayableContact = "";

                        if (reader["SecondaryAccountPayableTitle"] != DBNull.Value)
                        {
                            if ((string)reader["SecondaryAccountPayableTitle"] != "")
                                ftr.SecondaryAccountPayableTitle = (string)reader["SecondaryAccountPayableTitle"];
                            else
                                ftr.SecondaryAccountPayableTitle = "";
                        }
                        else
                            ftr.SecondaryAccountPayableTitle = "";

                        if (reader["SecondaryAccountPayablePhone"] != DBNull.Value)
                        {
                            if ((string)reader["SecondaryAccountPayablePhone"] != "")
                                ftr.SecondaryAccountPayablePhone = (string)reader["SecondaryAccountPayablePhone"];
                            else
                                ftr.SecondaryAccountPayablePhone = "";
                        }
                        else
                            ftr.SecondaryAccountPayablePhone = "";

                        if (reader["SecondaryAccountPayableEmail"] != DBNull.Value)
                        {
                            if ((string)reader["SecondaryAccountPayableEmail"] != "")
                                ftr.SecondaryAccountPayableEmail = (string)reader["SecondaryAccountPayableEmail"];
                            else
                                ftr.SecondaryAccountPayableEmail = "";
                        }
                        else
                            ftr.SecondaryAccountPayableEmail = "";

                        if (reader["BankEmailID"] != DBNull.Value)
                        {
                            if ((string)reader["BankEmailID"] != "")
                                ftr.BankEmailID = (string)reader["BankEmailID"];
                            else
                                ftr.BankEmailID = "";
                        }
                        else
                            ftr.BankEmailID = "";

                        if (reader["BillingPhone"] != DBNull.Value)
                        {
                            if ((string)reader["BillingPhone"] != "")
                                ftr.BillingPhone = (string)reader["BillingPhone"];
                            else
                                ftr.BillingPhone = "";
                        }
                        else
                            ftr.BillingPhone = "";

                        if (reader["PGFirstName"] != DBNull.Value)
                        {
                            if ((string)reader["PGFirstName"] != "")
                                ftr.PGFirstName = (string)reader["PGFirstName"];
                            else
                                ftr.PGFirstName = "";
                        }
                        else
                            ftr.PGFirstName = "";

                        if (reader["PGMiddleName"] != DBNull.Value)
                        {
                            if ((string)reader["PGMiddleName"] != "")
                                ftr.PGMiddleName = (string)reader["PGMiddleName"];
                            else
                                ftr.PGMiddleName = "";
                        }
                        else
                            ftr.PGMiddleName = "";

                        if (reader["PGLastName"] != DBNull.Value)
                        {
                            if ((string)reader["PGLastName"] != "")
                                ftr.PGLastName = (string)reader["PGLastName"];
                            else
                                ftr.PGLastName = "";
                        }
                        else
                            ftr.PGLastName = "";

                        if (reader["PGTitle"] != DBNull.Value)
                        {
                            if ((string)reader["PGTitle"] != "")
                                ftr.PGTitle = (string)reader["PGTitle"];
                            else
                                ftr.PGTitle = "";
                        }
                        else
                            ftr.PGTitle = "";

                        if (reader["PGPresentHomeAddress"] != DBNull.Value)
                        {
                            if ((string)reader["PGPresentHomeAddress"] != "")
                                ftr.PGPresentHomeAddress = (string)reader["PGPresentHomeAddress"];
                            else
                                ftr.PGPresentHomeAddress = "";
                        }
                        else
                            ftr.PGPresentHomeAddress = "";


                        if (reader["PGCity"] != DBNull.Value)
                        {
                            if ((string)reader["PGCity"] != "")
                                ftr.PGCity = (string)reader["PGCity"];
                            else
                                ftr.PGCity = "";
                        }
                        else
                            ftr.PGCity = "";

                        if (reader["PGState"] != DBNull.Value)
                        {
                            if ((string)reader["PGState"] != "")
                                ftr.PGState = (string)reader["PGState"];
                            else
                                ftr.PGState = "";
                        }
                        else
                            ftr.PGState = "";

                        if (reader["PGZip"] != DBNull.Value)
                        {
                            if ((string)reader["PGZip"] != "")
                                ftr.PGZip = (string)reader["PGZip"];
                            else
                                ftr.PGZip = "";
                        }
                        else
                            ftr.PGZip = "";

                        if (reader["PGDOB"] != DBNull.Value)
                        {
                            string s = (string)reader["PGDOB"].ToString();
                            DateTime cd = DateTime.Parse(s);

                            ftr.PGDOB = cd.ToString("yyyy-MM-dd");
                        }
                        else
                            ftr.PGDOB = "";                        

                        if (reader["PGSSN"] != DBNull.Value)
                        {
                            if ((string)reader["PGSSN"] != "")
                                ftr.PGSSN = (string)reader["PGSSN"];
                            else
                                ftr.PGSSN = "";
                        }
                        else
                            ftr.PGSSN = "";

                        if (reader["PGDriverLicenceNoAndState"] != DBNull.Value)
                        {
                            if ((string)reader["PGDriverLicenceNoAndState"] != "")
                                ftr.PGDriverLicenceNoAndState = (string)reader["PGDriverLicenceNoAndState"];
                            else
                                ftr.PGDriverLicenceNoAndState = "";
                        }
                        else
                            ftr.PGDriverLicenceNoAndState = "";

                        if (reader["PGDate"] != DBNull.Value)
                        {
                            string s = (string)reader["PGDate"].ToString();
                            DateTime cd = DateTime.Parse(s);

                            ftr.PGDate = cd.ToString("yyyy-MM-dd");
                        }
                        else
                            ftr.PGDate = "";                        

                        if (reader["BankRefDocumentFile"] != DBNull.Value)
                        {
                            if ((string)reader["BankRefDocumentFile"] != "")
                                ftr.BankRefDocumentFile = (string)reader["BankRefDocumentFile"];
                            else
                                ftr.BankRefDocumentFile = "";
                        }
                        else
                            ftr.BankRefDocumentFile = "";

                        if (reader["PersonalGuaranteeAuthSignFile"] != DBNull.Value)
                        {
                            if ((string)reader["PersonalGuaranteeAuthSignFile"] != "")
                                ftr.PersonalGuaranteeAuthSignFile = (string)reader["PersonalGuaranteeAuthSignFile"];
                            else
                                ftr.PersonalGuaranteeAuthSignFile = "";
                        }
                        else
                            ftr.PersonalGuaranteeAuthSignFile = "";

                        if (reader["ConsentAuthSignFile"] != DBNull.Value)
                        {
                            if ((string)reader["ConsentAuthSignFile"] != "")
                                ftr.ConsentAuthSignFile = (string)reader["ConsentAuthSignFile"];
                            else
                                ftr.ConsentAuthSignFile = "";
                        }
                        else
                            ftr.ConsentAuthSignFile = "";

                        if (reader["CASTermsAndConditions"] != DBNull.Value)
                        {
                            if (reader["CASTermsAndConditions"].ToString() != "")
                            {
                                if (reader["CASTermsAndConditions"].ToString() == "True")
                                    ftr.CASTermsAndConditions = "1";
                                if (reader["CASTermsAndConditions"].ToString() == "False")
                                    ftr.CASTermsAndConditions = "0";
                            }
                            else
                                ftr.CASTermsAndConditions = "";
                        }
                        else
                            ftr.CASTermsAndConditions = "";                        

                        if (reader["CASPrintName"] != DBNull.Value)
                        {
                            if ((string)reader["CASPrintName"] != "")
                                ftr.CASPrintName = (string)reader["CASPrintName"];
                            else
                                ftr.CASPrintName = "";
                        }
                        else
                            ftr.CASPrintName = "";

                        if (reader["CASTitle"] != DBNull.Value)
                        {
                            if ((string)reader["CASTitle"] != "")
                                ftr.CASTitle = (string)reader["CASTitle"];
                            else
                                ftr.CASTitle = "";
                        }
                        else
                            ftr.CASTitle = "";

                        if (reader["CASDate"] != DBNull.Value)
                        {
                            string s = (string)reader["CASDate"].ToString();
                            DateTime cd = DateTime.Parse(s);

                            ftr.CASDate = cd.ToString("yyyy-MM-dd");
                        }
                        else
                            ftr.CASDate = "";                        
                    }                    
                    cmd.Dispose();
                    reader.Close();

                    if (cnn.State == System.Data.ConnectionState.Open)
                        cnn.Close();
                    
                }
                string getCAA = "SELECT * FROM  NARF_TradeReference WHERE " +
                                    "CustomerID = " + fbTableRequest.CustomerID + ";";

                using (SqlCommand cmd = new SqlCommand(getCAA, cnn))
                {
                    cnn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<TradeReference> trlist = new List<TradeReference>();

                    while (reader.Read())
                    {
                        TradeReference tr = new TradeReference();
                        tr.CompanyName = (string)reader["CompanyName"];
                        tr.AccountID = (string)reader["AccountID"];
                        tr.Phone = (string)reader["Phone"];
                        tr.EmailID = (string)reader["EmailID"];
                        trlist.Add(tr);
                    }
                    cmd.Dispose();
                    reader.Close();

                    if (cnn.State == System.Data.ConnectionState.Open)
                        cnn.Close();
                    ftr.tradeReferences = trlist;
                    gfblr.data = ftr;
                    if (gfblr.data != null)
                    {
                        gfblr.result = "Success";
                        gfblr.error = "";
                    }
                    else
                    {
                        gfblr.result = "Failed";
                        gfblr.error = "No Records Found";
                    }
                }
            }
            catch (SqlException ex)
            {
                cnn.Close();
                gfblr.result = "Failed";
                gfblr.data = null;                
                gfblr.error = ex.ToString();
            }

            String json = JsonConvert.SerializeObject(gfblr, Formatting.Indented);            
            return json;
        }

        [HttpPost]
        [Route("getNARF")]
        public string getNARFResponse(NarfTableRequest narfTableRequest)
        {
            string connString = this.Configuration.GetConnectionString("DefaultConnection");
            getNarfAccountLookupResponse gnalr = new getNarfAccountLookupResponse();
            NarfTableResponse ntr = new NarfTableResponse();

            SqlConnection cnn = new SqlConnection(connString);

            try
            {
                string whereClause = "";
                if (narfTableRequest.CustomerID != "")
                    whereClause += "CustomerID = " + narfTableRequest.CustomerID;

                string getAccountDetails = "select * from NARF_Account WHERE " + whereClause;

                using (SqlCommand cmd = new SqlCommand(getAccountDetails, cnn))
                {
                    cnn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        ntr.CustomerID = reader["CustomerID"].ToString();

                        if (reader["purpose"] != DBNull.Value)
                        {
                            if (reader["purpose"].ToString() != "")
                                ntr.purpose = (string)reader["purpose"];
                            else
                                ntr.purpose = "";
                        }
                        else
                            ntr.purpose = "";

                        if (reader["operatingUnit"] != DBNull.Value)
                        {
                            if ((string)reader["operatingUnit"] != "")
                                ntr.operatingUnit = (string)reader["operatingUnit"];
                            else
                                ntr.operatingUnit = "";
                        }
                        else
                            ntr.operatingUnit = "";

                        if (reader["route"] != DBNull.Value)
                        {
                            if ((string)reader["route"] != "")
                                ntr.route = (string)reader["route"];
                            else
                                ntr.route = "";
                        }
                        else
                            ntr.route = "";

                        if (reader["originatorName"] != DBNull.Value)
                        {
                            if ((string)reader["originatorName"] != "")
                                ntr.originatorName = (string)reader["originatorName"];
                            else
                                ntr.originatorName = "";
                        }
                        else
                            ntr.originatorName = "";

                        ntr.CreatedUserID = reader["CreatedUserID"].ToString();
                        ntr.CreatedUserName = (string)reader["CreatedUserName"];
                        ntr.ModifiedUserID = reader["ModifiedUserID"].ToString();
                        ntr.ModifiedUserName = (string)reader["ModifiedUserName"];

                        if (reader["CreatedDate"] != DBNull.Value)
                        {
                            string s = (string)reader["CreatedDate"].ToString();
                            DateTime cd = DateTime.Parse(s);

                            ntr.CreatedDate = cd.ToString("yyyy-MM-dd");
                        }
                        else
                            ntr.CreatedDate = "";

                        if (reader["ModifiedDate"] != DBNull.Value)
                        {
                            string s = (string)reader["ModifiedDate"].ToString();
                            DateTime cd = DateTime.Parse(s);

                            ntr.ModifiedDate = cd.ToString("yyyy-MM-dd");
                        }
                        else
                            ntr.ModifiedDate = "";

                        if (reader["originatorPhone"] != DBNull.Value)
                        {
                            if ((string)reader["originatorPhone"] != "")
                                ntr.originatorPhone = (string)reader["originatorPhone"];
                            else
                                ntr.originatorPhone = "";
                        }
                        else
                            ntr.originatorPhone = "";

                        if (reader["searchType"] != DBNull.Value)
                        {
                            if ((string)reader["searchType"] != "")
                                ntr.searchType = (string)reader["searchType"];
                            else
                                ntr.searchType = "";
                        }
                        else
                            ntr.searchType = "";

                        if (reader["branch"] != DBNull.Value)
                        {
                            if ((string)reader["branch"] != "")
                                ntr.branch = (string)reader["branch"];
                            else
                                ntr.branch = "";
                        }
                        else
                            ntr.branch = "";

                        if (reader["district"] != DBNull.Value)
                        {
                            if ((string)reader["district"] != "")
                                ntr.district = (string)reader["district"];
                            else
                                ntr.district = "";
                        }
                        else
                            ntr.district = "";


                        if (reader["region"] != DBNull.Value)
                        {
                            if ((string)reader["region"] != "")
                                ntr.region = (string)reader["region"];
                            else
                                ntr.region = "";
                        }
                        else
                            ntr.region = "";

                        if (reader["customerCell"] != DBNull.Value)
                        {
                            if ((string)reader["customerCell"] != "")
                                ntr.customerCell = (string)reader["customerCell"];
                            else
                                ntr.customerCell = "";
                        }
                        else
                            ntr.customerCell = "";

                        if (reader["managedBy"] != DBNull.Value)
                        {
                            if ((string)reader["managedBy"] != "")
                                ntr.managedBy = (string)reader["managedBy"];
                            else
                                ntr.managedBy = "";
                        }
                        else
                            ntr.managedBy = "";

                        if (reader["customerSegment"] != DBNull.Value)
                        {
                            if ((string)reader["customerSegment"] != "")
                                ntr.customerSegment = (string)reader["customerSegment"];
                            else
                                ntr.customerSegment = "";
                        }
                        else
                            ntr.customerSegment = "";

                        if (reader["equipServiceLevel"] != DBNull.Value)
                        {
                            if ((string)reader["equipServiceLevel"] != "")
                                ntr.equipServiceLevel = (string)reader["equipServiceLevel"];
                            else
                                ntr.equipServiceLevel = "";
                        }
                        else
                            ntr.equipServiceLevel = "";

                        if (reader["customerGroup"] != DBNull.Value)
                        {
                            if ((string)reader["customerGroup"] != "")
                                ntr.customerGroup = (string)reader["customerGroup"];
                            else
                                ntr.customerGroup = "";
                        }
                        else
                            ntr.customerGroup = "";

                        if (reader["posProgram"] != DBNull.Value)
                        {
                            if ((string)reader["posProgram"] != "")
                                ntr.posProgram = (string)reader["posProgram"];
                            else
                                ntr.posProgram = "";
                        }
                        else
                            ntr.posProgram = "";

                        if (reader["alliedEquipProgram"] != DBNull.Value)
                        {
                            if ((string)reader["alliedEquipProgram"] != "")
                                ntr.alliedEquipProgram = (string)reader["alliedEquipProgram"];
                            else
                                ntr.alliedEquipProgram = "";
                        }
                        else
                            ntr.alliedEquipProgram = "";

                        if (reader["priceProtection"] != DBNull.Value)
                        {
                            if ((string)reader["priceProtection"] != "")
                                ntr.priceProtection = (string)reader["priceProtection"];
                            else
                                ntr.priceProtection = "";
                        }
                        else
                            ntr.priceProtection = "";

                        if (reader["alliedDiscount"] != DBNull.Value)
                        {
                            if ((string)reader["alliedDiscount"] != "")
                                ntr.alliedDiscount = (string)reader["alliedDiscount"];
                            else
                                ntr.alliedDiscount = "";
                        }
                        else
                            ntr.alliedDiscount = "";

                        if (reader["weeklyCoffeeVolume"] != DBNull.Value)
                        {
                            if ((string)reader["weeklyCoffeeVolume"] != "")
                                ntr.weeklyCoffeeVolume = (string)reader["weeklyCoffeeVolume"];
                            else
                                ntr.weeklyCoffeeVolume = "";
                        }
                        else
                            ntr.weeklyCoffeeVolume = "";

                        if (reader["equipmentProgram"] != DBNull.Value)
                        {
                            if ((string)reader["equipmentProgram"] != "")
                                ntr.equipmentProgram = (string)reader["equipmentProgram"];
                            else
                                ntr.equipmentProgram = "";
                        }
                        else
                            ntr.equipmentProgram = "";

                        if (reader["estBiWeeklySales"] != DBNull.Value)
                        {
                            if ((string)reader["estBiWeeklySales"] != "")
                                ntr.estBiWeeklySales = (string)reader["estBiWeeklySales"];
                            else
                                ntr.estBiWeeklySales = "";
                        }
                        else
                            ntr.estBiWeeklySales = "";

                        if (reader["termsOfSale"] != DBNull.Value)
                        {
                            if ((string)reader["termsOfSale"] != "")
                                ntr.termsOfSale = (string)reader["termsOfSale"];
                            else
                                ntr.termsOfSale = "";
                        }
                        else
                            ntr.termsOfSale = "";

                        if (reader["creditLimit"] != DBNull.Value)
                        {
                            if ((string)reader["creditLimit"] != "")
                                ntr.creditLimit = (string)reader["creditLimit"];
                            else
                                ntr.creditLimit = "";
                        }
                        else
                            ntr.creditLimit = "";

                        if (reader["adjustmentSchedule"] != DBNull.Value)
                        {
                            if ((string)reader["adjustmentSchedule"] != "")
                                ntr.adjustmentSchedule = (string)reader["adjustmentSchedule"];
                            else
                                ntr.adjustmentSchedule = "";
                        }
                        else
                            ntr.adjustmentSchedule = "";


                        if (reader["freightHandlingCode"] != DBNull.Value)
                        {
                            if ((string)reader["freightHandlingCode"] != "")
                                ntr.freightHandlingCode = (string)reader["freightHandlingCode"];
                            else
                                ntr.freightHandlingCode = "";
                        }
                        else
                            ntr.freightHandlingCode = "";

                        if (reader["parentNumber"] != DBNull.Value)
                        {
                            if ((string)reader["parentNumber"] != "")
                                ntr.parentNumber = (string)reader["parentNumber"];
                            else
                                ntr.parentNumber = "";
                        }
                        else
                            ntr.parentNumber = "";

                        if (reader["parentChainNumber"] != DBNull.Value)
                        {
                            if ((string)reader["parentChainNumber"] != "")
                                ntr.parentChainNumber = (string)reader["parentChainNumber"];
                            else
                                ntr.parentChainNumber = "";
                        }
                        else
                            ntr.parentChainNumber = "";                        

                        if (reader["employeeTitle"] != DBNull.Value)
                        {
                            if ((string)reader["employeeTitle"] != "")
                                ntr.employeeTitle = (string)reader["employeeTitle"];
                            else
                                ntr.employeeTitle = "";
                        }
                        else
                            ntr.employeeTitle = "";

                        if (reader["employeeName"] != DBNull.Value)
                        {
                            if ((string)reader["employeeName"] != "")
                                ntr.employeeName = (string)reader["employeeName"];
                            else
                                ntr.employeeName = "";
                        }
                        else
                            ntr.employeeName = "";                        

                        if (reader["Notes"] != DBNull.Value)
                        {
                            if ((string)reader["Notes"] != "")
                                ntr.Notes = (string)reader["Notes"];
                            else
                                ntr.Notes = "";
                        }
                        else
                            ntr.Notes = "";

                        if (reader["businessUnit"] != DBNull.Value)
                        {
                            if ((string)reader["businessUnit"] != "")
                                ntr.businessUnit = (string)reader["businessUnit"];
                            else
                                ntr.businessUnit = "";
                        }
                        else
                            ntr.businessUnit = "";

                        if (reader["acctAcquiredBySA"] != DBNull.Value)
                        {
                            if ((string)reader["acctAcquiredBySA"] != "")
                                ntr.acctAcquiredBySA = (string)reader["acctAcquiredBySA"];
                            else
                                ntr.acctAcquiredBySA = "";
                        }
                        else
                            ntr.acctAcquiredBySA = "";

                        if (reader["acctAcquiredByRSR"] != DBNull.Value)
                        {
                            if ((string)reader["acctAcquiredByRSR"] != "")
                                ntr.acctAcquiredByRSR = (string)reader["acctAcquiredByRSR"];
                            else
                                ntr.acctAcquiredByRSR = "";
                        }
                        else
                            ntr.acctAcquiredByRSR = "";

                        if (reader["resaleCertificateNoFile"] != DBNull.Value)
                        {
                            if ((string)reader["resaleCertificateNoFile"] != "")
                                ntr.resaleCertificateNoFile = (string)reader["resaleCertificateNoFile"];
                            else
                                ntr.resaleCertificateNoFile = "";
                        }
                        else
                            ntr.resaleCertificateNoFile = "";                       

                        //gnalr.data = ntr;
                    }
                    cmd.Dispose();
                    reader.Close();

                    if (cnn.State == System.Data.ConnectionState.Open)
                        cnn.Close();
                    /*if (gnalr.data != null)
                    {
                        gnalr.result = "Success";
                        gnalr.error = "";
                    }
                    else
                    {
                        gnalr.result = "Failed";
                        gnalr.error = "No Records Found";
                    }*/
                }
                string getCAA = "SELECT * FROM  Category_Allied_Adjustment WHERE " +
                                    " customerID = " + narfTableRequest.CustomerID + ";";

                using (SqlCommand cmd = new SqlCommand(getCAA, cnn))
                {
                    cnn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<CategoryAlliedAdjustment> caalist= new List<CategoryAlliedAdjustment>();

                    while (reader.Read())
                    {
                        CategoryAlliedAdjustment caa = new CategoryAlliedAdjustment();
                        caa.categoryCode = (string)reader["categoryCode"];
                        caa.categoryValue = (string)reader["categoryValue"];
                        caalist.Add(caa);
                    }
                    cmd.Dispose();
                    reader.Close();

                    if (cnn.State == System.Data.ConnectionState.Open)
                        cnn.Close();
                    ntr.categoryAlliedAdjustment = caalist;
                    gnalr.data = ntr;
                    if (gnalr.data != null)
                    {
                        gnalr.result = "Success";
                        gnalr.error = "";
                    }
                    else
                    {
                        gnalr.result = "Failed";
                        gnalr.error = "No Records Found";
                    }
                }

            }
            catch (SqlException ex)
            {
                cnn.Close();
                gnalr.result = "Failed";
                gnalr.data = null;
                gnalr.error = ex.ToString();
            }

            String json = JsonConvert.SerializeObject(gnalr, Formatting.Indented);
            return json;
        }
    }
}
