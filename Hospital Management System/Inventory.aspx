<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Inventory.aspx.cs" Inherits="HospitalManagementSystem.Inventory" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Inventory Management</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0/css/all.min.css" rel="stylesheet" />
    <style>
        :root {
            --primary-color: #5d87ff;
            --danger-color: #ff5b5b;
            --success-color: #39cb7f;
            --warning-color: #ffae1f;
            --dark-color: #2a3547;
        }
        
        body {
            font-family: 'Poppins', sans-serif;
            background-color: #f5f8fa;
        }
        
        .inventory-container {
            max-width: 1200px;
            margin: 20px auto;
            padding: 20px;
            background: white;
            border-radius: 10px;
            box-shadow: 0 0 20px rgba(0, 0, 0, 0.05);
        }
        
        .inventory-header {
            display: flex;
            justify-content: space-between;
            align-items: center;
            margin-bottom: 25px;
            padding-bottom: 15px;
            border-bottom: 1px solid rgba(0, 0, 0, 0.1);
        }
        
        .inventory-title {
            font-size: 24px;
            font-weight: 600;
            color: var(--dark-color);
        }
        
        .btn-add {
            background-color: var(--primary-color);
            color: white;
            border: none;
            padding: 8px 16px;
            border-radius: 6px;
            font-weight: 500;
        }
        
        .btn-add:hover {
            background-color: #4a75e6;
        }
        
        .inventory-tabs {
            margin-bottom: 20px;
        }
        
        .nav-tabs .nav-link {
            color: #5a6a85;
            font-weight: 500;
            border: none;
            padding: 10px 20px;
        }
        
        .nav-tabs .nav-link.active {
            color: var(--primary-color);
            border-bottom: 2px solid var(--primary-color);
            background: transparent;
        }
        
        .search-filter {
            display: flex;
            gap: 15px;
            margin-bottom: 20px;
        }
        
        .search-box {
            flex: 1;
            position: relative;
        }
        
        .search-box i {
            position: absolute;
            left: 12px;
            top: 10px;
            color: #5a6a85;
        }
        
        .search-box input {
            padding-left: 40px;
            border-radius: 6px;
            border: 1px solid #ddd;
            height: 40px;
            width: 100%;
        }
        
        .filter-select {
            width: 200px;
        }
        
        .inventory-table {
            width: 100%;
            border-collapse: separate;
            border-spacing: 0;
        }
        
        .inventory-table th {
            background-color: #f8f9fa;
            padding: 12px 15px;
            text-align: left;
            font-weight: 600;
            color: var(--dark-color);
            border-bottom: 1px solid #ddd;
        }
        
        .inventory-table td {
            padding: 12px 15px;
            border-bottom: 1px solid #eee;
            vertical-align: middle;
        }
        
        .inventory-table tr:hover td {
            background-color: #f5f8fa;
        }
        
        .stock-low {
            color: var(--danger-color);
            font-weight: 600;
        }
        
        .stock-ok {
            color: var(--success-color);
            font-weight: 600;
        }
        
        .stock-warning {
            color: var(--warning-color);
            font-weight: 600;
        }
        
        .action-btns {
            display: flex;
            gap: 8px;
        }
        
        .btn-action {
            width: 30px;
            height: 30px;
            border-radius: 50%;
            display: flex;
            align-items: center;
            justify-content: center;
            border: none;
            background: transparent;
            color: #5a6a85;
        }
        
        .btn-action:hover {
            background-color: rgba(0, 0, 0, 0.05);
        }
        
        .btn-edit {
            color: var(--primary-color);
        }
        
        .btn-delete {
            color: var(--danger-color);
        }
        
        .pagination {
            display: flex;
            justify-content: center;
            margin-top: 20px;
        }
        
        .page-link {
            padding: 5px 10px;
            border: 1px solid #ddd;
            border-radius: 4px;
            text-decoration: none;
            color: #5a6a85;
        }
        
        .page-link:hover {
            background-color: #f5f8fa;
        }
        
        .page-link.current-page {
            background-color: var(--primary-color);
            color: white;
            border-color: var(--primary-color);
        }
        
        .page-link.disabled {
            color: #ccc;
            cursor: not-allowed;
        }
        
        .modal-header {
            border-bottom: none;
            padding-bottom: 0;
        }
        
        .modal-footer {
            border-top: none;
            padding-top: 0;
        }
        
        .form-label {
            font-weight: 500;
            margin-bottom: 5px;
        }
        
        .form-control, .form-select {
            padding: 8px 12px;
            border-radius: 6px;
            border: 1px solid #ddd;
        }
        
        .form-control:focus, .form-select:focus {
            border-color: var(--primary-color);
            box-shadow: 0 0 0 0.25rem rgba(93, 135, 255, 0.25);
        }
        
        .btn-primary {
            background-color: var(--primary-color);
            border: none;
        }
        
        .btn-primary:hover {
            background-color: #4a75e6;
        }
        
        .badge {
            padding: 5px 10px;
            border-radius: 20px;
            font-weight: 500;
        }
        
        .badge-medicine {
            background-color: #e3f2fd;
            color: #1976d2;
        }
        
        .badge-equipment {
            background-color: #e8f5e9;
            color: #388e3c;
        }
        
        .badge-supplies {
            background-color: #fff3e0;
            color: #ffa000;
        }
        .btn-home {
    background-color:forestgreen;
    color: white;
    padding: 8px 20px;
    border-radius: 6px;
    text-decoration: none;
    display: inline-flex;
    align-items: center;
    transition: var(--transition);
}

