using Contracts.Events.Request;
using Contracts.Events.Response;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.RepoInterfaces.ViewRepo;

namespace UserService.Application.Consumers
{
    public class InstructorVerificationConsumer: IConsumer<InstructorVerificationRequested>
    {
        private readonly  IInstructorViewRepo _viewRepo;
        public InstructorVerificationConsumer(IInstructorViewRepo viewRepo)
        {
            _viewRepo = viewRepo;
        }

        public async Task Consume(ConsumeContext<InstructorVerificationRequested> context)
        {
            var instructor = await _viewRepo.GetInstructorById(context.Message.InstructorId);
            bool isVerified = instructor != null;
            await context.RespondAsync(new InstructorVerfied(isVerified));
        }
    }
}
