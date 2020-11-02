using System;
using System.Text;
using Application.Common.Interfaces;

namespace Infrastructure.Services
{
    class HashService : IHashService
    {
        public byte[] GetHash(byte[] salt, string password)
        {
            return Encoding.ASCII.GetBytes(BCrypt.Net.BCrypt.HashPassword(password + Encoding.ASCII.GetString(salt)));
        }

        public byte[] GetSalt(string userName, out byte[] salt)
        {
            var rnd = new Random();
            salt = Encoding.ASCII.GetBytes(userName.Insert(rnd.Next(0, userName.Length - 1), userName.ToUpper()));
            return salt;
        }

        public bool Verify(byte[] salt, byte[] hashPassword, string password)
        {
            return BCrypt.Net.BCrypt.Verify(Encoding.ASCII.GetString(salt) + password, Encoding.ASCII.GetString(hashPassword));
        }
    }
}
