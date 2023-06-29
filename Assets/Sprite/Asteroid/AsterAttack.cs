using SpaceShooter;
using UnityEngine;
/// <summary>
/// При входе в триггер корабля, летит по направлению к нему.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class AsterAttack : MonoBehaviour
{
    [SerializeField] private float m_RadiusAttackAster;
    [SerializeField] private float m_timerDestroy;
    [SerializeField] private ImpactEffect m_impactEffect;

    private Vector3 m_Target;
    private Vector3 m_VectorMove;
    private bool m_IsAttacking = false;

    private float timer;

    /// <summary>
    /// Ставим флаг действия и находим направление движения к цели. 
    /// </summary>
    /// <param name="ship"> Корабль зашедший в триггер астероида.</param>
    private void Attack(SpaceShip ship)
    {
        m_Target = ship.transform.position;
        m_VectorMove = m_Target - transform.position;
        m_IsAttacking =true;
    }

    private void Update()
    {
        if (m_IsAttacking == true)
        {
            timer += Time.deltaTime;
            transform.position +=  m_VectorMove * Time.deltaTime;    
            if(timer > m_timerDestroy)
                Destroy(gameObject);
        }
    }
    /// <summary>
    /// При столкновении с кораблем, уничтожаем астероид. 
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {

        var destructable = collision.transform.root.GetComponent<Destructible>();
        if (destructable != null)
        {
            var boom = Instantiate(m_impactEffect);
            boom.transform.position = this.transform.position;
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.root.TryGetComponent(out SpaceShip ship) )
        {
            Attack(ship);           
        }             
    }
    /// <summary>
    /// Блок для удобства настройки сцены.
    /// </summary>
#if UNITY_EDITOR
    private void OnValidate()
    {
        GetComponent<CircleCollider2D>().radius = m_RadiusAttackAster;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_RadiusAttackAster);
    }
#endif
}
