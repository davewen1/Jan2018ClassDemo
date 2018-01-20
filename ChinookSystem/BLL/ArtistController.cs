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
    public class ArtistController
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<Artist> Artists_List()
        {
            //create an transaction instance of your Context class
            using (var context = new ChinookContext())
            {
                return context.Artists.OrderBy(x => x.Name).ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select,false)]
        public Artist Artists_Get(int artistid)
        {
            //create an transaction instance of your Context class
            using (var context = new ChinookContext())
            {
                //return context.Artists.ToList(); This is not using LINQ, this is using extensions.
                //return context.Artists.OrderBy(x=>x.Name).ToList(); //This is using LINQ, because of the use of IEnumerble set, take it and sort it. X represent the current insidence of the set. 
                //ToList takes the IEunmerble and sent it to a list. 
                return context.Artists.Find(artistid); //this is an extension of a collection.
            }
        }
    }
}
