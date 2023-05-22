using System.Collections;
using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(SpaceShip))]
    public class AiController : MonoBehaviour
    {
        public enum AIBegavior
        { 
            Null,
            Patrol
        }

        [SerializeField] private AIBegavior m_AIBegavior;

        [SerializeField] private AIPointPatrol m_PointPatrol;

        [Range(-1.0f, 1.0f)]
        [SerializeField] private float m_Navigationlinear;
        [Range(0.0f, 1.0f)]
        [SerializeField] private float m_NavigationAngular;

        [SerializeField] private float m_RandomSelectMovePointTime;
        [SerializeField] private float m_FindNewTargetTime;
        [SerializeField] private float m_ShootDelay;
        [SerializeField] private float m_EvadeRayLength;

        private SpaceShip m_SpaceShip;
        private Vector3 m_MovePosition;
        private Destructible m_SelectedTarget;

        private Timer m_RandomizedDirectionTimer;
        private Timer m_FireTimer;
        private Timer m_FindNewTargetTimer;

        [Header("PatrolRoute")]
        [SerializeField] private bool IsActvePatrolRoute;

        private Vector3 m_MovePatrolPoint;
        private bool IsRotePatrol = false;

        private void Start()
        {
            m_SpaceShip = GetComponent<SpaceShip>();
            InitTimers();
        }

        private void Update()
        {
            UpdateTimers();
            UpdateAI();

        }
        private void FixedUpdate()
        {
            
        }

        private void UpdateAI()
        {
            if (m_AIBegavior == AIBegavior.Null)
            { 
            
            }

            if (m_AIBegavior == AIBegavior.Patrol)
            {
                UpdateBehaviourPatrol();
            }
        }

        private void UpdateBehaviourPatrol()
        {
            ActionFindNewMovePosition();
            ActionEvadeCollision();
            ActionControlShip();           
            ActionFindNewAttackTarget();
            ActionFire();

        }

        private void ActionFindNewMovePosition()
        {
            if (m_AIBegavior == AIBegavior.Patrol)
            {
                if (m_SelectedTarget != null)
                {
                    m_MovePosition = MakeLead();
                }
                else
                {
                    if (m_PointPatrol != null)
                    {
                        if (IsRaiusPoint(m_PointPatrol.transform.position, m_PointPatrol.Radius))
                        {
                            if (m_RandomizedDirectionTimer.IsFinished == true && IsActvePatrolRoute == false)
                            {
                                Vector2 newPoint = Random.onUnitSphere * m_PointPatrol.Radius + m_PointPatrol.transform.position;
                                m_MovePosition = newPoint;
                                m_RandomizedDirectionTimer.Restart();
                            }
                            if ( IsActvePatrolRoute == true)
                            {
                                if (IsRotePatrol == false)
                                {
                                    m_MovePatrolPoint = FindNearesTargetMovePointPatrol().position;
                                    m_MovePosition = m_MovePatrolPoint;
                                    IsRotePatrol = true;

                                }
                                if (IsRotePatrol == true && IsRaiusPoint(m_MovePatrolPoint, m_PointPatrol.RadiusPointPatrol))
                                {
                                    Debug.Log("�� � �����");
                                    for (int i = 0; i < m_PointPatrol.PointPatrolRoute.Count; ++i)
                                    {
                                        int nextTransform = i + 1;

                                        if (nextTransform >= m_PointPatrol.PointPatrolRoute.Count)
                                        {
                                            nextTransform = 0;
                                        }

                                        if (m_PointPatrol.PointPatrolRoute[i].transform.position == m_MovePatrolPoint)
                                        {
                                            m_MovePosition = m_PointPatrol.PointPatrolRoute[nextTransform].transform.position;
                                            m_MovePatrolPoint = m_MovePosition;

                                            return;
                                        }
                                    }

                                }                            
                            }                                                    
                        }
                        else
                        {
                            m_MovePosition = m_PointPatrol.transform.position;
                        }
                    }
                }

            }
        }
        private bool IsRaiusPoint(Vector3 targetPoint, float radiusPoint)
        { 
           return  (targetPoint - transform.position).magnitude < radiusPoint;
        }

        private Transform FindNearesTargetMovePointPatrol()
        {
            float maxDist = float.MaxValue;
            Transform potentialTarget = null;
            foreach (Transform t in m_PointPatrol.PointPatrolRoute)
            {
                float dist = Vector2.Distance(m_SpaceShip.transform.position, t.position);
                if (dist < maxDist)
                {
                    maxDist = dist;
                    potentialTarget = t;
                }                
            }
            return potentialTarget;
        }

        private float tempSpeed = 0.4f;
        private void ActionEvadeCollision()
        {
          /*  Collider2D[] collider2 = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y) , 5f);
             foreach (Collider2D collider in collider2)
             { 
                 float dis = (collider.transform.position - transform.position).magnitude;
                 if (dis < 3 && dis != 0)
                 {
                    
                     m_MovePosition = transform.position + transform.right;
                 }
             }*/
          /*  var ahead = transform.position.y + m_Navigationlinear * m_EvadeRayLength;
            var ahead2 = transform.position.y + m_Navigationlinear * m_EvadeRayLength * 0.5f;
            var distance = Vector2.Distance(new Vector2(transform.position.x, ahead2), new Vector2(transform.position.x, ahead));
            Collider2D col = Physics2D.Raycast(transform.position, transform.up, m_EvadeRayLength).collider;
            var Surfacepoint = Physics2D.Raycast(transform.position, transform.up, m_EvadeRayLength).point;
            var m_ColCentr = col.bounds.center;
            var RadiusObstecle = Vector3.Distance( new Vector3(Surfacepoint.x, Surfacepoint.y, 0), m_ColCentr);
            bool intRadius = Vector3.Distance(m_ColCentr, new Vector3(transform.position.x, ahead, 0)) <= RadiusObstecle || Vector3.Distance(m_ColCentr, new Vector3(transform.position.x, ahead2, 0)) <= RadiusObstecle;

            var Surfacepoint = Physics2D.Raycast(transform.position, transform.up, m_EvadeRayLength).point;*/
            if (Physics2D.Raycast(transform.position, transform.up, m_EvadeRayLength) == true)
            {

                if (tempSpeed == 0.0f)
                    tempSpeed = m_Navigationlinear;

               /* m_Navigationlinear = m_Navigationlinear * -0.9f;
                m_MovePosition = transform.position + transform.right *100;*/
                StartCoroutine(WaitTimerInvulnerability(1));
                IsRotePatrol = false;

                //m_MovePosition = transform.position + transform.right * offset;
            }
         /*   else if(tempSpeed != 0)
            {
                m_Navigationlinear = tempSpeed;
                tempSpeed = 0.0f;
            }              */
        }

        IEnumerator WaitTimerInvulnerability(float timer)
        {
            m_Navigationlinear = m_Navigationlinear * -1f;
           // m_MovePosition = transform.position + transform.right; 
            yield return new WaitForSeconds(timer);
            m_Navigationlinear = tempSpeed;

        }

        private void ActionControlShip()
        {
            m_SpaceShip.ThrustControl = m_Navigationlinear;

            m_SpaceShip.TorqueControl = ComputeAliginTorqueNormalized(m_MovePosition, m_SpaceShip.transform) * m_NavigationAngular;

        }

        private const float MAX_ANGLE = 45f;
        private static float ComputeAliginTorqueNormalized(Vector3 targetPosition, Transform ship)
        { 
            Vector2 localTargetPosition = ship.InverseTransformPoint(targetPosition);

            float angle = Vector3.SignedAngle(localTargetPosition, Vector3.up, Vector3.forward);

            angle = Mathf.Clamp(angle, -MAX_ANGLE, MAX_ANGLE) / MAX_ANGLE;
            return -angle;
        }


        private Destructible FindNearesDestructableTarget()
        {
            float maxDist = float.MaxValue;
            Destructible potentialTarget = null;
            foreach (var v in Destructible.AllDestructibles)
            {
                if (v.GetComponent<SpaceShip>() == m_SpaceShip) continue;
                if (v.TeamId == Destructible.TeamIdNeutral) continue;
                if (v.TeamId == m_SpaceShip.TeamId) continue;

                float dist = Vector2.Distance(m_SpaceShip.transform.position, v.transform.position);
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
            if (m_FindNewTargetTimer.IsFinished == true)
            {
                m_SelectedTarget = FindNearesDestructableTarget();
                m_FindNewTargetTimer.Restart();
            }
        }

        [SerializeField] private Projectile Bulet;
        private Vector3 MakeLead()
        {
            Rigidbody2D velocityTarget = m_SelectedTarget.GetComponent<Rigidbody2D>();
            float distance = Vector3.Distance(m_SelectedTarget.transform.position, transform.position);
            float timeToIntercept = distance / Bulet.VelocityProjectile;
            Vector3 leadTaarget = m_SelectedTarget.transform.position + ( new Vector3(velocityTarget.velocity.x, velocityTarget.velocity.y , 0) * timeToIntercept);
            return leadTaarget;
        }

        private void ActionFire()
        {
            if (m_SelectedTarget != null)
            {
                float distFire = Vector2.Distance(transform.position, m_SelectedTarget.transform.position);
                if (distFire < m_EvadeRayLength)
                {
                    Collider2D col = Physics2D.Raycast(transform.position, transform.up, m_EvadeRayLength).collider;

                    if (col == null)
                    {
                        if (m_FireTimer.IsFinished == true)
                        {
                            m_SpaceShip.Fire(TurretMode.Primary);
                            m_SpaceShip.Fire(TurretMode.Secondary);
                            m_FireTimer.Restart();
                        }
                    }
                    else if (col.transform.root.TryGetComponent<SpaceShip>(out var ship))
                    {
                        int idTeam = ship.TeamId;
                        if (idTeam != m_SpaceShip.TeamId)
                        {
                            if (m_FireTimer.IsFinished == true)
                            {
                                m_SpaceShip.Fire(TurretMode.Primary);
                                m_SpaceShip.Fire(TurretMode.Secondary);
                                m_FireTimer.Restart();
                            }
                        }
                    }                                    
                }               
            }
        }

        #region Timers
        private void InitTimers()
        {
            m_RandomizedDirectionTimer = new Timer(m_RandomSelectMovePointTime);
            m_FireTimer = new Timer(m_ShootDelay);
            m_FindNewTargetTimer = new Timer(m_FindNewTargetTime);
        }

        private void UpdateTimers()
        {
            m_RandomizedDirectionTimer.RemoveTime(Time.deltaTime);
            m_FireTimer.RemoveTime(Time.deltaTime);
            m_FindNewTargetTimer.RemoveTime(Time.deltaTime);
        }

        #endregion

        private void SetPatrolBehaviour(AIPointPatrol point)
        {
            m_AIBegavior = AIBegavior.Patrol;
            m_PointPatrol = point;
        }
    }
}

