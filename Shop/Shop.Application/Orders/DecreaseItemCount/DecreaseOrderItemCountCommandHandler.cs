using Common.Application;
using Shop.Domain.OrderAgg.Repository;

namespace Shop.Application.Orders.DecreaseItemCount;

public class DecreaseOrderItemCountCommandHandler : IBaseCommandHandler<DecreaseOrderItemCountCommand>
{
    private readonly IOrderRepository _orderRepository;

    public DecreaseOrderItemCountCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    public async Task<OperationResult> Handle(DecreaseOrderItemCountCommand request, CancellationToken cancellationToken)
    {
        var currentOrder = await _orderRepository.GetCurrentUserOrder(request.UserId);
        if (currentOrder == null)
            return OperationResult.NotFound();

        currentOrder.DecreaseItemCount(request.ItemId, request.Count);
        await _orderRepository.Save();
        return OperationResult.Success();
    }
}

