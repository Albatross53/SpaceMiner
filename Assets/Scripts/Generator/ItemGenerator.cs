using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    List<bool> isItem;
    /// <summary>
    /// 몬스터가 스폰될 위치
    /// </summary>
    [SerializeField] Transform[] m_genPos = null;
    /// <summary>
    /// 스폰될 몬스터 프리팹
    /// </summary>
    [SerializeField] GameObject[] m_ItemPrefabs = null;

    private void Start()
    {
        isItem = new List<bool>();
        for (int i = 0; i < m_genPos.Length; i++)
        {
            isItem.Add(false);
        }
        InvokeRepeating("SpawnEnemy", 4f, 3f);
    }

    /// <summary>
    /// 몬스터 스폰
    /// </summary>
    void SpawnEnemy()
    {
        int ran = Random.Range(0, m_genPos.Length);
        if (!isItem[ran])
        {
            int itemNum = Random.Range(0, m_ItemPrefabs.Length);
            GameObject _item;
            _item = Instantiate(m_ItemPrefabs[itemNum], m_genPos[ran].position, transform.rotation);
            isItem[ran] = true;
        }
        return;
    }
}
