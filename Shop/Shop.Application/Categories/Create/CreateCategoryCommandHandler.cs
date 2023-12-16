using Common.Application;
using Shop.Domain.CategoryAgg;
using Shop.Domain.CategoryAgg.Services;

namespace Shop.Application.Categories.Create
{
    public class CreateCategoryCommandHandler : IBaseCommandHandler<CreateCategoryCommand, long>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICategoryDomainService _categoryDomainService;

        public CreateCategoryCommandHandler(ICategoryRepository categoryRepository,
        ICategoryDomainService categoryDomainService)
        {
            _categoryRepository = categoryRepository;
            _categoryDomainService = categoryDomainService;
        }
        public async Task<OperationResult<long>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = new Category(request.Title, request.Slug, request.SeoData, _categoryDomainService);
            _categoryRepository.Add(category);
            await _categoryRepository.Save();

            return OperationResult<long>.Success(category.Id);
        }
    }

}
