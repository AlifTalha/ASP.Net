using FinalDemo.EF;
using System;
using System.Web;
using System.Web.Mvc;

public class AdminAccess : AuthorizeAttribute
{
    protected override bool AuthorizeCore(HttpContextBase httpContext)
    {
        var user = httpContext.Session["User"] as User;
        return user != null && user.Role == "Admin";
    }
}
