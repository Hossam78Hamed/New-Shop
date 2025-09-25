using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Core.DTO.AllAccountDTO;
using Microsoft.Identity.Client;
using Shop.Domain.Accounts;
using Shop.Core.Services.EmailServices;
using Microsoft.AspNetCore.Identity;

using static Org.BouncyCastle.Crypto.Engines.SM2Engine;
using Shop.Core.DTO.Shared;
using Shop.Core.Services.AddressServices;
using Shop.Core.interfaces;
using Shop.Core.DTO.AllAddressDTO;
using  AutoMapper;
namespace Shop.Core.Services.AccountServices
{
    public class AccountService : IAccountService
    {
        

        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
    
        private readonly IEmailService emailService;
        private readonly IGenerateToken generateToken;
        private readonly IAddressRepository addressRepository;
        private readonly IMapper mapper;

        public AccountService( UserManager<ApplicationUser> _userManager,IEmailService _emailService, SignInManager<ApplicationUser> _signInManager, IGenerateToken _generateToken, IAddressRepository _addressRepository, IMapper _mapper) {
            
            userManager = _userManager;
            emailService = _emailService;
            signInManager = _signInManager;
            generateToken= _generateToken;
            addressRepository = _addressRepository;
            mapper = _mapper;
        } 
      

        public async Task<string> RegisterAsync(RegisterDTO registerDTO)
        {
            ApplicationUser applicationUser = new ApplicationUser();

            if (registerDTO != null)
            {
                applicationUser.UserName = registerDTO.UserName;
                applicationUser.Email = registerDTO.Email;
                applicationUser.DisplayName = registerDTO.DisplayName;
            }
            if ( await userManager.FindByNameAsync(registerDTO.UserName)!=null) {
                return "this UserName is already registerd";
            }

            if (await userManager.FindByEmailAsync(registerDTO.Email) != null)
            {
                return "this Email is already registerd";
            }

            ////----------------Create User
            var result= await userManager.CreateAsync(applicationUser, registerDTO.Password);

            if (!result.Succeeded) { 
             return result.Errors.ToList() [0].Description;
            }
            ////-----------Send Emial
           var code=await userManager.GenerateEmailConfirmationTokenAsync(applicationUser);
           await SendEmail(registerDTO.Email,code, "active", "ActiveEmail", "Please active your email, click on button to active");



            ////--------------------
            return "Done";
          
        }

        public async Task SendEmail(string email, string code, string component, string subject, string message)
        { EmailDTO emailDTO =new EmailDTO(email,
            "adsa",subject,
            ///a.j47sharp@gmail.com
            EmailStringBody.send(email, code, component, message)
            );
          
            await  emailService.SendEmail(emailDTO);

        }

        public async Task<ResultView<LoginDTO>> LoginAsync(LoginDTO loginDTO) {

            ResultView<LoginDTO> resultView = new ResultView<LoginDTO>(); 
            if (loginDTO != null)
            {
                ApplicationUser? findUser = await userManager.FindByEmailAsync(loginDTO.Email);
                if (!findUser.EmailConfirmed)
                {
                    var code = await userManager.GenerateEmailConfirmationTokenAsync(findUser);
                    await SendEmail(loginDTO.Email, code, "active", "ActiveEmail", "Please active your email, click on button to active");
                    resultView.IsSuccess = false; 
                    resultView.Msg= "Please confirem your email first, we have send activat to your E-mail";
                    resultView.Data = loginDTO;
                    return resultView;
                    // return "Please confirem your email first, we have send activat to your E-mail";

                }
                var result = await signInManager.CheckPasswordSignInAsync(findUser, loginDTO.Password, lockoutOnFailure: true);
                if (result.Succeeded)
                {

                    //return generateToken.GetAndCreateToken(findUser); ;

                    resultView.IsSuccess = true;
                    resultView.Msg = generateToken.GetAndCreateToken(findUser);
                    resultView.Data = loginDTO;
                    return resultView;
                }
            }
               // return "please check your email and password, something went wrong";
            resultView.IsSuccess = false;
            resultView.Msg = "please check your email and password, something went wrong";
            resultView.Data = loginDTO;
            return resultView;

        }

        public async Task<bool> SendEmailForForgetPassword(string Email) {

            var findUser = await userManager.FindByEmailAsync(Email);
            if (findUser != null) {
                var token = await userManager.GeneratePasswordResetTokenAsync(findUser);
                await SendEmail(findUser.Email, token, "Reset-Password", "Rest pssword", "click on button to Reset your password");
                return true;

            }
            return false;
        }

        public async Task<string> ResetPassword(RestPasswordDTO restPasswordDTO) {
            var findUser = await userManager.FindByEmailAsync(restPasswordDTO.Email);
            if (findUser is null)
            {
                return null; 
            
            }
            var result = await userManager.ResetPasswordAsync(findUser, restPasswordDTO.Token, restPasswordDTO.Password);
            if (result.Succeeded) { return "Done"; }
                else {
                    return result.Errors.ToList()[0].Description;
                
                }

        }


        public async Task<bool> ActiveAccount(ActiveAccountDTO activeAccountDTO)
        {
            var findUser = await userManager.FindByEmailAsync(activeAccountDTO.Email);
            if (findUser is null) { return false; }

            var result = await userManager.ConfirmEmailAsync(findUser, activeAccountDTO.Token);

            if (result.Succeeded) { return true; }
            ////clear 2 line
            var token = await userManager.GenerateEmailConfirmationTokenAsync(findUser);

            await SendEmail(findUser.Email,token, "active", "ActiveEmail", "Please active your email, click on button to active");
            
            return false;
           
        }

        public async Task<AddressDTO> getUserAddress(string email)
        {
            var user=await userManager.FindByEmailAsync(email);
            var address = (await addressRepository.GetAllAsync())
               .FirstOrDefault(a => a.ApplicationUserId == user.Id);


            if (address != null) {

               var addressDTO=  mapper.Map<AddressDTO>(address);               
                return addressDTO; }
            else return null;

        }

        public async Task<bool> UpdateAddress(string email, UpdateAddressDTO addressDTO)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null) return false;

            var address = (await addressRepository.GetAllAsync())
                      .FirstOrDefault(a=>a.ApplicationUserId==user.Id) ;
           
            
            if (address == null)
            {
                var newAddress = mapper.Map<Address>(addressDTO);
                newAddress.ApplicationUserId =user.Id;
                var dbaddress = await addressRepository.AddAsync(newAddress);
                return true;
            }
            else
            {
                //context.Entry(Myaddress).State = EntityState.Detached;
                mapper.Map(addressDTO,address);
                //var newAddress = mapper.Map<Address>(addressDTO);
                //newAddress.Id = address.Id;
                //newAddress.ApplicationUserId = user.Id;
           
                var dbaddress = await addressRepository.UpdateAsync(address);
                return true;
            }
            
            







            throw new NotImplementedException();
        }

        ///----------
    }
}
