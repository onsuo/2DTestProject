using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Runtime.OUUN._2DTestProject
{
    public class EnemySpawn : MonoBehaviour
    {
        [SerializeField] private GameObject[] enemies;
        [SerializeField] private GameObject[] bosses;

        private float[] _spawnPoint;
        private float _spawnInterval;
        private float _moveSpeed;
        private int _maxRowEnemy;

        private void Start()
        {
            _spawnPoint = new []{ -4.4f, -3.3f, -2.2f, -1.1f, 0f, 1.1f, 2.2f, 3.3f, 4.4f };
            _spawnInterval = 3.0f;
            _moveSpeed = 4.0f;
            _maxRowEnemy = 6;
            
            StartEnemyRoutine();
        }
        
        public void StopEnemyRoutine()
        {
            StopCoroutine(nameof(EnemyRoutine));
        }

        private void StartEnemyRoutine()
        {
            StartCoroutine(nameof(EnemyRoutine));
        }
        
        private IEnumerator EnemyRoutine()
        {
            yield return new WaitForSeconds(3f);

            var enemyIndex = 0;
            var bossIndex = 0;
            var totalRowCount = 0;
            while (true)
            {
                SpawnRow(enemyIndex);
                totalRowCount++;
                
                // difficulty up
                if (totalRowCount % 10 == 0 & enemyIndex < enemies.Length - 1)
                {
                    enemyIndex++;
                    LevelUp();
                }
                
                // boss
                if (totalRowCount >= 100)
                {
                    totalRowCount = 0;
                    enemyIndex = 0;
                    StageUp();
                    SpawnBoss(bossIndex);
                    bossIndex++;
                    if (bossIndex > bosses.Length - 1) bossIndex = bosses.Length - 1;
                }

                yield return new WaitForSeconds(_spawnInterval);
            }
        }

        private void SpawnRow(int enemyIndex)
        {
            var spawnCount = 0;
            foreach (var pos in _spawnPoint)
            {
                var spawn = Random.Range(0, 2) == 0;
                if (spawn == false) continue;
                if (spawnCount >= _maxRowEnemy) break;

                SpawnEnemy(pos, enemyIndex);
                spawnCount++;
            }
        }

        private void SpawnEnemy(float posX, int enemyIndex)
        {
            if (Random.Range(0, 5) == 0 & enemyIndex < enemies.Length - 1) enemyIndex++;

            var enemyObj = Instantiate(enemies[enemyIndex], transform.position + Vector3.right * posX, Quaternion.identity);
            var enemy = enemyObj.GetComponent<Enemy>();
            enemy.SetMoveSpeed(_moveSpeed);
            enemy.SetHp((enemyIndex + 1) * 5);
            enemy.SetCoin((enemyIndex + 1) * 2);
            enemy.SetDamage(enemyIndex + 5);
        }

        private void SpawnBoss(int bossIndex)
        {
            var bossObj = Instantiate(bosses[bossIndex], transform.position, Quaternion.identity);
            var boss = bossObj.GetComponent<Boss>();
            boss.SetMoveSpeed(0.5f);
            boss.SetHp(4000f + bossIndex * 500);
            boss.SetCoin(30, 10 + bossIndex * 5);
            boss.SetDamage(20f + bossIndex * 3);
        }
        
        private void LevelUp()
        {
            _moveSpeed += 0.3f;
            _spawnInterval -= 0.2f;
        }

        private void StageUp()
        {
            _spawnInterval = 2.5f;
            _moveSpeed = 5.0f;
            _maxRowEnemy++;
        }
    }
}