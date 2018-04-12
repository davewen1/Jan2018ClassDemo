﻿using System;
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
                //List<TrackList> results = null; this was a temperaury line of code...

                var results = from x in context.Tracks
                                  // remember passed by 2 varibles, 1 str tracksby, 1 int argid
                              where tracksby.Equals("Artist") ? x.Album.ArtistId == argid :
                                    tracksby.Equals("Mediatype") ? x.MediaTypeId == argid :
                                    tracksby.Equals("Genre") ? x.GenreId == argid :
                                    x.AlbumId == argid
                              orderby x.Name
                              select new TrackList
                              {
                                  TrackID = x.TrackId,
                                  Name = x.Name,
                                  Title = x.Album.Title,
                                  MediaName = x.MediaType.Name,
                                  GenreName = x.Genre.Name,
                                  Composer = x.Composer,
                                  Milliseconds = x.Milliseconds,
                                  Bytes = x.Bytes,
                                  UnitPrice = x.UnitPrice
                              };


                return results.ToList();
            }
        }//eom

        //this method will create a flat POCO dataset to use in reporting
        [DataObjectMethod(DataObjectMethodType.Select,false)]
        public List<GenreAlbumReport> GenreAlbumReport_Get()
        {
            //there is no need to sort this dataset because the final report sort will be done in the report
            using (var context = new ChinookContext())
            {
                var results = from x in context.Tracks
                              select new GenreAlbumReport
                              {
                                  GenreName = x.Genre.Name,
                                  AlbumTitle = x.Album.Title,
                                  TrackName = x.Name,
                                  Milliseconds = x.Milliseconds,
                                  Bytes = x.Bytes,
                                  UnitPrice = x.UnitPrice
                              };
                return results.ToList();
            }
        }

    }
}