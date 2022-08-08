using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskTR.Models
{
    public class Tube
    {
        public int Id { get; set; }
        public string primaryName { get; set;  } 
        public string secondaryName {  get; set; } 
        public string colorHex { get; set;  }

    }
}