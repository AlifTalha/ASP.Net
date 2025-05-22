using System;
using System.Web.UI;

namespace HospitalManagementSystem
{
    public partial class HelpDesk : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if user is logged in
                if (Session["UserID"] == null)
                {
                    Response.Redirect("Login.aspx");
                }

                // Set user role label
                lblUserRole.Text = Session["UserRole"]?.ToString() ?? "User";
            }
        }

        protected void btnNewTicket_Click(object sender, EventArgs e)
        {
            pnlTickets.Visible = false;
            pnlNewTicket.Visible = true;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            pnlTickets.Visible = true;
            pnlNewTicket.Visible = false;
        }

        protected void btnSubmitTicket_Click(object sender, EventArgs e)
        {
            // Here you would typically save the ticket to a database
            // For demonstration, we'll just show a success message and return to the ticket list

            // Validate inputs
            if (string.IsNullOrWhiteSpace(txtSubject.Text) || string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showAlert", "alert('Please fill in all required fields.');", true);
                return;
            }

            // Save ticket logic would go here
            // ...

            // Show success message
            ScriptManager.RegisterStartupScript(this, GetType(), "showAlert", "alert('Ticket submitted successfully!');", true);

            // Reset form and show ticket list
            txtSubject.Text = "";
            txtDescription.Text = "";
            ddlPriority.SelectedIndex = 1; // Reset to Medium
            pnlTickets.Visible = true;
            pnlNewTicket.Visible = false;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            // Here you would typically filter tickets based on the search term
            // For demonstration, we'll just show all tickets
            ScriptManager.RegisterStartupScript(this, GetType(), "showAlert",
                $"alert('Search functionality would filter tickets based on: {txtSearch.Text}');", true);
        }
    }
}