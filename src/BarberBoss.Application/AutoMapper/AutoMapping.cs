using AutoMapper;
using BarberBoss.Communication.Request;
using BarberBoss.Communication.Response;
using BarberBoss.Domain.Entities;

namespace BarberBoss.Application.AutoMapper
{
    public class AutoMapping: Profile
    {
        public AutoMapping() 
        {
            RequestToEntity();
            EntityToRequest();
        }

        private void RequestToEntity()
        {
            CreateMap<RequestBillJson, Bill>()
                .ForMember(bill => bill.Tags , config => config.MapFrom(source => source.Tags.Distinct()));
            CreateMap<UserRequestJson, User>()
                .ForMember(user => user.Password , config => config.Ignore());
            CreateMap<Domain.Enums.Tag, Tag>()
                .ForMember(bill =>bill.TagValue , config => config.MapFrom(source => source));
            
        }

        private void EntityToRequest()
        {
            CreateMap<Bill, ResponseBillJson>()
                .ForMember(dest => dest.Tags , config =>  config.MapFrom(source => source.Tags.Select(tag => tag.TagValue)));
            CreateMap<User, UserRequestJson>();
            CreateMap<User, ResponseUserProfileJson>();
            
        }
    }
}
