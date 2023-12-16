using Common.Application;
using Shop.Domain.CategoryAgg.Services;
using Shop.Domain.CategoryAgg;

namespace Shop.Application.Categories.AddChild
{
    public class AddChildCategoryCommandHandler : IBaseCommandHandler<AddChildCategoryCommand, long>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICategoryDomainService _categoryDomainService;

        public AddChildCategoryCommandHandler(ICategoryRepository categoryRepository,
        ICategoryDomainService categoryDomainService)
        {
            _categoryRepository = categoryRepository;
            _categoryDomainService = categoryDomainService;
        }
        public async Task<OperationResult<long>> Handle(AddChildCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetTracking(request.ParentId);
            if (category == null)
                return OperationResult<long>.NotFound();

            category.AddChild(request.Title, request.Slug, request.SeoData, _categoryDomainService);
            await _categoryRepository.Save();
            return OperationResult<long>.Success(category.Id);
        }
    }
}
