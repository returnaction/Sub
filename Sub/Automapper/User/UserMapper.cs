using AutoMapper;
using Sub.Models.Entities.User.User;

namespace Sub.Automapper.User.User
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<Models.Entities.User.User.User, SignUpVM>().ReverseMap();
            CreateMap<Models.Entities.User.User.User, UserEditVM>().ReverseMap();

        }
    }
}
