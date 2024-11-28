//using FinalDemo.EF;
//using System.Web;
//using System.Web.Mvc;

//namespace FinalDemo.Auth
//{
//    public class UserAccess : AuthorizeAttribute
//    {
//        protected override bool AuthorizeCore(HttpContextBase httpContext)
//        {
//            var user = (User)httpContext.Session["User"];
//            if (user != null && user.Role.Equals("User"))
//            {
//                return true;
//            }

//            return false;
//        }
//    }
//}
