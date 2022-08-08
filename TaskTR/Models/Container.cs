using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskTR.Models
{
    public class Containers
    {
        public  int Id { get; set; } 
        public string primaryName { get; set; }
        public string secondaryName { get; set; }

        public int FK_TubeTypeId { get; set; } 
        public Tube TubeList { get; set; }
        
    }
}