.btn-home:hover {
    background-color:darkred;
    color: white;
    transform: translateY(-2px);
}

  body, html {
      height: 100%;
      margin: 0;
      font-family: Arial, sans-serif;
  }

  .wrapper {
      display: flex;
      height: 100vh;
  }

  .sidebar {
      width: 250px;
      background-color: #343a40;
      color: #fff;
      flex-shrink: 0;
      display: flex;
      flex-direction: column;
      padding: 20px;
  }

  .sidebar-brand {
      display: flex;
      align-items: center;
      margin-bottom: 20px;
  }

  .brand-logo {
      font-size: 24px;
      margin-right: 10px;
  }

  .brand-text {
      font-size: 20px;
      font-weight: bold;
  }

  .sidebar-user {
      text-align: center;
      margin-bottom: 30px;
  }

  .user-avatar {
      width: 80px;
      height: 80px;
      border-radius: 50%;
      margin-bottom: 10px;
  }

  .user-name {
      margin: 0;
  }

  .user-role {
      font-size: 14px;
      color: #bbb;
  }

  .sidebar-menu {
      flex-grow: 1;
  }

  .menu-title {
      font-size: 14px;
      text-transform: uppercase;
      margin: 20px 0 10px;
      color: #adb5bd;
  }

  .menu-item {
      display: flex;
      align-items: center;
      padding: 10px;
      color: #fff;
      text-decoration: none;
      border-radius: 4px;
      transition: background 0.3s;
  }

  .menu-item:hover,
  .menu-item.active {
      background-color: #495057;
  }

  .menu-icon {
      width: 25px;
      font-size: 16px;
  }

  .menu-text {
      margin-left: 10px;
  }

  .main-content {
      flex-grow: 1;
      background-color: #f8f9fa;
      padding: 30px;
      overflow-y: auto;
  }

  .card {
      box-shadow: 0 0 10px rgba(0,0,0,0.1);
  }

  .table th, .table td {
      vertical-align: middle;
  }

  .total {
      font-weight: bold;
      font-size: 18px;
      text-align: end;
      margin-top: 10px;
  }
    </style>
</head>
<body>
    <form id="form1" runat="server">


          <div class="wrapper">

      <!-- Sidebar -->
      <div class="sidebar">
          <div class="sidebar-brand">
              <div class="brand-logo"><i class="fas fa-hospital"></i></div>
              <span class="brand-text">TALHA's Hospital</span>
          </div>

          <div class="sidebar-user">
              <img src="https://ui-avatars.com/api/?name=<%= Session["FirstName"] %>+<%= Session["LastName"] %>&background=5d87ff&color=fff" class="user-avatar" />
              <h5 class="user-name"><%= Session["FirstName"] %> <%= Session["LastName"] %></h5>
              <span class="user-role">
                  <asp:Label ID="lblUserRole" runat="server"></asp:Label>
              </span>
          </div>

          <div class="sidebar-menu">
              <div class="menu-title">Main Menu</div>
              <a href="Default.aspx" class="menu-item"><div class="menu-icon"><i class="fas fa-tachometer-alt"></i></div><span class="menu-text">Dashboard</span></a>
              <a href="Patient.aspx" class="menu-item"><div class="menu-icon"><i class="fas fa-user-injured"></i></div><span class="menu-text">Patients</span></a>
              <a href="Doctor.aspx" class="menu-item"><div class="menu-icon"><i class="fas fa-user-md"></i></div><span class="menu-text">Doctors</span></a>
              <a href="Appointment.aspx" class="menu-item"><div class="menu-icon"><i class="fas fa-calendar-check"></i></div><span class="menu-text">Appointments</span></a>
               <a href="Receptionist.aspx" class="menu-item">
    <div class="menu-icon"><i class="fas fa-calendar-check"></i></div>
    <span class="menu-text">Receptionist</span>
