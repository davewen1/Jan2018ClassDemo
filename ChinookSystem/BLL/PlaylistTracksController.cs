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
                //what would happen if there is no match for the incoming parameter values?
                // validate that a playlist actually exists
                var results = (from x in context.Playlists
                               where x.UserName.Equals(username) && x.Name.Equals(playlistname)
                               select x).FirstOrDefault();
                if (results == null)
                {

                    return null;
                }
                else
                {
                    var theTracks = from x in context.PlaylistTracks
                                    where x.PlaylistId.Equals(results.PlaylistId)
                                    orderby x.TrackNumber
                                    select new UserPlaylistTrack
                                    {
                                        TrackID = x.TrackId,
                                        TrackNumber = x.TrackNumber,
                                        TrackName = x.Track.Name,
                                        Milliseconds = x.Track.Milliseconds,
                                        UnitPrice = x.Track.UnitPrice
                                    };
                    return theTracks.ToList();
                }
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
                var exists = (from x in context.Playlists
                              where x.Name.Equals(playlistname) && x.UserName.Equals(username)
                              select x).FirstOrDefault();
                              
                if (exists == null)
                {
                    throw new Exception("Playlist has been removed from the file.");
                }
                else
                {
                    PlaylistTrack moveTrack = (from x in exists.PlaylistTracks
                                               where x.TrackId == trackid
                                               select x).FirstOrDefault();
                    
                    if (moveTrack == null)
                    {
                        throw new Exception("Playlist track has been removed from the file.");
                    }
                    else
                    {
                        //direction
                        PlaylistTrack otherTrack = null;
                        if (direction.Equals("up"))
                        {
                            //up
                            //could someone have delected the track that I was about to move up.
                            //recheck that the track is NOT the first track
                            //if so, throw an error; otherwise moe the track
                            
                            if (moveTrack.TrackNumber == 1)
                            {
                                throw new Exception("Play list track already at top.");
                            }
                            else
                            {
                                otherTrack = (from x in exists.PlaylistTracks
                                              where x.TrackNumber == moveTrack.TrackNumber - 1
                                              select x).FirstOrDefault();
                                if (otherTrack == null)
                                {
                                    throw new Exception("Switching track is missing"); // for possible time share break. unlikely, but possible. 
                                }
                                else
                                {
                                    moveTrack.TrackNumber -= 1;
                                    otherTrack.TrackNumber += 1;
                                }
                            }

                        }
                        else
                        {
                            //down
                            //recheck the track is NOT the last track
                            if (moveTrack.TrackNumber == exists.PlaylistTracks.Count)
                            {
                                throw new Exception("Play list track already at bottom.");
                            }
                            else
                            {
                                otherTrack = (from x in exists.PlaylistTracks
                                              where x.TrackNumber == moveTrack.TrackNumber + 1
                                              select x).FirstOrDefault();
                                if (otherTrack == null)
                                {
                                    throw new Exception("Switching track is missing"); // for possible time share break. unlikely, but possible. 
                                }
                                else
                                {
                                    moveTrack.TrackNumber += 1;
                                    otherTrack.TrackNumber -= 1;
                                }
                            }

                        }//endofupdown

                        //saving the changes to the data
                        //we are saving 2 different entities
                        //indicate the property to save for a particular entity instance
                        context.Entry(moveTrack).Property(y => y.TrackNumber).IsModified = true;
                        context.Entry(otherTrack).Property(y => y.TrackNumber).IsModified = true;
                        //commit your changes
                        context.SaveChanges();

                    }
                }

            }
        }//eom


        public void DeleteTracks(string username, string playlistname, List<int> trackstodelete)
        {
            using (var context = new ChinookContext())
            {
                //code to go here
                //code to go here 
                var exists = (from x in context.Playlists
                              where x.Name.Equals(playlistname) && x.UserName.Equals(username)
                              select x).FirstOrDefault();

                if (exists == null)
                {
                    throw new Exception("Playlist has been removed from the file.");
                }
                else
                {
                    //get a list of tracks that will be kept IN ORDER of TRACKNUMBER
                    //you do NOT know if the physical order is the same as the logical TrackNumber order
                    //.Any() allows you to search for an item in a list using a condition, returns true if found
                    //looking for an item in ListA is inside ListB
                    //in this example we DO NOT want to find it, thus the "!"
                    //tod tracks to delete, just a number
                    //find something in one list, that is in another list. 
                    var trackskept = exists.PlaylistTracks.Where(tr => !trackstodelete.Any(tod => tod == tr.TrackId))
                        .OrderBy(tr => tr.TrackNumber)
                        .Select(tr => tr);

                    PlaylistTrack item = null;
                    foreach (var deletetrackid in trackstodelete)
                    {
                        item = exists.PlaylistTracks.Where(tr => tr.TrackId == deletetrackid).FirstOrDefault();
                        if(item != null)
                        {
                            exists.PlaylistTracks.Remove(item);
                        }
                        
                    }

                    //renumber remaining tracks (tracks that were kept)
                    int number = 1;
                    foreach (var tkept in trackskept)
                    {
                        tkept.TrackNumber = number;
                        number++;
                        context.Entry(tkept).Property(y => y.TrackNumber).IsModified = true;
                    }
                    //commit work
                    context.SaveChanges();

                }
            }
        }//eom
    }
}
