<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HospitalManagementSystem.Default" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Hospital Dashboard</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css2?family=Poppins:wght@300;400;500;600;700&display=swap" rel="stylesheet" />
    <style>
        :root {
            --primary-color: #5d87ff;
            --primary-light: #ecf2ff;
            --secondary-color: #49beff;
            --success-color: #13deb9;
            --warning-color: #ffae1f;
            --error-color: #fa896b;
            --dark-color: #2a3547;
            --light-color: #f5f8fa;
            --sidebar-width: 270px;
            --sidebar-collapsed-width: 80px;
            --transition: all 0.3s ease;
        }
        
        body {
            font-family: 'Poppins', sans-serif;
            background-color: #f5f8fa;
            overflow-x: hidden;
            color: #2a3547;
        }
        
        /* ========== Modern Sidebar ========== */
        .sidebar {
            position: fixed;
            top: 0;
            left: 0;
            width: var(--sidebar-width);
            height: 100vh;
            background: white;
            box-shadow: 0 0 45px 0 rgba(0, 0, 0, 0.12);
            transition: var(--transition);
            z-index: 1000;
            border-right: 1px solid rgba(0, 0, 0, 0.05);
        }
        
        .sidebar-brand {
            display: flex;
            align-items: center;
            justify-content: center;
            height: 80px;
            padding: 0 25px;
            border-bottom: 1px solid rgba(0, 0, 0, 0.05);
        }
        
        .brand-logo {
            width: 40px;
            height: 40px;
            background: var(--primary-color);
            border-radius: 8px;
            display: flex;
            align-items: center;
            justify-content: center;
            color: white;
            font-size: 20px;
            margin-right: 12px;
        }
        
        .brand-text {
            font-size: 20px;
            font-weight: 600;
            color: var(--dark-color);
            transition: var(--transition);
        }
        
        .sidebar-user {
            padding: 25px;
            text-align: center;
            border-bottom: 1px solid rgba(0, 0, 0, 0.05);
        }
        
        .user-avatar {
            width: 70px;
            height: 70px;
            border-radius: 50%;
            object-fit: cover;
            border: 3px solid var(--primary-light);
            margin-bottom: 10px;
        }
        
        .user-name {
            font-weight: 600;
            margin-bottom: 5px;
            color: var(--dark-color);
        }
        
        .user-role {
            font-size: 13px;
            color: #5a6a85;
            background: var(--primary-light);
            padding: 3px 10px;
            border-radius: 20px;
            display: inline-block;
        }
        
        .sidebar-menu {
            padding: 20px 0;
            height: calc(100vh - 230px);
            overflow-y: auto;
        }
        
        .menu-title {
            font-size: 12px;
            text-transform: uppercase;
            color: #5a6a85;
            font-weight: 600;
            letter-spacing: 0.5px;
            padding: 0 25px;
            margin-bottom: 15px;
        }
        
        .menu-item {
            display: flex;
            align-items: center;
            padding: 12px 25px;
            margin: 5px 0;
            color: #5a6a85;
            text-decoration: none;
            border-left: 3px solid transparent;
            transition: var(--transition);
        }
        
        .menu-item:hover, .menu-item.active {
            background: var(--primary-light);
            color: var(--primary-color);
            border-left-color: var(--primary-color);
        }
        
        .menu-item:hover .menu-icon, .menu-item.active .menu-icon {
            color: var(--primary-color);
        }
        
        .menu-icon {
            width: 24px;
            height: 24px;
            display: flex;
            align-items: center;
            justify-content: center;
            margin-right: 12px;
            color: #5a6a85;
            transition: var(--transition);
        }
        
        .menu-text {
            font-weight: 500;
            transition: var(--transition);
        }
        
        .sidebar-footer {
            position: absolute;
            bottom: 0;
            left: 0;
            width: 100%;
            padding: 15px;
            border-top: 1px solid rgba(0, 0, 0, 0.05);
        }
        
        .logout-btn {
            display: flex;
            align-items: center;
            justify-content: center;
            width: 100%;
            padding: 10px;
            background-color:brown;
            border: none;
            border-radius: 6px;
            font-weight: 500;
            transition: var(--transition);
        }
        
        .logout-btn:hover {
            background: var(--primary-color);
            color: white;
        }
        
        /* ========== Main Content ========== */
        .main-content {
            margin-left: var(--sidebar-width);
            min-height: 100vh;
            transition: var(--transition);
            background: #f5f8fa;
            padding: 20px;
        }
        
        .top-bar {
            display: flex;
            justify-content: space-between;
            align-items: center;
            padding: 15px 25px;
            background: white;
            border-radius: 10px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.03);
            margin-bottom: 25px;
        }
        
        .page-title h1 {
            font-size: 24px;
            font-weight: 600;
            margin-bottom: 0;
            color: var(--dark-color);
        }
        
        .page-actions .btn {
            margin-left: 10px;
            border-radius: 6px;
            font-weight: 500;
            padding: 8px 16px;
        }
        
        /* ========== Stats Cards ========== */
        .stats-grid {
            display: grid;
/*            background-color:aquamarine;*/
            grid-template-columns: repeat(auto-fit, minmax(240px, 1fr));
            gap: 20px;
            margin-bottom: 25px;
        }
        .stats-grid:hover{
            background-color:aqua;
        }
        
        .stat-card {
            background: white;
            border-radius: 10px;
            padding: 20px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.03);
            transition: var(--transition);
            border-left: 4px solid;
        }
        
        .stat-card:hover {
            transform: translateY(-5px);
            box-shadow: 0 5px 15px rgba(0, 0, 0, 0.1);
        }
        
        .stat-card.appointments {
            background-color: cornflowerblue;
            border-left-color: var(--primary-color);
        }
        
        .stat-card.patients {
            background-color:darkgrey;
            border-left-color: var(--success-color);
        }
        
        .stat-card.doctors {
            background-color:goldenrod;
            border-left-color: var(--secondary-color);
        }
        
        .stat-card.earnings {
            background-color:teal;
            border-left-color: var(--warning-color);
        }
        
        .stat-title {
            font-size: 14px;
            color: #5a6a85;
            margin-bottom: 10px;
        }
        
        .stat-value {
            font-size: 24px;
            font-weight: 600;
            margin-bottom: 5px;
        }
        
        .stat-icon {
            width: 50px;
            height: 50px;
            border-radius: 8px;
            display: flex;
            align-items: center;
            justify-content: center;
            font-size: 22px;
            float: right;
        }
        
        .stat-card.appointments .stat-icon {
            background: rgba(93, 135, 255, 0.1);
            color: var(--primary-color);
        }
        
        .stat-card.patients .stat-icon {
            background: rgba(19, 222, 185, 0.1);
            color: var(--success-color);
        }
        
        .stat-card.doctors .stat-icon {
            background: rgba(73, 190, 255, 0.1);
            color: var(--secondary-color);
        }
        
        .stat-card.earnings .stat-icon {
            background: rgba(255, 174, 31, 0.1);
            color: var(--warning-color);
        }
        
        /* ========== Charts Section ========== */
        .content-row {
            display: grid;
            grid-template-columns: 2fr 1fr;
            gap: 20px;
            margin-bottom: 20px;
        }
        
        @media (max-width: 1200px) {
            .content-row {
                grid-template-columns: 1fr;
            }
        }
        
        .chart-card, .summary-card {
            background: white;
            border-radius: 10px;
            padding: 20px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.03);
        }
        
        .card-header {
            display: flex;
            background-color:aquamarine;
            justify-content: space-between;
            align-items: center;
            margin-bottom: 20px;
            padding-bottom: 15px;
            border-bottom: 1px solid rgba(0, 0, 0, 0.05);
        }
        
        .card-title {
            font-size: 18px;
            font-weight: 600;
            margin-bottom: 0;
        }
        
        .time-filters {
            display: flex;
            gap: 10px;
        }
        
        .time-filter {
            padding: 5px 15px;
            border-radius: 20px;
            font-size: 13px;
            font-weight: 500;
            background: transparent;
            border: 1px solid rgba(0, 0, 0, 0.1);
            color: #5a6a85;
            cursor: pointer;
            transition: var(--transition);
        }
        
        .time-filter.active, .time-filter:hover {
            background: var(--primary-color);
            color: white;
            border-color: var(--primary-color);
        }
        
        /* ========== Calendar Section ========== */
        .calendar-card {
            background: white;
            border-radius: 10px;
            padding: 20px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.03);
        }
        
        .calendar-header {
            display: flex;
            justify-content: space-between;
            align-items: center;
            margin-bottom: 20px;
        }
        
        .calendar-nav {
            display: flex;
            align-items: center;
            gap: 15px;
        }
        
        .calendar-nav button {
            background: transparent;
            border: none;
            width: 30px;
            height: 30px;
            border-radius: 50%;
            display: flex;
            align-items: center;
            justify-content: center;
            color: #5a6a85;
            transition: var(--transition);
        }
        
        .calendar-nav button:hover {
            background: var(--primary-light);
            color: var(--primary-color);
        }
        
        .calendar-month {
            font-weight: 600;
        }
        
        .calendar-grid {
            width: 100%;
        }
        
        .calendar-grid th {
            background-color:crimson;
            font-weight: 500;
            color: #5a6a85;
            text-align: center;
            padding: 10px 0;
            font-size: 14px;
        }
        
        .calendar-grid td {
            
            text-align: center;
            padding: 12px 0;
            border-radius: 8px;
            transition: var(--transition);
        }
        
        .calendar-grid td:hover {
            background: var(--primary-light);
            color: var(--primary-color);
            cursor: pointer;
        }
        
        .calendar-grid td.active {
            background: var(--primary-color);
            color: white;
        }
        
        .calendar-grid td.other-month {
            color: #ccc;
        }
        .today {
    background-color: #007bff;
    color: white;
    border-radius: 50%;
}

