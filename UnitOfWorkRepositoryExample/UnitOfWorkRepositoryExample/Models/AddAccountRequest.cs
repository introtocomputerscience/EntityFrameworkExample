using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UnitOfWorkRepositoryExample.Models
{
    public class AddAccountRequest
    {
        public string AccountType { get; set; }
        public double Balance { get; set; }
        public string UserSSN { get; set; }
    }
}