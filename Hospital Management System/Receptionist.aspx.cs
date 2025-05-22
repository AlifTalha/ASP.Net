using System;
using System.Data;
using System.Data.SqlClient;

namespace asp.netloginpage
{
    public partial class Receptionist : System.Web.UI.Page
    {
        string connStr = @"Data Source=DESKTOP-1DUMAFQ\SQLEXPRESS;Initial Catalog=LoginDB;Integrated Security=True";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadData();
        }

        private void LoadData()
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Receptionist", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtPhone.Text) || string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                // You can show a validation message here if needed
                return;
            }

            using (SqlConnection con = new SqlConnection(connStr))
            {
                string query = "INSERT INTO Receptionist (Name, Phone, Email) VALUES (@Name, @Phone, @Email)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Name", txtName.Text.Trim());
                cmd.Parameters.AddWithValue("@Phone", txtPhone.Text.Trim());
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());

                con.Open();
                cmd.ExecuteNonQuery();
                LoadData(); // Refresh the GridView
            }
        }

        protected void GridView1_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Receptionist WHERE ReceptionistID=@id", con);
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                cmd.ExecuteNonQuery();
                LoadData(); // Refresh the GridView
            }
        }

        // Event handler for Go to Home button
        protected void btnGoHome_Click(object sender, EventArgs e)
        {
            // Redirect to the Default.aspx page (Home Page)
            Response.Redirect("Default.aspx");
        }
    }
}
