using Common.Application;
using Shop.Domain.OrderAgg;
using Shop.Domain.OrderAgg.Repository;
using Shop.Domain.OrderAgg.ValueObjects;
using Shop.Domain.SiteEntities.Repositories;

namespace Shop.Application.Orders.Checkout;

public class CheckoutOrderCommandHandler : IBaseCommandHandler<CheckoutOrderCommand>
{
    private readonly IOrderRepository _orderRepository;
    private IShippingMethodRepository _shippingMethodRepository;
    public CheckoutOrderCommandHandler(IOrderRepository orderRepository, IShippingMethodRepository shippingMethodRepository)
    {
        _orderRepository = orderRepository;
        _shippingMethodRepository = shippingMethodRepository;
    }
    public async Task<OperationResult> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
    {
        var currentOrder = await _orderRepository.GetCurrentUserOrder(request.UserId);
        if (currentOrder == null)
            return OperationResult.NotFound();

        var address = new OrderAddress(request.State, request.City, request.PostalCode,
                 request.PostalAddress, request.PhoneNumber, request.Name,
                 request.Family, request.NationalCode);

        var shippingMethod = await _shippingMethodRepository.GetAsync(request.ShippingMethodId);
        if (shippingMethod == null)
            return OperationResult.Error();

        currentOrder.CheckOut(address, new OrderShippingMethod(shippingMethod.Title, shippingMethod.Cost));

        await _orderRepository.Save();
        return OperationResult.Success();
    }
}

