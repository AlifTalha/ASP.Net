using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace asp.netloginpage
{
    public partial class Appointment : System.Web.UI.Page
    {
        string connStr = @"Data Source=DESKTOP-1DUMAFQ\SQLEXPRESS;Initial Catalog=LoginDB;Integrated Security=True";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadPatients();
                LoadDoctors();
                LoadAppointments();
            }
        }

        private void LoadPatients()
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("SELECT PatientID, Name FROM Patient", con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                ddlPatient.DataSource = dr;
                ddlPatient.DataTextField = "Name";
                ddlPatient.DataValueField = "PatientID";
                ddlPatient.DataBind();
            }
        }

        private void LoadDoctors()
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("SELECT DoctorID, Name FROM Doctor", con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                ddlDoctor.DataSource = dr;
                ddlDoctor.DataTextField = "Name";
                ddlDoctor.DataValueField = "DoctorID";
                ddlDoctor.DataBind();
            }
        }

        private void LoadAppointments()
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                string query = @"
                    SELECT A.AppointmentID, P.Name AS PatientName, D.Name AS DoctorName, 
                           A.AppointmentDate, A.Status
                    FROM Appointment A
                    INNER JOIN Patient P ON A.PatientID = P.PatientID
                    INNER JOIN Doctor D ON A.DoctorID = D.DoctorID";

                SqlDataAdapter da = new SqlDataAdapter(query, con);
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
                string query = "INSERT INTO Appointment (PatientID, DoctorID, AppointmentDate, Status) VALUES (@PatientID, @DoctorID, @Date, @Status)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@PatientID", ddlPatient.SelectedValue);
                cmd.Parameters.AddWithValue("@DoctorID", ddlDoctor.SelectedValue);
                cmd.Parameters.AddWithValue("@Date", DateTime.Parse(txtDate.Text));
                cmd.Parameters.AddWithValue("@Status", ddlStatus.SelectedValue);
                con.Open();
                cmd.ExecuteNonQuery();
                ClearFields();
                LoadAppointments();
            }
        }

        protected void GridView1_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Appointment WHERE AppointmentID=@id", con);
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                cmd.ExecuteNonQuery();
                LoadAppointments();
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GridView1.SelectedRow;
            ViewState["AppointmentID"] = GridView1.SelectedDataKey.Value;
            ddlPatient.SelectedItem.Text = row.Cells[1].Text;
            ddlDoctor.SelectedItem.Text = row.Cells[2].Text;
            txtDate.Text = DateTime.Parse(row.Cells[3].Text).ToString("yyyy-MM-dd");
            ddlStatus.SelectedValue = row.Cells[4].Text;

            btnAdd.Visible = false;
            btnUpdate.Visible = true;
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (ViewState["AppointmentID"] != null)
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    string query = "UPDATE Appointment SET PatientID=@PatientID, DoctorID=@DoctorID, AppointmentDate=@Date, Status=@Status WHERE AppointmentID=@id";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@PatientID", ddlPatient.SelectedValue);
                    cmd.Parameters.AddWithValue("@DoctorID", ddlDoctor.SelectedValue);
                    cmd.Parameters.AddWithValue("@Date", DateTime.Parse(txtDate.Text));
                    cmd.Parameters.AddWithValue("@Status", ddlStatus.SelectedValue);
                    cmd.Parameters.AddWithValue("@id", ViewState["AppointmentID"]);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    LoadAppointments();
                    ClearFields();

                    btnAdd.Visible = true;
                    btnUpdate.Visible = false;
                }
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
            btnAdd.Visible = true;
            btnUpdate.Visible = false;
        }

        private void ClearFields()
        {
            txtDate.Text = "";
            ddlStatus.SelectedIndex = 0;
            ddlPatient.SelectedIndex = 0;
            ddlDoctor.SelectedIndex = 0;
        }
    }
}
