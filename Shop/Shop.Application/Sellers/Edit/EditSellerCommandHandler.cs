using Common.Application;
using Shop.Domain.SellerAgg.Repository;
using Shop.Domain.SellerAgg.Services;

namespace Shop.Application.Sellers.Edit;

internal class EditSellerCommandHandler : IBaseCommandHandler<EditSellerCommand>
{
    private readonly ISellerRepository _sellerRepository;
    private readonly ISellerDomainService _domainService;

    public EditSellerCommandHandler(ISellerRepository sellerRepository, ISellerDomainService domainService)
    {
        _sellerRepository = sellerRepository;
        _domainService = domainService;
    }

    public async Task<OperationResult> Handle(EditSellerCommand request, CancellationToken cancellationToken)
    {
        var seller = await _sellerRepository.GetTracking(request.Id);
        if (seller == null)
            return OperationResult.NotFound();

        seller.Edit(request.ShopName, request.NationalCode,request.Status, _domainService);
        await _sellerRepository.Save();
        return OperationResult.Success();
    }
}
