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
            var users = unitOfWork.GetRepository<User>();
            var user = users.Get(x => x.FirstName.Equals("John")).FirstOrDefault();
            var result = new
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                SSN = user.SSN
            };
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [Route("User")]
        [HttpPost]
        public HttpResponseMessage PostUser(AddUserRequest request)
        {
            var hrm = new HttpResponseMessage();
            try
            {
                if (string.IsNullOrWhiteSpace(request?.FirstName) || string.IsNullOrWhiteSpace(request?.LastName) || string.IsNullOrWhiteSpace(request?.SSN))
                {
                    Request.CreateErrorResponse(HttpStatusCode.BadRequest, "FirstName, LastName, and SSN are required.");
                }
                else
                {
                    var users = unitOfWork.GetRepository<User>();
                    var user = users.Get(x => x.SSN == request.SSN).FirstOrDefault();
                    if (user == null)
                    {
                        //If the user is null then create a new user
                        user = new User()
                        {
                            FirstName = request.FirstName,
                            LastName = request.LastName,
                            SSN = request.SSN
                        };
                        users.Insert(user);
                    }
                    else
                    {
                        //If the user already exists then just update
                        user.FirstName = request.FirstName;
                        user.LastName = request.LastName;
                        users.Update(user);
                    }
                    //Save the changes
                    unitOfWork.Save();
                    hrm = Request.CreateResponse(HttpStatusCode.OK);
                }
            }
            catch (Exception e)
            {
                hrm = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.ToString());
            }
            return hrm;
        }

        [Route("Account")]
        public HttpResponseMessage GetAccount()
        {
            var accountRepo = unitOfWork.GetRepository<Account>();
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

        [Route("Account")]
        [HttpPost]
        public HttpResponseMessage PostAccount(AddAccountRequest request)
        {
            var hrm = new HttpResponseMessage();
            try
            {
                if (string.IsNullOrWhiteSpace(request?.UserSSN) || string.IsNullOrWhiteSpace(request?.AccountType))
                {
                    Request.CreateErrorResponse(HttpStatusCode.BadRequest, "UserSSN and AccountType are required.");
                }
                else
                {
                    var users = unitOfWork.GetRepository<User>();
                    var user = users.Get(x => x.SSN == request.UserSSN).FirstOrDefault();
                    if (user == null)
                    {
                        //If the user is null then return an error
                        hrm = Request.CreateErrorResponse(HttpStatusCode.BadRequest, "No user found with matching SSN.");
                    }
                    else
                    {
                        //If the user exists then create the account
                        var account = new Account()
                        {
                            Balance = request.Balance,
                            Type = request.AccountType
                        };
                        //TODO: Refactor account to only have a single account mapping instead of a collection
                        var accountMapping = new List<AccountMapping>()
                        {
                            new AccountMapping()
                            {
                                Account = account,
                                User = user
                            }
                        };
                        account.AccountMappings = accountMapping;
                        var accounts = unitOfWork.GetRepository<Account>();
                        accounts.Insert(account);
                        //Save the changes
                        unitOfWork.Save();
                        hrm = Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
            }
            catch (Exception e)
            {
                hrm = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.ToString());
            }
            return hrm;
        }

        [Route("Accounts")]
        public HttpResponseMessage GetAccounts()
        {
            var accountMappingRepo = unitOfWork.GetRepository<AccountMapping>();
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
