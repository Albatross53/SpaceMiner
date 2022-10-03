using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class ValueManager : MonoBehaviour
{
    /// <summary>
    /// 가지고 있는 소지금
    /// </summary>
    public int m_gold
    {
        get;
        set;
    }
    /// <summary>
    /// 얻은 소지금
    /// </summary>
    public int m_addGold
    {
        get;
        set;
    }

    public List<Inventory> inventoryList = new List<Inventory>();

    /// <summary>
    /// 몬스터 처치수
    /// </summary>
    public int monsterCount
    {
        get;
        set;
    }
    /// <summary>
    /// 폭탄아이템
    /// </summary>
    public int bombCount
    {
        get;
        set;
    }
    /// <summary>
    /// 에너지아이템
    /// </summary>
    public int energyCount
    {
        get;
        set;
    }
    /// <summary>
    /// 체력아이템
    /// </summary>
    public int heartCount
    {
        get;
        set;
    }

    public static bool isStart = false;

    static ValueManager g_scoreManager;
    public static ValueManager Instance
    {
        get { return g_scoreManager; }
    }

    GoldData _GoldData;
    string inventoryFilePath = null;
    string goldFilePath = null;

    private void Awake()
    {
        if(Instance == null)
        {
            g_scoreManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        inventoryFilePath = Application.persistentDataPath + "/Inventory.json";
        goldFilePath = Application.persistentDataPath + "/GoldData.json";

        GoldLoad();
        InventoryLoad();
    }

    public void AddGold(int argGold)
    {
        m_addGold += argGold;
    }


    /// <summary>
    /// 점수추가
    /// </summary>
    /// <param name="argScore">추가할 점수</param>
    public void AddScore(int argScore)
    {
        m_addGold += argScore;
        UIManager.Instance.GetScore();
    }

    public void GoldSave()
    {
        m_gold += m_addGold;
        _GoldData = new GoldData(m_gold);
        string jdata = JsonUtility.ToJson(_GoldData);
        File.WriteAllText(goldFilePath, jdata);
        GoldLoad();
    }

    public void GoldLoad()
    {
        if (!File.Exists(goldFilePath))
        {
            m_gold = 0;
            return;
        }
        string jdata = File.ReadAllText(goldFilePath);
        _GoldData = JsonUtility.FromJson<GoldData>(jdata);
        m_gold = _GoldData.gold;
    }

    public void InventorySave(int argCode, int argCount)
    {
        bool has = inventoryList.TrueForAll(x => x.itemCode != argCode);

        if (has)
        {
            Inventory _inventory = new Inventory(argCode, argCount);
            inventoryList.Add(_inventory);
        }
        else
        {
            int Index = inventoryList.FindIndex(x => x.itemCode == argCode);
            inventoryList[Index].itemCount += argCount;
            
        }

        string jdata = JsonUtility.ToJson(new Serialization<Inventory>(inventoryList));
        File.WriteAllText(inventoryFilePath, jdata);
        InventoryLoad();
    }

    public void InventoryLoad()
    {
        if (!File.Exists(inventoryFilePath))
        {
            InventoryReset();
            return;
        }
        string jdata = File.ReadAllText(inventoryFilePath);
        inventoryList = JsonUtility.FromJson<Serialization<Inventory>>(jdata).target;

    }

    void InventoryReset()
    {
        inventoryList = new List<Inventory>();
        string jdata = JsonUtility.ToJson(new Serialization<Inventory>(inventoryList));
        File.WriteAllText(inventoryFilePath, jdata);
        InventoryLoad();
    }

    public void GameStart()
    {
        isStart = false;
        bombCount = 0;
        energyCount = 0;
        heartCount = 0;
        monsterCount = 0;
        m_addGold = 0;
    }
}


/// <summary>
/// 아이템 - 아이템코드, 아이템갯수
/// </summary>
[Serializable]
public class Inventory
{
    public int itemCode;
    public int itemCount;

    public Inventory(int argCode, int argCount)
    {
        this.itemCode = argCode;
        this.itemCount = argCount;
    }
}

public class GoldData
{
    public int gold;

    public GoldData(int gold)
    {
        this.gold = gold;
    }
}

