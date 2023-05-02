using UnityEngine;

namespace Test.OUUN._2DTestProject
{
    public class TestPlayerMovement : MonoBehaviour
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
            var x = Input.GetAxisRaw("Horizontal");
            var y = Input.GetAxisRaw("Vertical");
            
            _moveDir = new Vector3(x, y, 0);
            _moveDir.Normalize();
            
            var dirx = _moveDir.x == 0 ? _rb.velocity.x : _moveDir.x * _moveSpeed;
            var diry = _moveDir.y == 0 ? _rb.velocity.y : _moveDir.y * _moveSpeed;

            _rb.velocity = new Vector2(dirx, diry);
        }
    }
}