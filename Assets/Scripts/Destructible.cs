using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Destructible object on scene.
    /// </summary>
    public class Destructible : Entity
    {
        #region Properties
        /// <summary>
        /// Object ignores damage.
        /// </summary>
        [SerializeField] private bool m_Indestructible;
        public bool IsIndestructible => m_Indestructible;

        /// <summary>
        /// Starting quantity hitponts.
        /// </summary>
        [SerializeField] private int m_HitPoints;

        /// <summary>
        /// Current hitpoints.
        /// </summary>
        private int m_CurrentHitPoints;
        public int HitPoints => m_HitPoints;
        #endregion

        #region Unity Events
        protected virtual void Start()
        {
            m_CurrentHitPoints = m_HitPoints;
        }
        #endregion

        #region Public API
        /// <summary>
        /// Applying damage to an object.
        /// </summary>
        /// <param name="damage"> Damage apply object</param>
        public void ApplyDamage(int damage)
        {
            if (m_Indestructible) return;

            m_CurrentHitPoints -= damage;

            if (m_CurrentHitPoints <= 0)
                OnDeath();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Overriding the object destruction event if the hitpoint is below zero.
        /// </summary>
        protected virtual void OnDeath()
        {
            Destroy(gameObject);
        }
        #endregion
    }
}