</a>
              <a href="Billing.aspx" class="menu-item "><div class="menu-icon"><i class="fas fa-file-invoice-dollar"></i></div><span class="menu-text">Billing</span></a>

              <div class="menu-title">Management</div>
              <a href="Inventory.aspx" class="menu-item active"><div class="menu-icon"><i class="fas fa-pills"></i></div><span class="menu-text">Inventory</span></a>
              <a href="HelpDesk.aspx" class="menu-item"><div class="menu-icon"><i class="fas fa-chart-bar"></i></div><span class="menu-text">Reports</span></a>
              <a href="Settings.aspx" class="menu-item"><div class="menu-icon"><i class="fas fa-cog"></i></div><span class="menu-text">Settings</span></a>
          </div>
      </div>
        <!-- Include your sidebar navigation here (same as Default.aspx) -->
        
        <div class="main-content">
            <div class="inventory-container">
                <div class="inventory-header">
                    <h1 class="inventory-title">Inventory Management</h1>
                    <button type="button" class="btn btn-add" data-bs-toggle="modal" data-bs-target="#addItemModal">
                        <i class="fas fa-plus me-1"></i> Add New Item
                    </button>
                </div>
                
                <div class="search-filter">
                    <div class="search-box">
                        <i class="fas fa-search"></i>
                        <asp:TextBox ID="txtSearch" runat="server" placeholder="Search inventory..." AutoPostBack="true" OnTextChanged="txtSearch_TextChanged"></asp:TextBox>
                    </div>
                    <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-select filter-select" AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                        <asp:ListItem Value="">All Categories</asp:ListItem>
                        <asp:ListItem Value="Medicine">Medicines</asp:ListItem>
                        <asp:ListItem Value="Equipment">Equipment</asp:ListItem>
                        <asp:ListItem Value="Supply">Supplies</asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlSort" runat="server" CssClass="form-select filter-select" AutoPostBack="true" OnSelectedIndexChanged="ddlSort_SelectedIndexChanged">
                        <asp:ListItem Value="Name">Sort by Name</asp:ListItem>
                        <asp:ListItem Value="Quantity">Sort by Quantity</asp:ListItem>
                        <asp:ListItem Value="ExpiryDate">Sort by Expiry Date</asp:ListItem>
                    </asp:DropDownList>
                </div>
                
                <asp:GridView ID="gvInventory" runat="server" AutoGenerateColumns="False" CssClass="inventory-table"
                    DataKeyNames="ItemId" OnRowCommand="gvInventory_RowCommand" OnRowDataBound="gvInventory_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="ItemCode" HeaderText="Item Code" />
                        <asp:BoundField DataField="Name" HeaderText="Item Name" />
                        <asp:TemplateField HeaderText="Category">
                            <ItemTemplate>
                                <span class='badge badge-<%# Eval("Category").ToString().ToLower() %>'>
                                    <%# Eval("Category") %>
                                </span>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Description" HeaderText="Description" />
                        <asp:BoundField DataField="Quantity" HeaderText="Qty" />
                        <asp:TemplateField HeaderText="Stock Status">
                            <ItemTemplate>
                                <asp:Label ID="lblStockStatus" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Unit" HeaderText="Unit" />
                        <asp:BoundField DataField="ExpiryDate" HeaderText="Expiry Date" DataFormatString="{0:dd-MMM-yyyy}" />
                        <asp:BoundField DataField="Supplier" HeaderText="Supplier" />
                        <asp:TemplateField HeaderText="Actions">
                            <ItemTemplate>
                                <div class="action-btns">
                                    <asp:LinkButton ID="btnEdit" runat="server" CommandName="EditItem" CommandArgument='<%# Eval("ItemId") %>'
                                        CssClass="btn-action btn-edit" ToolTip="Edit">
                                        <i class="fas fa-edit"></i>
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btnDelete" runat="server" CommandName="DeleteItem" CommandArgument='<%# Eval("ItemId") %>'
                                        CssClass="btn-action btn-delete" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this item?');">
                                        <i class="fas fa-trash"></i>
                                    </asp:LinkButton>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <div class="alert alert-info">No inventory items found.</div>
                    </EmptyDataTemplate>
                </asp:GridView>
                
                <div class="pagination">
                    <asp:Repeater ID="rptPager" runat="server">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkPage" runat="server" 
                                Text='<%# Eval("Text") %>' 
                                CommandArgument='<%# Eval("Value") %>'
                                CssClass='<%# (bool)Eval("Enabled") ? 
                                            ((bool)Eval("IsCurrent") ? "page-link current-page" : "page-link") : 
                                            "page-link disabled" %>'
                                OnClick="Page_Changed" 
                                OnClientClick='<%# !(bool)Eval("Enabled") ? "return false;" : "" %>'>
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                <div class="text-center mt-4">
    <a href="Default.aspx" class="btn-home">
        <i class="fas fa-home me-2"></i>Back to Dashboard
    </a>
