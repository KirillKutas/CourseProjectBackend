
namespace Application.Common.Interfaces
{
    public interface IHashService
    {
        void CreatePasswordHash(string password, out byte[] passwordSalt, out byte[] passwordHash);
        bool Verify(byte[] storedSalt, byte[] storedHash, string password);
    }
}
