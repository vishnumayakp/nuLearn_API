using CourseService.Application.Categories.Command;
using MediatR;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.Events.Request;
using MassTransit.Transports;

namespace CourseService.Application.Categories.CommandHandler
{
    public class RequestAdminVerificationHandler:IRequestHandler<RequestAdminVerificationCommand, bool>
    {
        private readonly IPublishEndpoint _endpoint;

        public RequestAdminVerificationHandler(IPublishEndpoint endpoint)
        {
            _endpoint = endpoint;
        }

        public async Task<bool> Handle(RequestAdminVerificationCommand request, CancellationToken cancellationToken)
        {
            var eventMessage = new AdminVerificationRequested(request.Id, request.CategoryName,request.Image);
            Console.WriteLine("Published AdminVerificationRequested Event");

            await _endpoint.Publish(eventMessage, cancellationToken);
            return true;
        }
    }   

}
