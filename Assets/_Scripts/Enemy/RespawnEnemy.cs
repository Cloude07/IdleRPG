using IdleRPG.Components;
using System.Collections;
using UnityEngine;

namespace IdleRPG.Enemys
{
    public class RespawnEnemy : MonoBehaviour
    {
        private const int SIZE_RADIUS_GIZMOS = 2;
        private const int RADIUS_AXIS_Z = 0;
        private const string ENEMY_TAG = "Enemy";

        [SerializeField] private Vector3 _originPoint = Vector3.zero;

        [SerializeField] private Transform _poolEnemy;
        [SerializeField] private BaseEnemy _enemy;
        [SerializeField] private float _radius = 5f;
        [SerializeField] private float _innerRadius = 4f;

        [Header("Debug")]
        public int waveCount = 1;

        [SerializeField] private int _checkInterval = 2;
        [Range(1,20)] [SerializeField] private int _enemyMult = 1;

        private PoolMono<BaseEnemy> pool;
        private float timeBetweenWaves = 2;

        private void Start()
        {
            CreatePool();
        }

        private void CreatePool()
        {
            this.pool = new PoolMono<BaseEnemy>(_enemy, _enemyMult, _poolEnemy);
            this.pool.autoExpend = true;
            StartCoroutine(SpawnWave());
        }

        IEnumerator SpawnWave()
        {
            while (true)
            {
                yield return new WaitForSeconds(_checkInterval);

                GameObject[] enemies = GameObject.FindGameObjectsWithTag(ENEMY_TAG);

                if (enemies.Length == 0)
                {

                    yield return new WaitForSeconds(timeBetweenWaves);

                    SpawnEnemy();
                }
            }
        }

        private void SpawnEnemy()
        {
            for (int index = 0; index < waveCount * _enemyMult; index++)
            {
                Vector2 spawnPos = new Vector2(Random.Range(0, _radius), Random.Range(0, _radius));

                while (Vector2.Distance(Vector2.zero, spawnPos) < _innerRadius)
                {
                    spawnPos = new Vector2(Random.Range(0, _radius), Random.Range(0, _radius));
                }
                //have a 50% chance to be below or on the left
                if (Random.Range(0, 100) > 50)
                {
                    spawnPos = -spawnPos;
                }
                var enemySpawn = pool.GetFreeElement();
                enemySpawn.transform.position = spawnPos;
                enemySpawn.transform.rotation = Quaternion.identity;

            }
            waveCount++;
        }
    }
}
