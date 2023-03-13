using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;

namespace TvMaze.Application.Tests.Shared;

public class TheoryAutoDataAttribute : AutoDataAttribute
{
    public TheoryAutoDataAttribute(Action<IFixture> fixtureLogic, int repeatCount = 0) : base(() =>
    {
        var fixture = new Fixture().Customize(new CompositeCustomization(
            new AutoMoqCustomization() { ConfigureMembers = true },
            new SupportMutableValueTypesCustomization()));

        fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => fixture.Behaviors.Remove(b));
        fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        fixture.RepeatCount = repeatCount;
        fixture.Inject(new CancellationToken(false));
        fixtureLogic(fixture);
        return fixture;
    })
    {
    }

    public TheoryAutoDataAttribute(int repeatCount = 0) : this((_) => { }, repeatCount)
    {
    }
}