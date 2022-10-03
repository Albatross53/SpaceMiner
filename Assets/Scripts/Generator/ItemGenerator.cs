using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    List<bool> isItem;
    /// <summary>
    /// ���Ͱ� ������ ��ġ
    /// </summary>
    [SerializeField] Transform[] m_genPos = null;
    /// <summary>
    /// ������ ���� ������
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
    /// ���� ����
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
