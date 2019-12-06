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
    public class DeleteCustomerRequestModel : IRequest<DeleteCustomerResponseModelResult>
    {
        [Key]
        public string? CustomerId { get; set; }

       
    }
    public class DeleteCustomerResponseModelResult
    {
        public bool success { get; set; }
    }

    internal class DeleteMovieHandler : IRequestHandler<DeleteCustomerRequestModel, DeleteCustomerResponseModelResult>
    {
        private readonly ICustDataAccessLayer _customerDataAccess;
        public DeleteMovieHandler(ICustDataAccessLayer customerDataAccess)
        {
            _customerDataAccess = customerDataAccess;
        }
        public async Task<DeleteCustomerResponseModelResult> Handle(DeleteCustomerRequestModel request, CancellationToken cancellationToken)
        {
            _customerDataAccess.DeleteCustomers(request.CustomerId);
            return new DeleteCustomerResponseModelResult { success = true };
        }
    }
}
