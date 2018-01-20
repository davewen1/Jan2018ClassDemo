using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using Chinook.Data.Entities;
using ChinookSystem.DAL;
using System.ComponentModel;
#endregion

namespace ChinookSystem.BLL
{
    [DataObject]
    public class AlbumController
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<Album> Albums_List()
        {
            //create an transaction instance of your Context class
            using (var context = new ChinookContext())
            {
                return context.Albums.OrderBy(x => x.Title).ToList();
            }
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public Album Albums_Get(int albumid)
        {
            //create an transaction instance of your Context class
            using (var context = new ChinookContext())
            {                
                return context.Albums.Find(albumid); //this is an extension of a collection.
            }
        }
    }
}