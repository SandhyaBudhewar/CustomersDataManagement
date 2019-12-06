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
    public class GetCustomerDetailsRequestModel : IRequest<GetCustomerDetailsResponseModelResult>
    {
        [Key]
        public string? CustomerId { get; set; }

    }
    public class GetCustomerDetailsResponseModelResult
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
    internal class GetMovieDetailsHandler : IRequestHandler<GetCustomerDetailsRequestModel, GetCustomerDetailsResponseModelResult>
    {
        private readonly ICustDataAccessLayer _customerDataAccess;
        private readonly IMapper _mapper;
        public GetMovieDetailsHandler(ICustDataAccessLayer customerDataAccess, IMapper mapper)
        {
            _customerDataAccess = customerDataAccess;
            _mapper = mapper;
        }
        public async Task<GetCustomerDetailsResponseModelResult> Handle(GetCustomerDetailsRequestModel request, CancellationToken cancellationToken)
        {
            var movie = _mapper.Map<GetCustomerDetailsResponseModelResult>(_customerDataAccess.GetCustomerData(request.CustomerId));
            return movie;
        }
    }

}
