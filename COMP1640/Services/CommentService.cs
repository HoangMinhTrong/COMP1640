using COMP1640.ViewModels.Comment.Requests;
using Domain;
using Domain.Interfaces;

namespace COMP1640.Services;

public class CommentService
{
    private readonly ICommentRepository _commentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CommentService(ICommentRepository commentRepository, IUnitOfWork unitOfWork)
    {
        _commentRepository = commentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> CommentIdea(CommentIdeaRequest commentIdeaRequest)
    {
        var comment = new Comment(commentIdeaRequest.Content, commentIdeaRequest.IdeaId);
        await _commentRepository.InsertAsync(comment);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }
}