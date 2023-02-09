using AutoFixture.Xunit2;
using Xunit.Sdk;

namespace TvMaze.Application.Tests.Shared
{
    public class InlineAutoMoqDataAttribute : CompositeDataAttribute
    {
        public InlineAutoMoqDataAttribute(params object[] values)
            : base(
                new DataAttribute[] {
                    new InlineDataAttribute(values),
                    new AutoMoqDataAttribute() })
        {
        }
    }
}
