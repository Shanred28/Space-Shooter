using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter
{
    public class Player : SingletonBase<Player>
    {
        [SerializeField] private int m_NumLives;
        public int LivesPlayer => m_NumLives;
        [SerializeField] private SpaceShip m_Ship;
        [SerializeField] private GameObject m_PlayerShipPrefab;
        public SpaceShip ActiveShip => m_Ship;

        [SerializeField] private CameraController m_CameraController;
        [SerializeField] private MovementController m_MovementController;


        protected override void Awake()
        {
            base.Awake();
            if(m_Ship != null)
                Destroy(m_Ship.gameObject);
        }

        private void Start()
        {
            Respawn();
        }

        private void OnShipDeath()
        {
            m_NumLives--;
            if (m_NumLives > 0)
            {
                Respawn();
            }
            else
            { 
                LevelSequenceController.Instance.FinishCurrentLevel(false);
            }
                
        }

        private void Respawn()
        {
            if (LevelSequenceController.PlayerShip != null)
            {
                var newPlayerShip = Instantiate(LevelSequenceController.PlayerShip);

                m_Ship = newPlayerShip.GetComponent<SpaceShip>();

                m_CameraController.SetTarget(m_Ship.transform);
                m_MovementController.SetTargetShip(m_Ship);
                m_Ship.EventOnDeath.AddListener(OnShipDeath);
            }          
        }

        #region Score
        public int Score { get; private set; }
        public int NumKills { get; private set; }

        public void AddKill()
        {
            ++NumKills;
        }

        public void AddScore(int num)
        { 
            Score += num;
        }

        #endregion
    }
}

