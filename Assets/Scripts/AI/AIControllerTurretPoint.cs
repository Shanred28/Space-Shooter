using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace SpaceShooter
{
    public class AIControllerTurretPoint : MonoBehaviour
    {

        [Range(0.0f, 1.0f)]
        [SerializeField] private float m_NavigationAngular;

        [SerializeField] private float m_FindNewTargetTime;

        [SerializeField] private float m_ShootDelay;

        [SerializeField] private float m_AttackRange;

        private Destructible m_SelectedTarget;
        public Destructible SelectedTarget => m_SelectedTarget;

        private TurretPoint m_TurretPoint;
        private Timer m_FireTimer;
        private Timer m_FindNewTargetTimer;

        [SerializeField] private Transform m_TransformTurrel;
        private void Start()
        {
            m_TurretPoint = GetComponent<TurretPoint>();
            InitTimers();
        }

        private void Update()
        {
            UpdateTimers();
            UpdateAI();
            ActionControlTurret();

        }

        private void UpdateAI()
        {

            ActionFindNewAttackTarget();
            ActionFire();
           // RotateTranform();

        }

        private void ActionControlTurret()
        {
            if (m_SelectedTarget != null)
            {
                m_TurretPoint.AngleRotation = ComputeAliginTorqueNormalized(m_SelectedTarget.transform.position, m_TurretPoint.transform)* m_NavigationAngular;
                
                Vector3 relativePos = m_SelectedTarget.transform.position - m_TransformTurrel.position;

                float angle = ComputeAliginTorqueNormalized(m_SelectedTarget.transform.position, m_TurretPoint.transform);

                m_TransformTurrel.rotation = Quaternion.Euler(0, 0, -angle);

            }

           
        }

        private static float ComputeAliginTorqueNormalized(Vector3 targetPosition, Transform turret)
        {
            Vector2 localTargetPosition = turret.InverseTransformPoint(targetPosition);

            float angle = Vector3.SignedAngle(localTargetPosition, Vector3.up, Vector3.forward);

            return angle;
        }

        private Destructible FindNearesDestructableTarget()
        {
            float maxDist = float.MaxValue;
            Destructible potentialTarget = null;
            foreach (var v in Destructible.AllDestructibles)
            {
                if (v.GetComponent<TurretPoint>() == m_TurretPoint) continue;
                if (v.TeamId == Destructible.TeamIdNeutral) continue;
                if (v.TeamId == m_TurretPoint.TeamId) continue;

                float dist = Vector2.Distance(m_TurretPoint.transform.position, v.transform.position);
                if (dist < maxDist)
                {
                    maxDist = dist;
                    potentialTarget = v;
                }
            }
            return potentialTarget;
        }

        private void ActionFindNewAttackTarget()
        {
            if (m_SelectedTarget == null && m_FindNewTargetTimer.IsFinished == true)
            {
                m_SelectedTarget = FindNearesDestructableTarget();
                m_FindNewTargetTimer.Restart();
            }
        }

        [SerializeField] private Projectile Bulet;
        private float MakeLead()
        {
            Rigidbody2D velocityTarget = m_SelectedTarget.GetComponent<Rigidbody2D>();
            float distance = Vector3.Distance(m_SelectedTarget.transform.position, transform.position);
            float timeToIntercept = distance / Bulet.VelocityProjectile;
            Vector3 leadTaarget = m_SelectedTarget.transform.position + (new Vector3(velocityTarget.velocity.x, velocityTarget.velocity.y, 0) * timeToIntercept);
            var angle = Vector3.Cross(leadTaarget, transform.up).z;
            return angle;
        }

        private void RotateTranform()
        {
            if (m_SelectedTarget != null)
            {
                float angle = MakeLead();
               // m_TransformTurrel.Rotate(m_SelectedTarget.transform.position, m_SelectedTarget.transform.rotation.z);
               // m_TransformTurrel.Rotate(m_SelectedTarget.transform.position, angle, Space.Self);
               // m_TransformTurrel.localRotation = Quaternion.RotateTowards(m_TransformTurrel.localRotation, Quaternion.Euler(0,0, angle), 10 * Time.deltaTime);
                Vector2 directTarget = (Vector2)m_SelectedTarget.transform.position - (Vector2)transform.position;
               /* directTarget = directTarget.normalized;
                float rotateAmount = Vector3.Cross(directTarget, transform.up).z;
                m_TransformTurrel.transform. (new Vector3(0,0, rotateAmount));*/
                Vector3 newDirection = Vector3.RotateTowards(transform.forward, m_SelectedTarget.transform.position, 10f, 0.0f);

            }
           
        }

        private void ActionFire()
        {
            if (m_SelectedTarget != null)
            {
                //m_SelectedTarget.transform.position = MakeLead();

                float distFire = Vector2.Distance(m_SelectedTarget.transform.position, transform.position);
                if (distFire < m_AttackRange)
                {

                    if (m_FireTimer.IsFinished == true)
                    {
                        Debug.Log("передаю навод");
                        m_TurretPoint.Fire(TurretMode.Primary);
                        m_FireTimer.Restart();
                    }
                    
                }
            }
        }

        private void InitTimers()
        {
            m_FindNewTargetTimer = new Timer(m_FindNewTargetTime);
            m_FireTimer = new Timer(m_ShootDelay);
        }

        private void UpdateTimers()
        {
            m_FindNewTargetTimer.RemoveTime(Time.deltaTime);
            m_FireTimer.RemoveTime(Time.deltaTime);
        }


    }
}

