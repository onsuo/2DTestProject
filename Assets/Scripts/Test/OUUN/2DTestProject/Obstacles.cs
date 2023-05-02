using UnityEngine;

namespace Test.OUUN._2DTestProject
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
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            _obstacleSprite.color = Color.black;
        }
    }
}