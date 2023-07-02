using EventSourcingDemo.Combat;
using Xunit;

namespace EventSourcingDemo.CombatSpec.Streams
{
    public class WhenInstantiatingACategoryStream
    {
        #region Requirements

        [Fact]
        public void ThenAllEventsBelongToTheSpecifiedCategory()
        {
            var streamId = new CategoryStreamId("Foo");
            var categoryStream = CategoryStream.From(
                new Bar { Category = "Foo" },
                new Bat { Category = "Foo" }
            );
        }

        #endregion
    }

    public record Bar : Event;

    public record Bat : Event;

    public record Baz : Event;
}
