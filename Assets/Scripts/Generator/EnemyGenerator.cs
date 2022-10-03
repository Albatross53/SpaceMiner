using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    /// <summary>
    /// �ִ� ���� ��
    /// </summary>
    [SerializeField] int maxEnemy = 100;
    /// <summary>
    /// ���� ���ͼ�
    /// </summary>
    int enemyCount = 0;
    /// <summary>
    /// ���Ͱ� ������ ��ġ
    /// </summary>
    [SerializeField] Transform[] m_genPos = null;
    /// <summary>
    /// ������ ���� ������
    /// </summary>
    [SerializeField] GameObject[] m_enemyPrefabs = null;

    private void Start()
    {
        InvokeRepeating("SpawnEnemy", 4f, 1f);
    }

    /// <summary>
    /// ���� ����
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
