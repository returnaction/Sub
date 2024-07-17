using AutoMapper;
using Sub.Models.Entities.User.User;

namespace Sub.Automapper.User.User
{
    public class SignUpMapper : Profile
    {
        public SignUpMapper()
        {
            CreateMap<Models.Entities.User.User.User, SignUpVM>().ReverseMap();
        }
    }
}
