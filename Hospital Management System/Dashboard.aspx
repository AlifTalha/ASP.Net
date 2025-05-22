<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="asp.netloginpage.Dashboard" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Dashboard - TALHA's Hospital</title>

    <!-- Bootstrap CSS -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />

    <!-- Font Awesome -->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css" rel="stylesheet" />

    <!-- Chart.js -->
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>

    <style>
        body, html {
            height: 100%;
            margin: 0;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        }

        .wrapper {
            display: flex;
            height: 100vh;
            background-color: #f5f7fa;
        }

        .sidebar {
            width: 250px;
            background-color: #343a40;
            color: #fff;
            flex-shrink: 0;
            display: flex;
            flex-direction: column;
            padding: 20px;
            box-shadow: 2px 0 10px rgba(0,0,0,0.1);
        }

        .sidebar-brand {
            display: flex;
            align-items: center;
            margin-bottom: 20px;
            padding-bottom: 20px;
            border-bottom: 1px solid #495057;
        }

        .brand-logo {
            font-size: 24px;
            margin-right: 10px;
            color: #4e73df;
        }

        .brand-text {
            font-size: 20px;
            font-weight: bold;
            color: #fff;
        }

        .sidebar-user {
            text-align: center;
            margin-bottom: 30px;
            padding-bottom: 20px;
            border-bottom: 1px solid #495057;
        }

        .user-avatar {
            width: 80px;
            height: 80px;
            border-radius: 50%;
            margin-bottom: 10px;
            border: 3px solid #4e73df;
        }

        .user-name {
            margin: 0;
            font-weight: 600;
        }

        .user-role {
            font-size: 14px;
            color: #adb5bd;
            background-color: #4e73df;
            padding: 3px 10px;
            border-radius: 20px;
            display: inline-block;
            margin-top: 5px;
        }

        .sidebar-menu {
            flex-grow: 1;
            overflow-y: auto;
        }

        .menu-title {
            font-size: 12px;
            text-transform: uppercase;
            margin: 20px 0 10px;
            color: #adb5bd;
            letter-spacing: 1px;
            font-weight: 600;
        }

        .menu-item {
            display: flex;
            align-items: center;
            padding: 12px 15px;
            color: #dee2e6;
            text-decoration: none;
            border-radius: 4px;
            transition: all 0.3s;
            margin-bottom: 5px;
        }

        .menu-item:hover,
        .menu-item.active {
            background-color: #4e73df;
            color: white;
        }

        .menu-icon {
            width: 25px;
            font-size: 16px;
            text-align: center;
        }

        .menu-text {
            margin-left: 10px;
            font-size: 14px;
        }

        .main-content {
            flex-grow: 1;
            padding: 30px;
            overflow-y: auto;
            background-color: #f8f9fc;
        }

        .card {
            border: none;
            border-radius: 10px;
            box-shadow: 0 0.15rem 1.75rem 0 rgba(58, 59, 69, 0.15);
            transition: transform 0.3s ease;
            margin-bottom: 20px;
            background-color: #fff;
        }

        .card:hover {
            transform: translateY(-5px);
            box-shadow: 0 0.5rem 1.5rem 0 rgba(58, 59, 69, 0.2);
        }

        .card-header {
            background-color: #f8f9fc;
            border-bottom: 1px solid #e3e6f0;
            padding: 1rem 1.35rem;
            border-radius: 10px 10px 0 0 !important;
            font-weight: 600;
            color: #4e73df;
        }

        .card-body {
            padding: 1.35rem;
        }

        .logout-btn {
            background-color: #e74a3b;
            color: white;
            border: none;
            padding: 10px;
            border-radius: 4px;
            margin-top: 20px;
            font-weight: bold;
            width: 100%;
            transition: all 0.3s;
        }

        .logout-btn:hover {
            background-color: #be2617;
        }

        /* Dashboard specific styles */
        .dashboard-title {
            color: #5a5c69;
            font-weight: 600;
            margin-bottom: 1.5rem;
        }

        .activity-overview {
            margin-bottom: 2rem;
        }

        .activity-item {
            display: flex;
            align-items: center;
            margin-bottom: 15px;
            padding: 15px;
            background-color: #fff;
            border-radius: 8px;
            box-shadow: 0 0.125rem 0.25rem rgba(0, 0, 0, 0.075);
        }

        .activity-icon {
            width: 40px;
            height: 40px;
            border-radius: 50%;
            display: flex;
            align-items: center;
            justify-content: center;
            margin-right: 15px;
            color: white;
            font-size: 18px;
        }

        .appointments-icon {
            background-color: #4e73df;
        }

        .patients-icon {
            background-color: #1cc88a;
        }

        .activity-text {
            flex-grow: 1;
        }

        .activity-number {
            font-size: 24px;
            font-weight: 600;
            color: #5a5c69;
        }

        .activity-label {
            font-size: 14px;
            color: #858796;
        }

        .hotspot-survey {
            background-color: #fff;
            padding: 20px;
            border-radius: 8px;
            box-shadow: 0 0.125rem 0.25rem rgba(0, 0, 0, 0.075);
        }

        .hotspot-title {
            font-size: 18px;
            font-weight: 600;
            margin-bottom: 20px;
            color: #5a5c69;
        }

        .hotspot-years {
            display: flex;
            flex-wrap: wrap;
            gap: 10px;
        }

        .year-badge {
            padding: 8px 12px;
            background-color: #f8f9fc;
            border-radius: 20px;
            font-size: 14px;
            color: #5a5c69;
            transition: all 0.3s;
        }

        .year-badge:hover {
            background-color: #4e73df;
            color: white;
            cursor: pointer;
        }

        .year-badge.active {
            background-color: #4e73df;
            color: white;
        }

        .chart-container {
            position: relative;
            height: 300px;
            margin-top: 30px;
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
                    <a href="Dashboard.aspx" class="menu-item active">
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

                    <asp:Button ID="btnLogout" runat="server" Text="Logout" CssClass="logout-btn" OnClick="btnLogout_Click" />
                </div>
            </div>

            <!-- Main Content -->
            <div class="main-content">
                <h1 class="dashboard-title">Dashboard</h1>
                
                <div class="row">
                    <div class="col-md-8">
                        <!-- Activity Overview Section -->
                        <div class="card activity-overview">
                            <div class="card-header">
                                <i class="fas fa-chart-line mr-2"></i>Activity Overview
                            </div>
                            <div class="card-body">
                                <div class="activity-item">
                                    <div class="activity-icon appointments-icon">
                                        <i class="fas fa-calendar"></i>
                                    </div>
                                    <div class="activity-text">
                                        <div class="activity-number">500</div>
                                        <div class="activity-label">Appointments</div>
                                    </div>
                                </div>
                                
                                <div class="activity-item">
                                    <div class="activity-icon patients-icon">
                                        <i class="fas fa-user-plus"></i>
                                    </div>
                                    <div class="activity-text">
                                        <div class="activity-number">150</div>
                                        <div class="activity-label">New Patients</div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        
                        <!-- Hotspot Survey Section -->
                        <div class="card">
                            <div class="card-header">
                                <i class="fas fa-search mr-2"></i>Hotspot Survey
                            </div>
                            <div class="card-body">
                                <div class="hotspot-years">
                                    <span class="year-badge">2004</span>
                                    <span class="year-badge">2005</span>
                                    <span class="year-badge">2006</span>
                                    <span class="year-badge">2007</span>
                                    <span class="year-badge">2008</span>
                                    <span class="year-badge">2010</span>
                                    <span class="year-badge">2011</span>
                                    <span class="year-badge">2012</span>
                                    <span class="year-badge">2013</span>
                                    <span class="year-badge">2014</span>
                                    <span class="year-badge">2015</span>
                                    <span class="year-badge">2016</span>
                                    <span class="year-badge">2017</span>
                                    <span class="year-badge">2018</span>
                                    <span class="year-badge">2020</span>
                                    <span class="year-badge">2021</span>
                                    <span class="year-badge">2022</span>
                                    <span class="year-badge">2023</span>
                                    <span class="year-badge">2024</span>
                                    <span class="year-badge">2025</span>
                                </div>
                                
                                <div class="chart-container">
                                    <canvas id="hotspotChart"></canvas>
                                </div>
                            </div>
                        </div>
                    </div>
                    
                    <div class="col-md-4">
                        <!-- Quick Stats Section -->
                        <div class="card">
                            <div class="card-header">
                                <i class="fas fa-chart-pie mr-2"></i>Quick Stats
                            </div>
                            <div class="card-body">
                                <div class="mb-4">
                                    <h6 class="text-primary">Total Doctors</h6>
                                    <h3 class="font-weight-bold">24</h3>
                                </div>
                                
                                <div class="mb-4">
                                    <h6 class="text-success">Total Patients</h6>
                                    <h3 class="font-weight-bold">150</h3>
                                </div>
                                
                                <div class="mb-4">
                                    <h6 class="text-info">Today's Appointments</h6>
                                    <h3 class="font-weight-bold">15</h3>
                                </div>
                                
                                <div>
                                    <h6 class="text-warning">Pending Bills</h6>
                                    <h3 class="font-weight-bold">8</h3>
                                </div>
                            </div>
                        </div>
                        
                        <!-- Recent Activity Section -->
                        <div class="card">
                            <div class="card-header">
                                <i class="fas fa-history mr-2"></i>Recent Activity
                            </div>
                            <div class="card-body">
                                <div class="mb-3">
                                    <div class="d-flex justify-content-between">
                                        <span class="font-weight-bold">New Patient Registration</span>
                                        <small class="text-muted">10 min ago</small>
                                    </div>
                                    <small>John Doe registered as new patient</small>
                                </div>
                                
                                <div class="mb-3">
                                    <div class="d-flex justify-content-between">
                                        <span class="font-weight-bold">Appointment Scheduled</span>
                                        <small class="text-muted">30 min ago</small>
                                    </div>
                                    <small>Jane Smith scheduled an appointment</small>
                                </div>
                                
                                <div class="mb-3">
                                    <div class="d-flex justify-content-between">
                                        <span class="font-weight-bold">Prescription Created</span>
                                        <small class="text-muted">1 hour ago</small>
                                    </div>
                                    <small>Dr. Williams created a prescription</small>
                                </div>
                                
                                <div class="mb-3">
                                    <div class="d-flex justify-content-between">
                                        <span class="font-weight-bold">Payment Received</span>
                                        <small class="text-muted">2 hours ago</small>
                                    </div>
                                    <small>Payment of $150 received from Robert Johnson</small>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <script>
            // Initialize hotspot chart
            document.addEventListener('DOMContentLoaded', function() {
                const ctx = document.getElementById('hotspotChart').getContext('2d');
                const hotspotChart = new Chart(ctx, {
                    type: 'line',
                    data: {
                        labels: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
                        datasets: [{
                            label: 'Patient Visits',
                            data: [65, 59, 80, 81, 56, 55, 40, 72, 88, 76, 65, 90],
                            backgroundColor: 'rgba(78, 115, 223, 0.05)',
                            borderColor: 'rgba(78, 115, 223, 1)',
                            pointBackgroundColor: 'rgba(78, 115, 223, 1)',
                            pointBorderColor: '#fff',
                            pointHoverBackgroundColor: '#fff',
                            pointHoverBorderColor: 'rgba(78, 115, 223, 1)',
                            borderWidth: 2,
                            tension: 0.3,
                            fill: true
                        }]
                    },
                    options: {
                        maintainAspectRatio: false,
                        plugins: {
                            legend: {
                                display: false
                            }
                        },
                        scales: {
                            y: {
                                beginAtZero: true,
                                grid: {
                                    color: 'rgba(0, 0, 0, 0.05)',
                                    drawBorder: false
                                },
                                ticks: {
                                    maxTicksLimit: 5
                                }
                            },
                            x: {
                                grid: {
                                    display: false,
                                    drawBorder: false
                                }
                            }
                        }
                    }
                });

                // Make year badges clickable
                document.querySelectorAll('.year-badge').forEach(badge => {
                    badge.addEventListener('click', function() {
                        document.querySelectorAll('.year-badge').forEach(b => b.classList.remove('active'));
                        this.classList.add('active');
                        
                        // Here you would typically update the chart data based on selected year
                        // For demo purposes, we're just changing the chart data randomly
                        const newData = Array.from({length: 12}, () => Math.floor(Math.random() * 100));
                        hotspotChart.data.datasets[0].data = newData;
                        hotspotChart.update();
                    });
                });

                // Set current year as active by default
                document.querySelector('.year-badge:contains("2025")').classList.add('active');
            });
        </script>
    </form>
</body>
</html>