.other-month {
    color: #ccc;
}

    </style>
</head>
<body>
    <form id="form1" runat="server">
        <!-- Modern Sidebar -->
        <div class="sidebar">
            <div class="sidebar-brand">
                <div class="brand-logo">
                    <i class="fas fa-hospital"></i>
                </div>
                <span class="brand-text">TALHA's Hospital</span>
            </div>
            
           <!-- In the sidebar section, update the user-role span -->
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
                <a href="Dashboard.aspx" class="menu-item active">
                    <div class="menu-icon">
                        <i class="fas fa-tachometer-alt"></i>
                    </div>
                    <span class="menu-text">Dashboard</span>
                </a>
                <a href="Patient.aspx" class="menu-item">
                    <div class="menu-icon">
                        <i class="fas fa-user-injured"></i>
                    </div>
                    <span class="menu-text">Patients</span>
                </a>
                <a href="Doctor.aspx" class="menu-item">
                    <div class="menu-icon">
                        <i class="fas fa-user-md"></i>
                    </div>
                    <span class="menu-text">Doctors</span>
                </a>
                <a href="Appointment.aspx" class="menu-item">
                    <div class="menu-icon">
                        <i class="fas fa-calendar-check"></i>
                    </div>
                    <span class="menu-text">Appointments</span>
                </a>
                  <a href="Receptionist.aspx" class="menu-item">
                  <div class="menu-icon"><i class="fas fa-calendar-check"></i></div>
                  <span class="menu-text">Receptionist</span>
              </a>
                <a href="Billing.aspx" class="menu-item">
                    <div class="menu-icon">
                        <i class="fas fa-file-invoice-dollar"></i>
                    </div>
                    <span class="menu-text">Billing</span>
                </a>
                
                <div class="menu-title">Management</div>
                <a href="Inventory.aspx" class="menu-item">
                    <div class="menu-icon">
                        <i class="fas fa-pills"></i>
                    </div>
                    <span class="menu-text">Inventory</span>
                </a>
                <a href="HelpDesk.aspx" class="menu-item">
                    <div class="menu-icon">
                        <i class="fas fa-chart-bar"></i>
                    </div>
                    <span class="menu-text">Reports</span>
                </a>
                <a href="Settings.aspx" class="menu-item">
                    <div class="menu-icon">
                        <i class="fas fa-cog"></i>
                    </div>
                    <span class="menu-text">Settings</span>
                </a>
            </div>
            
            <div class="sidebar-footer">
                <asp:Button ID="btnLogout" runat="server" Text="Logout" CssClass="logout-btn" OnClick="btnLogout_Click" />
            </div>
        </div>
        
        <!-- Main Content -->
        <div class="main-content">
            <div class="top-bar">
                <div class="page-title">
                    <h1>Dashboard Overview</h1>
                </div>
                <div class="page-actions">
                    <button class="btn btn-outline-secondary">
                        <i class="fas fa-share-alt me-1"></i> Share
                    </button>
                    <button class="btn btn-outline-secondary">
                        <i class="fas fa-download me-1"></i> Export
                    </button>
                </div>
            </div>
            
            <!-- Stats Cards -->
            <div class="stats-grid">
                <div class="stat-card appointments">
                    <div class="stat-title" style="color:red;"> <h1> <b> Appointments </b></h1></div>
                    <div class="stat-value">76</div>
                    <div class="stat-icon">
                        <i class="fas fa-calendar"></i>
                    </div>
                </div>
                
                <div class="stat-card patients">
                    <div class="stat-title"style="color:red;"><h1><b>Total Patients </b></h1></div>
                    <div class="stat-value">124,551</div>
                    <div class="stat-icon">
                        <i class="fas fa-user-injured"></i>
                    </div>
                </div>
                
                <div class="stat-card doctors">
                    <div class="stat-title" style="color:red;"><h1><b>Total Doctors </b></h1></div>
                    <div class="stat-value">442</div>
                    <div class="stat-icon">
                        <i class="fas fa-user-md"></i>
                    </div>
                </div>
                
                <div class="stat-card earnings">
                    <div class="stat-title" style="color:red;"> <h1><b> Hospital Earnings </b> </h1></div>
                    <div class="stat-value">$5,034</div>
                    <div class="stat-icon">
                        <i class="fas fa-dollar-sign"></i>
                    </div>
                </div>
            </div>
            
            <!-- Charts Row -->
            <div class="content-row">
                <div class="chart-card">
                    <div class="card-header">
                        <h3 class="card-title">Patient Percentage</h3>
                        <div class="time-filters">
                            <button class="time-filter active">Daily</button>
                            <button class="time-filter">Weekly</button>
                            <button class="time-filter">Monthly</button>
                        </div>
                    </div>
                    <div class="chart-container">
                        <canvas id="myAreaChart" height="300"></canvas>
                    </div>
                </div>
                
                <div class="summary-card">
                    <div class="card-header">
                        <h3 class="card-title">Total Patients</h3>
                    </div>
                    <div class="text-center py-4">
                        <div class="display-4 fw-bold">562,084</div>
                        <div class="mt-3">
                            <span class="text-success">
                                <i class="fas fa-caret-up"></i> 12%
                            </span>
                            <span class="text-muted">Since last month</span>
                        </div>
                    </div>
                </div>
            </div>



            
            <!-- Calendar Section -->
           <div class="calendar-card">
    <div class="calendar-header">
        <h3 class="card-title">Appointment Schedule</h3>
        <div class="calendar-nav">
            <button id="prevMonth"><i class="fas fa-chevron-left"></i></button>
            <span class="calendar-month" id="monthYear"></span>
            <button id="nextMonth"><i class="fas fa-chevron-right"></i></button>
        </div>
    </div>

    <table class="calendar-grid">
        <thead>
            <tr>
                <th>Sun</th><th>Mon</th><th>Tue</th><th>Wed</th><th>Thu</th><th>Fri</th><th>Sat</th>
            </tr>
        </thead>
        <tbody id="calendarBody">
            <!-- Calendar rows will be inserted here -->
        </tbody>
    </table>
