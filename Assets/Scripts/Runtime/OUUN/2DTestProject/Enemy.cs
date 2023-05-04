using UnityEngine;

namespace Runtime.OUUN._2DTestProject
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] protected GameObject coinPrefab;

        protected float Hp;
        protected float Damage;
        protected float Speed;
        protected int Coin;

        protected virtual void Update()
        {
            transform.position += Vector3.down * (Speed * Time.deltaTime);

            if (transform.position.y < -6)
            {
                Destroy(gameObject);
            }
        }

        protected void OnTriggerStay2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                other.gameObject.GetComponent<Player>().Hit(Damage);
            }
        }
        
        public void Hit(float damage)
        {
            Hp -= damage;
            if (Hp <= 0)
            {
                Kill();
            }
        }

        public void SetMoveSpeed(float speed)
        {
            Speed = speed;
        }

        public void SetHp(float hp)
        {
            Hp = hp;
        }

        public void SetCoin(int coin)
        {
            Coin = coin;
        }

        public void SetDamage(float damage)
        {
            Damage = damage;
        }

        protected virtual void Kill()
        {
            Hp = 0;
            Destroy(gameObject);
            var coinObj = Instantiate(coinPrefab, transform.position, Quaternion.identity);
            coinObj.GetComponent<Coin>().SetAmount(Coin);
        }
    }
}