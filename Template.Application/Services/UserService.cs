using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Template.Application.Interfaces;
using Template.Application.ViewModels;
using Template.Domain.Entities;
using Template.Domain.Interfaces;

namespace Template.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        public List<UserViewModel> Get()
        {
            return mapper.Map<List<UserViewModel>>(this.userRepository.GetAll());
        }

        public bool Post(UserViewModel userViewModel)
        {
            this.userRepository.Create(mapper.Map<User>(userViewModel));

            return true;
        }
    }
}
