using UnityEngine;

namespace Runtime.OUUN._2DTestProject
{
    public class PlayerMovement : MonoBehaviour
    {
        private Rigidbody2D _rb;
        private Vector3 _moveDir;
        private float _moveSpeed = 3.0f;

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _moveDir = Vector3.right;
            _moveSpeed = 5.0f;
        }

        private void Update()
        {
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");
            
            _moveDir = new Vector3(x, y, 0);
            _moveDir.Normalize();
            
            float dirx = _moveDir.x == 0 ? _rb.velocity.x : _moveDir.x * _moveSpeed;
            float diry = _moveDir.y == 0 ? _rb.velocity.y : _moveDir.y * _moveSpeed;

            _rb.velocity = new Vector2(dirx, diry);
        }
    }
}