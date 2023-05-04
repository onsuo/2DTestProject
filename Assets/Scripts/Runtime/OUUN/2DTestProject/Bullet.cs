using UnityEngine;

namespace Runtime.OUUN._2DTestProject
{
    public class Bullet : MonoBehaviour
    {
        private Vector3 _shootDir = Vector3.up;
        private float _damage;
        private float _speed;
        
        private Rigidbody2D _rb;

        public void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            
            var shootRot = -_shootDir;
            _rb.velocity = new Vector2(_shootDir.x, _shootDir.y).normalized * _speed;
            
            var rot = Mathf.Atan2(shootRot.y, shootRot.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, rot + 90);
        }

        public void Update()
        {
            if (transform.position.y > 5.5)
                Destroy(gameObject);
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Structure"))
            {
                Destroy(gameObject);
            }

            if (other.gameObject.CompareTag("Enemy"))
            {
                other.GetComponent<Enemy>().Hit(_damage);
                Destroy(gameObject);
            }
        }
        
        public void SetDirection(Vector3 dir)
        {
            _shootDir = dir;
        }

        public void SetDamage(float amount)
        {
            _damage = amount;
        }

        public void SetSpeed(float amount)
        {
            _speed = amount;
        }
    }
}