</div>

<script>
    const calendarBody = document.getElementById("calendarBody");
    const monthYear = document.getElementById("monthYear");
    const prevMonth = document.getElementById("prevMonth");
    const nextMonth = document.getElementById("nextMonth");

    let today = new Date();
    let currentMonth = today.getMonth();
    let currentYear = today.getFullYear();

    const monthNames = [
        "January", "February", "March", "April", "May", "June",
        "July", "August", "September", "October", "November", "December"
    ];

    function generateCalendar(month, year) {
        calendarBody.innerHTML = ""; // Clear previous cells
        monthYear.innerText = `${monthNames[month]} ${year}`;

        const firstDay = new Date(year, month, 1).getDay();
        const daysInMonth = new Date(year, month + 1, 0).getDate();
        const daysInPrevMonth = new Date(year, month, 0).getDate();

        let date = 1;
        let nextMonthDate = 1;

        for (let i = 0; i < 6; i++) {
            let row = document.createElement("tr");

            for (let j = 0; j < 7; j++) {
                let cell = document.createElement("td");

                if (i === 0 && j < firstDay) {
                    // Previous month's dates
                    cell.classList.add("other-month");
                    cell.innerText = daysInPrevMonth - firstDay + j + 1;
                } else if (date > daysInMonth) {
                    // Next month's dates
                    cell.classList.add("other-month");
                    cell.innerText = nextMonthDate++;
                } else {
                    cell.innerText = date;
                    // Highlight today
                    if (
                        date === today.getDate() &&
                        month === today.getMonth() &&
                        year === today.getFullYear()
                    ) {
                        cell.classList.add("today");
                    }
                    date++;
                }

                row.appendChild(cell);
            }

            calendarBody.appendChild(row);
            if (date > daysInMonth && nextMonthDate > 7) break;
        }
    }

    prevMonth.addEventListener("click", () => {
        currentMonth--;
        if (currentMonth < 0) {
            currentMonth = 11;
            currentYear--;
        }
        generateCalendar(currentMonth, currentYear);
    });

    nextMonth.addEventListener("click", () => {
        currentMonth++;
        if (currentMonth > 11) {
            currentMonth = 0;
            currentYear++;
        }
        generateCalendar(currentMonth, currentYear);
    });

    generateCalendar(currentMonth, currentYear);
