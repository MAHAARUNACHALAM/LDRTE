using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Reflection;

namespace LDRTE.Pages.CreateDocument
{
    public class Document
    {
        //id,Title,Description,Content
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public string Content { get; set; }
    }
    public class CreateDocumentModel : PageModel
    {

        //Creating mysql connection and command to store and retrieve data from database
        IConfiguration _configuration;
        SqlConnection con;
        //adding constructor to get the connection string from appsettings.json file
        public CreateDocumentModel(IConfiguration configuration)
        {
            _configuration = configuration;
            con = new SqlConnection(_configuration.GetConnectionString("LDRTEConnection"));
        }

        //Create post method to post to db RichEditor table

        public void OnPost(Document formData)
        {
          
            con.Open();
            string query = "INSERT INTO RichEditor (Id,Title,Description,Content) VALUES (@Id,@Title,@Description,@Content)";
            SqlCommand cmd = new SqlCommand(query, con);
            //Add parameter to the query
            cmd.Parameters.AddWithValue("@Id", formData.Id);
            cmd.Parameters.AddWithValue("@Title", formData.Title);
            cmd.Parameters.AddWithValue("@Description", formData.Description);
            cmd.Parameters.AddWithValue("@Content", formData.Content);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void OnGet()
        {

        }
    }
}
