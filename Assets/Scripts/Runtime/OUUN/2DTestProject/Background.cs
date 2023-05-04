using UnityEngine;

namespace Runtime.OUUN._2DTestProject
{
    public class Background : MonoBehaviour
    {
        private float _scrollSpeed = 3.0f;
        
        private void Update()
        {
            if (GameManager.Instance.isGameOver) return;
            
            transform.position += Vector3.down * (_scrollSpeed * Time.deltaTime);

            if (transform.position.y < -11.5)
                transform.position = new Vector3(0, 11.5f, 0);
        }
    }
}