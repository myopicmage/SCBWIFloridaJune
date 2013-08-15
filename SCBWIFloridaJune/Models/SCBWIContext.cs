using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SCBWIFloridaJune.Models
{
    public class SCBWIContext : DbContext
    {
        public SCBWIContext() : base("SCBWI") 
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<Critique> Critiques { get; set; }
        public DbSet<Information> Information { get; set; }
        public DbSet<Date> Dates { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<LisaWheeler> LisaWheelers { get; set; }
    }
}