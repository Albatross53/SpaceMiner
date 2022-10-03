using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

public class RankingManager : MonoBehaviour
{
    /// <summary>
    /// 점수 - 이름 랭크 리스트
    /// </summary>
    [SerializeField] List<Rank> m_rankingList = new List<Rank>();

    /// <summary>
    /// 랭킹텍스트
    /// </summary>
    [SerializeField] Text[] m_rankingTextList;

    /// <summary>
    /// 리플레이, 나가기, 엔터버튼
    /// </summary>
    [SerializeField] Button m_ExitBtn, m_EnterBtn;

    /// <summary>
    /// 게임 점수 랭크 텍스트
    /// </summary>
    [SerializeField] Text m_RankText;

    /// <summary>
    /// 랭커 이름 필드
    /// </summary>
    [SerializeField] InputField m_RankInputText;

    /// <summary>
    /// 랭킹저장 경로
    /// </summary>
    string filePath = null;

    /// <summary>
    /// 랭킹점수인지반환
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
    /// 스코어 랭크 저장
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
    /// 게임 점수가 랭킹인지 확인
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
    /// 로드된 파일로 랭킹표시
    /// </summary>
    void GetScoreRank()
    {
        m_rankingList.Sort((x, y) => x.score.CompareTo(y.score));
        m_rankingList.Reverse();
        for(int i = 0; i < 10; i++)
        {
            m_rankingTextList[i].text = (i + 1) + "등 / " +m_rankingList[i].score.ToString() +" / " + m_rankingList[i].name;
        }
        if (m_rankingList.Count >= 10)
        {
            m_rankingList.RemoveAt(10);
        }
    }

    /// <summary>
    /// 데이터로드
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
    /// 데이터세이브
    /// </summary>
    void Save()
    {
        string jdata = JsonUtility.ToJson(new Serialization<Rank>(m_rankingList));
        File.WriteAllText(filePath, jdata);
        Load();
    }

    /// <summary>
    /// 타이틀로 이동
    /// </summary>
    public void GoTitle()
    {
        SceneController.Instance.ChangeScene("Menu");
    }
}

/// <summary>
/// 랭크클래스 점수 - 이름
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
/// 직열화
/// </summary>
/// <typeparam name="T">직열화대상</typeparam>
[Serializable]
public class Serialization<T>
{
    public Serialization(List<T> _target) => target = _target;
    public List<T> target;
}
