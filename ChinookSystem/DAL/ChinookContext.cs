using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using Chinook.Data.Entities;
using System.Data.Entity;
#endregion

namespace ChinookSystem.DAL
{
    internal class ChinookContext:DbContext  //for general security, it stops accidental access. internal: The type or member can be accessed by any code in the same assembly, but not from another assembly.
    {
        
        public ChinookContext():base("name=ChinookDB") // name holds the name value of yoru web connection string
        //base means there is a parenmeter in the DbContext. base is passing something up to the DbContext during its construction. 
        {

        }

        //need a reference to each table in the databse as avirtural data set
        public virtual DbSet<Artist> Artists { get; set; }
            //the description<> here is "Artist"
            //Artists is the pluro form of a data set.

        public virtual DbSet<Album> Albums { get; set; }
        public virtual DbSet<Track> Tracks { get; set; }
    }
}
