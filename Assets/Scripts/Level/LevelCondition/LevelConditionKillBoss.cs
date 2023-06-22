using UnityEngine;

namespace SpaceShooter
{
    public class LevelConditionKillBoss : MonoBehaviour, ILevelCondition
    {
        [SerializeField] private SpaceShip m_BossShip;

        private bool m_IsKill;
        private bool m_Reached;

        private void Start()
        {
            m_BossShip.EventOnDeath.AddListener(IsKill);
        }

        private void IsKill()
        {
            m_IsKill = true;
            m_BossShip.EventOnDeath.RemoveListener(IsKill);
        }


        bool ILevelCondition.IsCompleted
        {
           get
           {
                if (m_IsKill)
                {
                   m_Reached = true;
                }
                return m_Reached;
           }
        }
    }
}
