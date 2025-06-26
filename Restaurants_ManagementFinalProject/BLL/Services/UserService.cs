using AutoMapper;
using BLL.DTOs;
using DAL.EF;
using DAL.Repos;
using System.Collections.Generic;

namespace BLL.Services
{
    public class UserService
    {
        private static IMapper _mapper;

        static UserService()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDTO>();
                cfg.CreateMap<UserDTO, User>();
            });
            _mapper = new Mapper(config);
        }

        public static UserDTO Login(string username, string password)
        {
            var repo = new UserRepository();
            var user = repo.GetByUsername(username);
            if (user != null && user.Password == password)
            {
                return _mapper.Map<UserDTO>(user);
            }
            return null;
        }

        public static bool Register(UserDTO dto)
        {
            var repo = new UserRepository();
            if (repo.Exists(dto.Username)) return false;

            var user = _mapper.Map<User>(dto);
            return repo.Register(user) != null;
        }
    }
}
