using LDRTE.Pages.CreateDocument;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace LDRTE.Pages.ViewDocument
{
    public class ViewDocumentModel : PageModel
    {
        public Document Document { get; set; }
        IConfiguration _configuration;
        SqlConnection con;
        //adding constructor to get the connection string from appsettings.json file
        public ViewDocumentModel(IConfiguration configuration)
        {
            _configuration = configuration;
            con = new SqlConnection(_configuration.GetConnectionString("LDRTEConnection"));
        }

        public void OnGet()
        {
            //Query to fetch from db all the data from RichEditor table

            con.Open();
            string query = "SELECT * FROM RichEditor";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader = cmd.ExecuteReader();
            //Display each and every data from db in html page
            foreach (var item in reader)
            {
                Document = new Document();
                Document.Id = reader.GetInt32(0);
                Document.Title = reader.GetString(1);
                Document.Description = reader.GetString(2);
                Document.Content = reader.GetString(3);
            }

            con.Close();

        }
    }
}
