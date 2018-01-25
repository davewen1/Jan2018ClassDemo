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

        protected void AlbumTracks_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            // ListViewCommandEventsArgs e contains the value that was attach to the link on the listview row
            //The property that you need to access is called CommandArgument, This prop is NOT a string(duh, it is an object)
            CommandArgID.Text = e.CommandArgument.ToString();
            //extract a value from a column on the listview item, You can think of it as a (row), which is the row that was selected, find the id of that column, 
            //Use .Text to get the content of the label.
            ColumnID.Text = (e.Item.FindControl("TrackIdLabel") as Label).Text;

        }

        protected void Totals_Click(object sender, EventArgs e)
        {
            double time = 0;
            double size = 0;
            //use foreeach to cycle through the listview
            //foreeach is an Object orianted loop, it is a pretest loop. It gives one row at a time, and stops automatically when reaching the end.
            //.Items is all of the list view items(all the rowss) (a collection)
            foreach(ListViewItem item in this.AlbumTracks.Items)
            {
                //what do i use to point to a specific column on row? FindControl. And use .Text to access the content of the label.
                time += double.Parse((item.FindControl("MillisecondsLabel") as Label).Text);
                size += double.Parse((item.FindControl("BytesLabel") as Label).Text);
            }

            TracksTime.Text = time.ToString();
            TracksSize.Text = size.ToString();
        }
    }
}