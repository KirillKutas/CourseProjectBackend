using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MediatR;
using Xunit;

namespace Application.UnitTests
{
    public class CheckUnitTestExisting
    {
        [Theory]
        [InlineData(typeof(IBaseRequest))]
        public void UnitTests_ShouldExist(Type typeArg)
        {
            var expectedUnitTests = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => typeArg.IsAssignableFrom(p) && p.IsClass)
                .Select(x => $"{x.Name}Tests");

            var actualUnitTests = Assembly.GetExecutingAssembly().GetTypes();

            var missingUnitTests = new List<string>();
            foreach (var expectedUnitTest in expectedUnitTests)
            {
                if (actualUnitTests.All(x => x.Name != expectedUnitTest))
                {
                    missingUnitTests.Add(expectedUnitTest);
                }
            }

            Assert.False(missingUnitTests.Any(), string.Join(Environment.NewLine + new string(' ', 4), new[] { "Following unit tests are expected:" }.Concat(missingUnitTests)));
        }
    }
}