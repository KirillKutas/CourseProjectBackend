using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Extensions
{
    public class Enum<T> where T : Enum
    {
        public static int MinValue
        {
            get
            {
                if (!typeof(T).IsEnum)
                    throw new ArgumentException("T must be an enumerated type");

                return Enum.GetValues(typeof(T)).Cast<int>().Min();
            }
        }

        public static int MaxValue
        {
            get
            {
                if (!typeof(T).IsEnum)
                    throw new ArgumentException("T must be an enumerated type");

                return Enum.GetValues(typeof(T)).Cast<int>().Max();
            }
        }

        public static int Length
        {
            get
            {
                if (!typeof(T).IsEnum)
                    throw new ArgumentException("T must be an enumerated type");

                return Enum.GetNames(typeof(T)).Length;
            }
        }

        public static IEnumerable<T> Values
        {
            get
            {
                if (!typeof(T).IsEnum)
                    throw new ArgumentException("T must be an enumerated type");

                return Enum.GetValues(typeof(T)).OfType<T>();
            }
        }

        public static T Parse(string value)
        {
            return (T)Enum.Parse(typeof(T), value);
        }
    }

}