using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Shop.Application._Utilities;
using Shop.Domain.ProductAgg.Repository;

namespace Shop.Application.Products.RemoveImage;

internal class RemoveProductImageCommandHandler : IBaseCommandHandler<RemoveProductImageCommand>
{
    private readonly IProductRepository _productRepository;
    private readonly IFileService _fileService;

    public RemoveProductImageCommandHandler(IProductRepository productRepository, IFileService fileService)
    {
        _productRepository = productRepository;
        _fileService = fileService;
    }

    public async Task<OperationResult> Handle(RemoveProductImageCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetTracking(request.ProductId);
        if (product == null)
            return OperationResult.NotFound();

        var imageName = product.RemoveImage(request.ImageId);
        await _productRepository.Save();
        _fileService.DeleteFile(Directories.ProductGalleryImage, imageName);
        return OperationResult.Success();
    }
}
