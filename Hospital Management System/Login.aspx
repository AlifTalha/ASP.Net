<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="HospitalManagementSystem.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Hospital Management - Login</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/animate.css/4.1.1/animate.min.css" />
    <link href="https://fonts.googleapis.com/css2?family=Poppins:wght@300;400;500;600;700&family=Montserrat:wght@700&display=swap" rel="stylesheet" />
    <style>
        :root {
            --primary-color: #4e73df;
            --secondary-color: #1cc88a;
            --accent-color: #f6c23e;
            --dark-color: #2e2e3a;
            --light-color: #f8f9fc;
            --shadow: 0 4px 20px rgba(0, 0, 0, 0.15);
            --transition: all 0.4s cubic-bezier(0.175, 0.885, 0.32, 1.275);
        }

        body {
            font-family: 'Poppins', sans-serif;
            min-height: 100vh;
            overflow-x: hidden;
            color: var(--dark-color);
            background: linear-gradient(135deg, #2c3e50 0%, #4ca1af 100%);
            perspective: 1000px;
        }

        .bg-wrapper {
            position: fixed;
            width: 100%;
            height: 100%;
            top: 0;
            left: 0;
            z-index: -1;
            overflow: hidden;
            transform-style: preserve-3d;
        }

        .bg-layer {
            position: absolute;
            width: 120%;
            height: 120%;
            top: -10%;
            left: -10%;
            background-repeat: repeat;
            will-change: transform;
        }

        .bg-layer-1 {
            background: radial-gradient(circle at center, rgba(76, 161, 175, 0.3) 0%, transparent 70%);
            transform: translateZ(-500px);
            animation: float 40s infinite linear;
        }

        .bg-layer-2 {
            background: 
                radial-gradient(circle at 20% 30%, rgba(46, 204, 113, 0.15) 0%, transparent 25%),
                radial-gradient(circle at 80% 70%, rgba(52, 152, 219, 0.15) 0%, transparent 25%);
            transform: translateZ(-300px);
            animation: float 30s infinite linear reverse;
        }

        .bg-layer-3 {
            background: 
                linear-gradient(45deg, rgba(255,255,255,0.03) 0%, transparent 50%),
                linear-gradient(-45deg, rgba(255,255,255,0.03) 0%, transparent 50%);
            transform: translateZ(-100px);
            animation: float 20s infinite linear;
        }

        .floating-shape {
            position: absolute;
            border-radius: 50%;
            filter: blur(30px);
            opacity: 0.4;
            transform-style: preserve-3d;
            animation: float 30s infinite linear;
        }

        .shape-1 {
            width: 300px;
            height: 300px;
            background: var(--primary-color);
            top: 10%;
            left: 10%;
            transform: translateZ(-200px);
            animation-duration: 25s;
        }

        .shape-2 {
            width: 400px;
            height: 400px;
            background: var(--secondary-color);
            bottom: 5%;
            right: 5%;
            transform: translateZ(-150px);
            animation-duration: 35s;
        }

        .shape-3 {
            width: 200px;
            height: 200px;
            background: var(--accent-color);
            top: 50%;
            left: 30%;
            transform: translateZ(-100px);
            animation-duration: 20s;
        }

        @keyframes float {
            0% {
                transform: translate3d(0, 0, 0) rotate(0deg);
            }
            100% {
                transform: translate3d(100px, 50px, 0) rotate(360deg);
            }
        }

        .login-wrapper {
            display: flex;
            justify-content: center;
            align-items: center;
            min-height: 100vh;
            padding: 2rem;
            transform-style: preserve-3d;
        }

        .login-container {
            width: 100%;
            max-width: 500px;
            transform-style: preserve-3d;
            transition: transform 0.6s;
        }

        .login-card {
            background: rgba(255, 255, 255, 0.95);
            border-radius: 20px;
            overflow: hidden;
            box-shadow: 
                0 15px 35px rgba(0, 0, 0, 0.2),
                0 5px 15px rgba(0, 0, 0, 0.1);
            backdrop-filter: blur(10px);
            transform: translateZ(50px);
            transition: var(--transition);
            position: relative;
            z-index: 10;
        }

        .login-card:hover {
            transform: translateZ(80px);
            box-shadow: 
                0 25px 60px rgba(0, 0, 0, 0.3),
                0 10px 25px rgba(0, 0, 0, 0.2);
        }

        .login-header {
            background: linear-gradient(135deg, var(--primary-color) 0%, var(--secondary-color) 100%);
            color: white;
            padding: 2.5rem;
            text-align: center;
            position: relative;
            overflow: hidden;
            transform-style: preserve-3d;
        }

        .login-header::before {
            content: '';
            position: absolute;
            width: 200%;
            height: 200%;
            top: -50%;
            left: -50%;
            background: 
                radial-gradient(circle, rgba(255,255,255,0.1) 0%, transparent 70%),
                linear-gradient(45deg, transparent 48%, rgba(255,255,255,0.1) 50%, transparent 52%);
            animation: pulse 15s infinite linear;
            transform: translateZ(-20px);
        }

        .hospital-icon {
            font-size: 3rem;
            margin-bottom: 1rem;
            display: inline-block;
            transform: translateZ(30px);
            text-shadow: 0 5px 15px rgba(0,0,0,0.2);
        }

        .login-body {
            padding: 2.5rem;
        }

        .form-group {
            margin-bottom: 1.5rem;
            position: relative;
        }

        .form-label {
            display: block;
            margin-bottom: 0.5rem;
            font-weight: 500;
            color: var(--dark-color);
            transition: var(--transition);
        }

        .form-control {
            width: 100%;
            padding: 1rem;
            border: none;
            border-radius: 10px;
            font-size: 1rem;
            transition: var(--transition);
            background: rgba(255, 255, 255, 0.9);
            box-shadow: 
                inset 0 2px 4px rgba(0,0,0,0.05),
                0 4px 8px rgba(0,0,0,0.05);
            transform-style: preserve-3d;
        }

        .form-control:focus {
            box-shadow: 
                inset 0 2px 4px rgba(0,0,0,0.1),
                0 6px 12px rgba(78, 115, 223, 0.2);
            transform: translateY(-3px) translateZ(10px);
            outline: none;
        }

        .input-icon {
            position: absolute;
            right: 15px;
            top: 50%;
            transform: translateY(-50%);
            color: var(--dark-color);
            opacity: 0.5;
        }

        .btn-login {
            background: linear-gradient(to right, var(--primary-color), var(--secondary-color));
            color: white;
            border: none;
            padding: 1rem;
            border-radius: 10px;
            font-size: 1rem;
            font-weight: 600;
            cursor: pointer;
            width: 100%;
            transition: var(--transition);
            position: relative;
            overflow: hidden;
            z-index: 1;
            transform-style: preserve-3d;
            box-shadow: 
                0 5px 15px rgba(0,0,0,0.2),
                0 2px 5px rgba(0,0,0,0.1);
        }

        .btn-login:hover {
            transform: translateY(-5px) translateZ(10px);
            box-shadow: 
                0 15px 30px rgba(0,0,0,0.3),
                0 5px 15px rgba(0,0,0,0.2);
        }

        .btn-login::before {
            content: '';
            position: absolute;
            top: 0;
            left: -100%;
            width: 100%;
            height: 100%;
            background: linear-gradient(to right, var(--secondary-color), var(--primary-color));
            transition: var(--transition);
            z-index: -1;
        }

        .btn-login:hover::before {
            left: 0;
        }

        .login-footer {
            text-align: center;
            margin-top: 2rem;
            color: var(--dark-color);
        }

        .login-footer a {
            color: var(--primary-color);
            text-decoration: none;
            font-weight: 600;
            transition: var(--transition);
        }

        .login-footer a:hover {
            color: var(--secondary-color);
            text-decoration: underline;
        }

        .status-message {
            padding: 1rem;
            border-radius: 10px;
            margin-bottom: 1.5rem;
            text-align: center;
            font-weight: 500;
            animation: fadeIn 0.5s ease-out;
        }

        @keyframes fadeIn {
            from {
                opacity: 0;
                transform: translateY(-10px);
            }
            to {
                opacity: 1;
                transform: translateY(0);
            }
        }

        @keyframes pulse {
            0% {
                transform: rotate(0deg) translateZ(-20px);
            }
            100% {
                transform: rotate(360deg) translateZ(-20px);
            }
        }

        .remember-me {
            display: flex;
            align-items: center;
            margin-bottom: 1.5rem;
        }

        .remember-me input {
            margin-right: 8px;
        }

        @media (max-width: 768px) {
            .login-wrapper {
                padding: 1rem;
            }
            
            .login-card {
                transform: none !important;
            }
            
            .bg-layer {
                animation: none !important;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <!-- 3D Background Layers -->
        <div class="bg-wrapper">
            <div class="bg-layer bg-layer-1"></div>
            <div class="bg-layer bg-layer-2"></div>
            <div class="bg-layer bg-layer-3"></div>
            <div class="floating-shape shape-1"></div>
            <div class="floating-shape shape-2"></div>
            <div class="floating-shape shape-3"></div>
        </div>

        <div class="login-wrapper">
            <div class="login-container animate__animated animate__fadeIn">
                <div class="login-card">
                    <div class="login-header">
                        <div class="hospital-icon">
                            <i class="fas fa-hospital"></i>
                        </div>
                        <h3 class="animate__animated animate__fadeInDown">TALHA's Hospital</h3>
                        <p class="animate__animated animate__fadeInUp animate__delay-1s">Welcome back! Please login</p>
                    </div>
                    
                    <div class="login-body">
                        <asp:Panel ID="pnlMessage" runat="server" Visible="false" CssClass="status-message">
                            <asp:Label ID="lblErrorMessage" runat="server"></asp:Label>
                        </asp:Panel>
                        
                        <div class="form-group animate__animated animate__fadeInUp animate__delay-1s">
                            <label for="txtUserName" class="form-label">Username</label>
                            <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control" placeholder="Enter username"></asp:TextBox>
                            <i class="fas fa-user input-icon"></i>
                        </div>
                        
                        <div class="form-group animate__animated animate__fadeInUp animate__delay-2s">
                            <label for="txtPassword" class="form-label">Password</label>
                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="Enter password"></asp:TextBox>
                            <i class="fas fa-lock input-icon"></i>
                        </div>
                        
                        <div class="remember-me animate__animated animate__fadeInUp animate__delay-3s">
                            <asp:CheckBox ID="chkRememberMe" runat="server" />
                            <label for="chkRememberMe">Remember me</label>
                        </div>
                        
                        <div class="form-group animate__animated animate__fadeInUp animate__delay-3s">
                            <asp:Button ID="btnLogin" runat="server" Text="Login" 
                                CssClass="btn btn-login" OnClick="btnLogin_Click" />
                        </div>
                        
                        <div class="login-footer animate__animated animate__fadeIn animate__delay-4s">
                            <p>Don't have an account? <a href="Register.aspx">Register here</a></p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        
        <!-- Scripts -->
        <script src="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/js/all.min.js"></script>
        <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
        <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
        
        <script>
            $(document).ready(function () {
                // Parallax effect
                $(window).on('mousemove', function (e) {
                    const x = e.clientX / window.innerWidth;
                    const y = e.clientY / window.innerHeight;

                    $('.bg-layer-1').css('transform', `translate3d(${x * 30}px, ${y * 30}px, -500px)`);
                    $('.bg-layer-2').css('transform', `translate3d(${x * 20}px, ${y * 20}px, -300px)`);
                    $('.bg-layer-3').css('transform', `translate3d(${x * 10}px, ${y * 10}px, -100px)`);

                    // Card tilt effect
                    const tiltX = (window.innerWidth / 2 - e.pageX) / 20;
                    const tiltY = (window.innerHeight / 2 - e.pageY) / 20;
                    $('.login-container').css('transform',
                        `rotateY(${tiltX}deg) rotateX(${tiltY}deg) translateZ(50px)`);
                });

                // Reset position when mouse leaves
                $('.login-container').on('mouseleave', function () {
                    $(this).css('transform', 'rotateY(0) rotateX(0) translateZ(50px)');
                });

                // Form field focus effects
                $('.form-control').focus(function () {
                    $(this).parent().addClass('animate__animated animate__pulse');
                }).blur(function () {
                    $(this).parent().removeClass('animate__animated animate__pulse');
                });
            });
        </script>
    </form>
</body>
</html>