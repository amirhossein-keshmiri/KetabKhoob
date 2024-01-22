using Common.Query;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.SellerAgg.Enums;
using Shop.Infrastructure.Persistent.Dapper;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Comments.DTOs;

namespace Shop.Query.Comments.GetByFilter;
internal class GetCommentByFilterQueryHandler : IQueryHandler<GetCommentByFilterQuery, CommentFilterResult>
{
    private readonly ShopContext _context;
    private readonly DapperContext _dapperContext;

    public GetCommentByFilterQueryHandler(ShopContext context, DapperContext dapperContext)
    {
        _context = context;
        _dapperContext = dapperContext;
    }
    public async Task<CommentFilterResult> Handle(GetCommentByFilterQuery request, CancellationToken cancellationToken)
    {
        var @params = request.FilterParams;
        string conditions = "";

        //var result = _context.Comments.OrderByDescending(d => d.CreationDate).AsQueryable();
        //if (@params.ProductId != null)
        //    result = result.Where(r => r.ProductId == @params.ProductId);
        //if (@params.UserId != null)
        //    result = result.Where(r => r.UserId == @params.UserId);
        //if (@params.CommentStatus != null)
        //    result = result.Where(r => r.Status == @params.CommentStatus);
        //if (@params.StartDate != null)
        //    result = result.Where(r => r.CreationDate.Date >= @params.StartDate.Value.Date);

        //if (@params.EndDate != null)
        //    result = result.Where(r => r.CreationDate.Date <= @params.EndDate.Value.Date);

        if (@params.ProductId != null)
        {
            conditions += $"and c.ProductId={@params.ProductId}";
        }

        if (@params.ProductTitle != null)
        {
            conditions += $"and p.Title Like N'%{@params.ProductTitle}%'";
        }

        if (@params.UserId != null)
        {
            conditions += $"and c.UserId={@params.UserId}";
        }

        if (@params.UserFullName != null)
        {
            conditions += $"and (u.Name Like N'%{@params.UserFullName}%' OR u.Family Like N'%{@params.UserFullName}%')";
        }

        if (@params.CommentStatus != null)
        {
            conditions += $"and c.Status={(int)@params.CommentStatus}";
        }

        if (@params.StartDate != null)
        {
            conditions += $"and c.CreationDate>='{@params.StartDate.Value.Date}'";
        }

        if (@params.EndDate != null)
        {
            conditions += $"and c.CreationDate<='{@params.EndDate.Value.Date}'";
        }

       
        using var connection = _dapperContext.CreateConnection();

        var skip = (@params.PageId - 1) * @params.Take;

        var sql = @$"SELECT c.Id, c.UserId , c.ProductId ,c.Text , c.Status, c.CreationDate, 
                    u.Name , u.Family, 
                    p.Title as ProductTitle
            FROM {_dapperContext.Comments} c inner join {_dapperContext.Users} u on c.UserId=u.Id  
           inner join {_dapperContext.Products} p on c.ProductId=p.Id WHERE 1=1 {conditions} order By c.CreationDate Desc OFFSET @skip ROWS FETCH NEXT @take ROWS ONLY";

       
        var result = await connection.QueryAsync<CommentDto>(sql, new { skip, take = @params.Take });

        var sqlCount = @$"SELECT Count(c.Id)
            FROM {_dapperContext.Comments} c inner join {_dapperContext.Users} u on c.UserId=u.Id
            inner join {_dapperContext.Products} p on c.ProductId=p.Id WHERE 1=1 {conditions}";

        var count = await connection.QueryFirstAsync<int>(sqlCount);


        //var skip = (@params.PageId - 1) * @params.Take;
        var model = new CommentFilterResult()
        {
            Data = result.ToList(),
            FilterParams = @params
        };
        model.GeneratePaging(count, @params.Take, @params.PageId);
        return model;
    }
}

