using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;

namespace TvMaze.Application.Tests.Shared
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AutoMoqDataAttribute : AutoDataAttribute
    {
        public AutoMoqDataAttribute(Action<IFixture> fixtureLogic, int repeatCount = 0) : base(() =>
        {
            var fixture = new Fixture().Customize(new CompositeCustomization(
                new AutoMoqCustomization(), new SupportMutableValueTypesCustomization()));

            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            fixture.RepeatCount = repeatCount;
            fixture.Inject(new CancellationToken(false));
            fixtureLogic(fixture);
            return fixture;
        })
        {
        }

        public AutoMoqDataAttribute(int repeatCount = 0) : this((_) => { }, repeatCount)
        {
        }
    }
}

