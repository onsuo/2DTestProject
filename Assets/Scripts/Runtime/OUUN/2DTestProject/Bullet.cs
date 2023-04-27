using UnityEngine;

namespace Runtime.OUUN._2DTestProject
{
    public class Bullet : MonoBehaviour
    {
        private Camera _camera;
        private Rigidbody2D _rb;
        private Vector3 _clickPos;
        private float _force = 15.0f;
        private float _timer;
        private const float Lifetime = 5.0f;

        public void Start()
        {
            _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            _rb = GetComponent<Rigidbody2D>();
            
            _clickPos = _camera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 shootDir = _clickPos - transform.position;
            Vector3 shootRot = -shootDir;
            _rb.velocity = new Vector2(shootDir.x, shootDir.y).normalized * _force;
            
            float rot = Mathf.Atan2(shootRot.y, shootRot.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, rot + 90);
        }

        public void Update()
        {
            _timer += Time.deltaTime;
            if (_timer > Lifetime)
            {
                Destroy(gameObject);
            }
        }

        public void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Target"))
            {
                Destroy(gameObject);
            }
        }
    }
}