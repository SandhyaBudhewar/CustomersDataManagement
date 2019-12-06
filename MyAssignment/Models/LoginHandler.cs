using AutoMapper;
using System.ComponentModel.DataAnnotations;
using MediatR;
using MyAssignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;



namespace MyAssignment.Models
{
    public class LoginRequestModel : IRequest<LoginResponseModelResult>
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Minimum 6 characters required")]
        public string Password { get; set; }
    }
    public class LoginResponseModelResult
    {
        public bool Success { get; set; }
        public string ResponseText { get; set; }
    }
    internal class LoginHandler : IRequestHandler<LoginRequestModel, LoginResponseModelResult>
    {
        IUserDataAccessLayer _userDataAccessLayer;
        IMapper _mapper;

        public LoginHandler(IUserDataAccessLayer userDataAccessLayer, IMapper mapper)
        {
            _userDataAccessLayer = userDataAccessLayer;
            _mapper = mapper;
        }
        public async Task<LoginResponseModelResult> Handle(LoginRequestModel request, CancellationToken cancellationToken)
        {
            bool success = _userDataAccessLayer.CheckLogin(_mapper.Map<Users>(request));
            return new LoginResponseModelResult() { Success = success, ResponseText = "Login Successfull" };
        }
    }
}