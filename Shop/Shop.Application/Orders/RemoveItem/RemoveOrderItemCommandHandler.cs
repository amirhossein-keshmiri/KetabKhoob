using Common.Application;
using Shop.Domain.OrderAgg.Repository;

namespace Shop.Application.Orders.RemoveItem;

public class RemoveOrderItemCommandHandler : IBaseCommandHandler<RemoveOrderItemCommand>
{
    private readonly IOrderRepository _orderRepository;

    public RemoveOrderItemCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    public async Task<OperationResult> Handle(RemoveOrderItemCommand request, CancellationToken cancellationToken)
    {
        var currentOrder = await _orderRepository.GetCurrentUserOrder(request.UserId);
        if (currentOrder == null)
            return OperationResult.NotFound();

        currentOrder.RemoveItem(request.ItemId);
        await _orderRepository.Save();
        return OperationResult.Success();
    }
}


