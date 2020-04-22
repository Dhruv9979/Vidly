using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models;
using Vidly.Dtos;
using AutoMapper;
/*Load this when application is started. Open Global.asax.cs.
    Add Mapper.Initialize(c => c.AddProfile<MappingProfile>()); Here c id for configuration.
    Then go to controller.
 */
namespace Vidly.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to Dto
            Mapper.CreateMap<Customer, CustomerDto>();
            Mapper.CreateMap<Movie, MovieDto>();
            Mapper.CreateMap<MembershipType, MembershipTypeDto>();
            Mapper.CreateMap<Genre, GenreDto>();
            Mapper.CreateMap<Rental, RentalDto>();

            /*Turning this around,when your API gets stuff sent by people, 
             * all the data passes through the Dto first, and then to the Customer Object.
             * Now if you would copy paste the .ForMember line below the outbound mapping "route" you'd 
             * say to your AutoMapper "Hey, don't worry about the id, don't map that.
             * "Now if you would perform a GET request with postman at /api/customers, you'd still get all the data. 
             * Just that every id is 0, because you told AutoMapper to not care about that.*/

            // Dto to Domain
            Mapper.CreateMap<CustomerDto, Customer>()
                .ForMember(c => c.Id, opt => opt.Ignore());

            Mapper.CreateMap<MovieDto, Movie>()
                .ForMember(c => c.Id, opt => opt.Ignore());
        }
    }
}