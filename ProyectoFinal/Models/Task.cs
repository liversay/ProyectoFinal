using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Web;
using static System.Net.Mime.MediaTypeNames;

namespace ProyectoFinal.Models
{
    public class Task
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public bool isCompleted { get; set; }
        public DateTime? expDate { get; set; }
    }



}