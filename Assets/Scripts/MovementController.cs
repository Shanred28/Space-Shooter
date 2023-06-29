using UnityEngine;

namespace SpaceShooter
{
    public class MovementController : MonoBehaviour
    {
        public enum ControlMode
        { 
            Keyboard,
            Mobile
        }

        [SerializeField] private SpaceShip m_TargetShip;
        public void SetTargetShip(SpaceShip ship) => m_TargetShip = ship;

        [SerializeField] private VirtualJoystick m_MobileJoystick;

        [SerializeField] private ControlMode m_ControlMode;

        [SerializeField] private PointerClickHold m_MobileFirePrimary;
        [SerializeField] private PointerClickHold m_MobileFireSecondary;
        [SerializeField] private PointerClickHold m_MobileFireAlt;

        private void Start()
        {
            if (m_ControlMode == ControlMode.Keyboard)
            {
                m_MobileJoystick.gameObject.SetActive(false);
                m_MobileFirePrimary.gameObject.SetActive(false);
                m_MobileFireSecondary.gameObject.SetActive(false);
                m_MobileFireAlt.gameObject.SetActive(false);
            }

            else
            {
                m_MobileJoystick.gameObject.SetActive(true);
                m_MobileFirePrimary.gameObject.SetActive(true);
                m_MobileFireSecondary.gameObject.SetActive(true);
                m_MobileFireAlt.gameObject.SetActive(true);
            }               
        }

        private void Update()
        {
            if(m_TargetShip == null) return;

            if (m_ControlMode == ControlMode.Keyboard)
            {
                m_MobileJoystick.gameObject.SetActive(false);
                ControlKeyboard();
            }

            if (m_ControlMode == ControlMode.Mobile)
            {
                m_MobileJoystick.gameObject.SetActive(true);
                ControlMobile();
            }              
        }

        private void ControlMobile()
        {

            Vector3 dir = m_MobileJoystick.Value;

            var dot = Vector2.Dot(dir, m_TargetShip.transform.up);
            var dot2 = Vector2.Dot(dir, m_TargetShip.transform.right);

            m_TargetShip.ThrustControl = Mathf.Max(0, dot);
            m_TargetShip.TorqueControl = -dot2;

            if (m_MobileFirePrimary.IsHold == true)
                m_TargetShip.Fire(TurretMode.Primary);

            if (m_MobileFireSecondary.IsHold == true)
                m_TargetShip.Fire(TurretMode.PrimaryDouble);   
            
            if (m_MobileFireAlt.IsHold == true)
                m_TargetShip.Fire(TurretMode.Secondary);
        }

        private void ControlKeyboard()
        {
            float thrust = 0;
            float torque = 0;

            if (Input.GetKey(KeyCode.UpArrow))
                thrust = 1.0f;

            if (Input.GetKey(KeyCode.DownArrow))
                thrust = -1.0f;

            if (Input.GetKey(KeyCode.LeftArrow))
                torque = 1.0f;

            if (Input.GetKey(KeyCode.RightArrow))
                torque = -1.0f;

            if (Input.GetKey(KeyCode.Space))
                m_TargetShip.Fire(TurretMode.Primary);

            if (Input.GetKey(KeyCode.RightControl))
                m_TargetShip.Fire(TurretMode.PrimaryDouble);

            if (Input.GetKey(KeyCode.RightAlt))
                m_TargetShip.Fire(TurretMode.Secondary);

            m_TargetShip.ThrustControl = thrust;
            m_TargetShip.TorqueControl = torque;
        }
    }
}

