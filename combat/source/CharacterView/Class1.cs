using System;
using System.Collections.Generic;
using System.Linq;

namespace EventSourcingDemo.Combat.CharacterView
{
    public class Aggregator : IAggregator
    {
        private readonly IViewRepository _repository;
        private readonly IEventStore _store;
        private static readonly Dictionary<Type, Func<CharacterView, Event, CharacterView>> Handlers = new();

        #region Creation

        static Aggregator()
        {
            Register<CharacterCreated>(Handle);
            Register<CharacterRenamed>(Handle);
        }

        public Aggregator(IEventStore store, IViewRepository repository)
        {
            _store = store;
            _repository = repository;
        }

        #endregion

        #region IAggregator Implementation

        public Result Start() =>
            _repository.Create("CharacterView")
                .Bind(() => _store.Find(new(CharacterManagementService.Category)))
                .Bind(Process)
                .Bind(view => _repository.Update("CharacterView", view));

        #endregion

        #region Static Interface

        private static CharacterView Handle(CharacterView view, CharacterCreated @event) =>
            new(
                view.Characters.Union(new Character[] { new(@event.EntityId, @event.Name) }).ToHashSet()
            );

        private static CharacterView Handle(CharacterView view, CharacterRenamed @event)
        {
            var renamedCharacter = view.Characters.First(x => x.Id == @event.EntityId) with { Name = @event.Name };

            return new HashSet<Character> { renamedCharacter }
                .Union(view.Characters)
                .ToHashSet();
        }

        private static Result<CharacterView> Process(params Event[] stream)
        {
            using var events = stream.ToList().GetEnumerator();

            CharacterView view = new(new());

            while (events.MoveNext())
            {
                var type = events.Current.GetType();

                if (Handlers.ContainsKey(type))
                    view = Handlers[type](view, events.Current);
            }

            return view;
        }

        private static void Register<T>(Func<CharacterView, T, CharacterView> handler) where T : Event
        {
            Handlers.Add(typeof(T), (view, @event) => handler(view, (T) @event));
        }

        #endregion
    }

    public record CharacterCreated(string Name) : Event;

    public record CharacterRenamed(string Name) : Event;

    public record Character(Guid Id, string Name);

    public record CharacterView(HashSet<Character> Characters) : View
    {
        #region Static Interface

        public static implicit operator CharacterView(HashSet<Character> source) => new(source);

        #endregion
    }
}
