using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyAssignment.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Users, LoginRequestModel>();
            CreateMap<LoginRequestModel, Users>();

            CreateMap<Users, RegisterRequestModel>();
            CreateMap<RegisterRequestModel, Users>();

            CreateMap<Customers, AddCustomerRequestModel>();
            CreateMap<AddCustomerRequestModel, Customers>();

            CreateMap<Customers, EditCustomerRequestModel>();
            CreateMap<EditCustomerRequestModel, Customers>();

         //   CreateMap<Customers, DeleteCustomerRequestModel>();
          //  CreateMap<DeleteCustomerRequestModel, Customers>();

            CreateMap<Customers, GetCustomerDetailsResponseModelResult>();
            CreateMap<GetCustomerDetailsResponseModelResult, Customers>();

            CreateMap<GetCustomerDetailsResponseModelResult, EditCustomerRequestModel>();
            CreateMap<EditCustomerRequestModel, GetCustomerDetailsResponseModelResult>();



        }
    }
}
