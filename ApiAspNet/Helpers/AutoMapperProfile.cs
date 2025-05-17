using ApiAspNet.Entities;
using ApiAspNet.Models.Users;
using AutoMapper;

namespace ApiAspNet.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
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
