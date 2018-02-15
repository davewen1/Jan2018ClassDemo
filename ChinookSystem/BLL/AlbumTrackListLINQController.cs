using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using Chinook.Data.Entities;
using ChinookSystem.DAL;
using System.ComponentModel;
using Chinook.Data.DTOs;
using Chinook.Data.POCOs;
#endregion

namespace ChinookSystem.BLL
{
    [DataObject]
    public class AlbumTrackListLINQController
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)] //expose this method
        public List<AlbumTrack> Album_GetTrackList()
        {
            using (var context = new ChinookContext())
            { 
                var results = from x in context.Albums
                              where x.Tracks.Count() >= 5
                              select new AlbumTrack
                              {
                                  AlbumTitle = x.Title,
                                  ArtistName = x.Artist.Name,
                                  TrackList = (from y in x.Tracks//.ToList(), then can use string.format inside the select new.
                                               select new TrackInfo
                                               {
                                                   TrackName = y.Name,
                                                   length = y.Milliseconds / 60000 + ":" + (y.Milliseconds % 60000) / 1000    //first part is minutes, the remainder/1000 to get the seconds
                                               }).ToList()

                              };
            return results.ToList();
            }
        }
    }
}
