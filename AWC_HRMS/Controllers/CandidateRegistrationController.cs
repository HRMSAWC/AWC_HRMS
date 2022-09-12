using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AWC_HRMS.Models;
using Microsoft.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.IO;


namespace AWC_HRMS_Web.Controllers
{
    public class CandidateRegistrationController : Controller
    {
        // string connection = "data source= DESKTOP-B6EBP3L; initial catalog= AWC_HRMS; integrated security=True";

       
       
        public IActionResult CandidateRegisterationDetail()
        {
           


            return View();
        }
        
        [HttpPost]
        public IActionResult CandidateRegisterationDetail(CandidateMaster candidate, IFormFile file)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfiguration configuration = builder.Build();
            string constring = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
            string FN = Path.GetFileName(file.FileName);
            using (SqlConnection con = new SqlConnection(constring))
            {
                
                if (ViewData["btn"] == "Save")
               // if (Convert.ToInt32(candidate.CandidateId) > 0)
                {


                    con.Open();
                    SqlCommand conCommand = new SqlCommand("Usp_SaveCandidateDetails", con);
                    conCommand.CommandType = CommandType.StoredProcedure;
                    conCommand.Parameters.AddWithValue("@ActionType","Insert");
                    conCommand.Parameters.AddWithValue("@candidateId", candidate.CandidateId);
                    conCommand.Parameters.AddWithValue("@candidatename", candidate.CandidateName);
                    conCommand.Parameters.AddWithValue("@gender", candidate.Gender);
                    conCommand.Parameters.AddWithValue("@marital_status", candidate.MaritalStatus);
                    conCommand.Parameters.AddWithValue("@dob", candidate.Dob);
                    conCommand.Parameters.AddWithValue("@candidatecontactnumber", candidate.CandidateContactNumber);
                    conCommand.Parameters.AddWithValue("@fathername", candidate.FatherName);
                    conCommand.Parameters.AddWithValue("@candidate_emailid", candidate.CandidateEmailId);
                    conCommand.Parameters.AddWithValue("@candidate_image", FN);
                    conCommand.ExecuteNonQuery();
                    con.Close();
                   // ViewData["btn"] = "Update";


                }
                else  {

                    con.Open();

                    SqlCommand conCommand = new SqlCommand("Usp_SaveCandidateDetails", con);
                    conCommand.CommandType = CommandType.StoredProcedure;
                    conCommand.Parameters.AddWithValue("@ActionType", "Update");
                    conCommand.Parameters.AddWithValue("@candidateId", candidate.CandidateId);
                    conCommand.Parameters.AddWithValue("@candidatename", candidate.CandidateName);
                    conCommand.Parameters.AddWithValue("@gender", candidate.Gender);
                    conCommand.Parameters.AddWithValue("@marital_status", candidate.MaritalStatus);
                    conCommand.Parameters.AddWithValue("@dob", candidate.Dob);
                    conCommand.Parameters.AddWithValue("@candidatecontactnumber", candidate.CandidateContactNumber);
                    conCommand.Parameters.AddWithValue("@fathername", candidate.FatherName);
                    conCommand.Parameters.AddWithValue("@candidate_emailid", candidate.CandidateEmailId);
                    conCommand.Parameters.AddWithValue("@candidate_image", FN);
                    SqlDataAdapter da = new SqlDataAdapter(conCommand);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    con.Close();


                }



            }
                         
            return View();

        }

        // Get Data Testing Purpose Action Start


        // Get Data Testing Purpose Action End
       
        
        
        
        // For Next View Start Testing
        public IActionResult CandidateRegisterationDetailNext()
        {

            return View();
        }
        // End Here Testing
    }
}
