using System.Security.Claims;
using COMP1640.ViewModels.Comment.Requests;
using COMP1640.ViewModels.Comment.Responses;
using Domain;
using Domain.DomainEvents;
using Domain.Interfaces;
using Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace COMP1640.Services;

public class CommentService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ICommentRepository _commentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CommentService(ICommentRepository commentRepository
        , IUnitOfWork unitOfWork
        , IServiceProvider serviceProvider)
    {
        _commentRepository = commentRepository;
        _unitOfWork = unitOfWork;
        _serviceProvider = serviceProvider;
    }

    public async Task<bool> CommentIdea(CommentIdeaRequest commentIdeaRequest)
    {
        var comment = new Comment(commentIdeaRequest.Content, commentIdeaRequest.IdeaId, commentIdeaRequest.IsAnonymous);
        await _commentRepository.InsertAsync(comment);
        await _unitOfWork.SaveChangesAsync();

        await HandleSendMailOnAddCommentAsync(comment);
        return true;
    }

    public async Task<List<CommentInfoResponse>> CommentList(int ideaId)
    {
        var comments =  await _commentRepository.GetQuery(x => x.IdeaId == ideaId).ToListAsync();
        var commentInfos = new List<CommentInfoResponse>();
        foreach (var comment in comments)
        {
            var commentInfo = new CommentInfoResponse(comment.Id, comment.Content, 
                comment.CreatedByNavigation.UserName, 
                comment.CreatedByNavigation.Id,
                comment.IsAnonymous);
            commentInfos.Add(commentInfo);
        }

        return commentInfos;
    }

    #region Send Mail
    private async Task HandleSendMailOnAddCommentAsync(Comment comment)
    {
        var mediator = _serviceProvider.GetService<IMediator>();
        if (mediator != null)
            await mediator.Publish(new AddCommentDomainEvent(comment));
    }

    #endregion
}