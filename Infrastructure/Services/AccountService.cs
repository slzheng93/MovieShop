using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Servicces;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using ApplicationCore.Entities;

namespace Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;

        public AccountService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Register(UserRegisterRequestModel model)
        {
            // check if user has not entered already registered email
            var user = await _userRepository.GetUserByEmail(model.Email);
            if (user != null)
            {
                throw new Exception("email already exists , please login again");
            }

            // register the user, with hashing and salt
            // create a random uniqiee salt

            var salt = GetRandomSalt();

            // generate the hash with salt

            var hashedPassword = GetHashedPassword(model.Password, salt);

            // save the user (user info with salt and hashedpassword) in the user table

            var newUser = new User
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Salt = salt,
                HashedPassword = hashedPassword,
                DateOfBirth = model.DateOfBirth
            };

            var dbCreatedUser = await _userRepository.Add(newUser);

            if (dbCreatedUser.Id > 1)
            {
                return true;
            }
            return false;
        }

        public Task<UserLoginResponseModel> Validate(string email, string password)
        {
            // 
            throw new NotImplementedException();
        }

        private string GetRandomSalt()
        {
            byte[] salt = new byte[128 / 8];
            using (var rngCsp = RandomNumberGenerator.Create())
            {
                rngCsp.GetBytes(salt);
            }
            return Convert.ToBase64String(salt);
        }

        private string GetHashedPassword(string password, string salt)
        {
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: Convert.FromBase64String(salt),
            prf: KeyDerivationPrf.HMACSHA512,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));

            return hashed;
        }

    }
}