</script>

        
        <!-- JavaScript Libraries -->
        <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
        <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/js/all.min.js"></script>
        <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
        
        <script>
            // Area Chart
            var ctx = document.getElementById("myAreaChart").getContext('2d');
            var myChart = new Chart(ctx, {
                type: 'line',
                data: {
                    labels: ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"],
                    datasets: [{
                        label: "Patients",
                        data: [5000, 6200, 7000, 8500, 9200, 10500, 12000, 11500, 13000, 14500, 15000, 16500],
                        backgroundColor: 'rgba(93, 135, 255, 0.1)',
                        borderColor: 'rgba(93, 135, 255, 1)',
                        borderWidth: 2,
                        pointBackgroundColor: 'rgba(93, 135, 255, 1)',
                        pointBorderColor: '#fff',
                        pointRadius: 4,
                        pointHoverRadius: 6,
                        tension: 0.3,
                        fill: true
                    }]
                },
                options: {
                    responsive: true,
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
                                drawBorder: false,
                                color: "rgba(0, 0, 0, 0.05)"
                            },
                            ticks: {
                                stepSize: 5000
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

            // Time filter buttons
            document.querySelectorAll('.time-filter').forEach(button => {
                button.addEventListener('click', function () {
                    document.querySelectorAll('.time-filter').forEach(btn => {
                        btn.classList.remove('active');
                    });
                    this.classList.add('active');
                });
            });
        </script>
    </form>
</body>
</html>