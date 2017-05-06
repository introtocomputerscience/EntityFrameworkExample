using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UnitOfWorkRepositoryExample.Models
{
    public class AddUserRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SSN { get; set; }
    }
}