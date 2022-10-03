using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Text readyText;

    [SerializeField] Text scoreText;
    [SerializeField] Text timeText;
    [SerializeField] Slider healthSlider;
    [SerializeField] Text healthText;

    [SerializeField] GameObject ItemCountObj;
    [SerializeField] Text healthItemCount;
    [SerializeField] Text energyItemCount;
    [SerializeField] Text bombItemCount;

    [SerializeField] GameObject OptionObj;

    float time = 60;
    string loadSceneName = "Result";

    static UIManager g_UIManager;

    public static UIManager Instance
    {
        get { return g_UIManager; }
    }

    private void Awake()
    {
        if(Instance == null)
        {
            g_UIManager = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ValueManager.Instance.GameStart();
        GameManager.Instance.GameModStart();
        StartCoroutine("Ready");
    }

    private void Update()
    {
        if(time <= 0)
        {
            GameEnd();
        }
    }

    public void GameEnd()
    {
        SceneController.Instance.ChangeScene(loadSceneName);
    }

    public void GoMenu()
    {
        SceneController.Instance.ChangeScene("Menu");
    }

    public void RankModStart()
    {
        scoreText.gameObject.SetActive(true);
        scoreText.text = "점수: 0000000000";

        loadSceneName = "Ranking";
    }

    public void MiningModStart()
    {
        ItemCountObj.SetActive(true);
        bombItemCount.text = "x" + ValueManager.Instance.bombCount.ToString("00");
        energyItemCount.text = "x" + ValueManager.Instance.energyCount.ToString("00");
        healthItemCount.text = "x" + ValueManager.Instance.heartCount.ToString("00");

        loadSceneName = "Result";
    }

    public void ItemText()
    {
        bombItemCount.text = "x" + ValueManager.Instance.bombCount.ToString("00");
        energyItemCount.text = "x" + ValueManager.Instance.energyCount.ToString("00");
        healthItemCount.text = "x" + ValueManager.Instance.heartCount.ToString("00");
    }

    void TimerText()
    {
        timeText.text = "남은시간: " + time.ToString("00");
    }

    public void TimerOff()
    {
        StopCoroutine("Timer");
    }

    public void TimerOn()
    {
        StartCoroutine("Timer");
    }

    public void GetPlayerHealth()
    {
        healthSlider.value = PlayerController.Instance.health;
        healthText.text = PlayerController.Instance.health.ToString();
    }

    public void GetScore()
    {
        scoreText.text = "점수: " + ValueManager.Instance.m_addGold.ToString("0000000000");
    }

    IEnumerator Timer()
    {
        time--;
        TimerText();
        yield return new WaitForSeconds(1.0f);
        StartCoroutine("Timer");
    }

    IEnumerator Ready()
    {
        readyText.gameObject.SetActive(true);
        readyText.text = "3";
        yield return new WaitForSeconds(1.0F);
        readyText.text = "2";
        yield return new WaitForSeconds(1.0F);
        readyText.text = "1";
        yield return new WaitForSeconds(1.0F);
        readyText.text = "시작";
        yield return new WaitForSeconds(1.0F);
        readyText.gameObject.SetActive(false);
        ValueManager.isStart = true;
        StartCoroutine("Timer");
    }

    public void GameExit()
    {
        SceneController.Instance.ChangeScene("Menu");
    }

    public void OptionOn()
    {
        OptionObj.SetActive(true);
        GameManager.Instance.ChangeState(GameManager.GAMESTATE.STOP);
    }

    public void OptionOff()
    {
        OptionObj.SetActive(false);
        GameManager.Instance.ChangeState(GameManager.GAMESTATE.NONE);
    }
}
