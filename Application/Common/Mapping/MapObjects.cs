using AutoMapper;

namespace Application.Common.Mapping
{
    public class BaseMapFrom<T> : IMapObject<T>
    {
        public virtual void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
    }

    public class BaseMapFromTo<T> : IMapObject<T>
    {
        public virtual void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType()).ReverseMap();
    }
}
