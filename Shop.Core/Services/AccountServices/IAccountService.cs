using Shop.Core.DTO.AllAccountDTO;
using Shop.Core.DTO.AllAddressDTO;
using Shop.Core.DTO.Shared;
using Shop.Domain.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Core.Services.AccountServices
{
    public interface IAccountService
    {
        public Task<string> RegisterAsync(RegisterDTO registerDTO);
        public  Task SendEmail(string email, string code, string component, string subject, string message);

        Task<bool> SendEmailForForgetPassword(string Email);
        Task<ResultView<LoginDTO>> LoginAsync(LoginDTO loginDTO);
        Task<string> ResetPassword(RestPasswordDTO restPasswordDTO);
        Task<bool> ActiveAccount(ActiveAccountDTO activeAccountDTO);
          Task<AddressDTO> getUserAddress(string email);
        Task<bool> UpdateAddress(string email, UpdateAddressDTO addressDTO);
    }
}
