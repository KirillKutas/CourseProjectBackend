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

        public byte[] GetSalt(string userName)
        {
            return Encoding.ASCII.GetBytes(userName.Insert())
        }

        public bool Verify(byte[] salt, byte[] hashPassword, string password)
        {
            throw new System.NotImplementedException();
        }
    }
}
