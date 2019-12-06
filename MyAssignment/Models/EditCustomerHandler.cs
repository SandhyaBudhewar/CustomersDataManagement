using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MyAssignment.Models;
using System.Threading;

namespace MyAssignment.Models
{
    public class EditCustomerRequestModel : IRequest<EditCustomerResponseModelResult>
    {
        [Key]
        public string CustomerId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string PaymentCategory { get; set; }

        [Required]
        public string Phone { get; set; }

    }
    public class EditCustomerResponseModelResult
    {
        public bool Success { get; set; }
    }
    internal class EditCustomerHandler : IRequestHandler<EditCustomerRequestModel, EditCustomerResponseModelResult>
    {
        private readonly ICustDataAccessLayer _customerDataAccess;
        private readonly IMapper _mapper;
        public EditCustomerHandler(ICustDataAccessLayer customerDataAccess, IMapper mapper)
        {
            _customerDataAccess = customerDataAccess;
            _mapper = mapper;
        }
        public async Task<EditCustomerResponseModelResult> Handle(EditCustomerRequestModel request, CancellationToken cancellationToken)
        {
            _customerDataAccess.UpdateCustomers(_mapper.Map<Customers>(request));
            return new EditCustomerResponseModelResult() { Success = true };
        }
    }
}
