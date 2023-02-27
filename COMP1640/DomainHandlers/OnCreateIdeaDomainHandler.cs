using Domain.DomainEvents;
using Domain.Interfaces;
using MediatR;
using Utilities.EmailService.Interface;
using Utilities.EmailService.Interfaces;
using Utilities.Identity.Interfaces;

namespace COMP1640.DomainHandlers
{
    public class OnCreateIdeaDomainHandler : INotificationHandler<CreateIdeaDomainEvent>
    {
        private readonly IRazorViewRenderer _razorViewRenderer;
        private readonly ICurrentUserInfo _currentUser;
        private readonly IEmailSender _emailSender;
        private readonly IDepartmentRepository _departmentRepo;
        public OnCreateIdeaDomainHandler(ICurrentUserInfo currentUserInfo
            , IEmailSender emailSender
            , IRazorViewRenderer razorViewRenderer
            , IDepartmentRepository departmentRepo)
        {
            _currentUser = currentUserInfo;
            _emailSender = emailSender;
            _razorViewRenderer = razorViewRenderer;
            _departmentRepo = departmentRepo;
        }

        public async Task Handle(CreateIdeaDomainEvent @event, CancellationToken cancellationToken)
        {
            var department = await _departmentRepo.GetAsync(@event.Idea.DepartmentId);
            var QACoordinator = department?.QaCoordinator;
            if (QACoordinator == null)
                return;

            var body = await _razorViewRenderer.RenderToStringAsync("OnIdeaAddedEmailTemplate", @event.Idea);
            if (string.IsNullOrEmpty(body))
                return;

            await _emailSender.SendEmailAsync
                (
                    $"[COMP1640]: Idea \"{@event.Idea.Title}\" Was Created"
                    , body
                    , new List<string> { "trongltgcd18787@fpt.edu.vn" }
                );
        }
    }
}
