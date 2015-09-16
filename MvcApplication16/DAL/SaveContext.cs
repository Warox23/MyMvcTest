using Model.DB;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DAL
{
    public class SaveContext : DbContext
    {
       public SaveContext() : base("Result") { }
        
       public DbSet<ResoultSaveModel> Save { get; set; }

    }
}