using Common.Application;
using Shop.Domain.UserAgg;
using Shop.Domain.UserAgg.Repository;

namespace Shop.Application.Users.ChargeWallet;

internal class ChargeUserWalletCommandHandler : IBaseCommandHandler<ChargeUserWalletCommand>
{
    private readonly IUserRepository _userRepository;

    public ChargeUserWalletCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<OperationResult> Handle(ChargeUserWalletCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetTracking(request.UserId);
        if (user == null)
            return OperationResult.NotFound();

        var wallet = new Wallet(request.Price, request.Description, request.IsFinally, request.Type);
        user.ChargeWallet(wallet);
        await _userRepository.Save();
        return OperationResult.Success();
    }
}


