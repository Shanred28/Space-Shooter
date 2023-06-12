using UnityEngine;
namespace SpaceShooter
{
    public class TurretsStzcionarTuurel : MonoBehaviour
    {
        [SerializeField] private TurretMode m_Mode;
        public TurretMode Mode => m_Mode;

        [SerializeField] private TurretProperties m_TurretProperties;

        private float m_RefireTimer;

        public bool CanFire => m_RefireTimer <= 0;

        private TurretPoint m_Turret;

        [SerializeField] private AudioSource m_AudioSource;


        #region Unity Event 
        private void Start()
        {
            m_Turret = transform.root.GetComponent<TurretPoint>();
        }

        private void Update()
        {
            if (m_RefireTimer > 0)
                m_RefireTimer -= Time.deltaTime;
        }
        #endregion


        #region Public API

        public void Fire()
        {
            if (m_TurretProperties == null) return;

            if (m_RefireTimer > 0) return;

            Debug.Log(m_TurretProperties.EnergyUsage);
            //if (m_Turret.DrawEnergy(m_TurretProperties.EnergyUsage) == false) return;

            Debug.Log("ѕытаюсь создать снар€д3");

            if (m_Turret.DrawAmmo(m_TurretProperties.AmmoUsage) == false) return;

            Debug.Log("ѕытаюсь создать снар€д!!!!!");
            Projectile projectile = Instantiate(m_TurretProperties.ProjectilePrefab).GetComponent<Projectile>();
            projectile.transform.position = transform.position;
            projectile.transform.up = transform.up;

            projectile.SetPerentShooter(m_Turret);

            m_RefireTimer = m_TurretProperties.RateOfFire;

            m_AudioSource.PlayOneShot(m_TurretProperties.LaunchSFX);

        }

        public void AssignLoadout(TurretProperties props)
        {
            if (m_Mode != props.Mode) return;

            m_RefireTimer = 0;
            m_TurretProperties = props;
        }
        #endregion
    }
}
