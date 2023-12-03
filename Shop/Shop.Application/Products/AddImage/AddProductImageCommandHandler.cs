using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Shop.Application._Utilities;
using Shop.Domain.ProductAgg;
using Shop.Domain.ProductAgg.Repository;

namespace Shop.Application.Products.AddImage;

internal class AddProductImageCommandHandler : IBaseCommandHandler<AddProductImageCommand>
{
    private readonly IProductRepository _productRepository;
    private readonly IFileService _fileService;

    public AddProductImageCommandHandler(IProductRepository productRepository, IFileService fileService)
    {
        _productRepository = productRepository;
        _fileService = fileService;
    }

    public async Task<OperationResult> Handle(AddProductImageCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetTracking(request.ProductId);
        if (product == null)
            return OperationResult.NotFound();

        var imageName = await _fileService
             .SaveFileAndGenerateName(request.ImageFile, Directories.ProductGalleryImage);

        var productImage = new ProductImage(imageName, request.Sequence);
        product.AddImage(productImage);
        await _productRepository.Save();
        return OperationResult.Success();
    }
}

