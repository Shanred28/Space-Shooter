using UnityEngine;
namespace SpaceShooter
{
    public class PowerupBoostStat : Powerup
    {
        public enum EffectStats
        {
            Invulnerability,
            BoostSpeed
        }

        [SerializeField] private EffectStats m_EffectStats;
        [SerializeField] private float m_ValueTimerInvulnerability;
        [SerializeField] private float m_ValueTimetBoostSpeed;
        [SerializeField] private float m_BoostSpeed;


        protected override void OnPickedUp(SpaceShip ship)
        {
            if (m_EffectStats == EffectStats.Invulnerability)
               ship.AddInvulnerability(m_ValueTimerInvulnerability);

            if(m_EffectStats == EffectStats.BoostSpeed)
                ship.AddThrust(m_ValueTimetBoostSpeed, m_BoostSpeed);
        }
    }
}
