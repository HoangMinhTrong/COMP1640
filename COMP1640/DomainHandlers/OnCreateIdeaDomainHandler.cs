using COMP1640.ViewModels.Idea.Requests;
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
        private readonly IConfiguration _configuration;
        public OnCreateIdeaDomainHandler(ICurrentUserInfo currentUserInfo
            , IEmailSender emailSender
            , IRazorViewRenderer razorViewRenderer
            , IDepartmentRepository departmentRepo
            , IConfiguration configuration)
        {
            _currentUser = currentUserInfo;
            _emailSender = emailSender;
            _razorViewRenderer = razorViewRenderer;
            _departmentRepo = departmentRepo;
            _configuration = configuration;
        }

        public async Task Handle(CreateIdeaDomainEvent @event, CancellationToken cancellationToken)
        {
            var department = await _departmentRepo.GetAsync(@event.Idea.DepartmentId);
            var QACoordinator = department?.QaCoordinator;
            if (QACoordinator == null)
                return;

            var model = new IdeaAddedEmailModel(@event.Idea, _configuration.GetSection("APIRoute").Value);
            var body = await _razorViewRenderer.RenderToStringAsync("OnIdeaAddedEmailTemplate", model);
            if (string.IsNullOrEmpty(body))
                return;

            await _emailSender.SendEmailAsync
                (
                    $"[COMP1640]: Idea \"{@event.Idea.Title}\" Was Created"
                    , body
                    , new List<string> { QACoordinator.Email }
                );
        }
    }
}
