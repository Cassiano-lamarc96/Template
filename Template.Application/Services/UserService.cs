using AutoMapper;
using System;
using System.Collections.Generic;
using Template.Application.Interfaces;
using Template.Application.ViewModels;
using Template.Auth.Services;
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

        public UserViewModel GetById(string id)
        {
            if (!Guid.TryParse(id, out Guid userId))
                throw new Exception("UserID is not valid");

            User user = this.userRepository.Find(x => x.Id == userId && !x.IsDeleted);
            if (user == null)
                throw new Exception("User not found");

            return mapper.Map<UserViewModel>(user);
        }

        public bool Put(UserViewModel userViewModel)
        {
            User user = this.userRepository.Find(x => x.Id == userViewModel.Id && !x.IsDeleted);
            if (user == null)
                throw new Exception("User not found");

            return this.userRepository.Update(mapper.Map<User>(userViewModel));
        }

        public bool Delete(string id)
        {
            if (!Guid.TryParse(id, out Guid userId))
                throw new Exception("UserID is not valid");

            User user = this.userRepository.Find(x => x.Id == userId && !x.IsDeleted);
            if (user == null)
                throw new Exception("User not found");

            return this.userRepository.Delete(user);
        }

        public UserAuthenticateResponseViewModel Authenticate(UserAuthenticateRequestViewModel userRequest)
        {
            User user = this.userRepository.Find(x => !x.IsDeleted && x.Email.ToLower() == userRequest.Email.ToLower());

            if (user == null)
                throw new Exception("User not found");

            return new UserAuthenticateResponseViewModel(mapper.Map<UserViewModel>(user), TokenService.GenerateToken(user));
        }
    }
}
