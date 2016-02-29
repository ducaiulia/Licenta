using System.Web.Helpers;
using AllShare.Services.Account;
using AllShare.Services.DTOs;
using AllShare.Services.Utils;
using Nelibur.ObjectMapper;
using Action = AllShare.Services.Utils.Action;

namespace AllShare.Models.Builders
{
    public class AccountViewModelBuilder: IViewModelBuilder<AccountViewModel>
    {
        private IAccountService AccountService { get; set; }
        private readonly AccountInput _accountInput;
        public AccountViewModelBuilder(IAccountService accountService, AccountInput input)
        {
            AccountService = accountService;
            _accountInput = input;
        }
        public AccountViewModel Build()
        {
            if (_accountInput.Action == Action.Login)
            {
                var result = AccountService.GetUser(_accountInput.Username);
                if (result.ResultType == ResultType.Error)
                    return null;
                if (!Crypto.VerifyHashedPassword(result.Result.Password, _accountInput.Password))
                    return null;
                return TinyMapper.Map<AccountViewModel>(result.Result);
            }

            if (_accountInput.Action == Action.Register)
            {
                _accountInput.Password = Crypto.HashPassword(_accountInput.Password);
                var dto = TinyMapper.Map<UserDTO>(_accountInput);
                var result = AccountService.Register(dto);
                if (result.ResultType == ResultType.Error)
                    return null;
                return TinyMapper.Map<AccountViewModel>(result.Result);
            }

            return null;
        }
    }
}