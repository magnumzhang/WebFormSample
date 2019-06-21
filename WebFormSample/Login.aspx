<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebFormSample.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <!--sb-admin-master-->
    <link href="Content/vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/vendor/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="Content/vendor/datatables/dataTables.bootstrap4.css" rel="stylesheet" />
    <link href="Content/css/sb-admin.css" rel="stylesheet" />

    <!--sb-admin-master-->
    <!--sb-admin-master index页面中，这些引用是在body的最后进行加载的，但是由于子页面中，也会用到其中一些脚本，故在此优先加载。-->
    <script src="Content/vendor/jquery/jquery.min.js"></script>
    <script src="Content/vendor/bootstrap/js/bootstrap.bundle.min.js"></script>
    <script src="Content/vendor/jquery-easing/jquery.easing.min.js"></script>
    <script src="Content/vendor/chart.js/Chart.js"></script>
    <script src="Content/vendor/datatables/jquery.dataTables.js"></script>
    <script src="Content/vendor/datatables/dataTables.bootstrap4.js"></script>
    
    <script src="Content/js/sb-admin-datatables.min.js"></script>
    <script src="Content/js/sb-admin-charts.min.js"></script>

    <script src="Content/myCommon.js"></script>
    <script src="Content/Session.js"></script>
    
   
</head>
<body class="bg-dark">
   <!--content page-->
    <link href="Content/jquery.dataTables.min.css" rel="stylesheet" />
    <link href="Content/dcalendar.picker.css" rel="stylesheet" />

    <script src="Content/bootstrap.min.js"></script>
    <script src="Content/JSonStringBuilder.js"></script>
    <script src="Content/dcalendar.picker.js"></script>
    <script src="Content/jquery.dataTables.min.js"></script>
    <script>
        $(function () {
           
            $("#cltUserName").focus();

        });

        function clt_login() {
 
            var UserName = $("#cltUserName").val();
            var Password = $("#cltPassword").val();

            //TODO:set login type here.
            var LoginCheckType = "System"; //System

            var JBuilder = new JSonStringBuilder();
            JBuilder.Begin();
            JBuilder.Add("UserName", UserName);
            JBuilder.Add("Password", Password);
            JBuilder.Add("LoginCheckType", LoginCheckType);
            JBuilder.End();
            var JSonStr = JBuilder.toString();

            ajax_call_action("Login.aspx/PassLoginCheck", JSonStr, AfterLogin);
            
         
        }

        function AfterLogin(data) {
            //it is defferent from MVC, ajac actual post back data at data.d not data self.
            var JSonObj = $.parseJSON(data.d);

            if (JSonObj.IsPassCheck == "TRUE") {
                //TODO:set your site main page here.
                window.location = "Index.aspx";

            }
            else {
                alert("Login Fail:" + JSonObj.Message);
            }

        }
    </script>

    <div class="container">
        <div class="card card-login mx-auto mt-5">
            <div class="card-header">Login</div>
            <div class="card-body">
                <form>
                    <div class="form-group">
                        <label for="exampleInputEmail1">User Name</label>
                        <input class="form-control" id="cltUserName" type="text" />
                    </div>
                    <div class="form-group">
                        <label for="exampleInputPassword1">Password</label>
                        <input class="form-control" id="cltPassword" type="password" />
                    </div>

                    <button type="button" class="btn btn-primary btn-block" onclick="clt_login()">Login</button>

                </form>

            </div>
        </div>
    </div>

    <!--sb-admin-master-->
    <!--保持在页面最后加载，因为该脚本中包含很多对于页面初始化的事件与处理，在head中加载，将会导致很多事件失效，如左侧的菜单最小化按钮事件等。-->
    <script src="Content/js/sb-admin.min.js"></script>

   
</body>
</html>
