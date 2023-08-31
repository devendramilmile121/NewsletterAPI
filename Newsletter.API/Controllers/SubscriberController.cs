using Application.Subscribers.Commands.CreateSubscriber;
using Application.Subscribers.Queries.GetSubscribers;
using Microsoft.AspNetCore.Mvc;

namespace Newsletter.API.Controllers
{
    /// <summary>
    /// Subscriber
    /// </summary>
    public class SubscriberController : ApiControllerBase
    {
        /// <summary>
        /// Add newsletter subscriber
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Identity Id</returns>
        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateSubscriberCommand command)
        {
            return await Mediator.Send(command);
        }

        /// <summary>
        /// List of Subscribers as well as Referral Source list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<SubscribersVM>> Get()
        {
            return await Mediator.Send(new GetSubscribersQuery());
        }
    }
}
