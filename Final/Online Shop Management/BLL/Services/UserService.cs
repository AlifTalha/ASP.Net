
using AutoMapper;
using BLL.DTOs;
using DAL;
using DAL.EF.Tables;
using System;
using System.Collections.Generic;

namespace BLL.Services
{
    public class UserService
    {
        private static Dictionary<string, string> ActiveSessions = new Dictionary<string, string>(); // In-memory session store

        private static Mapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDTO>();
                cfg.CreateMap<UserDTO, User>();
            });
            return new Mapper(config);
        }

        public static UserDTO Login(string username, string password)
        {
            var repo = DataAccessFactory.UserData();
            var user = repo.GetByUsername(username);
            if (user != null && user.Password == password)
            {
                var userDto = GetMapper().Map<UserDTO>(user);

                // Generate token
                var token = Guid.NewGuid().ToString();
                ActiveSessions[token] = username;

                userDto.Token = token; // Attach token to the response DTO
                return userDto;
            }
            return null;
        }

        public static bool InvalidateSessionToken(string token)
        {
            if (ActiveSessions.ContainsKey(token))
            {
                ActiveSessions.Remove(token);
                return true;
            }
            return false;
        }

        public static bool IsTokenValid(string token)
        {
            return ActiveSessions.ContainsKey(token);
        }

        public static bool Register(UserDTO userDto)
        {
            var user = GetMapper().Map<User>(userDto);
            var repo = DataAccessFactory.UserData();
            return repo.Create(user);
        }
    }
}
