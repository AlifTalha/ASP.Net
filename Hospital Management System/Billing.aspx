<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Billing.aspx.cs" Inherits="asp.netloginpage.Billing" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Billing and Payments</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.0/css/all.min.css" rel="stylesheet" />
    <style>
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
                    <a href="Billing.aspx" class="menu-item active"><div class="menu-icon"><i class="fas fa-file-invoice-dollar"></i></div><span class="menu-text">Billing</span></a>

                    <div class="menu-title">Management</div>
                    <a href="Inventory.aspx" class="menu-item"><div class="menu-icon"><i class="fas fa-pills"></i></div><span class="menu-text">Inventory</span></a>
                    <a href="HelpDesk.aspx" class="menu-item"><div class="menu-icon"><i class="fas fa-chart-bar"></i></div><span class="menu-text">Reports</span></a>
                    <a href="Settings.aspx" class="menu-item"><div class="menu-icon"><i class="fas fa-cog"></i></div><span class="menu-text">Settings</span></a>
                </div>
            </div>

            <!-- Main Content -->
            <div class="main-content">
                <div class="container-fluid">
                    <div class="card p-4">
                        <h2 class="text-center mb-4">Billing & Invoice Generation</h2>

                        <div class="row mb-3">
                            <div class="col-md-6">
                                <label for="ddlPatients" class="form-label">Select Patient</label>
                                <asp:DropDownList ID="ddlPatients" runat="server" CssClass="form-select" AppendDataBoundItems="true">
                                    <asp:ListItem Text="-- Select Patient --" Value="" />
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-6">
                                <label for="ddlReceptionists" class="form-label">Select Receptionist</label>
                                <asp:DropDownList ID="ddlReceptionists" runat="server" CssClass="form-select" AppendDataBoundItems="true">
                                    <asp:ListItem Text="-- Select Receptionist --" Value="" />
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="row g-3 mb-3">
                            <div class="col-md-4">
                                <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" Placeholder="Description" />
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control" TextMode="Number" Placeholder="Quantity" />
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="txtUnitPrice" runat="server" CssClass="form-control" TextMode="Number" Placeholder="Unit Price" />
                            </div>
                            <div class="col-md-2">
                                <asp:Button ID="btnAddItem" runat="server" Text="Add" CssClass="btn btn-success w-100" OnClick="btnAddItem_Click" />
                            </div>
                        </div>

                        <asp:GridView ID="gvBillItems" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped">
                            <Columns>
                                <asp:BoundField DataField="Description" HeaderText="Description" />
                                <asp:BoundField DataField="Quantity" HeaderText="Qty" />
                                <asp:BoundField DataField="UnitPrice" HeaderText="Unit Price ($)" />
                                <asp:BoundField DataField="TotalPrice" HeaderText="Total ($)" />
                            </Columns>
                        </asp:GridView>

                        <asp:Label ID="lblTotalAmount" runat="server" CssClass="total text-success" Text="Total: $0.00" />

                        <div class="d-grid gap-2 mt-4">
                            <asp:Button ID="btnGenerateInvoice" runat="server" Text="Generate Invoice" CssClass="btn btn-primary" OnClick="btnGenerateInvoice_Click" />
                            <asp:Button ID="btnGeneratePDF" runat="server" Text="Download Invoice as PDF" CssClass="btn btn-secondary" OnClick="btnGeneratePDF_Click" Visible="false" />
                            <asp:Button ID="btnGoHome" runat="server" Text="Back to Home" CssClass="btn btn-outline-dark" OnClick="btnGoHome_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
