using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region Additional Namespaces
using Chinook.Data.POCOs;
#endregion

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

        protected void CountAlbums_Click(object sender, EventArgs e)
        {
            //traversing a GridView display
            //just like a list view, the only records available to us at this time out of the dataset assigned to the GridView are the rows being displayed.
            //eg, I have 100 rows, out of all of those records, I am only deal with what I want to display.
            //IF to deal with all records, No gridview, do Datasets!

            //create a List<T> to hold the counts of the display
            // need the using name space to access entity
            List<ArtistAlbumCounts> Artists = new List<ArtistAlbumCounts>();

            //reusable pointers to an instance of the specified class
            ArtistAlbumCounts item = null;
            int artistid = 0;

            //setup the loop to traver the gridview
            foreach(GridViewRow line in AlbumList.Rows)
            {
                //access the artistid
                //use the select value instead of selected index because list can be sorted.
                //The only time to use selected index is to determin if something has been selected. 
                artistid = int.Parse((line.FindControl("ArtistList") as DropDownList).SelectedValue);

                //determine if you have already created a count instance in the list<T> for this artist.
                //if Not, create a new instance fo the artist and set its count to 1.
                //if ound, increment the count (+1)

                //1. search for the art in list<T>
                //x is the current record of the find opearation is looking at at that instance
                //=> means for x, do the following:
                //ArtistID from the list of T, compare to the artistid from the grid view.
                //what will be returned is either null (not found) or the instance in the list<T>
                item = Artists.Find(x => x.ArtistId == artistid);

                if(item == null)
                {
                    //Create instance, initialize, add it List<T>, 
                    item = new ArtistAlbumCounts();
                    item.ArtistId = artistid;
                    item.AlbumCount = 1;
                    Artists.Add(item);
                }
                else
                {
                    item.AlbumCount++; 
                }
            }
            //attach the List<T> (collection) to the display control
            ArtistAlbumCountList.DataSource = Artists;
            ArtistAlbumCountList.DataBind();
        }
    }
}