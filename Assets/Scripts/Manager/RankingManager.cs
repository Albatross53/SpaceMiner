using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

public class RankingManager : MonoBehaviour
{
    /// <summary>
    /// ���� - �̸� ��ũ ����Ʈ
    /// </summary>
    [SerializeField] List<Rank> m_rankingList = new List<Rank>();

    /// <summary>
    /// ��ŷ�ؽ�Ʈ
    /// </summary>
    [SerializeField] Text[] m_rankingTextList;

    /// <summary>
    /// ���÷���, ������, ���͹�ư
    /// </summary>
    [SerializeField] Button m_ExitBtn, m_EnterBtn;

    /// <summary>
    /// ���� ���� ��ũ �ؽ�Ʈ
    /// </summary>
    [SerializeField] Text m_RankText;

    /// <summary>
    /// ��Ŀ �̸� �ʵ�
    /// </summary>
    [SerializeField] InputField m_RankInputText;

    /// <summary>
    /// ��ŷ���� ���
    /// </summary>
    string filePath = null;

    /// <summary>
    /// ��ŷ����������ȯ
    /// </summary>
    public bool isRank = false;

    void Start()
    {
        filePath = Application.persistentDataPath + "/Ranking.json";
        m_ExitBtn.interactable = false;
        isRank = false;
        Load();
        ScoreCheck();
    }

    /// <summary>
    /// ���ھ� ��ũ ����
    /// </summary>
    public void ScoreEnter()
    {
        string RankName = m_RankInputText.text;
        Rank _rank = new Rank(ValueManager.Instance.m_addGold, RankName);
        m_rankingList.Add(_rank);
        //ScoreCheck();
        Save();
        m_EnterBtn.interactable = false;
        m_ExitBtn.interactable = true;
    }

    /// <summary>
    /// ���� ������ ��ŷ���� Ȯ��
    /// </summary>
    void ScoreCheck()
    {
        isRank = m_rankingList.TrueForAll(x => x.score > ValueManager.Instance.m_addGold);
        if (!isRank)
        {
            m_EnterBtn.interactable = true;
            m_RankText.text = "<color=#00FFFF>record</color>";
        }
        else
        {
            m_EnterBtn.interactable = false;
            m_RankText.text = "<color=#FF0000>NULL</color>";
            m_ExitBtn.interactable = true;
        }
    }

    /// <summary>
    /// �ε�� ���Ϸ� ��ŷǥ��
    /// </summary>
    void GetScoreRank()
    {
        m_rankingList.Sort((x, y) => x.score.CompareTo(y.score));
        m_rankingList.Reverse();
        for(int i = 0; i < 10; i++)
        {
            m_rankingTextList[i].text = (i + 1) + "�� / " +m_rankingList[i].score.ToString() +" / " + m_rankingList[i].name;
        }
        if (m_rankingList.Count >= 10)
        {
            m_rankingList.RemoveAt(10);
        }
    }

    /// <summary>
    /// �����ͷε�
    /// </summary>
    void Load()
    {
        if (!File.Exists(filePath))
        {
            Save();
            return;
        }
        string jdata = File.ReadAllText(filePath);
        m_rankingList = JsonUtility.FromJson<Serialization<Rank>>(jdata).target;
        GetScoreRank();
    }

    /// <summary>
    /// �����ͼ��̺�
    /// </summary>
    void Save()
    {
        string jdata = JsonUtility.ToJson(new Serialization<Rank>(m_rankingList));
        File.WriteAllText(filePath, jdata);
        Load();
    }

    /// <summary>
    /// Ÿ��Ʋ�� �̵�
    /// </summary>
    public void GoTitle()
    {
        SceneController.Instance.ChangeScene("Menu");
    }
}

/// <summary>
/// ��ũŬ���� ���� - �̸�
/// </summary>
[Serializable]
public class Rank
{
    public int score;
    public string name;
    public Rank(int argScore, string argName)
    {
        this.score = argScore;
        this.name = argName;
    }
}

/// <summary>
/// ����ȭ
/// </summary>
/// <typeparam name="T">����ȭ���</typeparam>
[Serializable]
public class Serialization<T>
{
    public Serialization(List<T> _target) => target = _target;
    public List<T> target;
}
