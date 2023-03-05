using COMP1640.ViewModels.Attachment.Responses;
using COMP1640.ViewModels.Common;
using Domain;
using System.Linq.Expressions;


namespace COMP1640.ViewModels.Idea.Responses
{
    public class GetIdeaDetailResponse
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Department { get; set; }
        public IdeaAuthor Author { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UserRole { get; set; }
        public int LikeCount { get; set; }
        public int DislikeCount { get; set; }
        public int CommentCount { get; set; }
        public string Category { get; set; }
        public bool IsAnonymous { get; set; }
        public List<AttachmentResponse> Attachments{ get; set; }

        public Expression<Func<Domain.Idea , GetIdeaDetailResponse>> GetSelection()
        {
            return _ => new GetIdeaDetailResponse
            {
                Id = _.Id,
                Title = _.Title,
                Content = _.Content,
                Department = _.Department.Name,
                Author = new IdeaAuthor
                {
                    Id = _.CreatedBy,
                    UserName = _.CreatedByNavigation.UserName
                },
                CreatedOn = _.CreatedOn,
                UserRole = _.CreatedByNavigation.RoleUsers.Select(r => r.Role.Name).FirstOrDefault(),
                LikeCount = _.Reactions.Where(r => r.Status == ReactionStatusEnum.Like).Count(),
                DislikeCount = _.Reactions.Where(r => r.Status == ReactionStatusEnum.DisLike).Count(),
                CommentCount = _.Comments.Count(),
                Category = _.Category.Name,
                IsAnonymous = _.IsAnonymous,
            };
        }       

    }
}