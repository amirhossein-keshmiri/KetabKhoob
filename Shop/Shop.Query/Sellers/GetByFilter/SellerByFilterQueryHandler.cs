using Common.Query;
using Dapper;
using Shop.Infrastructure.Persistent.Dapper;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Sellers.DTOs;

namespace Shop.Query.Sellers.GetByFilter;

public class SellerByFilterQueryHandler : IQueryHandler<GetSellerByFilterQuery, SellerFilterResult>
{
    private readonly ShopContext _context;
    private readonly DapperContext _dapperContext;

    public SellerByFilterQueryHandler(ShopContext context, DapperContext dapperContext)
    {
        _context = context;
        _dapperContext = dapperContext;
    }

    public async Task<SellerFilterResult> Handle(GetSellerByFilterQuery request, CancellationToken cancellationToken)
    {
        var @params = request.FilterParams;
        string conditions = "";

        if (!string.IsNullOrWhiteSpace(@params.NationalCode))
        {
            conditions += $"and s.NationalCode Like N'%{@params.NationalCode}%'";
        }
        if (!string.IsNullOrWhiteSpace(@params.ShopName))
        {
            conditions += $"and s.ShopName Like N'%{@params.ShopName}%'";
        }
        if (@params.Status != null)
        {
            conditions += $"and s.Status={(int)@params.Status}";
        }
        using var connection = _dapperContext.CreateConnection();
        var skip = (@params.PageId - 1) * @params.Take;

        var sql = @$"SELECT s.Id, s.UserId, s.ShopName, s.NationalCode, s.Status, s.LastUpdate, s.CreationDate, 
                    u.Name, u.Family, u.PhoneNumber
            FROM {_dapperContext.Sellers} s inner join {_dapperContext.Users} u on s.UserId=u.Id  
           WHERE 1=1 {conditions} order By s.CreationDate Desc OFFSET @skip ROWS FETCH NEXT @take ROWS ONLY";

        var result = await connection.QueryAsync<SellerDto>(sql, new { skip, take = @params.Take });

        var sqlCount = @$"SELECT Count(s.Id)
            FROM {_dapperContext.Sellers} s inner join {_dapperContext.Users} u on s.UserId=u.Id
            WHERE 1=1 {conditions}";

        var count = await connection.QueryFirstAsync<int>(sqlCount);

        var model = new SellerFilterResult()
        {
            FilterParams = @params,
            Data = result.ToList()
        };
        model.GeneratePaging(count, @params.Take, @params.PageId);
        return model;
    }
}

