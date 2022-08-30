using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaContatos.Data.Configurations
{
    public class SqlServerConfiguration
    {
        public static string GetConnectionString()
        {
            //return @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BDAgendaContatos;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            return @"Data Source = SQL8004.site4now.net; Initial Catalog = db_a8c36f_bdagendacontatos; User Id = db_a8c36f_bdagendacontatos_admin; Password = fred1989";
        }
    }
}


