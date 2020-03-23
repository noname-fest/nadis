using System;

namespace nadis.Models
{
    public class User
    {
        public int Id { get; set; }
        public int Iduser { get; set; }
        public string username { get; set; }
        public string userpassword { get; set; }

        public string KIDro { get; set; }
        public string Role { get; set; }
        
        public DateTime reportDt {get; set;}
    }
}