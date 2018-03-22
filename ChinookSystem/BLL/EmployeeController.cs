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
    public class EmployeeController
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)] //expose this method
        public List<EmployeeClients> Employee_GetClientList()
        {
            //our accesss to the DB is via the Context class
            using (var context = new ChinookContext())
            {

            
                var results = from employeeRow in context.Employees     //the query is a C# statement. varible = expression;
                              where employeeRow.Title.Contains("Support")
                              orderby employeeRow.LastName + ", " + employeeRow.FirstName
                              select new EmployeeClients
                              {
                                  Name = employeeRow.LastName + ", " + employeeRow.FirstName,

                                  ClientCount = employeeRow.Customers.Count(),      //, seperate the field

                                  ClientList = from customerRowOfEmployeeRow in employeeRow.Customers     //x.SupportRepIdCustomers is the datasource for the second from. 
                                               orderby customerRowOfEmployeeRow.LastName,
                                               customerRowOfEmployeeRow.FirstName


                                               select new ClientInfo
                                               {
                                                   client = customerRowOfEmployeeRow.LastName + ", " + customerRowOfEmployeeRow.FirstName,
                                                   phone = customerRowOfEmployeeRow.Phone
                                               }

                              };
            return results.ToList();
        }
        }

        public Employee Employee_Get (int employeeid)
        {
            using (var context = new ChinookContext())
            {
                return context.Employees.Find(employeeid);
            }
        }

    }
}
