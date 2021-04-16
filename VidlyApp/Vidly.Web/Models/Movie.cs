using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Web.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string  Name { get; set; }
    }
}
