using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Kernel.Geom;
using iText.Layout.Properties;
using iText.IO.Font.Constants;
using iText.Kernel.Font;
using System.IO;


namespace asp.netloginpage
{
    public partial class Billing : System.Web.UI.Page
    {
        string connStr = @"Data Source=DESKTOP-1DUMAFQ\SQLEXPRESS;Initial Catalog=LoginDB;Integrated Security=True";
        List<BillItem> billItems;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadPatients();
                LoadReceptionists();
                billItems = new List<BillItem>();
                ViewState["BillItems"] = billItems;
                btnGeneratePDF.Visible = false;
            }
        }

        private void LoadPatients()
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("SELECT PatientID, Name FROM Patient", con);
                con.Open();
                ddlPatients.DataSource = cmd.ExecuteReader();
                ddlPatients.DataTextField = "Name";
                ddlPatients.DataValueField = "PatientID";
                ddlPatients.DataBind();
            }
        }

        private void LoadReceptionists()
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("SELECT ReceptionistID, Name FROM Receptionist", con);
                con.Open();
                ddlReceptionists.DataSource = cmd.ExecuteReader();
                ddlReceptionists.DataTextField = "Name";
                ddlReceptionists.DataValueField = "ReceptionistID";
                ddlReceptionists.DataBind();
            }
        }

        protected void btnAddItem_Click(object sender, EventArgs e)
        {
            billItems = ViewState["BillItems"] as List<BillItem>;
            BillItem item = new BillItem
            {
                Description = txtDescription.Text,
                Quantity = int.Parse(txtQuantity.Text),
                UnitPrice = decimal.Parse(txtUnitPrice.Text)
            };

            billItems.Add(item);
            ViewState["BillItems"] = billItems;
            gvBillItems.DataSource = billItems;
            gvBillItems.DataBind();
            UpdateTotalAmount();
        }

        private void UpdateTotalAmount()
        {
            decimal total = 0;
            billItems = ViewState["BillItems"] as List<BillItem>;
            foreach (var item in billItems)
                total += item.TotalPrice;

            lblTotalAmount.Text = "Total: $" + total.ToString("F2");
        }

        protected void btnGenerateInvoice_Click(object sender, EventArgs e)
        {
            billItems = ViewState["BillItems"] as List<BillItem>;

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                SqlTransaction transaction = con.BeginTransaction();

                try
                {
                    SqlCommand cmd = new SqlCommand(@"
                        INSERT INTO Bill (PatientID, ReceptionistID, TotalAmount, PaymentStatus) 
                        OUTPUT INSERTED.BillID 
                        VALUES (@PatientID, @ReceptionistID, @TotalAmount, @PaymentStatus)", con, transaction);

                    cmd.Parameters.AddWithValue("@PatientID", ddlPatients.SelectedValue);
                    cmd.Parameters.AddWithValue("@ReceptionistID", ddlReceptionists.SelectedValue);

                    decimal totalAmount = 0;
                    foreach (var item in billItems)
                        totalAmount += item.TotalPrice;

                    cmd.Parameters.AddWithValue("@TotalAmount", totalAmount);
                    cmd.Parameters.AddWithValue("@PaymentStatus", "Unpaid");

                    int billID = (int)cmd.ExecuteScalar();

                    foreach (var item in billItems)
                    {
                        SqlCommand itemCmd = new SqlCommand(@"
                            INSERT INTO BillItem (BillID, Description, Quantity, UnitPrice) 
                            VALUES (@BillID, @Description, @Quantity, @UnitPrice)", con, transaction);

                        itemCmd.Parameters.AddWithValue("@BillID", billID);
                        itemCmd.Parameters.AddWithValue("@Description", item.Description);
                        itemCmd.Parameters.AddWithValue("@Quantity", item.Quantity);
                        itemCmd.Parameters.AddWithValue("@UnitPrice", item.UnitPrice);

                        itemCmd.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    lblTotalAmount.Text = "Invoice Generated. Total: $" + totalAmount.ToString("F2");
                    btnGeneratePDF.Visible = true;
                }
                catch
                {
                    transaction.Rollback();
                    lblTotalAmount.Text = "Invoice generation failed!";
                }
            }
        }

        protected void btnGeneratePDF_Click(object sender, EventArgs e)
        {
            billItems = ViewState["BillItems"] as List<BillItem>;
            decimal totalAmount = 0;

            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    PdfWriter writer = new PdfWriter(ms);
                    PdfDocument pdf = new PdfDocument(writer);
                    Document document = new Document(pdf, PageSize.A4);
                    PdfFont boldFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
                    PdfFont normalFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);

                    document.Add(new Paragraph("INVOICE")
                        .SetFont(boldFont)
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetFontSize(20));

                    document.Add(new Paragraph("Date: " + DateTime.Now.ToString("yyyy-MM-dd")).SetFont(normalFont));
                    document.Add(new Paragraph("Patient: " + ddlPatients.SelectedItem.Text).SetFont(normalFont));
                    document.Add(new Paragraph("Receptionist: " + ddlReceptionists.SelectedItem.Text).SetFont(normalFont));
                    document.Add(new Paragraph(" "));

                    Table table = new Table(UnitValue.CreatePercentArray(4)).UseAllAvailableWidth();
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Description").SetFont(boldFont)));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Quantity").SetFont(boldFont)));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Unit Price").SetFont(boldFont)));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Total Price").SetFont(boldFont)));

                    foreach (var item in billItems)
                    {
                        table.AddCell(new Cell().Add(new Paragraph(item.Description).SetFont(normalFont)));
                        table.AddCell(new Cell().Add(new Paragraph(item.Quantity.ToString()).SetFont(normalFont)));
                        table.AddCell(new Cell().Add(new Paragraph(item.UnitPrice.ToString("F2")).SetFont(normalFont)));
                        table.AddCell(new Cell().Add(new Paragraph(item.TotalPrice.ToString("F2")).SetFont(normalFont)));

                        totalAmount += item.TotalPrice;
                    }

                    document.Add(table);
                    document.Add(new Paragraph("Total Amount: $" + totalAmount.ToString("F2"))
                        .SetFont(boldFont)
                        .SetTextAlignment(TextAlignment.RIGHT));

                    document.Close();

                    // Write to browser (download)
                    byte[] pdfBytes = ms.ToArray();
                    Response.Clear();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("Content-Disposition", "attachment; filename=Invoice_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".pdf");
                    Response.OutputStream.Write(pdfBytes, 0, pdfBytes.Length);
                    Response.Flush();
                    Response.End(); // Properly end response
                }
            }
            catch (Exception ex)
            {
                lblTotalAmount.Text = "PDF generation failed: " + ex.Message;
            }
        }


        protected void btnGoHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

        [Serializable]
        public class BillItem
        {
            public string Description { get; set; }
            public int Quantity { get; set; }
            public decimal UnitPrice { get; set; }
            public decimal TotalPrice
            {
                get { return Quantity * UnitPrice; }
            }
        }
    }
}
