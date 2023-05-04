using UnityEngine;
using Random = UnityEngine.Random;

namespace Runtime.OUUN._2DTestProject
{
    public class Coin : MonoBehaviour
    {
        private Rigidbody2D _rb;
        private int _amount;
        
        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            
            var randDirX = Random.Range(-2f, 2f);
            var randJumpForce = Random.Range(0f, 5f);
            _rb.AddForce(new Vector2(randDirX, randJumpForce), ForceMode2D.Impulse);
        }

        private void Update()
        {
            if (transform.position.y < -5.5)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Destroy(gameObject);
                GameManager.Instance.AddCoin(_amount);
            }
        }

        public void SetAmount(int amount)
        {
            _amount = amount;
        }
    }
}