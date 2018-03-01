using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using Chinook.Data.Entities;
using ChinookSystem.DAL;
using System.ComponentModel;
using Chinook.Data.POCOs;
#endregion

namespace ChinookSystem.BLL
{
    //anytime you make a change to the controller, make sure you rebuild it.
    [DataObject]
    public class TrackController
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<Track> Tracks_List()
        {
            //create an transaction instance of your Context class
            using (var context = new ChinookContext())
            {
                return context.Tracks.OrderBy(x=>x.Name).ToList(); 
            }
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public Track Tracks_Get(int trackid)
        {
            //create an transaction instance of your Context class
            using (var context = new ChinookContext())
            {
                //return context.Artists.ToList(); This is not using LINQ, this is using extensions.
                //return context.Artists.OrderBy(x=>x.Name).ToList(); //This is using LINQ, because of the use of IEnumerble set, take it and sort it. X represent the current insidence of the set. 
                //ToList takes the IEunmerble and sent it to a list. 
                return context.Tracks.Find(trackid); //this is an extension of a collection.
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<Track> Tracks_GetByAlbumID(int albumid)
        {
            //create an transaction instance of your Context class
            using (var context = new ChinookContext())
            {
                // => means i want you to do this against x
                //.Select( x=> x) return x. 
                return context.Tracks.Where(x => x.AlbumId == albumid).Select( x=> x).ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<TrackList> List_TracksForPlaylistSelection(string tracksby, int argid)
        {
            using (var context = new ChinookContext())
            {
                List<TrackList> results = null;

                //code to go here

                return results;
            }
        }//eom
    }
}