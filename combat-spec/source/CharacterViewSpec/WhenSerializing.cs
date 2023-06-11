using System;
using System.Collections.Generic;
using System.Text.Json;
using EventSourcingDemo.Combat.CharacterView;
using FluentAssertions;
using Xunit;

namespace EventSourcingDemo.CombatSpec.CharacterViewSpec
{
    public class WhenSerializing
    {
        #region Requirements

        [Fact]
        public void ThenOutputIsExpected()
        {
            Guid marioId, mallowId;

            var characters = new HashSet<Character>
            {
                new(marioId = Guid.NewGuid(), "Mario"),
                new(mallowId = Guid.NewGuid(), "Mallow")
            };

            var characterView = new CharacterView(characters);

            var options = new JsonSerializerOptions { WriteIndented = true };

            var json = JsonSerializer.Serialize(characterView, options);

            var deserializedView = JsonSerializer.Deserialize<CharacterView>(json);
            deserializedView.Characters.Should().BeEquivalentTo(characterView.Characters);

//            json.Should().Be(
//                @$"{{
//  ""Characters"": [
//    {{
//      ""Name"": ""Mario"",
//      ""Id"": ""{marioId}""
//    }},
//    {{
//      ""Name"": ""Mallow"",
//      ""Id"": ""{mallowId}""
//    }}
//  ]
//}}"
//            );
        }

        #endregion
    }
}