</div>
            </div>
        </div>
        
        <!-- Add/Edit Item Modal -->
        <div class="modal fade" id="addItemModal" tabindex="-1" aria-hidden="true">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">
                            <asp:Label ID="lblModalTitle" runat="server" Text="Add New Inventory Item"></asp:Label>
                        </h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-6 mb-3">
                                <label class="form-label">Item Code</label>
                                <asp:TextBox ID="txtItemCode" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-6 mb-3">
                                <label class="form-label">Item Name*</label>
                                <asp:TextBox ID="txtItemName" runat="server" CssClass="form-control" required></asp:TextBox>
                            </div>
                        </div>
                        
                        <div class="row">
                            <div class="col-md-6 mb-3">
                                <label class="form-label">Category*</label>
                                <asp:DropDownList ID="ddlCategoryModal" runat="server" CssClass="form-select" required>
                                    <asp:ListItem Value="">Select Category</asp:ListItem>
                                    <asp:ListItem Value="Medicine">Medicine</asp:ListItem>
                                    <asp:ListItem Value="Equipment">Equipment</asp:ListItem>
                                    <asp:ListItem Value="Supply">Supply</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-6 mb-3">
                                <label class="form-label">Description</label>
                                <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2"></asp:TextBox>
                            </div>
                        </div>
                        
                        <div class="row">
                            <div class="col-md-4 mb-3">
                                <label class="form-label">Quantity*</label>
                                <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control" TextMode="Number" min="0" required></asp:TextBox>
                            </div>
                            <div class="col-md-4 mb-3">
                                <label class="form-label">Unit*</label>
                                <asp:DropDownList ID="ddlUnit" runat="server" CssClass="form-select" required>
                                    <asp:ListItem Value="">Select Unit</asp:ListItem>
                                    <asp:ListItem Value="Pieces">Pieces</asp:ListItem>
                                    <asp:ListItem Value="Boxes">Boxes</asp:ListItem>
                                    <asp:ListItem Value="Bottles">Bottles</asp:ListItem>
                                    <asp:ListItem Value="Packs">Packs</asp:ListItem>
                                    <asp:ListItem Value="Vials">Vials</asp:ListItem>
                                    <asp:ListItem Value="Kits">Kits</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-4 mb-3">
                                <label class="form-label">Reorder Level</label>
                                <asp:TextBox ID="txtReorderLevel" runat="server" CssClass="form-control" TextMode="Number" min="0"></asp:TextBox>
                            </div>
                        </div>
                        
                        <div class="row">
                            <div class="col-md-6 mb-3">
                                <label class="form-label">Expiry Date</label>
                                <asp:TextBox ID="txtExpiryDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                            </div>
                            <div class="col-md-6 mb-3">
                                <label class="form-label">Supplier</label>
                                <asp:TextBox ID="txtSupplier" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        
                        <div class="row">
                            <div class="col-md-6 mb-3">
                                <label class="form-label">Purchase Price</label>
                                <asp:TextBox ID="txtPurchasePrice" runat="server" CssClass="form-control" TextMode="Number" step="0.01" min="0"></asp:TextBox>
                            </div>
                            <div class="col-md-6 mb-3">
                                <label class="form-label">Selling Price</label>
                                <asp:TextBox ID="txtSellingPrice" runat="server" CssClass="form-control" TextMode="Number" step="0.01" min="0"></asp:TextBox>
                            </div>
                        </div>
                        
                        <div class="mb-3">
                            <label class="form-label">Location/Storage</label>
                            <asp:TextBox ID="txtLocation" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>

                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
                        <asp:Button ID="btnSaveItem" runat="server" Text="Save Item" CssClass="btn btn-primary" OnClick="btnSaveItem_Click" />
                    </div>
                </div>
            </div>
        </div>
        
        <!-- JavaScript Libraries -->
        <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
        <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0/js/all.min.js"></script>
        
        <script>
            // Function to open modal for editing
            function openEditModal(itemId) {
                // You would typically make an AJAX call here to get item details
                // For now, we'll just show the modal
                $('#addItemModal').modal('show');
            }

            // Initialize tooltips
            $(document).ready(function () {
                $('[data-bs-toggle="tooltip"]').tooltip();
            });
        </script>
    </form>
</body>
</html>