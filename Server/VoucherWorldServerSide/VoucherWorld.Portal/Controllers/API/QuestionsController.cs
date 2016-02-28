using System.Net;
using System.Net.Http;
using System.Web.Http;
using Repository.Pattern.Ef6;
using Repository.Pattern.Infrastructure;
using VoucherWorld.Data;
using VoucherWorld.Data.Entities;

namespace VoucherWorld.Portal.Controllers.API
{
    public class QuestionsController : ApiController
    {
        //POST /api/questions/scaling?userId=<value>&placeId=<value>
        //Body data: answer=<value>
        [HttpPost]
        [ActionName("scaling")]
        public HttpResponseMessage AnswerScalingQuestion(int userId, int placeId, int answer)
        {
            UnitOfWork myUnitOfWork = new UnitOfWork(new VoucherWorldContext());

            var user = myUnitOfWork.Repository<NormalUser>().Find(userId);

            var question = myUnitOfWork.Repository<ScalingQuestion>().Find(placeId);

            var scalingQuestion = new ScalingAnswer()
            {
                NormalUser = user,
                ScalingQuestion = question,
                Content = answer,
                ObjectState = ObjectState.Added
            };

            myUnitOfWork.Repository<ScalingAnswer>().InsertGraph(scalingQuestion);
            myUnitOfWork.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, answer);
        }

        //POST /api/questions/openended?userId=<value>&routeId=<value>
        //Body data: answer=<value>
        [HttpPost]
        [ActionName("openended")]
        public HttpResponseMessage AnswerOpenEndedQuestion(int userId, int routeId, string answer)
        {
            UnitOfWork myUnitOfWork = new UnitOfWork(new VoucherWorldContext());

            var user = myUnitOfWork.Repository<NormalUser>().Find(userId);

            var question = myUnitOfWork.Repository<OpenEndedQuestion>().Find(routeId);

            var openEndedAnswer = new OpenEndedAnswer()
            {
                NormalUser = user,
                OpenEndedQuestion = question,
                Content = answer,
                ObjectState = ObjectState.Added
            };

            myUnitOfWork.Repository<OpenEndedAnswer>().InsertGraph(openEndedAnswer);
            myUnitOfWork.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, answer);
        }
    }
}
