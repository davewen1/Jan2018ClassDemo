﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Jan2018DemoWebsite.SamplePages
{
    public partial class ODSQuery : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void AlbumList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //first, we wish to access the specific row that was selected by pressing the "View" link ==> The select command button of the gridview.
            //remember, the view link is a Command Button.
            GridViewRow agvrow = AlbumList.Rows[AlbumList.SelectedIndex];
            //Access the data from the GridView Template control, use the .FindControl("IdControlName")to access the desired control.
            //1.point to the row, 2.find the control.
            string albumid = (agvrow.FindControl("AlbumId") as Label).Text;
            // !!!!!!for unknown references error, it is looking for control whos id is AlbumID, go to the gridview
            //send the extracted value to another specified page
            //parameters are created using a label and a value. means that it will be seen in the url. older browser used to limit this string to 250 chars. now it is longer, but still limited characters.
            //string, no need to parse to int
            //it is in plan text in the url, therefore dont want to send sensitive data.
            //pagename?parameterset&parameterset&...
            // ? parameter set following
            // Parameter set idlabel = value
            // & separates multiple parameter sets
            Response.Redirect("AlbumDetails.aspx?aid=" + albumid);
        }
    }
}