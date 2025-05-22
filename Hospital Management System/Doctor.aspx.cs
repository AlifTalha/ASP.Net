using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace asp.netloginpage
{
    public partial class Doctor : System.Web.UI.Page
    {
        string connStr = @"Data Source=DESKTOP-1DUMAFQ\SQLEXPRESS;Initial Catalog=LoginDB;Integrated Security=True";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadDoctor();
        }

        private void LoadDoctor()
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Doctor", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                string query = @"INSERT INTO Doctor (Name, Specialization, Phone, Email, Schedule, Notes)
                                 VALUES (@Name, @Spec, @Phone, @Email, @Schedule, @Notes)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Name", txtName.Text.Trim());
                cmd.Parameters.AddWithValue("@Spec", txtSpec.Text.Trim());
                cmd.Parameters.AddWithValue("@Phone", txtPhone.Text.Trim());
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                cmd.Parameters.AddWithValue("@Schedule", txtSchedule.Text.Trim());
                cmd.Parameters.AddWithValue("@Notes", txtNotes.Text.Trim());

                con.Open();
                cmd.ExecuteNonQuery();
                LoadDoctor();
            }
        }

        protected void GridView1_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            int doctorID = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Doctor WHERE DoctorID=@id", con);
                cmd.Parameters.AddWithValue("@id", doctorID);
                con.Open();
                cmd.ExecuteNonQuery();
                LoadDoctor();
            }
        }

        protected void GridView1_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            LoadDoctor();
        }

        protected void GridView1_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            LoadDoctor();
        }

        protected void GridView1_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        {
            GridViewRow row = GridView1.Rows[e.RowIndex];
            int doctorID = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
            string name = ((System.Web.UI.WebControls.TextBox)row.Cells[1].Controls[0]).Text;
            string spec = ((System.Web.UI.WebControls.TextBox)row.Cells[2].Controls[0]).Text;
            string phone = ((System.Web.UI.WebControls.TextBox)row.Cells[3].Controls[0]).Text;
            string email = ((System.Web.UI.WebControls.TextBox)row.Cells[4].Controls[0]).Text;
            string schedule = ((System.Web.UI.WebControls.TextBox)row.Cells[5].Controls[0]).Text;
            string notes = ((System.Web.UI.WebControls.TextBox)row.Cells[6].Controls[0]).Text;

            using (SqlConnection con = new SqlConnection(connStr))
            {
                string query = @"UPDATE Doctor SET Name=@Name, Specialization=@Spec, Phone=@Phone,
                                 Email=@Email, Schedule=@Schedule, Notes=@Notes WHERE DoctorID=@id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Spec", spec);
                cmd.Parameters.AddWithValue("@Phone", phone);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Schedule", schedule);
                cmd.Parameters.AddWithValue("@Notes", notes);
                cmd.Parameters.AddWithValue("@id", doctorID);

                con.Open();
                cmd.ExecuteNonQuery();
                GridView1.EditIndex = -1;
                LoadDoctor();
            }
        }
    }
}
