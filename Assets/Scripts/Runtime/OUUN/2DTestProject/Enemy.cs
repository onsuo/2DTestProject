using System;
using UnityEngine;

namespace Runtime.OUUN._2DTestProject
{
    public class Enemy : MonoBehaviour
    {
        private float _speed;

        private void Update()
        {
            transform.position += Vector3.down * (_speed * Time.deltaTime);

            if (transform.position.y < -6)
            {
                Destroy(gameObject);
            }
        }

        public void SetMoveSpeed(float speed)
        {
            _speed = speed;
        }
    }
}