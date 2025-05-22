using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HospitalManagementSystem
{
    public partial class Inventory : System.Web.UI.Page
    {
        private int pageSize = 10;
        private int currentPage = 1;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserId"] == null)
                {
                    Response.Redirect("Login.aspx");
                }

                BindInventory();
            }
        }

        protected void Page_Changed(object sender, EventArgs e)
        {
            currentPage = int.Parse((sender as LinkButton).CommandArgument);
            BindInventory();
        }

        private void BindInventory()
        {
            string connectionString = @"Data Source=DESKTOP-1DUMAFQ\SQLEXPRESS;Initial Catalog=LoginDB;Integrated Security=True";
            string baseQuery = "SELECT * FROM Inventory";
            string countQuery = "SELECT COUNT(*) FROM Inventory";

            string whereClause = "";
            if (!string.IsNullOrEmpty(txtSearch.Text))
            {
                whereClause = " WHERE Name LIKE @Search OR ItemCode LIKE @Search OR Description LIKE @Search";
            }

            if (!string.IsNullOrEmpty(ddlCategory.SelectedValue))
            {
                whereClause += string.IsNullOrEmpty(whereClause) ? " WHERE" : " AND";
                whereClause += " Category = @Category";
            }

            int totalRecords = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand countCommand = new SqlCommand(countQuery + whereClause, connection);

                if (!string.IsNullOrEmpty(txtSearch.Text))
                {
                    countCommand.Parameters.AddWithValue("@Search", "%" + txtSearch.Text + "%");
                }

                if (!string.IsNullOrEmpty(ddlCategory.SelectedValue))
                {
                    countCommand.Parameters.AddWithValue("@Category", ddlCategory.SelectedValue);
                }

                connection.Open();
                totalRecords = (int)countCommand.ExecuteScalar();
            }

            int totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            string query = $@"
                WITH InventoryCTE AS (
                    SELECT *, ROW_NUMBER() OVER (ORDER BY {ddlSort.SelectedValue}) AS RowNum
                    FROM Inventory
                    {whereClause}
                )
                SELECT * FROM InventoryCTE
                WHERE RowNum BETWEEN @StartIndex AND @EndIndex";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                if (!string.IsNullOrEmpty(txtSearch.Text))
                {
                    command.Parameters.AddWithValue("@Search", "%" + txtSearch.Text + "%");
                }

                if (!string.IsNullOrEmpty(ddlCategory.SelectedValue))
                {
                    command.Parameters.AddWithValue("@Category", ddlCategory.SelectedValue);
                }

                command.Parameters.AddWithValue("@StartIndex", (currentPage - 1) * pageSize + 1);
                command.Parameters.AddWithValue("@EndIndex", currentPage * pageSize);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                gvInventory.DataSource = dt;
                gvInventory.DataBind();
            }

            BindPager(totalRecords, totalPages);
        }

        private void BindPager(int totalRecords, int totalPages)
        {
            var pages = new List<PagerItem>();

            if (currentPage > 1)
            {
                pages.Add(new PagerItem { Text = "«", Value = (currentPage - 1).ToString(), Enabled = true });
            }
            else
            {
                pages.Add(new PagerItem { Text = "«", Value = "1", Enabled = false });
            }

            int startPage = Math.Max(1, currentPage - 2);
            int endPage = Math.Min(totalPages, currentPage + 2);

            if (startPage > 1)
            {
                pages.Add(new PagerItem { Text = "1", Value = "1", Enabled = true });
                if (startPage > 2)
                {
                    pages.Add(new PagerItem { Text = "...", Value = (startPage - 1).ToString(), Enabled = false });
                }
            }

            for (int i = startPage; i <= endPage; i++)
            {
                pages.Add(new PagerItem
                {
                    Text = i.ToString(),
                    Value = i.ToString(),
                    Enabled = true,
                    IsCurrent = (i == currentPage)
                });
            }

            if (endPage < totalPages)
            {
                if (endPage < totalPages - 1)
                {
                    pages.Add(new PagerItem { Text = "...", Value = (endPage + 1).ToString(), Enabled = false });
                }
                pages.Add(new PagerItem { Text = totalPages.ToString(), Value = totalPages.ToString(), Enabled = true });
            }

            if (currentPage < totalPages)
            {
                pages.Add(new PagerItem { Text = "»", Value = (currentPage + 1).ToString(), Enabled = true });
            }
            else
            {
                pages.Add(new PagerItem { Text = "»", Value = totalPages.ToString(), Enabled = false });
            }

            rptPager.DataSource = pages;
            rptPager.DataBind();
        }

        protected void gvInventory_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView rowView = (DataRowView)e.Row.DataItem;
                int quantity = Convert.ToInt32(rowView["Quantity"]);
                int reorderLevel = rowView["ReorderLevel"] != DBNull.Value ? Convert.ToInt32(rowView["ReorderLevel"]) : 0;
                Label lblStockStatus = (Label)e.Row.FindControl("lblStockStatus");

                if (reorderLevel > 0 && quantity <= reorderLevel)
                {
                    if (quantity == 0)
                    {
                        lblStockStatus.Text = "Out of Stock";
                        lblStockStatus.CssClass = "stock-low";
                    }
                    else if (quantity <= (reorderLevel * 0.5))
                    {
                        lblStockStatus.Text = "Very Low";
                        lblStockStatus.CssClass = "stock-low";
                    }
                    else
                    {
                        lblStockStatus.Text = "Low";
                        lblStockStatus.CssClass = "stock-warning";
                    }
                }
                else
                {
                    lblStockStatus.Text = "In Stock";
                    lblStockStatus.CssClass = "stock-ok";
                }
            }
        }

        protected void gvInventory_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditItem")
            {
                int itemId = Convert.ToInt32(e.CommandArgument);
                LoadItemForEdit(itemId);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showModal", "$('#addItemModal').modal('show');", true);
            }
            else if (e.CommandName == "DeleteItem")
            {
                int itemId = Convert.ToInt32(e.CommandArgument);
                DeleteItem(itemId);
            }
        }

        private void LoadItemForEdit(int itemId)
        {
            string connectionString = @"Data Source=DESKTOP-1DUMAFQ\SQLEXPRESS;Initial Catalog=LoginDB;Integrated Security=True";
            string query = "SELECT * FROM Inventory WHERE ItemId = @ItemId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ItemId", itemId);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    lblModalTitle.Text = "Edit Inventory Item";
                    ViewState["EditItemId"] = itemId;

                    txtItemCode.Text = reader["ItemCode"].ToString();
                    txtItemName.Text = reader["Name"].ToString();
                    ddlCategoryModal.SelectedValue = reader["Category"].ToString();
                    txtDescription.Text = reader["Description"].ToString();
                    txtQuantity.Text = reader["Quantity"].ToString();
                    ddlUnit.SelectedValue = reader["Unit"].ToString();
                    txtReorderLevel.Text = reader["ReorderLevel"].ToString();

                    if (reader["ExpiryDate"] != DBNull.Value)
                    {
                        txtExpiryDate.Text = Convert.ToDateTime(reader["ExpiryDate"]).ToString("yyyy-MM-dd");
                    }

                    txtSupplier.Text = reader["Supplier"].ToString();
                    txtPurchasePrice.Text = reader["PurchasePrice"].ToString();
                    txtSellingPrice.Text = reader["SellingPrice"].ToString();
                    txtLocation.Text = reader["Location"].ToString();
                }
            }
        }

        private void DeleteItem(int itemId)
        {
            string connectionString = @"Data Source=DESKTOP-1DUMAFQ\SQLEXPRESS;Initial Catalog=LoginDB;Integrated Security=True";
            string query = "DELETE FROM Inventory WHERE ItemId = @ItemId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ItemId", itemId);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    ShowMessage("Item deleted successfully", "success");
                    BindInventory();
                }
                else
                {
                    ShowMessage("Error deleting item", "danger");
                }
            }
        }

        protected void btnSaveItem_Click(object sender, EventArgs e)
        {
            if (ViewState["EditItemId"] != null)
            {
                UpdateItem();
            }
            else
            {
                AddNewItem();
            }
        }

        private void AddNewItem()
        {
            string connectionString = @"Data Source=DESKTOP-1DUMAFQ\SQLEXPRESS;Initial Catalog=LoginDB;Integrated Security=True";
            string query = @"INSERT INTO Inventory (ItemCode, Name, Category, Description, Quantity, Unit, ReorderLevel, 
                           ExpiryDate, Supplier, PurchasePrice, SellingPrice, Location, CreatedBy, CreatedDate)
                           VALUES (@ItemCode, @Name, @Category, @Description, @Quantity, @Unit, @ReorderLevel, 
                           @ExpiryDate, @Supplier, @PurchasePrice, @SellingPrice, @Location, @CreatedBy, @CreatedDate)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                SetParameters(command);

                command.Parameters.AddWithValue("@CreatedBy", Session["UserId"]);
                command.Parameters.AddWithValue("@CreatedDate", DateTime.Now);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    ShowMessage("Item added successfully", "success");
                    BindInventory();
                    ClearForm();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "hideModal", "$('#addItemModal').modal('hide');", true);
                }
                else
                {
                    ShowMessage("Error adding item", "danger");
                }
            }
        }

        private void UpdateItem()
        {
            string connectionString = @"Data Source=DESKTOP-1DUMAFQ\SQLEXPRESS;Initial Catalog=LoginDB;Integrated Security=True";
            string query = @"UPDATE Inventory SET 
                            ItemCode = @ItemCode, 
                            Name = @Name, 
                            Category = @Category, 
                            Description = @Description, 
                            Quantity = @Quantity, 
                            Unit = @Unit, 
                            ReorderLevel = @ReorderLevel, 
                            ExpiryDate = @ExpiryDate, 
                            Supplier = @Supplier, 
                            PurchasePrice = @PurchasePrice, 
                            SellingPrice = @SellingPrice, 
                            Location = @Location,
                            ModifiedBy = @ModifiedBy,
                            ModifiedDate = @ModifiedDate
                            WHERE ItemId = @ItemId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                SetParameters(command);

                command.Parameters.AddWithValue("@ItemId", ViewState["EditItemId"]);
                command.Parameters.AddWithValue("@ModifiedBy", Session["UserId"]);
                command.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    ShowMessage("Item updated successfully", "success");
                    BindInventory();
                    ClearForm();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "hideModal", "$('#addItemModal').modal('hide');", true);
                }
                else
                {
                    ShowMessage("Error updating item", "danger");
                }
            }
        }

        private void SetParameters(SqlCommand command)
        {
            command.Parameters.AddWithValue("@ItemCode", txtItemCode.Text.Trim());
            command.Parameters.AddWithValue("@Name", txtItemName.Text.Trim());
            command.Parameters.AddWithValue("@Category", ddlCategoryModal.SelectedValue);
            command.Parameters.AddWithValue("@Description", txtDescription.Text.Trim());
            command.Parameters.AddWithValue("@Quantity", Convert.ToInt32(txtQuantity.Text));
            command.Parameters.AddWithValue("@Unit", ddlUnit.SelectedValue);
            command.Parameters.AddWithValue("@ReorderLevel", string.IsNullOrEmpty(txtReorderLevel.Text) ? (object)DBNull.Value : Convert.ToInt32(txtReorderLevel.Text));
            command.Parameters.AddWithValue("@ExpiryDate", string.IsNullOrEmpty(txtExpiryDate.Text) ? (object)DBNull.Value : Convert.ToDateTime(txtExpiryDate.Text));
            command.Parameters.AddWithValue("@Supplier", txtSupplier.Text.Trim());
            command.Parameters.AddWithValue("@PurchasePrice", string.IsNullOrEmpty(txtPurchasePrice.Text) ? (object)DBNull.Value : Convert.ToDecimal(txtPurchasePrice.Text));
            command.Parameters.AddWithValue("@SellingPrice", string.IsNullOrEmpty(txtSellingPrice.Text) ? (object)DBNull.Value : Convert.ToDecimal(txtSellingPrice.Text));
            command.Parameters.AddWithValue("@Location", txtLocation.Text.Trim());
        }

        private void ClearForm()
        {
            ViewState["EditItemId"] = null;
            lblModalTitle.Text = "Add New Inventory Item";
            txtItemCode.Text = "";
            txtItemName.Text = "";
            ddlCategoryModal.SelectedIndex = 0;
            txtDescription.Text = "";
            txtQuantity.Text = "";
            ddlUnit.SelectedIndex = 0;
            txtReorderLevel.Text = "";
            txtExpiryDate.Text = "";
            txtSupplier.Text = "";
            txtPurchasePrice.Text = "";
            txtSellingPrice.Text = "";
            txtLocation.Text = "";
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            currentPage = 1;
            BindInventory();
        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPage = 1;
            BindInventory();
        }

        protected void ddlSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindInventory();
        }

        private void ShowMessage(string message, string type)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "showAlert",
                $"alert('{message}');", true);
        }

        public class PagerItem
        {
            public string Text { get; set; }
            public string Value { get; set; }
            public bool Enabled { get; set; }
            public bool IsCurrent { get; set; }
        }
    }
}