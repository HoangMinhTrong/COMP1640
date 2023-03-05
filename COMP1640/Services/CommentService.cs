using System.Security.Claims;
using COMP1640.ViewModels.Comment.Requests;
using COMP1640.ViewModels.Comment.Responses;
using Domain;
using Domain.Interfaces;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace COMP1640.Services;

public class CommentService
{
    private readonly ICommentRepository _commentRepository;
    private readonly ApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;


    public CommentService(ICommentRepository commentRepository, IUnitOfWork unitOfWork, ApplicationDbContext context)
    {
        _commentRepository = commentRepository;
        _unitOfWork = unitOfWork;
        _context = context;
    }

    public async Task<bool> CommentIdea(CommentIdeaRequest commentIdeaRequest)
    {
        var comment = new Comment(commentIdeaRequest.Content, commentIdeaRequest.IdeaId);
        await _commentRepository.InsertAsync(comment);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<List<CommentInfoResponse>> CommentList(int ideaId)
    {
        var comments = await _context.Comments.Where(x => x.IdeaId == ideaId).ToListAsync();
        var commentInfos = new List<CommentInfoResponse>();
        foreach (var comment in comments)
        {
            var commentInfo = new CommentInfoResponse(comment.Id, comment.Content, comment.CreatedByNavigation.UserName, comment.CreatedByNavigation.Id);
            commentInfos.Add(commentInfo);
        }

        return commentInfos;
    }
}