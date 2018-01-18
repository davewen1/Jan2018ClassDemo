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
    //When creating the entities, this is the Minimum standard! need annotations!
    [Table("Artists")] //table tag with the name of my table in SQL
    public class Artist
    {
        [Key] //this indicate the Id as PK. 
        public int ArtistId { get; set; }

        [StringLength(120, ErrorMessage ="Artist Name has a maximum of 120 characters")] 
        public string Name { get; set; }

        //navigation properties
        public virtual ICollection<Album> Albums { get; set; } // Use Plural naming convention.
    }
}
