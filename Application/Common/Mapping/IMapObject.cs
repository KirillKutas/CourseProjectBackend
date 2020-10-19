using AutoMapper;

namespace Application.Common.Mapping
{
    // ReSharper disable once UnusedTypeParameter
    public interface IMapObject<T>
    {
        void Mapping(Profile profile);
    }
}
