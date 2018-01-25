using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Jan2018DemoWebsite.SamplePages
{
    public partial class AlbumDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //do the following on the 1st time through this page
            if(!Page.IsPostBack)
            {
                //Response.Redirect sent a value to this page
                //Request.QueryString["labelid"] will obtain the value sent by Redirect.
                //The value is a string, if no value was sent, the value will be null.
                string albumid = Request.QueryString["aid"];
                //test this
                if (string.IsNullOrEmpty(albumid))
                {
                    //send it back to that page
                    Response.Redirect("ODSQuery.aspx");
                }
                else
                {
                    AlbumIDArg.Text = albumid;
                }
            }
        }
    }
}