using Common.Application;
using MediatR;
using Shop.Application.Comments.ChangeStatus;
using Shop.Application.Comments.Create;
using Shop.Application.Comments.Delete;
using Shop.Application.Comments.Edit;
using Shop.Query.Comments.DTOs;
using Shop.Query.Comments.GetByFilter;
using Shop.Query.Comments.GetById;

namespace Shop.Presentation.Facade.Comments;
internal class CommentFacade : ICommentFacade
{
    private readonly IMediator _mediatR;

    public CommentFacade(IMediator mediatR)
    {
        _mediatR = mediatR;
    }

    public async Task<OperationResult> ChangeStatus(ChangeCommentStatusCommand command)
    {
        return await _mediatR.Send(command);
    }

    public async Task<OperationResult> CreateComment(CreateCommentCommand command)
    {
        return await _mediatR.Send(command);
    }

    public async Task<OperationResult> DeleteComment(DeleteCommentCommand command)
    {
        return await _mediatR.Send(command);
    }

    public async Task<OperationResult> EditComment(EditCommentCommand command)
    {
        return await _mediatR.Send(command);
    }

    public async Task<CommentDto?> GetCommentById(long id)
    {
        return await _mediatR.Send(new GetCommentByIdQuery(id));
    }

    public async Task<CommentFilterResult> GetCommentsByFilter(CommentFilterParams filterParams)
    {
       return await _mediatR.Send(new GetCommentByFilterQuery(filterParams));
    }
}

