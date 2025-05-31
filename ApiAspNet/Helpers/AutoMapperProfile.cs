using ApiAspNet.Entities;
using ApiAspNet.Models.Users;
using ApiAspNet.Models.Flottes;
using ApiAspNet.Models.Voyages;
using AutoMapper;
using ApiAspNet.Models.offre;
using ApiAspNet.Models.client;
using ApiAspNet.Models.chauffeur;
using ApiAspNet.Models.Agence;
using ApiAspNet.Models.Gestionnaire;
using ApiAspNet.Models.Reservations;

namespace ApiAspNet.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            /*CreateMap<CreateOffreRequest, Offre>();
            CreateMap<UpdateOffreRequest, Offre>();*/
            CreateMap<CreateClientRequest, Client>();
            CreateMap<UpdateClientRequest, Client>();
            CreateMap<CreateChauffeurRequest, Chauffeur>();
            CreateMap<UpdateChauffeurRequest, Chauffeur>();
            CreateMap<CreateAgenceRequest, Agence>();
            CreateMap<UpdateAgenceRequest, Agence>();

            CreateMap<CreateRequestGes, Gestionnaire>();
            CreateMap<UpdateRequests, Gestionnaire>();
            CreateMap<CreateReservationRequest, Reservation>();
            CreateMap<UpdateReservationRequest, Reservation>();


            // CreateRequest -> Flotte 
            CreateMap<CreateRequest, Flotte>();
            // CreateRequest -> Flotte 
            CreateMap<UpdateRequest, Flotte>();

            // CreateRequest -> Voyage 
            /*CreateMap<CreateRequestV, Voyage>();*/
            // CreateRequest -> Voyage 
            /*CreateMap<UpdateRequestV, Voyage>();*/

            // CreateRequest -> User 
            CreateMap<CreateRequests, User>();
            
            // UpdateRequest -> User 
            CreateMap<UpdateRequestUser, User>()
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
