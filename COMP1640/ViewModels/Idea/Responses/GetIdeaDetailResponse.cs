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
        public UserReaction? UserReacted { get; set; }


        public Expression<Func<Domain.Idea , GetIdeaDetailResponse>> GetSelection(int userId)
        {
            var userReactionSelection = new UserReaction().GetSelection().Compile();

            return _ => new GetIdeaDetailResponse
            {
                Id = _.Id,
                Title = _.Title,
                Content = _.Content,
                Department = _.Department.Name,
                Author = _.IsAnonymous ? null : new IdeaAuthor
                {
                    Id = _.CreatedByNavigation.Id,
                    UserName = _.CreatedByNavigation.UserName,
                },
                CreatedOn = _.CreatedOn,
                UserRole = _.CreatedByNavigation.RoleUsers.Select(r => r.Role.Name).FirstOrDefault(),
                LikeCount = _.Reactions.Where(r => r.Status == ReactionStatusEnum.Like).Count(),
                DislikeCount = _.Reactions.Where(r => r.Status == ReactionStatusEnum.DisLike).Count(),
                CommentCount = _.Comments.Count(),
                Category = _.Category.Name,
                IsAnonymous = _.IsAnonymous,
                UserReacted = userReactionSelection(_.Reactions.FirstOrDefault(r => r.UserId == userId && r.IdeaId == _.Id)),
            };
        }       

    }
}