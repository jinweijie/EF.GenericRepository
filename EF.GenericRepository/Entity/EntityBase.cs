using System;

namespace EF.GenericRepository.Entity
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TId">The type of the id.</typeparam>
    [Serializable]
    public abstract class EntityBase<TId>
    {
        #region abstract member

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>
        /// The id.
        /// </value>
        public abstract TId Id { get; set; }

        #endregion

        #region public member

        #endregion

        #region override methods

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            return Equals(obj as EntityBase<TId>);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return Equals(Id, default(TId)) ? base.GetHashCode() : Id.GetHashCode();
        }

        #endregion

        #region public methods

        /// <summary>
        /// Equalses the specified other.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        public virtual bool Equals(EntityBase<TId> other)
        {
            if (other == null) return false;

            if (ReferenceEquals(this, other)) return true;

            if (!IsEntityTransient(this) && !IsEntityTransient(other) && Equals(Id, other.Id))
            {
                var otherType = other.GetUnproxiedType();
                var thisType = GetUnproxiedType();
                return thisType.IsAssignableFrom(otherType) ||
                    otherType.IsAssignableFrom(thisType);
            }

            return false;
        }

        /// <summary>
        /// Determines whether this instance is transient.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is transient; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool IsTransient()
        {
            return Id.Equals(default(TId));
        }

        #endregion

        #region protected methods

        /// <summary>
        /// Determines whether [is entity transient] [the specified entity].
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>
        ///   <c>true</c> if [is entity transient] [the specified entity]; otherwise, <c>false</c>.
        /// </returns>
        protected virtual bool IsEntityTransient(EntityBase<TId> entity)
        {
            bool result = (entity != null) &&
                            (Object.Equals(entity.Id, default(TId))
                                || Object.Equals(entity.Id, null));

            return result;
        }

        /// <summary>
        /// Gets the type of the unproxied.
        /// </summary>
        /// <returns></returns>
        protected virtual Type GetUnproxiedType()
        {
            return GetType();
        }

        #endregion
    }
}
