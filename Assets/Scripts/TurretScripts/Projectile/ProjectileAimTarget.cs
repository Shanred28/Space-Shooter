using UnityEngine;

namespace SpaceShooter
{
    public class ProjectileAimTarget : Projectile
    {

        private SpaceShip mainTarget;
        [SerializeField ]private float speed;
        [SerializeField] private float m_RadisuTarget;

        private Destructible secondTarget;
        [SerializeField] private float rotateSpeed;

        /// <summary>
        /// Находим все коллайдеры в радиусе
        /// </summary>
        protected override void Update()
        {
            
            if (mainTarget == null || secondTarget == null)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, m_RadisuTarget);
                SearchTargetCollider(colliders);
            }          
        }

        /// <summary>
        /// Проверяем столкновения с целью, вызываем урон. Летим к цели. 
        /// </summary>
        private void FixedUpdate()
        {
            float stepLenght = Time.deltaTime * speed;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, stepLenght);

            if (mainTarget == hit && mainTarget != null)
            {
                mainTarget.ApplyDamage(m_Damage);
                var boom = Instantiate(m_ImpactEffectPrefab);
                    boom.transform.position = this.transform.position ;
                Destroy(gameObject);

            }

            if (secondTarget == hit && secondTarget != null)
            {
                secondTarget.ApplyDamage(m_Damage);
                var boom = Instantiate(m_ImpactEffectPrefab);
                boom.transform.position = this.transform.position;
                Destroy(gameObject);

            }
            if (mainTarget != null)
                AttackTarget(mainTarget.transform);
            else
            { 
                if(secondTarget != null)
                    AttackTarget(secondTarget.transform);
            } 
              
        }

        /// <summary>
        /// Выбираем из всех колайдеров цели. Если есть корабли то они в приоритете. 
        /// </summary>
        /// <param name="colliders"></param>
        private void SearchTargetCollider(Collider2D[] colliders)
        {
            int index = 0;
            for (int i = 0; i < colliders.Length; i++)
            {
                index ++;
               
                 SpaceShip  targetSelect = colliders[i].transform.root.GetComponent<SpaceShip>();
                if (targetSelect != null && targetSelect.Nicname != "Player")
                {
                    mainTarget = targetSelect;
                    return;
                }
                else
                {
                    Destructible targetSelectSecond = colliders[i].transform.root.GetComponent<Destructible>();
                    if (targetSelectSecond != null && targetSelectSecond.Nicname != "Player")
                    {
                        secondTarget = targetSelectSecond;
                    }                   
                }                         
            }           
        }

        /// <summary>
        /// летим к цели.
        /// </summary>
        /// <param name="target"></param>
        private void AttackTarget(Transform target)
        {
            Vector2 directTarget = (Vector2)target.transform.position - (Vector2)transform.position;
            directTarget = directTarget.normalized;

            float rotateeAmount = Vector3.Cross (directTarget, transform.up).z;

            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.angularVelocity = -rotateeAmount * rotateSpeed;

            rb.velocity = transform.up * speed;
        }
        /// <summary>
        /// Для отображения радуса поиска коллайдеров. 
        /// </summary>
#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere (transform.position, m_RadisuTarget);
        }      
    }
#endif
}

