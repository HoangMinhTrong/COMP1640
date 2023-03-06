using COMP1640.ViewModels.EmailModel;
using Domain.DomainEvents;
using Domain.Interfaces;
using MediatR;
using Utilities.Constants;
using Utilities.EmailService.Interface;
using Utilities.EmailService.Interfaces;

namespace COMP1640.DomainHandlers
{
    public class OnCreateIdeaDomainHandler : INotificationHandler<CreateIdeaDomainEvent>
    {
        private readonly IRazorViewRenderer _razorViewRenderer;
        private readonly IEmailSender _emailSender;
        private readonly IDepartmentRepository _departmentRepo;
        private readonly IConfiguration _configuration;
        public OnCreateIdeaDomainHandler(IEmailSender emailSender
            , IRazorViewRenderer razorViewRenderer
            , IDepartmentRepository departmentRepo
            , IConfiguration configuration)
        {
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

            var model = new IdeaAddedEmailModel(@event.Idea, _configuration.GetSection(AppSetting.APIRoute).Value);
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
