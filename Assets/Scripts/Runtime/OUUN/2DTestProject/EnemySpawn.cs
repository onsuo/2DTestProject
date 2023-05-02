using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Runtime.OUUN._2DTestProject
{
    public class EnemySpawn : MonoBehaviour
    {
        [SerializeField] private GameObject[] enemies;

        private readonly float[] _spawnPoint = { -2.2f, -1.1f, 0f, 1.1f, 2.2f };
        private float _spawnInterval = 3.0f;
        private float _moveSpeed = 4.0f;

        private void Start()
        {
            StartEnemyRoutine();
        }

        private void StartEnemyRoutine()
        {
            StartCoroutine(EnemyRoutine());
        }

        private IEnumerator EnemyRoutine()
        {
            yield return new WaitForSeconds(3f);

            var totalRowCount = 0;
            var enemyIndex = 0;
            while (true)
            {
                var rowSpawnCount = 0;
                foreach (var pos in _spawnPoint)
                {
                    var spawn = Random.Range(0, 2) == 0;
                    if (spawn == false) continue;
                    if (rowSpawnCount >= 4) break;

                    SpawnEnemy(pos, enemyIndex);
                    rowSpawnCount++;
                }

                totalRowCount++;
                if (totalRowCount % 10 == 0 & totalRowCount < 90)
                {
                    enemyIndex++;
                    if (enemyIndex >= enemies.Length) enemyIndex = enemies.Length - 1;

                    _moveSpeed += 0.3f;
                    _spawnInterval -= 0.2f;
                }

                yield return new WaitForSeconds(_spawnInterval);
            }
        }

        private void SpawnEnemy(float posX, int index)
        {
            if (Random.Range(0, 5) == 0 & index < enemies.Length - 1) index++;

            var tmp = Instantiate(enemies[index], transform.position + Vector3.right * posX, Quaternion.identity);
            tmp.GetComponent<Enemy>().SetMoveSpeed(_moveSpeed);
        }
    }
}