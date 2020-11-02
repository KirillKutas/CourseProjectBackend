
namespace Application.Common.Interfaces
{
    public interface IHashService
    {
        byte[] GetSalt(string userName, out byte[] salt);
        byte[] GetHash(byte[] salt, string password);
        bool Verify(byte[] salt, byte[] hashPassword, string password);
    }
}
