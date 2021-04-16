using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vidly.Web.Models;

namespace Vidly.Web.ViewModels
{
    public class RandomMovieViewModel
    {
        public Movie Movie { get; set; }
        public List<Customer> Customers { get; set; }
    }
}
