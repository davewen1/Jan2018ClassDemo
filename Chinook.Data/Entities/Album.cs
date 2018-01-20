using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
#endregion

namespace Chinook.Data.Entities
{
    [Table("Albums")]
    public class Album
    {
        [Key]
        public int AlbumId { get; set; }

        [Required(ErrorMessage = "Album Title Name is required.")]
        [StringLength(160, ErrorMessage = "Title has a maximum of 160 characters")]
        public string Title { get; set; }
        //Do not need FK annotations. If it has a FK, and a created entity, then make virtual connection to the singualar parent!
        public int ArtistID { get; set; }

        public int ReleaseYear { get; set; }

        [StringLength(50, ErrorMessage = "Release Label has a maximum of 160 characters")] //ErrorMsg will pop up in the user control
        public string ReleaseLabel { get; set; }

        //navigational properties
        public virtual Artist Artist { get; set;}            //The Artist is Singular here, the parent is Singular, not a collection
        public virtual ICollection<Track> Tracks { get; set; } //pointing to a children, it is an ICollection

    }
}
