using COMP1640.ViewModels.EmailModel;
using Domain;
using Domain.DomainEvents;
using Domain.Interfaces;
using MediatR;
using Utilities.Constants;
using Utilities.EmailService.Interface;
using Utilities.EmailService.Interfaces;

namespace COMP1640.DomainHandlers
{
    public class OnIdeaApprovalDomainHandler : INotificationHandler<IdeaApprovalDomainEvent>
    {
        private readonly IRazorViewRenderer _razorViewRenderer;
        private readonly IEmailSender _emailSender;
        private readonly IDepartmentRepository _departmentRepo;
        private readonly IConfiguration _configuration;
        public OnIdeaApprovalDomainHandler(IEmailSender emailSender
            , IRazorViewRenderer razorViewRenderer
            , IDepartmentRepository departmentRepo
            , IConfiguration configuration)
        {
            _emailSender = emailSender;
            _razorViewRenderer = razorViewRenderer;
            _departmentRepo = departmentRepo;
            _configuration = configuration;
        }

        public async Task Handle(IdeaApprovalDomainEvent @event, CancellationToken cancellationToken)
        {
            var department = await _departmentRepo.GetAsync(@event.Idea.DepartmentId);
            var QACoordinator = department?.QaCoordinator;
            if (QACoordinator == null)
                return;

            var model = new IdeaApprovalEmailModel(@event.Idea, _configuration.GetSection(AppSetting.APIRoute).Value);
            string body = null;
            switch (@event.Idea.Status)
            {
                case IdeaStatusEnum.Approved:
                    body = await _razorViewRenderer.RenderToStringAsync("OnIdeaApprovedEmailTemplate", model);
                    break;
                case IdeaStatusEnum.Rejected:
                    body = await _razorViewRenderer.RenderToStringAsync("OnIdeaRejectedEmailTemplate", model);
                    break;
            }

            if (string.IsNullOrEmpty(body))
                return;

            await _emailSender.SendEmailAsync
                (
                    $"[COMP1640]: Idea \"{@event.Idea.Title}\" Was {@event.Idea.Status.ToString()}"
                    , body
                    , new List<string> { @event.Idea.CreatedByNavigation.Email}
                );
        }
    }
}
