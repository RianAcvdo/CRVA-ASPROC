using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//se agregaron librerias para crear base de datos
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
//libreria de entidades
using Agape.ReservacionVhl.EN;

namespace Agape.ReservacionVhl.DAL
{
    public class DBCommon:DbContext
    {
        //Codigo Hecho por Cesar Garcia

        //Cadena de conexion donde se ubica el gestor de base de datos

        //Cadena de conexion Ricardo
        //   const string StrConexion = @"Data Source=P\SQLEXPRESS;Initial Catalog=Agape.ReservacionVhl;Integrated Security=True";
        //const string StrConexion = @"Data Source=.;Initial Catalog=Agape.ReservacionVhlV2;Integrated Security=True";


        //Cadena de conexion Moises
        //const string StrConexion = @"Data Source=192.168.10.14,1433; Initial catalog=CRVA-DB; user id=crva;password=Rosa9477;";
        //const string StrConexion = @"Data Source=Escobar\MSSQL2017EB;Initial Catalog=CRVA;Integrated Security=True";
        const string StrConexion = @"Data Source=Escobar\MSSQL2017EB; Initial catalog=CRVA-DB; user id=moises;password=crva@1234;";

        //Cadena de conexion Cesar
        //const string StrConexion = @"Data Source=.;Initial Catalog=DBAGAPE;Integrated Security=True";

        //Cadena de conexion Roberto
        //const string StrConexion = @"Data Source=.;Initial Catalog=CRVA-DB;Integrated Security=True";


        //aqui se hereda la base de contexto para crear un metodo que usara para navegar en la base de datos
        //con entity framework
        public DBCommon() : base(StrConexion)
        {
            var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        

        /// <summary>
        /// Aqui declaramos los database set para Asignar las propiedades que necesitaremos para navegar
        /// con entity y tambien nos permitira crear la base de datos.
        /// </summary>
        public DbSet<User> Users { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        //public DbSet<Position> Positions { get; set; }
        public DbSet<Office> Offices { get; set; }
        public DbSet<Mark> Marks { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Car> Cars { get; set; }


        //metodo que construye la base de datos en vase a los modelos(entidades)
        /// <summary>
        /// Utiliza la clase modelBuilder
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        //Codigo Hecho por Cesar Garcia
    }
}
