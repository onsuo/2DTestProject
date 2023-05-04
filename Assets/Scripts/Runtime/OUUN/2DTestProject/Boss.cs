using UnityEngine;

namespace Runtime.OUUN._2DTestProject
{
    public class Boss : Enemy
    {
        private int _coinNum;
        
        protected override void Update()
        {
            transform.position += Vector3.down * (Speed * Time.deltaTime);

            if (transform.position.y < -6)
            {
                GameManager.Instance.SetGameOver(false);
                Destroy(gameObject);
            }
        }

        protected override void Kill()
        {
            Hp = 0;
            Destroy(gameObject);
            if (GameManager.Instance.GetStage() == GameManager.Instance.GetMaxStage())
            {
                GameManager.Instance.SetGameOver(true);
            }
            var amount = Coin / _coinNum;
            for (var i = 0; i < _coinNum; i++)
            {
                var coinObj = Instantiate(coinPrefab, transform.position, Quaternion.identity);
                coinObj.GetComponent<Coin>().SetAmount(amount);
                Coin -= amount;
            }
            GameManager.Instance.NextStage();
        }

        public void SetCoin(int amount, int num)
        {
            Coin = num * amount;
            _coinNum = num;
        }
    }
}