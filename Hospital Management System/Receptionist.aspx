<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Receptionist.aspx.cs" Inherits="asp.netloginpage.Receptionist" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Manage Receptionists</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@4.5.2/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css" />

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

        .card {
            border: none;
            border-radius: 10px;
            box-shadow: 0 0 15px rgba(0, 0, 0, 0.1);
        }

        .card-header {
            background-color: #343a40;
            color: white;
            border-radius: 10px 10px 0 0;
        }

        .btn-primary, .btn-danger, .btn-success {
            border-radius: 5px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
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
                    <a href="Patient.aspx" class="menu-item">
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
                    <a href="Receptionist.aspx" class="menu-item active">
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

            <!-- Main Content -->
            <div class="main-content">
                <div class="container-fluid">
                    <div class="card">
                        <div class="card-header text-center">
                            <h3>Manage Receptionists</h3>
                        </div>
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-6">
                                    <label for="txtName">Name:</label>
                                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Enter Name" />
                                </div>
                                <div class="col-md-6">
                                    <label for="txtPhone">Phone:</label>
                                    <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control" placeholder="Enter Phone" />
                                </div>
                                <div class="col-md-6">
                                    <label for="txtEmail">Email:</label>
                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Enter Email" />
                                </div>
                                <div class="col-md-12 text-center">
                                    <asp:Button ID="btnAdd" runat="server" Text="Add Receptionist" CssClass="btn btn-success mt-3" OnClick="btnAdd_Click" />
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="card mt-4">
                        <div class="card-header">
                            <h5>Receptionist List</h5>
                        </div>
                        <div class="card-body">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered" OnRowDeleting="GridView1_RowDeleting">
                                <Columns>
                                    <asp:BoundField DataField="ReceptionistID" HeaderText="ID" />
                                    <asp:BoundField DataField="Name" HeaderText="Name" />
                                    <asp:BoundField DataField="Phone" HeaderText="Phone" />
                                    <asp:BoundField DataField="Email" HeaderText="Email" />
                                    <asp:CommandField ShowDeleteButton="True" ButtonType="Button" HeaderText="Actions" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>

                    <div class="text-center mt-4">
                        <asp:Button ID="btnGoHome" runat="server" Text="Go to Home" CssClass="btn btn-primary" OnClick="btnGoHome_Click" />
                    </div>
                </div>
            </div>
        </div>
    </form>

    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.5.2/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
