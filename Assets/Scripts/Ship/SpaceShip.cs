using System.Collections;
using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SpaceShip : Destructible
    {
        #region Properties
        /// <summary>
        /// Weight to indicate in Rigidbody.
        /// </summary>
        [Header("Space ship")]
        [SerializeField] private float m_Mass;

        /// <summary>
        /// Force pushing forward.
        /// </summary>
        [SerializeField] private float m_Thrust;

        /// <summary>
        /// Rotation force.
        /// </summary>
        [SerializeField] private float m_Mobility;

        /// <summary>
        /// Maximum linear speed.
        /// </summary>
        [SerializeField] private float m_MaxLinearVelocity;
        public float MaxLinearVelocity => m_MaxLinearVelocity;

        /// <summary>
        /// Maximum rotation speed. In Degrees/Seconds.
        /// </summary>
        [SerializeField] private float m_MaxAngularVelocity;
        public float MaxAngularVelocity => m_MaxAngularVelocity;

        [SerializeField] private Sprite m_PreviewImage;
        public Sprite PreviewImage => m_PreviewImage;

        /// <summary>
        /// Reference to Rigidbody2D.
        /// </summary>
        private Rigidbody2D m_Rigid;

        #endregion

        #region Public API
        /// <summary>
        /// Linear thrust control. From -1.0 to +1.0.
        /// </summary>
        public float ThrustControl { get; set; }

        /// <summary>
        /// Rotational thrust control. From -1.0 to +1.0.
        /// </summary>
        public float TorqueControl { get; set; }
        #endregion

        #region Unity Event
        protected override void Start()
        {
            base.Start();

            m_Rigid = GetComponent<Rigidbody2D>();
            m_Rigid.mass = m_Mass;
            m_Rigid.inertia = 1;

            InitOffensive();
        }

        private void FixedUpdate()
        {
            UpdateRigiBody();
            UpdateEnergyRegen();
        }
        #endregion

        /// <summary>
        /// Method of adding forces to control the ship.
        /// </summary>
        private void UpdateRigiBody()
        {
            m_Rigid.AddForce(ThrustControl * m_Thrust * transform.up * Time.fixedDeltaTime, ForceMode2D.Force);
            m_Rigid.AddForce(-m_Rigid.velocity * (m_Thrust / m_MaxLinearVelocity) * Time.fixedDeltaTime, ForceMode2D.Force);

            m_Rigid.AddTorque(TorqueControl * m_Mobility * Time.fixedDeltaTime, ForceMode2D.Force);
            m_Rigid.AddTorque(-m_Rigid.angularVelocity * (m_Mobility / m_MaxAngularVelocity) * Time.fixedDeltaTime, ForceMode2D.Force);
        }

        [SerializeField] private Turret[] m_Turrets;

        public void Fire(TurretMode mode)
        {
            for (int i = 0; i < m_Turrets.Length; i++)
            {
                if (m_Turrets[i].Mode == mode)
                {
                    m_Turrets[i].Fire();
                }           
            }
        }

        [SerializeField] private int m_MaxEnergy;
        public int MaxEnergy => m_MaxEnergy;
        [SerializeField] private int m_MaxAmmo;
        [SerializeField] private int m_EnergyRegenPerSecond;

        private float m_CurrentEnergy;
        public float CurrentEnergy => m_CurrentEnergy;
        private int m_CurrentAmmo;
        public int CurrentAmmo => m_CurrentAmmo;

        public void AddEnergy(int e)
        {
            m_CurrentEnergy = Mathf.Clamp(m_CurrentEnergy + e, 0, m_MaxEnergy);        
        }

        public void AddAmmo(int ammo)
        {
            m_CurrentAmmo = Mathf.Clamp(m_CurrentAmmo + ammo, 0, m_MaxAmmo);
        }

        private void InitOffensive()
        {
            m_CurrentEnergy = m_MaxEnergy;
            m_CurrentAmmo = m_MaxAmmo;
        }

        private void UpdateEnergyRegen()
        {
            m_CurrentEnergy += (float)m_EnergyRegenPerSecond * Time.deltaTime;
            m_CurrentEnergy = Mathf.Clamp(m_CurrentEnergy, 0, m_MaxEnergy);
        }

        public bool DrawEnergy(int count)
        {
            if (count == 0)
                return true;

            if (m_CurrentEnergy >= count)
            {
                m_CurrentEnergy -= count;
                return true;
            }
            return false;
        }

        public bool DrawAmmo(int count)
        {
            if (count == 0)
                return true;

            if (m_CurrentAmmo >= count)
            { 
                m_CurrentAmmo -= count;
                return true;
            }
            return false;
        }

        public void AssignWeapon(TurretProperties props)
        { 
            for (int i = 0; i < m_Turrets.Length; i++) 
            {
                m_Turrets[i].AssignLoadout(props);
            }
        }

        [SerializeField] Transform visualModelSheld;
        public void AddInvulnerability(float timer)
        { 
            m_Indestructible = true;
            visualModelSheld.gameObject.SetActive(true);
            StartCoroutine(WaitTimerInvulnerability(timer));

        }

        public void AddThrust(float timer, float value)
        {
           float tempThrust = m_Thrust;
            m_Thrust = value;
            StartCoroutine(WaitTimerThrust(timer, tempThrust));
        }

        IEnumerator WaitTimerInvulnerability(float timer)
        {
            yield return new WaitForSeconds(timer);
            m_Indestructible = false;
            visualModelSheld.gameObject.SetActive(false);
        }
        IEnumerator WaitTimerThrust(float timer, float tempThrust)
        {
            yield return new WaitForSeconds(timer);
            m_Thrust = tempThrust;
        }
    }
}

