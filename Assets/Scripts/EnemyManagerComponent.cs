using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tanks
{
    public class EnemyManagerComponent : MonoBehaviour
    {
        [SerializeField] private List<GameObject> m_enemyes;
        [SerializeField] private List<SpawnPointComponent> m_spawnPointComponents;
        [SerializeField, Range(1, 3)] private int m_maxEnemy = 1;
        [SerializeField] private GameObject m_enemyPrefab;
        private List<Transform> m_spawnPoints;

        private void Start()
        {
            AddEnemy();
        }

        public void RemoveEnemy(GameObject enemy)
        {
            m_enemyes.Remove(enemy.gameObject);
            if (m_maxEnemy < m_enemyes.Count)
            {
                AddEnemy();
            }
        }

        private void AddEnemy()
        {
            StartCoroutine(SpawnEnemy(m_maxEnemy));
        }

        private SpawnPointComponent GetFreeSpawnPoint()
        {
            SpawnPointComponent freePoint = null;
            foreach (var point in m_spawnPointComponents)
            {
                if (point.GetCanSpawn)
                {
                    freePoint = point;
                }
            }
            return freePoint;
        }

        private IEnumerator SpawnEnemy(int enemyCount)
        {
            for (int i = 0; i < enemyCount; i++)
            {
                SpawnPointComponent res = GetFreeSpawnPoint();
                do
                {
                    yield return new WaitForFixedUpdate();
                    res = GetFreeSpawnPoint();
                } while (res == null);

                GameObject enemy = Instantiate(m_enemyPrefab, res.transform.position, Quaternion.identity);
                m_enemyes.Add(enemy);
            }
        }
    }
}