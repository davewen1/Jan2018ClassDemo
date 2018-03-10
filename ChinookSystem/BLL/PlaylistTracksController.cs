using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using Chinook.Data.Entities;
using Chinook.Data.DTOs;
using Chinook.Data.POCOs;
using ChinookSystem.DAL;
using System.ComponentModel;
using DMIT2018Common.UserControls;
#endregion

namespace ChinookSystem.BLL
{
    public class PlaylistTracksController
    {
        public List<UserPlaylistTrack> List_TracksForPlaylist(
            string playlistname, string username)
        {
            using (var context = new ChinookContext())
            {
               
                //code to go here

                return null;
            }
        }//eom
        public void Add_TrackToPLaylist(string playlistname, string username, int trackid)
        {
            //Part one --optional add of the new playlist to validate track is NOT on the existing playlist. 
            //this list of strings will be used with the BusinessRuleException. Every item in the list will be an error to displayed. useful for multiple Business rules.
            List<string> reasons = new List<string>();

            using (var context = new ChinookContext())
            {
                //code to go here
                //throw new Exception(playlistname + "," + username + "," + trackid.ToString());
                //Determine if the playlist already exists on the database
                Playlist exists = context.Playlists.Where(x => x.Name.Equals(playlistname, StringComparison.OrdinalIgnoreCase)
                                && x.UserName.Equals(username, StringComparison.OrdinalIgnoreCase)).Select(x => x).FirstOrDefault();
                PlaylistTrack newTrack = null;
                int tracknumber = 0;
                if (exists == null)
                {
                    //add the parent record (playlist record)
                    //no tracks exists yet for the new palylist, threfore the tracknumber is 1.
                    exists = new Playlist();
                    exists.Name = playlistname;
                    exists.UserName = username;
                    exists = context.Playlists.Add(exists);
                    //this is the first track, set the track number to 1
                    tracknumber = 1;
                }
                else
                {
                    //The playlist exists on the database, the playlist may or MAYNOT have any tracks
                    //Tracks can only appear once per Playlist.
                    //Adjust the tracknumber to be the next track
                    tracknumber = exists.PlaylistTracks.Count() + 1;
                    //will this be a duplicate track? look upo the tracks of the playlist, testing for the incoming trackid.
                    newTrack = exists.PlaylistTracks.SingleOrDefault(x => x.TrackId == trackid); //TrackId equals to the incomming parimeter. 
                    //validation rule: track may only exists once on the playlist.
                    //the default of an object is null. null means you dont find it, or you will find a track => then throw an exception.
                    if (newTrack != null)
                    {
                        //rule is violated, track Already Exists on playlist,
                        //throw exception to stop OLTP processing.
                        //this example will demonstrate using the BusinesssRuleException.
                        reasons.Add("Track already exists on the playlist.");                      
                    }                   
                   
                }
                //Part Two -- add teh track to the playlisttracks

                //check if any errors were found
                if (reasons.Count() > 0)
                {
                    //issue a BusinessRuleException
                    //A businessRuleException is an object that has been designed to hold multiple errors.
                    throw new BusinessRuleException("Adding track to playlist.", reasons);
                }
                else
                {


                    //we only want to do this if this is a new track
                    newTrack = new PlaylistTrack();
                    newTrack.TrackNumber = tracknumber;
                    newTrack.TrackId = trackid;

                    //What about the foreign key to Playlist?
                    //the parent entity has been setup with a Hashset
                    //Therefore, if you add a child via the nvigational property. The Hashset will take care of fill the foreign key with the appropriate pKey value during .SaveChanges().

                    //add the new track to the playlist using the NAVIGATIONAL property
                    exists.PlaylistTracks.Add(newTrack);

                    //Physically place the record(s) on the database AND commit the transaction that is the (using) with .SaveChanges.
                    context.SaveChanges();

                }

            }
        }//eom
        public void MoveTrack(string username, string playlistname, int trackid, int tracknumber, string direction)
        {
            using (var context = new ChinookContext())
            {
                //code to go here 

            }
        }//eom


        public void DeleteTracks(string username, string playlistname, List<int> trackstodelete)
        {
            using (var context = new ChinookContext())
            {
               //code to go here


            }
        }//eom
    }
}
