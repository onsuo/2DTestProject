using Runtime.OUUN._2DTestProject;
using UnityEngine;

namespace Test.OUUN._2DTestProject
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
            var shootDir = _mousePos - transform.position;
            var rotz = Mathf.Atan2(shootDir.y, shootDir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, rotz);

            if (!_canFire)
            {
                _timer += Time.deltaTime;
                if (_timer >= reloadingTime)
                {
                    _canFire = true;
                    _timer = 0;
                }
            }

            if (Input.GetMouseButton(0) && _canFire)
            {
                _canFire = false;
                var bulletInstance = Instantiate(bullet, _aimTransform.position, Quaternion.identity);
                bulletInstance.GetComponent<Bullet>().SetDirection(shootDir);
            }
        }
    }
}