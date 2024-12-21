using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalTaskLab1.Models
{
    using System;

    public class Token
    {
        public int Id { get; set; }
        public string TokenNumber { get; set; }
        public string CounterType { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

