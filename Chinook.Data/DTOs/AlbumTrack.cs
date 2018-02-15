using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#region Additional Namespaces
using Chinook.Data.POCOs;
#endregion

namespace Chinook.Data.DTOs
{
    public class AlbumTrack
    {
        public string AlbumTitle { get; set; }
        public string ArtistName { get; set; }
        public List<TrackInfo> TrackList { get; set; }

    }
}
