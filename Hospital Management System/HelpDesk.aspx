<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HelpDesk.aspx.cs" Inherits="HospitalManagementSystem.HelpDesk" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Help Desk - TALHA's Hospital</title>
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

        .helpdesk-container {
            background: white;
            border-radius: 8px;
            box-shadow: 0 0 15px rgba(0, 0, 0, 0.1);
            padding: 25px;
        }

        .helpdesk-header {
            display: flex;
            justify-content: space-between;
            align-items: center;
            margin-bottom: 30px;
            border-bottom: 1px solid #eee;
            padding-bottom: 15px;
        }

        .helpdesk-title {
            font-size: 24px;
            color: #333;
            margin: 0;
        }

        .ticket-card {
            background: white;
            border-radius: 8px;
            box-shadow: 0 2px 10px rgba(0, 0, 0, 0.05);
            padding: 20px;
            margin-bottom: 20px;
            transition: transform 0.3s;
        }

        .ticket-card:hover {
            transform: translateY(-5px);
            box-shadow: 0 5px 15px rgba(0, 0, 0, 0.1);
        }

        .ticket-header {
            display: flex;
            justify-content: space-between;
            margin-bottom: 10px;
        }

        .ticket-title {
            font-weight: bold;
            font-size: 18px;
            color: #333;
        }

        .ticket-status {
            padding: 5px 10px;
            border-radius: 20px;
            font-size: 12px;
            font-weight: bold;
        }

        .status-open {
            background-color: #ffeeba;
            color: #856404;
        }

        .status-in-progress {
            background-color: #bee5eb;
            color: #0c5460;
        }

        .status-resolved {
            background-color: #c3e6cb;
            color: #155724;
        }

        .ticket-meta {
            display: flex;
            justify-content: space-between;
            color: #6c757d;
            font-size: 14px;
            margin-bottom: 15px;
        }

        .ticket-description {
            color: #495057;
            margin-bottom: 15px;
        }

        .btn {
            padding: 8px 15px;
            border-radius: 4px;
            border: none;
            cursor: pointer;
            font-weight: 500;
            transition: all 0.3s;
        }

        .btn-primary {
            background-color: #007bff;
            color: white;
        }

        .btn-primary:hover {
            background-color: #0069d9;
        }

        .btn-success {
            background-color: #28a745;
            color: white;
        }

        .btn-success:hover {
            background-color: #218838;
        }

        .btn-danger {
            background-color: #dc3545;
            color: white;
        }

        .btn-danger:hover {
            background-color: #c82333;
        }

        .search-box {
            display: flex;
            margin-bottom: 20px;
        }

        .search-input {
            flex-grow: 1;
            padding: 10px;
            border: 1px solid #ddd;
            border-radius: 4px 0 0 4px;
            outline: none;
        }

        .search-btn {
            padding: 10px 15px;
            background-color: #007bff;
            color: white;
            border: none;
            border-radius: 0 4px 4px 0;
            cursor: pointer;
        }

        .new-ticket-form {
            background: white;
            padding: 20px;
            border-radius: 8px;
            box-shadow: 0 0 15px rgba(0, 0, 0, 0.1);
            margin-top: 30px;
        }

        .form-group {
            margin-bottom: 20px;
        }

        .form-label {
            display: block;
            margin-bottom: 8px;
            font-weight: 500;
            color: #495057;
        }

        .form-input {
            width: 100%;
            padding: 10px;
            border: 1px solid #ddd;
            border-radius: 4px;
            font-size: 16px;
        }

        .form-textarea {
            width: 100%;
            padding: 10px;
            border: 1px solid #ddd;
            border-radius: 4px;
            min-height: 120px;
            font-size: 16px;
        }

        .priority-high {
            border-left: 4px solid #dc3545;
        }

        .priority-medium {
            border-left: 4px solid #ffc107;
        }

        .priority-low {
            border-left: 4px solid #28a745;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="wrapper">
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
                    <a href="Billing.aspx" class="menu-item">
                        <div class="menu-icon"><i class="fas fa-file-invoice-dollar"></i></div>
                        <span class="menu-text">Billing</span>
                    </a>

                    <div class="menu-title">Management</div>
                    <a href="Inventory.aspx" class="menu-item">
                        <div class="menu-icon"><i class="fas fa-pills"></i></div>
                        <span class="menu-text">Inventory</span>
                    </a>
                    <a href="Reports.aspx" class="menu-item">
                        <div class="menu-icon"><i class="fas fa-chart-bar"></i></div>
                        <span class="menu-text">Reports</span>
                    </a>
                    <a href="HelpDesk.aspx" class="menu-item active">
                        <div class="menu-icon"><i class="fas fa-headset"></i></div>
                        <span class="menu-text">Help Desk</span>
                    </a>
                    <a href="Settings.aspx" class="menu-item">
                        <div class="menu-icon"><i class="fas fa-cog"></i></div>
                        <span class="menu-text">Settings</span>
                    </a>
                </div>
            </div>

            <div class="main-content">
                <div class="helpdesk-container">
                    <div class="helpdesk-header">
                        <h1 class="helpdesk-title">Help Desk</h1>
                        <asp:Button ID="btnNewTicket" runat="server" Text="New Ticket" CssClass="btn btn-primary" OnClick="btnNewTicket_Click" />
                    </div>

                    <div class="search-box">
                        <asp:TextBox ID="txtSearch" runat="server" CssClass="search-input" placeholder="Search tickets..."></asp:TextBox>
                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="search-btn" OnClick="btnSearch_Click" />
                    </div>

                    <asp:Panel ID="pnlTickets" runat="server">
                        <div class="ticket-card priority-high">
                            <div class="ticket-header">
                                <div class="ticket-title">Login Issues</div>
                                <div class="ticket-status status-in-progress">In Progress</div>
                            </div>
                            <div class="ticket-meta">
                                <span><i class="fas fa-user"></i> Submitted by: John Doe</span>
                                <span><i class="fas fa-calendar-alt"></i> Created: 2023-06-15</span>
                            </div>
                            <div class="ticket-description">
                                Unable to login to the system. Getting "Invalid credentials" error even with correct username and password.
                            </div>
                            <div>
                                <asp:Button ID="btnView1" runat="server" Text="View" CssClass="btn btn-primary" />
                                <asp:Button ID="btnResolve1" runat="server" Text="Resolve" CssClass="btn btn-success" />
                            </div>
                        </div>

                        <div class="ticket-card priority-medium">
                            <div class="ticket-header">
                                <div class="ticket-title">Patient Records Not Loading</div>
                                <div class="ticket-status status-open">Open</div>
                            </div>
                            <div class="ticket-meta">
                                <span><i class="fas fa-user"></i> Submitted by: Sarah Smith</span>
                                <span><i class="fas fa-calendar-alt"></i> Created: 2023-06-14</span>
                            </div>
                            <div class="ticket-description">
                                When trying to access patient records, the page keeps loading indefinitely. This happens for all patients.
                            </div>
                            <div>
                                <asp:Button ID="btnView2" runat="server" Text="View" CssClass="btn btn-primary" />
                                <asp:Button ID="btnResolve2" runat="server" Text="Resolve" CssClass="btn btn-success" />
                            </div>
                        </div>

                        <div class="ticket-card priority-low">
                            <div class="ticket-header">
                                <div class="ticket-title">Billing Report Formatting</div>
                                <div class="ticket-status status-resolved">Resolved</div>
                            </div>
                            <div class="ticket-meta">
                                <span><i class="fas fa-user"></i> Submitted by: Michael Johnson</span>
                                <span><i class="fas fa-calendar-alt"></i> Created: 2023-06-10</span>
                            </div>
                            <div class="ticket-description">
                                The billing reports are not properly formatted when printed. Some columns are cut off on the right side.
                            </div>
                            <div>
                                <asp:Button ID="btnView3" runat="server" Text="View" CssClass="btn btn-primary" />
                                <asp:Button ID="btnClose3" runat="server" Text="Close" CssClass="btn btn-danger" />
                            </div>
                        </div>
                    </asp:Panel>

                    <asp:Panel ID="pnlNewTicket" runat="server" Visible="false" CssClass="new-ticket-form">
                        <h2>Create New Ticket</h2>
                        <div class="form-group">
                            <label class="form-label">Subject</label>
                            <asp:TextBox ID="txtSubject" runat="server" CssClass="form-input" placeholder="Brief description of the issue"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label class="form-label">Priority</label>
                            <asp:DropDownList ID="ddlPriority" runat="server" CssClass="form-input">
                                <asp:ListItem Text="Low" Value="Low"></asp:ListItem>
                                <asp:ListItem Text="Medium" Value="Medium" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="High" Value="High"></asp:ListItem>
                                <asp:ListItem Text="Critical" Value="Critical"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label class="form-label">Description</label>
                            <asp:TextBox ID="txtDescription" runat="server" CssClass="form-textarea" TextMode="MultiLine" placeholder="Detailed description of the issue"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label class="form-label">Attachment (Optional)</label>
                            <asp:FileUpload ID="fileAttachment" runat="server" CssClass="form-input" />
                        </div>
                        <div>
                            <asp:Button ID="btnSubmitTicket" runat="server" Text="Submit Ticket" CssClass="btn btn-primary" OnClick="btnSubmitTicket_Click" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger" OnClick="btnCancel_Click" />
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </form>
</body>
</html>