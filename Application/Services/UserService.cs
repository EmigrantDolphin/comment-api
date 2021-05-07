using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using Application.Models;
using Domain.Entities;
using Persistence;

namespace Application.Services
{
    public interface IUserService
    {
        Task CreateUserAsync(UserCreateModel userCreateModel);
        Task<UserModel> GetUserByUsernameAndPasswordAsync(string username, string password);
    }

    public class UserService : IUserService 
    {
        private readonly CommentContext _context;

        public UserService(CommentContext context)
        {
            _context = context;
        }

        public async Task CreateUserAsync(UserCreateModel userCreateModel)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username.Equals(userCreateModel.Username));

            if (user != null)
            {
                throw new ApplicationException("User with this username already exists");
            }

            var newUser = new User(
                userCreateModel.Username,
                userCreateModel.Password,
                userCreateModel.FullName
            );

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
        }


        public async Task<UserModel> GetUserByUsernameAndPasswordAsync(string username, string password)
        {
            var user = await _context.Users
                .Where(x => x.Username.Equals(username))
                .Where(x => x.Password.Equals(password))
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ApplicationException("Username or Password don't match");
            }

            return UserModel.ToModel(user);
        } 
    }
}