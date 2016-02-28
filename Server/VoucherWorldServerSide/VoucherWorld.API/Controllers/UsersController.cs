using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Repository.Pattern.Ef6;
using Repository.Pattern.Infrastructure;
using VoucherWorld.Data;
using VoucherWorld.Data.Entities;

namespace VoucherWorld.API.Controllers
{
    public class UsersController : ApiController
    {
        //POST /api/users/register
        [HttpPost]
        [ActionName("signup")]
        public HttpResponseMessage Signup(NormalUser normalUser)
        {
            UnitOfWork myUnitOfWork = new UnitOfWork(new VoucherWorldContext());

            var emails =
                myUnitOfWork.Repository<User>()
                    .Query()
                    .Select(u => u.Email)
                    .ToList();

            var usernames =
                myUnitOfWork.Repository<User>()
                    .Query()
                    .Select(u => u.UserName)
                    .ToList();

            if (emails.Contains(normalUser.Email) || usernames.Contains(normalUser.UserName))
            {
                return Request.CreateResponse(HttpStatusCode.Conflict, "UserName or Email already exists");
            }
            
            normalUser.ObjectState = ObjectState.Added;

            myUnitOfWork.Repository<NormalUser>().InsertGraph(normalUser);

            myUnitOfWork.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        //POST /api/users/signin?username=<value>&password=<>
        [HttpPost]
        [ActionName("signin")]
        public HttpResponseMessage Signin(string username, string password)
        {
            UnitOfWork myUnitOfWork = new UnitOfWork(new VoucherWorldContext());

            var user =
                myUnitOfWork.Repository<NormalUser>()
                    .Query(nu => nu.UserName == username && nu.Password == password)
                    .Select()
                    .ToList();

            if (user.Count == 1)
            {
                return Request.CreateResponse(HttpStatusCode.OK, user[0]);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
        }
    }
}
