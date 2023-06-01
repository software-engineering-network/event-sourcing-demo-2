﻿using System;

namespace EventSourcingDemo.Combat
{
    public abstract class Entity
    {
        #region Creation

        protected Entity(Guid id)
        {
            Id = id;
        }

        #endregion

        #region Public Interface

        public Guid Id { get; }

        #endregion
    }
}
