using ApiAspNet.Entities;
using ApiAspNet.Models.Users;
using ApiAspNet.Models.Flottes;
using ApiAspNet.Models.Voyages;
using AutoMapper;

namespace ApiAspNet.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            // CreateRequest -> Flotte 
            CreateMap<CreateRequest, Flotte>();
            // CreateRequest -> Flotte 
            CreateMap<UpdateRequest, Flotte>();

            // CreateRequest -> Voyage 
            CreateMap<CreateRequestV, Voyage>();
            // CreateRequest -> Voyage 
            CreateMap<UpdateRequestV, Voyage>();

            // CreateRequest -> User 
            CreateMap<CreateRequests, User>();
            
            // UpdateRequest -> User 
            CreateMap<UpdateRequests, User>()
                .ForAllMembers(x => x.Condition(
                    (src, dest, prop) =>
                    {
                        // ignore both null & empty string properties 
                        if (prop == null) return false;
                        if (prop.GetType() == typeof(string) &&
    string.IsNullOrEmpty((string)prop)) return false;

                        // ignore null role 
                        if (x.DestinationMember.Name == "Role" && src.Role ==
    null) return false;

                        return true;
                    }
                ));
        }
    }
}
