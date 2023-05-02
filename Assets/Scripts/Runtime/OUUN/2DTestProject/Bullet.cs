using UnityEngine;

namespace Runtime.OUUN._2DTestProject
{
    public class Bullet : MonoBehaviour
    {
        public Vector3 shootDir;
        
        private Rigidbody2D _rb;
        private const float Force = 15.0f;
        private float _timer;
        private const float Lifetime = 0.7f;

        public void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            
            var shootRot = -shootDir;
            _rb.velocity = new Vector2(shootDir.x, shootDir.y).normalized * Force;
            
            var rot = Mathf.Atan2(shootRot.y, shootRot.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, rot + 90);
        }

        public void Update()
        {
            _timer += Time.deltaTime;
            if (_timer > Lifetime | transform.position.y > 5.5)
            {
                Destroy(gameObject);
            }
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Structure"))
            {
                Destroy(gameObject);
            }

            if (other.gameObject.CompareTag("Target"))
            {
                Destroy(gameObject);
                // other.gameObject.SetActive(false);
                Destroy(other.gameObject);
            }
        }
    }
}