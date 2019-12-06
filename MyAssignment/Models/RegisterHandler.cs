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
    public class RegisterRequestModel : IRequest<RegisterResponseModelResult>
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Minimum 6 characters required")]
        public string Password { get; set; }
    }
    public class RegisterResponseModelResult
    {
        public bool Success { get; set; }
        public string ResponseText { get; set; }
    }
    internal class RegisterHandler : IRequestHandler<RegisterRequestModel, RegisterResponseModelResult>
    {
        IUserDataAccessLayer _userDataAccessLayer;
        IMapper _mapper;

        public RegisterHandler(IUserDataAccessLayer userDataAccessLayer, IMapper mapper)
        {
            _userDataAccessLayer = userDataAccessLayer;
            _mapper = mapper;
        }
        public async Task<RegisterResponseModelResult> Handle(RegisterRequestModel request, CancellationToken cancellationToken)
        {
            bool success = _userDataAccessLayer.AddUser(_mapper.Map<Users>(request));
            return new RegisterResponseModelResult() { Success = success, ResponseText = "Register Successfull" };
        }
    }
}

