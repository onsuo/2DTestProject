using UnityEngine;

namespace Runtime.OUUN._2DTestProject
{
    public class Obstacles : MonoBehaviour
    {
        private SpriteRenderer _obstacleSprite;

        private void Start()
        {
            _obstacleSprite = GetComponent<SpriteRenderer>();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            _obstacleSprite.color = Color.red;
            
            if (other.gameObject.CompareTag("Bullet"))
            {
                gameObject.SetActive(false);
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            _obstacleSprite.color = Color.black;
        }
    }
}