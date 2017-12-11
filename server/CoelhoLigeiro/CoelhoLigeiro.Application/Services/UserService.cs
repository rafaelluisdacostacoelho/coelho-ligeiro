using CoelhoLigeiro.Application.Interfaces;
using CoelhoLigeiro.Application.Models.Requests;
using CoelhoLigeiro.Application.Models.Responses;
using CoelhoLigeiro.Domain.Interfaces.Repositories;
using CoelhoLigeiro.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoelhoLigeiro.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public int Create(UserRequest user)
        {
            return userRepository.Create(new User
            {
                Id = Guid.Empty,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                Situation = user.Situation,
                SupplierId = user.SupplierId
            });
        }

        public IEnumerable<UserResponse> Read()
        {
            return userRepository
                .Read()
                .Select(user => new UserResponse
                {
                    Id = Guid.Empty,
                    Name = user.Name,
                    Email = user.Email,
                    Password = user.Password,
                    Situation = user.Situation,
                    SupplierId = user.SupplierId
                });
        }

        public void Update(Guid id, UserRequest user)
        {
            userRepository.Update(new User
            {
                Id = id,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                Situation = user.Situation,
                SupplierId = user.SupplierId
            });
        }

        public void Delete(Guid id)
        {
            userRepository.Delete(id);
        }

        public UserResponse GetById(Guid id)
        {
            var user = userRepository.GetById(id);

            return new UserResponse
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                Situation = user.Situation,
                SupplierId = user.SupplierId
            };
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}