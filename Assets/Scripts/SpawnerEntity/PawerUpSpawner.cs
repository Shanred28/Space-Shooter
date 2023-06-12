using UnityEngine;

namespace SpaceShooter
{
    public class PawerUpSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject[] m_PowerUpPref;
        [SerializeField] private CircleArea m_Area;
        [SerializeField] private int m_NumPowerUp;
        [SerializeField] private float m_TimeSpawn;

        private float m_Timer;

        private void Start () 
        {
            m_Timer = m_TimeSpawn;
        }
        private void Update()
        {
            if (m_Timer > 0)
                m_Timer -= Time.deltaTime;

            if (m_Timer < 0)
            {
                SpawnPawerUp();

                m_Timer = m_TimeSpawn;
            }
        }

        private void SpawnPawerUp()
        {
            for (int i = 0; i < m_NumPowerUp; i++)
            {
                int index = Random.Range(0, m_PowerUpPref.Length);

                GameObject e = Instantiate(m_PowerUpPref[index].gameObject);

                e.transform.position = m_Area.GetRandominsideZone();
            }
        }
    }
}
