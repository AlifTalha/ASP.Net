<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Doctor.aspx.cs" Inherits="asp.netloginpage.Doctor" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>Doctors</title>

    <!-- Bootstrap CSS CDN -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" />

    <!-- Font Awesome CDN -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0/css/all.min.css" />

    <style>
        body {
            display: flex;
            min-height: 100vh;
            margin: 0;
            font-family: Arial, sans-serif;
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

        .content-area {
            flex-grow: 1;
            padding: 30px;
            background-color: #f8f9fa;
        }

        .form-control {
            margin-bottom: 15px;
        }
    </style>
</head>
<body>
    <!-- Sidebar -->
    <div class="sidebar">
        <div class="sidebar-brand">
            <div class="brand-logo">
                <i class="fas fa-hospital"></i>
            </div>
            <span class="brand-text">TALHA's Hospital</span>
        </div>

        <div class="sidebar-user">
            <img src="https://ui-avatars.com/api/?name=<%= Session["FirstName"] %>+<%= Session["LastName"] %>&background=5d87ff&color=fff"
                 class="user-avatar" />
            <h5 class="user-name">
                <%= Session["FirstName"] %> <%= Session["LastName"] %>
            </h5>
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
            <a href="Doctor.aspx" class="menu-item active">
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
    <form id="form1" runat="server" class="content-area">
        <div class="container-fluid">
            <h2 class="text-primary mb-4">Manage Doctors</h2>

            <div class="form-group">
                <asp:TextBox ID="txtName" runat="server" CssClass="form-control" Placeholder="Name" />
            </div>
            <div class="form-group">
                <asp:TextBox ID="txtSpec" runat="server" CssClass="form-control" Placeholder="Specialization" />
            </div>
            <div class="form-group">
                <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control" Placeholder="Phone" />
            </div>
            <div class="form-group">
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Placeholder="Email" />
            </div>
            <div class="form-group">
                <asp:TextBox ID="txtSchedule" runat="server" CssClass="form-control" Placeholder="Availability Schedule (e.g. Mon-Fri 9AM-2PM)" />
            </div>
            <div class="form-group">
                <asp:TextBox ID="txtNotes" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" Placeholder="Consultation Notes" />
            </div>
            <asp:Button ID="btnAdd" runat="server" Text="Save Doctor" CssClass="btn btn-success" OnClick="btnAdd_Click" />
            <asp:Button ID="btnBack" runat="server" Text="Back to Home" CssClass="btn btn-secondary ml-2" PostBackUrl="~/Default.aspx" />

            <hr />

            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                CssClass="table table-bordered table-striped mt-3" DataKeyNames="DoctorID"
                OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing"
                OnRowUpdating="GridView1_RowUpdating" OnRowCancelingEdit="GridView1_RowCancelingEdit">
                <Columns>
                    <asp:BoundField DataField="DoctorID" HeaderText="ID" ReadOnly="True" />
                    <asp:BoundField DataField="Name" HeaderText="Name" />
                    <asp:BoundField DataField="Specialization" HeaderText="Specialization" />
                    <asp:BoundField DataField="Phone" HeaderText="Phone" />
                    <asp:BoundField DataField="Email" HeaderText="Email" />
                    <asp:BoundField DataField="Schedule" HeaderText="Schedule" />
                    <asp:BoundField DataField="Notes" HeaderText="Consultation Notes" />
                    <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
