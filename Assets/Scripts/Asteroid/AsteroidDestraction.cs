using UnityEngine;

namespace SpaceShooter
{
    public class AsteroidDestraction : Destructible
    {
        private Transform[] GetChildren;
        [SerializeField] private Transform perent;
        [SerializeField] private float m_Force;
        [SerializeField] private float m_Torque;

        private  Rigidbody2D[] rbChildren;
        private PolygonCollider2D[] cbChidren;

        protected override void Start()
        {
            base.Start();
            GetChildren = new Transform[perent.childCount];

            for (int i = 0; i < perent.childCount; i++)
            {
                GetChildren[i] =  perent.GetChild(i).transform;
               // GetChildren[i].gameObject.SetActive(false);
            }
            
            rbChildren = new Rigidbody2D[GetChildren.Length];
            for (int i = 0; i < GetChildren.Length; i++)
            {
                rbChildren[i] = GetChildren[i].transform.GetComponent<Rigidbody2D>();
                rbChildren[i].isKinematic = true;
            }

            cbChidren = new PolygonCollider2D[GetChildren.Length];

            for (int i = 0; i < cbChidren.Length; i++)
            {
                cbChidren[i] = GetChildren[i].transform.GetComponentInChildren <PolygonCollider2D>();
                cbChidren[i].enabled = false;
            }          
        }
    

        public void DestractionAster()
        {
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<Rigidbody2D>().Sleep();

            foreach(PolygonCollider2D polygonCollider2D in cbChidren) 
            {
                polygonCollider2D.enabled = false;
            }
            for (int i = 0; i < rbChildren.Length; i++)
            {
                rbChildren[i].isKinematic = false;
            }
        }

        protected override void OnDeath()
        {
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<Rigidbody2D>().isKinematic = true;

            foreach (PolygonCollider2D polygonCollider2D in cbChidren)
            {
                polygonCollider2D.enabled = true;
            }
            for (int i = 0; i < rbChildren.Length; i++)
            {
                rbChildren[i].isKinematic = false;
                rbChildren[i].AddTorque(m_Torque);
            }   
            foreach (Transform t in GetChildren)
            { 
                t.SetParent( null);
            }
            Destroy(gameObject);
        }

    }
}

