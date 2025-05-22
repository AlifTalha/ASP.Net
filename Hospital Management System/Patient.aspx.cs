using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace asp.netloginpage
{
    public partial class Patient : System.Web.UI.Page
    {
        string connStr = @"Data Source=DESKTOP-1DUMAFQ\SQLEXPRESS;Initial Catalog=LoginDB;Integrated Security=True";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadData();
        }

        private void LoadData(string search = "")
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                string query = "SELECT * FROM Patient";
                if (!string.IsNullOrEmpty(search))
                    query += " WHERE Name LIKE @search OR Phone LIKE @search";

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                if (!string.IsNullOrEmpty(search))
                    da.SelectCommand.Parameters.AddWithValue("@search", "%" + search + "%");

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
                con.Open();
                SqlCommand cmd = new SqlCommand(@"INSERT INTO Patient (Name, Gender, Age, Phone, Address, BloodGroup, MaritalStatus, NationalID)
                                                  OUTPUT INSERTED.PatientID
                                                  VALUES (@Name, @Gender, @Age, @Phone, @Address, @BloodGroup, @MaritalStatus, @NationalID)", con);
                cmd.Parameters.AddWithValue("@Name", txtName.Text.Trim());
                cmd.Parameters.AddWithValue("@Gender", ddlGender.SelectedValue);
                cmd.Parameters.AddWithValue("@Age", int.Parse(txtAge.Text));
                cmd.Parameters.AddWithValue("@Phone", txtPhone.Text.Trim());
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text.Trim());
                cmd.Parameters.AddWithValue("@BloodGroup", txtBloodGroup.Text.Trim());
                cmd.Parameters.AddWithValue("@MaritalStatus", txtMaritalStatus.Text.Trim());
                cmd.Parameters.AddWithValue("@NationalID", txtNationalID.Text.Trim());

                int patientID = (int)cmd.ExecuteScalar();

                SqlCommand medCmd = new SqlCommand(@"INSERT INTO MedicalHistory (PatientID, Condition, Treatment, Notes, DateRecorded)
                                                     VALUES (@PatientID, @Condition, @Treatment, @Notes, @Date)", con);
                medCmd.Parameters.AddWithValue("@PatientID", patientID);
                medCmd.Parameters.AddWithValue("@Condition", txtMedicalCondition.Text.Trim());
                medCmd.Parameters.AddWithValue("@Treatment", txtTreatment.Text.Trim());
                medCmd.Parameters.AddWithValue("@Notes", txtNotes.Text.Trim());
                medCmd.Parameters.AddWithValue("@Date", DateTime.Now);

                medCmd.ExecuteNonQuery();
                ClearFields();
                LoadData();
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (ViewState["PatientID"] != null)
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(@"UPDATE Patient SET Name=@Name, Gender=@Gender, Age=@Age, Phone=@Phone,
                                                     Address=@Address, BloodGroup=@BloodGroup, MaritalStatus=@MaritalStatus,
                                                     NationalID=@NationalID WHERE PatientID=@id", con);

                    cmd.Parameters.AddWithValue("@Name", txtName.Text.Trim());
                    cmd.Parameters.AddWithValue("@Gender", ddlGender.SelectedValue);
                    cmd.Parameters.AddWithValue("@Age", int.Parse(txtAge.Text));
                    cmd.Parameters.AddWithValue("@Phone", txtPhone.Text.Trim());
                    cmd.Parameters.AddWithValue("@Address", txtAddress.Text.Trim());
                    cmd.Parameters.AddWithValue("@BloodGroup", txtBloodGroup.Text.Trim());
                    cmd.Parameters.AddWithValue("@MaritalStatus", txtMaritalStatus.Text.Trim());
                    cmd.Parameters.AddWithValue("@NationalID", txtNationalID.Text.Trim());
                    cmd.Parameters.AddWithValue("@id", ViewState["PatientID"]);

                    cmd.ExecuteNonQuery();
                    LoadData();
                    ClearFields();
                    btnAdd.Visible = true;
                    btnUpdate.Visible = false;
                }
            }
        }

        protected void GridView1_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Patient WHERE PatientID=@id", con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                LoadData();
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GridView1.SelectedRow;
            ViewState["PatientID"] = GridView1.DataKeys[row.RowIndex].Value;
            txtName.Text = row.Cells[1].Text;
            ddlGender.SelectedValue = row.Cells[2].Text;
            txtPhone.Text = row.Cells[3].Text;
            btnAdd.Visible = false;
            btnUpdate.Visible = true;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData(txtSearch.Text.Trim());
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            ClearFields();
            LoadData();
        }

        private void ClearFields()
        {
            txtName.Text = "";
            ddlGender.ClearSelection();
            txtAge.Text = "";
            txtPhone.Text = "";
            txtAddress.Text = "";
            txtBloodGroup.Text = "";
            txtMaritalStatus.Text = "";
            txtNationalID.Text = "";
            txtMedicalCondition.Text = "";
            txtTreatment.Text = "";
            txtNotes.Text = "";
        }
    }
}
