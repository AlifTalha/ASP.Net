using System;
using System.Web.UI;

namespace asp.netloginpage
{
    public partial class Dashboard : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                // Set user details
                lblUserRole.Text = Session["role"] != null ? Session["role"].ToString() : "User";

                // You can add any additional initialization code here
                // For example, you could load real data from your database
                // and populate the dashboard metrics
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("Login.aspx");
        }
    }
}