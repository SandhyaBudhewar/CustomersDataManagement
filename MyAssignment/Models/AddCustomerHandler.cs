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
    public class AddCustomerRequestModel : IRequest<AddCustomerResponseModelResult>
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
}
public class AddCustomerResponseModelResult
{
    public bool Success { get; set; }
}
internal class AddMovieHandler : IRequestHandler<AddCustomerRequestModel, AddCustomerResponseModelResult>
{
    private readonly ICustDataAccessLayer _customerDataAccess;
    private readonly IMapper _mapper;
    public AddMovieHandler(ICustDataAccessLayer customerDataAccess, IMapper mapper)
    {
        _customerDataAccess = customerDataAccess;
        _mapper = mapper;
    }
    public async Task<AddCustomerResponseModelResult> Handle(AddCustomerRequestModel request, CancellationToken cancellationToken)
    {
        _customerDataAccess.AddCustomers(_mapper.Map<Customers>(request));
        return new AddCustomerResponseModelResult() { Success = true };
    }
}

