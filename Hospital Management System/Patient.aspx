<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Patient.aspx.cs" Inherits="asp.netloginpage.Patient" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Manage Patients</title>
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css" rel="stylesheet" />
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

        .form-control {
            margin-bottom: 15px;
        }
    </style>
</head>
<body>
    <div class="wrapper">
        <!-- Sidebar -->
        <div class="sidebar">
            <div class="sidebar-brand">
                <div class="brand-logo">
                    <i class="fas fa-hospital"></i>
                </div>
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
                <a href="Default.aspx" class="menu-item">
                    <div class="menu-icon"><i class="fas fa-tachometer-alt"></i></div>
                    <span class="menu-text">Dashboard</span>
                </a>
                <a href="Patient.aspx" class="menu-item active">
                    <div class="menu-icon"><i class="fas fa-user-injured"></i></div>
                    <span class="menu-text">Patients</span>
                </a>
                <a href="Doctor.aspx" class="menu-item">
                    <div class="menu-icon"><i class="fas fa-user-md"></i></div>
                    <span class="menu-text">Doctors</span>
                </a>
                <a href="Appointment.aspx" class="menu-item">
                    <div class="menu-icon"><i class="fas fa-calendar-check"></i></div>
                    <span class="menu-text">Appointments</span>
                </a>
                  <a href="Receptionist.aspx" class="menu-item">
      <div class="menu-icon"><i class="fas fa-calendar-check"></i></div>
      <span class="menu-text">Receptionist</span>
  </a>
                <a href="Billing.aspx" class="menu-item">
                    <div class="menu-icon"><i class="fas fa-file-invoice-dollar"></i></div>
                    <span class="menu-text">Billing</span>
                </a>

                <div class="menu-title">Management</div>
                <a href="Inventory.aspx" class="menu-item">
                    <div class="menu-icon"><i class="fas fa-pills"></i></div>
                    <span class="menu-text">Inventory</span>
                </a>
                <a href="HelpDesk.aspx" class="menu-item">
                    <div class="menu-icon"><i class="fas fa-chart-bar"></i></div>
                    <span class="menu-text">Reports</span>
                </a>
                <a href="Settings.aspx" class="menu-item">
                    <div class="menu-icon"><i class="fas fa-cog"></i></div>
                    <span class="menu-text">Settings</span>
                </a>
            </div>
        </div>

        <!-- Main Content Area -->
        <form id="form1" runat="server" class="main-content">
            <div class="container-fluid">
                <h2 class="text-primary">Patient Registration</h2>
                <div class="row">
                    <div class="col-md-6">
                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control" Placeholder="Full Name" />
                        <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-control">
                            <asp:ListItem Text="Select Gender" Value="" />
                            <asp:ListItem Text="Male" />
                            <asp:ListItem Text="Female" />
                            <asp:ListItem Text="Others" />
                        </asp:DropDownList>
                        <asp:TextBox ID="txtAge" runat="server" CssClass="form-control" Placeholder="Age" />
                        <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control" Placeholder="Phone" />
                        <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" Placeholder="Address" />
                    </div>
                    <div class="col-md-6">
                        <asp:DropDownList ID="txtBloodGroup" runat="server" CssClass="form-control">
                            <asp:ListItem Text="Select Blood Group" Value="" />
                            <asp:ListItem Text="O+" />
                            <asp:ListItem Text="O-" />
                            <asp:ListItem Text="A+" />
                            <asp:ListItem Text="A-" />
                            <asp:ListItem Text="B+" />
                            <asp:ListItem Text="B-" />
                            <asp:ListItem Text="AB+" />
                            <asp:ListItem Text="AB-" />
                        </asp:DropDownList>
                        <asp:TextBox ID="txtMaritalStatus" runat="server" CssClass="form-control" Placeholder="Marital Status" />
                        <asp:TextBox ID="txtNationalID" runat="server" CssClass="form-control" Placeholder="National ID" />
                        <asp:TextBox ID="txtMedicalCondition" runat="server" CssClass="form-control" Placeholder="Condition" />
                        <asp:TextBox ID="txtTreatment" runat="server" CssClass="form-control" Placeholder="Treatment" />
                        <asp:TextBox ID="txtNotes" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2" Placeholder="Medical Notes" />
                        <div class="mt-2">
                            <asp:Button ID="btnAdd" runat="server" Text="Register Patient" CssClass="btn btn-success" OnClick="btnAdd_Click" />
                            <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-warning ml-2" OnClick="btnUpdate_Click" Visible="false" />
                        </div>
                    </div>
                </div>

                <hr />
                <h3>Search Patients</h3>
                <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control mb-2" Placeholder="Search by Name or Phone" />
                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
                <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-secondary ml-2" OnClick="btnClear_Click" />

                <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered mt-4" AutoGenerateColumns="False"
                    OnRowDeleting="GridView1_RowDeleting" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" DataKeyNames="PatientID">
                    <Columns>
                        <asp:BoundField DataField="PatientID" HeaderText="ID" />
                        <asp:BoundField DataField="Name" HeaderText="Name" />
                        <asp:BoundField DataField="Gender" HeaderText="Gender" />
                        <asp:BoundField DataField="Phone" HeaderText="Phone" />
                        <asp:CommandField ShowDeleteButton="true" ShowSelectButton="true" />
                    </Columns>
                </asp:GridView>

                <asp:HyperLink ID="btnBack" runat="server" NavigateUrl="Default.aspx" CssClass="btn btn-dark mt-3">← Back to Dashboard</asp:HyperLink>
            </div>
        </form>
    </div>
</body>
</html>
