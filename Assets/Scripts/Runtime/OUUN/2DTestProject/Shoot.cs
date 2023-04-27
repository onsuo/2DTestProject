using UnityEngine;

namespace Runtime.OUUN._2DTestProject
{
    public class Shoot : MonoBehaviour
    {
        public GameObject bullet;
        
        [SerializeField] private float reloadingTime;
        
        private Transform _aimTransform;
        private Camera _camera;
        private Vector3 _mousePos;
        private bool _canFire;
        private float _timer;

        private void Start()
        {
            _aimTransform = GameObject.Find("BulletPoint").GetComponent<Transform>();
            _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        }

        private void Update()
        {
            _mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 rotation = _mousePos - transform.position;
            float rotz = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, rotz);

            if (!_canFire)
            {
                _timer += Time.deltaTime;
                if (_timer > reloadingTime)
                {
                    _canFire = true;
                    _timer = 0;
                }
            }

            if (Input.GetMouseButton(0) && _canFire)
            {
                _canFire = false;
                Instantiate(bullet, _aimTransform.position, Quaternion.identity);
            }
        }
    }
}