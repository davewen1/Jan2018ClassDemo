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
    [Table("Tracks")]
    public class Track
    {
        [Key]
        public int TrackId { get; set; }

        [Required(ErrorMessage = "Track Name is required.")]
        [StringLength(200, ErrorMessage = "Name has a maximum of 200 characters")]
        public string Name { get; set; }
        
        public int? AlbumId { get; set; }

        //No need to setup FK mapping, because havent created the Entity yet.
        public int MediaTypeId { get; set; }

        //
        public int? GenreId { get; set; }

        [StringLength(220, ErrorMessage = "Composer has a maximum of 220 characters")]
        public string Composer { get; set; }

        public int Milliseconds { get; set; }

        public int? Bytes { get; set; }

        public decimal UnitPrice { get; set; }

        //Navigational properties
        public virtual Album Album { get; set; }
    }
}
