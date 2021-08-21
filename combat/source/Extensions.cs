using System;

namespace EventSourcingDemo.CombatSpec
{
    public static class Extensions
    {
        #region Static Interface

        public static bool Is(this object o, Type t) => o.GetType() == t;

        #endregion
    }
}
