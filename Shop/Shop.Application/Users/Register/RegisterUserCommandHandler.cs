using Common.Application;
using Common.Application.SecurityUtil;
using Shop.Domain.UserAgg;
using Shop.Domain.UserAgg.Repository;
using Shop.Domain.UserAgg.Services;

namespace Shop.Application.Users.Register;

internal class RegisterUserCommandHandler : IBaseCommandHandler<RegisterUserCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserDomainService _domainService;

    public RegisterUserCommandHandler(IUserRepository userRepository, IUserDomainService domainService)
    {
        _userRepository = userRepository;
        _domainService = domainService;
    }

    public async Task<OperationResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = User.RegisterUser(request.PhoneNumber.Value, Sha256Hasher.Hash(request.Password), _domainService);

        _userRepository.Add(user);
        await _userRepository.Save();
        return OperationResult.Success();
    }
}

