using UnityEngine;

namespace SpaceShooter
{
    public class DeathExplosion : MonoBehaviour
    {
        [SerializeField] private SpaceShip m_Ship;
        [SerializeField] private Destructible destructible;
        [SerializeField] private GameObject m_ExplosionPrefab;
        private GameObject m_Explosion;

        private void Start()
        {
            destructible.EventOnDeath.AddListener(Explosion);
            
        }

        private void Explosion()
        {
           
            m_Explosion = Instantiate(m_ExplosionPrefab);
            m_Explosion.transform.position = m_Ship.transform.position;

        }

    }
}

