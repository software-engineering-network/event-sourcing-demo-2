using EventSourcingDemo.Combat;
using FluentAssertions;
using Xunit;

namespace EventSourcingDemo.CombatSpec.CategoryStreams
{
    public class WhenInstantiating
    {
        #region Requirements

        [Fact]
        public void WithMoreThanOneCategory_ThenReturnError()
        {
            var streamId = new CategoryStreamId("Foo");
            var error = CategoryStream.From(
                new Bar { Category = "Foo" },
                new Bat { Category = "Foo" },
                new Baz { Category = "Baz" }
            ).Error;

            error.Should().Be(CategoryStream.EventsFromDifferentCategories());
        }

        #endregion
    }

    public record Bar : Event;

    public record Bat : Event;

    public record Baz : Event;
}
