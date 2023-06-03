using System;
using EventSourcingDemo.Combat;
using FluentAssertions;
using Xunit;
using static EventSourcingDemo.Combat.Character;

namespace EventSourcingDemo.CombatSpec.Characters
{
    public class CharacterSpec
    {
        #region Core

        private readonly Character _character;
        private readonly StreamId _streamId;

        public CharacterSpec()
        {
            var entityId = Guid.NewGuid();
            _streamId = new StreamId(CharacterManagementService.Category, entityId);
            _character = From(new CharacterCreated(_streamId, "Mario")).Value;
        }

        #endregion

        #region Test Methods

        [Fact]
        public void WhenSettingAttributes()
        {
            var attributes = new Attributes(20, 0, 20, 10, 2, 20);

            Event @event = _character.Set(attributes);

            @event.Should().Be(new AttributesSet(_streamId, attributes) { Id = @event.Id });
        }

        #endregion

        //#endregion

        //#region Test Methods

        //[Fact]
        //public void WhenModifyingAttributes()
        //{
        //    _character
        //        .ModifyAttributes(new(1, 1, 1, 1, 1, 1))
        //        .ModifyAttributes(new(2, 2, 2, 2, 2, 2));

        //    _character.Events.Should().Contain(x => x.Is(typeof(AttributesModified)));
        //    _character.Attributes.Should().BeEquivalentTo(new Attributes(3, 3, 3, 3, 3, 3));
        //}

        //[Fact]
        //public void WhenRenaming()
        //{
        //    _character.Rename("Maria");

        //    _character.Events.Should().Contain(x => x.Is(typeof(Renamed)));
        //    _character.Name.Should().Be("Maria");
        //}
    }
}
