using UnityEngine;

namespace SpaceShooter
{
    public class Projectile : Entity
    {
        [SerializeField] protected float m_Velocity;
        public float VelocityProjectile => m_Velocity;

        [SerializeField] protected float m_LifeTime;

        [SerializeField] protected int m_Damage;

        [SerializeField] protected ImpactEffect m_ImpactEffectPrefab;

        protected float m_Timer;

        protected virtual void Update()
        {
            float stepLenght = Time.deltaTime * m_Velocity;
            Vector2 step = transform.up * stepLenght;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, stepLenght);

            
            if (hit)
            { 
                Destructible dest = hit.collider.transform.root.GetComponent<Destructible>();
                if (dest != null && dest != m_Perent)
                { 
                    dest.ApplyDamage(m_Damage);

                    var boom = Instantiate(m_ImpactEffectPrefab);
                    boom.transform.position = this.transform.position;

                    if (IsProjectile)
                    {
                        Player.Instance.AddScore(dest.ScoreValue);

                        // Ѕез этих условий, если два projectile попадает в один корабль засчитываетс€ два килла. — этим правилом все работает.
                        if (hit.collider.transform.root.TryGetComponent<SpaceShip>(out var ship) && dest.CurrentHitPoints <= m_Damage  )
                        {
                            if(!ship.IsDestroy)
                                Player.Instance.AddKill();                           
                        }

                        if (hit.collider.transform.root.TryGetComponent<TurretPoint>(out var turretPoint) && dest.CurrentHitPoints <= m_Damage)
                        {
                            if (!turretPoint.IsDestroy)
                                Player.Instance.AddKill();
                        }
                    }                      
                }

                OnProjectileLifeEnd(hit.collider, hit.point);
            }

            m_Timer += Time.deltaTime;
            if (m_Timer > m_LifeTime)
                Destroy(gameObject);

            transform.position += new Vector3(step.x, step.y, 0);
        }

        protected void OnProjectileLifeEnd(Collider2D col, Vector2 pos)
        { 
            Destroy(gameObject);
        }

        private Destructible m_Perent;
        private bool IsProjectile =false;
        public void SetPerentShooter(Destructible perent)
        {
            m_Perent = perent;

            if(m_Perent == Player.Instance.ActiveShip)
                IsProjectile = true;
        }
    }
}

