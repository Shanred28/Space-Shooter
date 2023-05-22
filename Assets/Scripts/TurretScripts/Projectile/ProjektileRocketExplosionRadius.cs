using UnityEngine;

namespace SpaceShooter
{
    public class ProjektileRocketExplosionRadius : Projectile
    {
        [SerializeField] private float m_ExplosionRadius;
        [SerializeField] private float forceExplosion;


        protected override void Update()
        {
            float stepLenght = Time.deltaTime * m_Velocity;
            Vector2 step = transform.up * stepLenght;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, stepLenght);

            if (hit)
            {
                Destructible dest = hit.collider.transform.root.GetComponent<Destructible>();
                if (dest != null )
                {
                    var boom = Instantiate(m_ImpactEffectPrefab);
                    boom.transform.position = this.transform.position;
                    ExplosionDamage();
                    dest.ApplyDamage(m_Damage);
                }
                OnProjectileLifeEnd(hit.collider, hit.point);
            }

            m_Timer += Time.deltaTime;
            if (m_Timer > m_LifeTime)
                Destroy(gameObject);

            transform.position += new Vector3(step.x, step.y, 0);
        }

        private void ExplosionDamage()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, m_ExplosionRadius);

            for (int i = 0; i < colliders.Length; i++)
            { 
                Rigidbody2D rb2d = colliders[i].attachedRigidbody;
                if (rb2d)
                { 
                    Vector2 direct = rb2d.transform.position - transform.position;
                    rb2d.AddForce(direct.normalized * forceExplosion);
                }
                Destructible des = colliders[i].transform.root.GetComponent<Destructible>();
                if (des)
                {
                    Vector2 direct = des.transform.position - transform.position;
                    float dist = direct.magnitude;
                    des.ApplyDamage(m_Damage - (int)dist);
                }
            }


        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, m_ExplosionRadius);
        }

    }
}

