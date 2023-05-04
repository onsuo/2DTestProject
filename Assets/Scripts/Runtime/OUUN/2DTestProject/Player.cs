using UnityEngine;

namespace Runtime.OUUN._2DTestProject
{
    public class Player : MonoBehaviour
    {
        public float speed;
        public float reloadingTime;
        public float invulnerableTime;
        public float bulletDamage;
        public float bulletSpeed;
        
        [SerializeField] private GameObject[] bullets;
        
        private Camera _camera;
        private Vector2 _viewportSize; // half
        private Vector2 _spriteSize; // half
        
        private float _reloadTimer;
        private bool _canFire;
        private int _bulletIndex;
        
        private Vector3 _mousePosPrev;
        private Vector3 _positionPrev;
        
        private float _invulnerableTimer;
        private bool _vulnerable;
        private Animator _anim;
        private static readonly int Invulnerable = Animator.StringToHash("Invulnerable");

        private void Start()
        {
            _camera = GameManager.Instance.GetCamera();
            _viewportSize = GameManager.Instance.GetViewportSize();
            _spriteSize = GetComponent<SpriteRenderer>().bounds.extents;

            _anim = GetComponent<Animator>();
        }

        private void Update()
        {
            if (GameManager.Instance.isGameOver) return;
            
            var position = transform.position;
            
            // keyboard movement
            var dirx = Input.GetAxisRaw("Horizontal");
            var diry = Input.GetAxisRaw("Vertical");
            var moveDir = new Vector2(dirx, diry).normalized;
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
                var bulletObj = Instantiate(bullets[_bulletIndex], position + Vector3.forward, Quaternion.identity);
                var bullet = bulletObj.GetComponent<Bullet>();
                bullet.SetDamage(bulletDamage);
                bullet.SetSpeed(bulletSpeed);
            }
            
            RestoreVulnerability();
        }

        public void Hit(float damage)
        {
            if (_vulnerable == false) return;

            var hp = GameManager.Instance.ChangePlayerHp(-damage);
            if (hp <= 0)
            {
                GameManager.Instance.SetGameOver(false);
                gameObject.SetActive(false);
                return;
            }

            _vulnerable = false;
            _anim.SetBool(Invulnerable, true);
        }
        
        public void Upgrade(bool bulletUpgrade)
        {
            if (bulletUpgrade & _bulletIndex < bullets.Length - 1)
            {
                _bulletIndex++;
                bulletDamage += 10;
                bulletSpeed += 10;
            }
            speed *= 1.1f;
            reloadingTime *= 0.8f;
            invulnerableTime += 0.2f;
        }
        
        private Vector3 LimitPosition(Vector3 position, Vector2 areaSize, Vector2 spriteSize)
        {
            position.x = Mathf.Clamp(position.x, -areaSize.x + spriteSize.x, areaSize.x - spriteSize.x);
            position.y = Mathf.Clamp(position.y, -areaSize.y + spriteSize.y, -spriteSize.y);

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

        private void RestoreVulnerability()
        {
            if (_vulnerable) return;
            _invulnerableTimer += Time.deltaTime;
            
            if (_invulnerableTimer < invulnerableTime) return;
            _vulnerable = true;
            _anim.SetBool(Invulnerable, false);
            _invulnerableTimer = 0;
        }
    }
}