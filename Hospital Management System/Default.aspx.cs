using System;
using System.Web.UI;

namespace HospitalManagementSystem
{
    public partial class Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if user is logged in
                if (Session["Username"] == null)
                {
                    Response.Redirect("Login.aspx");
                }
                else
                {
                    // Display user information
                    lblUserRole.Text = Session["Role"].ToString();

                    // Set active menu item based on current page
                    string currentPage = System.IO.Path.GetFileName(Request.Url.AbsolutePath);
                    SetActiveMenuItem(currentPage);
                }
            }
        }

        private void SetActiveMenuItem(string currentPage)
        {
            // Remove active class from all menu items
            // This would be handled via JavaScript in a real application
            // For ASP.NET Web Forms, we can use server-side code to set the active item
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            // Clear all session variables
            Session.Clear();
            Session.Abandon();

            // Redirect to login page
            Response.Redirect("Login.aspx");
        }
    }
}