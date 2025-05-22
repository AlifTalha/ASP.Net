<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Settings.aspx.cs" Inherits="HospitalManagementSystem.Settings" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Account Settings - Hospital Management</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.10.0/font/bootstrap-icons.css" />
    <style>
        :root {
            --primary-color: #4e73df;
            --secondary-color: #f8f9fc;
            --accent-color: #2e59d9;
            --text-color: #5a5c69;
        }
        
        body {
            background-color: #f8f9fc;
            color: var(--text-color);
            font-family: 'Nunito', -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, 'Helvetica Neue', Arial, sans-serif;
        }
        
        .settings-card {
            border: none;
            border-radius: 15px;
            box-shadow: 0 0.15rem 1.75rem 0 rgba(58, 59, 69, 0.15);
            transition: all 0.3s ease;
        }
        
        .settings-card:hover {
            box-shadow: 0 0.5rem 2rem 0 rgba(58, 59, 69, 0.2);
            transform: translateY(-2px);
        }
        
        .card-header {
            border-radius: 15px 15px 0 0 !important;
            background: linear-gradient(135deg, var(--primary-color) 0%, var(--accent-color) 100%);
            border-bottom: none;
        }
        
        .form-control, .form-select {
            border-radius: 10px;
            padding: 10px 15px;
            border: 1px solid #d1d3e2;
            transition: all 0.3s;
        }
        
        .form-control:focus, .form-select:focus {
            border-color: var(--primary-color);
            box-shadow: 0 0 0 0.25rem rgba(78, 115, 223, 0.25);
        }
        
        .btn-update {
            background: linear-gradient(135deg, var(--primary-color) 0%, var(--accent-color) 100%);
            border: none;
            border-radius: 10px;
            padding: 10px 25px;
            font-weight: 600;
            transition: all 0.3s;
        }
        
        .btn-update:hover {
            transform: translateY(-2px);
            box-shadow: 0 0.5rem 1rem rgba(78, 115, 223, 0.3);
        }
        
        .profile-section {
            text-align: center;
            margin-bottom: 30px;
        }
        
        .profile-icon {
            width: 100px;
            height: 100px;
            background-color: var(--primary-color);
            border-radius: 50%;
            display: flex;
            align-items: center;
            justify-content: center;
            margin: 0 auto 15px;
            color: white;
            font-size: 40px;
        }
        
        .password-toggle {
            position: absolute;
            right: 15px;
            top: 50%;
            transform: translateY(-50%);
            cursor: pointer;
            color: #6c757d;
        }
        
        .password-container {
            position: relative;
        }
        
        .section-title {
            color: var(--primary-color);
            font-weight: 600;
            margin-bottom: 20px;
            position: relative;
            padding-bottom: 10px;
        }
        
        .section-title:after {
            content: '';
            position: absolute;
            left: 0;
            bottom: 0;
            width: 50px;
            height: 3px;
            background: linear-gradient(135deg, var(--primary-color) 0%, var(--accent-color) 100%);
            border-radius: 3px;
        }
        
        .floating-label {
            position: relative;
            margin-bottom: 1.5rem;
        }
        
        .floating-label .form-control {
            height: calc(3.5rem + 2px);
            padding: 1rem 0.75rem;
        }
        
        .floating-label label {
            position: absolute;
            top: 0;
            left: 0;
            height: 100%;
            padding: 1rem 0.75rem;
            pointer-events: none;
            border: 1px solid transparent;
            transform-origin: 0 0;
            transition: opacity .1s ease-in-out, transform .1s ease-in-out;
            color: #6c757d;
        }
        
        .floating-label .form-control:focus ~ label,
        .floating-label .form-control:not(:placeholder-shown) ~ label {
            transform: scale(.85) translateY(-.75rem) translateX(.15rem);
            opacity: .65;
            background-color: white;
            padding: 0 5px;
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

 .form-control {
     margin-bottom: 15px;
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
             <a href="Patient.aspx" class="menu-item ">
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
             <a href="Settings.aspx" class="menu-item active">
                 <div class="menu-icon"><i class="fas fa-cog"></i></div>
                 <span class="menu-text">Settings</span>
             </a>
         </div>
     </div>
        <div class="container py-5">
            <div class="row justify-content-center">
                <div class="col-lg-8">
                    <div class="settings-card card mb-4">
                        <div class="card-header py-3 d-flex justify-content-between align-items-center">
                            <h4 class="m-0 text-white"><i class="bi bi-gear-fill me-2"></i>Account Settings</h4>
                            <asp:Label ID="lblRoleBadge" runat="server" CssClass="badge bg-light text-dark"></asp:Label>
                        </div>
                        <div class="card-body p-4">
                            <!-- Profile Section -->
                            <div class="profile-section">
                                <div class="profile-icon">
                                    <i class="bi bi-person-fill"></i>
                                </div>
                                <h5 class="fw-bold mb-1" id="displayName" runat="server"></h5>
                                <p class="text-muted mb-4" id="displayEmail" runat="server"></p>
                            </div>
                            
                            <!-- Message Alert -->
                           <div id="messageAlert" runat="server" class="alert alert-dismissible fade" visible="false" role="alert">
                                <asp:Label ID="lblMessage" runat="server"></asp:Label>
                                <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                            </div>
                            
                            <!-- Personal Information Section -->
                            <h5 class="section-title">Personal Information</h5>
                            <div class="row g-3">
                                <div class="col-md-6">
                                    <div class="floating-label">
                                        <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" placeholder=" " />
                                        <label for="txtFirstName">First Name</label>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="floating-label">
                                        <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" placeholder=" " />
                                        <label for="txtLastName">Last Name</label>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="floating-label">
                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" placeholder=" " />
                                        <label for="txtEmail">Email Address</label>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="floating-label">
                                        <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" placeholder=" " />
                                        <label for="txtUsername">Username</label>
                                    </div>
                                </div>
                            </div>
                            
                            <!-- Password Section -->
                            <h5 class="section-title mt-5">Change Password</h5>
                            <div class="row g-3">
                                <div class="col-md-6">
                                    <div class="floating-label password-container">
                                        <asp:TextBox ID="txtOldPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder=" " />
                                        <label for="txtOldPassword">Current Password</label>
                                        <i class="bi bi-eye-slash password-toggle" onclick="togglePassword('txtOldPassword', this)"></i>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="floating-label password-container">
                                        <asp:TextBox ID="txtNewPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder=" " />
                                        <label for="txtNewPassword">New Password</label>
                                        <i class="bi bi-eye-slash password-toggle" onclick="togglePassword('txtNewPassword', this)"></i>
                                    </div>
                                </div>
                            </div>
                            
                            <!-- Role Section -->
                            <h5 class="section-title mt-5">Account Type</h5>
                            <div class="mb-4">
                                <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-select">
                                    <asp:ListItem Value="Admin">Administrator</asp:ListItem>
                                    <asp:ListItem Value="Doctor">Doctor</asp:ListItem>
                                    <asp:ListItem Value="Receptionist">Receptionist</asp:ListItem>
                                    <asp:ListItem Value="Patient">Patient</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            
                            <!-- Action Buttons -->
                            <div class="d-flex justify-content-between mt-5">
                                <a href="Default.aspx" class="btn btn-outline-secondary">
                                    <i class="bi bi-arrow-left me-2"></i>Back to Dashboard
                                </a>
                                <asp:Button ID="btnUpdate" runat="server" Text="Save Changes" 
                                    CssClass="btn btn-update text-white" OnClick="btnUpdate_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
    <script>
        // Toggle password visibility
        function togglePassword(inputId, icon) {
            const input = document.getElementById(inputId);
            if (input.type === "password") {
                input.type = "text";
                icon.classList.remove("bi-eye-slash");
                icon.classList.add("bi-eye");
            } else {
                input.type = "password";
                icon.classList.remove("bi-eye");
                icon.classList.add("bi-eye-slash");
            }
        }

        // Auto-dismiss alert after 5 seconds
        window.addEventListener('DOMContentLoaded', (event) => {
            const alert = document.querySelector('.alert');
            if (alert) {
                setTimeout(() => {
                    const bsAlert = new bootstrap.Alert(alert);
                    bsAlert.close();
                }, 5000);
            }
        });
    </script>
</body>
</html>