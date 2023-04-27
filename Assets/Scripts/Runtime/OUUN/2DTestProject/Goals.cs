using UnityEngine;

namespace Runtime.OUUN._2DTestProject
{
    public class Goals : MonoBehaviour
    {
        private SpriteRenderer _goalSprite;

        private Vector3 _scale = new(0.01f, 0.01f, 0);

        private void Start()
        {
            _goalSprite = GetComponent<SpriteRenderer>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            _goalSprite.color = Color.white;
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                other.transform.localScale += _scale * Time.deltaTime;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            _goalSprite.color = Color.blue;
        }
    }
}