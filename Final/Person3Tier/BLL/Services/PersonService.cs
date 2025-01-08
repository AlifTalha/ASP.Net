using AutoMapper;
using BLL.DTOs;
using DAL.EF;
using DAL.Repos;
using System;
using System.Collections.Generic;

namespace BLL.Services
{
    public class PersonService
    {
        // AutoMapper Configuration
        private static MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Person, PersonDTO>().ReverseMap();
        });

        private static Mapper mapper = new Mapper(config);

        public static List<PersonDTO> Get()
        {
            var repo = new PersonRepo();
            var data = repo.GetAll();
            return mapper.Map<List<PersonDTO>>(data);
        }

        public static PersonDTO Get(int id)
        {
            var repo = new PersonRepo();
            var data = repo.GetById(id);
            return mapper.Map<PersonDTO>(data);
        }

        public static void Create(PersonDTO personDTO)
        {
            var repo = new PersonRepo();
            var entity = mapper.Map<Person>(personDTO);
            repo.Create(entity);
        }

        public static void Delete(int id)
        {
            var repo = new PersonRepo();
            repo.Delete(id);
        }
    }
}
