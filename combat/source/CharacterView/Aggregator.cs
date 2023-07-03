using System;
using System.Collections.Generic;
using System.Linq;
using static EventSourcingDemo.Combat.Event;

namespace EventSourcingDemo.Combat.CharacterView
{
    /// <summary>
    ///     make composite
    /// </summary>
    public class Aggregator :
        IAggregator,
        IHandler<CharacterCreated>,
        IHandler<CharacterRenamed>
    {
        private const string Key = "CharacterView";
        private readonly IViewRepository _repository;
        private readonly IEventStore _store;
        private static readonly Dictionary<Type, Func<CharacterView, Event, CharacterView>> Handlers = new();

        #region Creation

        static Aggregator()
        {
            Register<CharacterCreated>(CreateCharacter);
            Register<CharacterRenamed>(RenameCharacter);
        }

        public Aggregator(IEventStore store, IViewRepository repository)
        {
            _store = store;
            _repository = repository;
        }

        #endregion

        #region IAggregator Implementation

        public Result Start() =>
            _repository.Create(Key, new CharacterView(new()))
                .Bind(() => _store.Find(CharacterManager.StreamId))
                .Bind(Process)
                .Bind(view => _repository.Update(Key, view));

        #endregion

        #region IHandler<CharacterCreated> Implementation

        public Result Handle(CharacterCreated @event) =>
            _repository.Find(Key)
                .Bind(
                    view =>
                    {
                        var next = CreateCharacter((CharacterView) view, @event);
                        return _repository.Update(Key, next);
                    }
                );

        #endregion

        #region IHandler<CharacterRenamed> Implementation

        public Result Handle(CharacterRenamed @event) =>
            _repository.Find(Key)
                .Bind(
                    view =>
                    {
                        var next = RenameCharacter((CharacterView) view, @event);
                        return _repository.Update(Key, next);
                    }
                );

        #endregion

        #region Static Interface

        private static CharacterView CreateCharacter(CharacterView view, CharacterCreated @event) =>
            new(
                view.Characters.Union(new Character[] { new(@event.EntityId, @event.Name) }).ToHashSet()
            );

        private static Result<CharacterView> Process(CategoryStream stream)
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

        private static CharacterView RenameCharacter(CharacterView view, CharacterRenamed @event)
        {
            var renamedCharacter = view.Characters.First(x => x.Id == @event.EntityId) with { Name = @event.Name };

            return new HashSet<Character> { renamedCharacter }
                .Union(view.Characters.Where(x => x.Id != renamedCharacter.Id))
                .ToHashSet();
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
