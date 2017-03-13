using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UnitOfWorkRepositoryExample.Implementations;
using UnitOfWorkRepositoryExample.Interfaces;
using UnitOfWorkRepositoryExample.Models;

namespace UnitOfWorkRepositoryExample.Controllers
{
    [RoutePrefix("example")]
    public class ExampleController : ApiController
    {
        IUnitOfWork unitOfWork;
        public ExampleController()
        {
            this.unitOfWork = new UnitOfWork<entityframeworkexampleEntities>();
        }

        [Route("User")]
        public HttpResponseMessage GetUser()
        {
            IRepository<User> users = unitOfWork.GetRepository<User>();
            var user = users.Get(x => x.FirstName.Equals("John")).FirstOrDefault();
            var result = new
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                SSN = user.SSN
            };
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [Route("Account")]
        public HttpResponseMessage GetAccount()
        {
            IRepository<Account> accountRepo = unitOfWork.GetRepository<Account>();
            var accounts = accountRepo.Get(x => x.Type.Equals("Checking"));
            var result = accounts.ToList().ConvertAll(x =>
            {
                return new
                {
                    AccountType = x.Type,
                    Balance = x.Balance
                };
            });
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [Route("Accounts")]
        public HttpResponseMessage GetAccounts()
        {
            IRepository<AccountMapping> accountMappingRepo = unitOfWork.GetRepository<AccountMapping>();
            var accounts = accountMappingRepo.Get();
            var result = accounts.ToList().ConvertAll(x =>
            {
                return new
                {
                    FullName = $"{x.User.FirstName} {x.User.LastName}",
                    SSN = x.User.SSN,
                    AccountType = x.Account.Type,
                    Balance = x.Account.Balance
                };
            });
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}
