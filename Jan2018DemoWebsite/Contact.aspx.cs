using AppSecurity.BLL;
using AppSecurity.DAL;
using AppSecurity.Entities;
using Chinook.Data.POCOs;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Jan2018DemoWebsite
{
    public partial class Contact : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!Request.IsAuthenticated)
                {
                    Response.Redirect("~/Account/Login.aspx");
                }
                else
                {
                    if (!User.IsInRole(SecurityRoles.Staff))
                    {
                        Response.Redirect("~/Account/Login.aspx");
                    }
                }
            }


        }

        protected void GetUserName_Click(object sender, EventArgs e)
        {
            //grab the username from security (User)
            string username = User.Identity.Name;  //I need to use this name to find the appllication user record. 
            UserDisplayName.Text = username;

            //obtain the employee information for this username
            MessageUserControl.TryRun(() =>
            {
                //connect to the ApplicationUserManager    //type casting 
                //connection string to applicationusermanager
                ApplicationUserManager secmgr = new ApplicationUserManager(
                    new UserStore<ApplicationUser>(new ApplicationDbContext()));
                EmployeeInfo info = secmgr.User_GetEmployee(username);
                EmployeeID.Text = info.EmployeeID.ToString();
                EmployeeName.Text = info.FullName;
            });
        }
    }
}