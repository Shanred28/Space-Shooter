using UnityEngine;

namespace SpaceShooter
{
    public class TurretPoint : Destructible
    {
        [SerializeField] private float m_MaxAngularVelocity;
        [SerializeField] private float m_Mobility;


        [SerializeField] private int m_MaxEnergy;
        public int MaxEnergy => m_MaxEnergy;
        [SerializeField] private int m_MaxAmmo;
        [SerializeField] private int m_EnergyRegenPerSecond;

        private float m_CurrentEnergy;
        public float CurrentEnergy => m_CurrentEnergy;
        private int m_CurrentAmmo;
        public int CurrentAmmo => m_CurrentAmmo;

        public float AngleRotation { get; set; }


        private Rigidbody2D m_Rigid;
        private Animator m_Animator;

        protected override void Start()
        {
            base.Start();

            m_Rigid = GetComponentInChildren<Rigidbody2D>();          
            m_Animator = GetComponent<Animator>();

        }

        private void Update()
        {
            UpdateSpriteRotation();
        }

        private void FixedUpdate()
        {

        }

        private void UpdateRigiBody(float angle)
        {
            m_Rigid.AddTorque(-angle);
        }

        private void UpdateSpriteRotation()
        {
            float angle;
            angle = (AngleRotation / 64) / 5.625f;
            if (angle > 0)
            {
                m_Animator.SetFloat("Blend", angle);
            }
            else
            {
                angle = angle + 1;
                m_Animator.SetFloat("Blend", angle);

            }
            m_Animator.SetFloat("Blend", angle);        
        }


        [SerializeField] private TurretsStzcionarTuurel[] m_Turrets;
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
    }
}
