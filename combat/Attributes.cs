namespace EventSourcingDemo.Combat
{
    public class Attributes
    {
        #region Creation

        public Attributes(
            ushort attack,
            ushort defense,
            ushort hitPoints,
            ushort magicAttack,
            ushort magicDefense,
            ushort speed
        )
        {
            Attack = attack;
            Defense = defense;
            HitPoints = hitPoints;
            MagicAttack = magicAttack;
            MagicDefense = magicDefense;
            Speed = speed;
        }

        #endregion

        #region Public Interface

        public ushort Attack { get; }
        public ushort Defense { get; }
        public ushort HitPoints { get; }
        public ushort MagicAttack { get; }
        public ushort MagicDefense { get; }
        public ushort Speed { get; }

        #endregion
    }
}
