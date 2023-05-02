using UnityEngine;

namespace Runtime.OUUN._2DTestProject
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private GameObject bullet;
        [SerializeField] private float reloadingTime;

        // private Rigidbody2D _rb;
        private Camera _camera;
        private Bounds _bounds;
        private Vector2 _viewportSize; // half
        private Vector2 _spriteSize; // half
        private float _reloadTimer;
        private bool _canFire;
        
        private Vector3 _mousePosPrev;
        private Vector3 _positionPrev;

        private void Start()
        {
            // _rb = GetComponent<Rigidbody2D>();
            _camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
            _bounds = GetComponent<SpriteRenderer>().bounds;
            _spriteSize = _bounds.extents;
            var viewportHeight = _camera.orthographicSize;
            _viewportSize = new Vector2(viewportHeight * _camera.aspect, viewportHeight);
        }

        private void Update()
        {
            var position = transform.position;
            
            // keyboard movement
            var dirx = Input.GetAxisRaw("Horizontal");
            var diry = Input.GetAxisRaw("Vertical");
            var moveDir = new Vector2(dirx, diry).normalized;
            // _rb.velocity = moveDir * speed;
            position += new Vector3(moveDir.x, moveDir.y, 0f) * (speed * Time.deltaTime);
            position = LimitPosition(position, _viewportSize, _spriteSize);
            transform.position = position;
            
            // mouse movement
            if (Input.GetMouseButtonDown(0))
            {
                _mousePosPrev = _camera.ScreenToWorldPoint(Input.mousePosition);
                _positionPrev = position;
            }
            else if (Input.GetMouseButton(0))
            {
                var mousePosCurr = _camera.ScreenToWorldPoint(Input.mousePosition);
                var dir = mousePosCurr - _mousePosPrev;
                
                position = _positionPrev + new Vector3(dir.x, dir.y, 0f);
                position = LimitPosition(position, _viewportSize, _spriteSize);
                transform.position = position;
                
                _mousePosPrev = mousePosCurr;
                _positionPrev = position;
            }

            Reload();
            if (Input.GetKey(KeyCode.Space) && _canFire)
            {
                _canFire = false;
                Instantiate(bullet, position + Vector3.forward, Quaternion.identity);
            }
        }

        /**
         * Limit object's position insider a specific area(World coordinate) considering its sprite size.
         */
        private Vector3 LimitPosition(Vector3 position, Vector2 areaSize, Vector2 spriteSize)
        {
            position.x = Mathf.Clamp(position.x, -areaSize.x + spriteSize.x, areaSize.x - spriteSize.x);
            position.y = Mathf.Clamp(position.y, -areaSize.y + spriteSize.y, -areaSize.x / 3 - spriteSize.y);

            return position;
        }

        private void Reload()
        {
            if (_canFire) return;
            _reloadTimer += Time.deltaTime;
            
            if (_reloadTimer < reloadingTime) return;
            _canFire = true;
            _reloadTimer = 0;
        }
    }
}