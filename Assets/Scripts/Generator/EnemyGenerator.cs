using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    /// <summary>
    /// 최대 몬스터 수
    /// </summary>
    [SerializeField] int maxEnemy = 100;
    /// <summary>
    /// 현재 몬스터수
    /// </summary>
    int enemyCount = 0;
    /// <summary>
    /// 몬스터가 스폰될 위치
    /// </summary>
    [SerializeField] Transform[] m_genPos = null;
    /// <summary>
    /// 스폰될 몬스터 프리팹
    /// </summary>
    [SerializeField] GameObject[] m_enemyPrefabs = null;

    private void Start()
    {
        InvokeRepeating("SpawnEnemy", 4f, 1f);
    }

    /// <summary>
    /// 몬스터 스폰
    /// </summary>
    void SpawnEnemy()
    {
        if(enemyCount < maxEnemy)
        {
            int randomPos = Random.Range(0, m_genPos.Length);
            int randomEnemy = Random.Range(0, m_enemyPrefabs.Length);
            GameObject Enemy;
            Enemy = Instantiate(m_enemyPrefabs[randomEnemy], m_genPos[randomPos].position, transform.rotation);
            enemyCount++;
        }
    }


}
