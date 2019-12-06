using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using System.Threading;

namespace MyAssignment.Models
{
    public class ChangePasswordRequestModel : IRequest<ChangePasswordResponseModelResult>
    {
        [Display(Name = "Username")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email ID required")]
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Minimum 6 characters required")]
        public string NewPassword { get; set; }

        
    }
    public class ChangePasswordResponseModelResult
    {
        public bool Success { get; set; }
    }
    internal class ChangePasswordHandler : IRequestHandler<ChangePasswordRequestModel, ChangePasswordResponseModelResult>
    {
        IUserDataAccessLayer _userDataAccessLayer;
        public ChangePasswordHandler(IUserDataAccessLayer userDataAccessLayer)
        {
            _userDataAccessLayer = userDataAccessLayer;
        }
        public async Task<ChangePasswordResponseModelResult> Handle(ChangePasswordRequestModel request, CancellationToken cancellationToken)
        {
            return new ChangePasswordResponseModelResult() { Success = _userDataAccessLayer.UpdatePassword(request.Username, request.NewPassword) };
        }
    }
}


