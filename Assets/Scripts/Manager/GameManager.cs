using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GAMEMOD { MINING, RANKING }
    public enum GAMESTATE { NONE, STOP }

    GAMEMOD m_GameMod = GAMEMOD.MINING;
    GAMESTATE m_GameState = GAMESTATE.NONE;

    static GameManager g_gameManager;

    private void Awake()
    {
        if(Instance == null)
        {
            g_gameManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ChangeState(GAMESTATE argState)
    {
        m_GameState = argState;
        GameState(m_GameState);
    }

    public void changeMod(GAMEMOD argMod)
    {
        m_GameMod = argMod;
        GameState(m_GameState);
    }

    public void GameModStart()
    {
        switch (m_GameMod)
        {
            case GAMEMOD.MINING:
                UIManager.Instance.MiningModStart();
                break;
            case GAMEMOD.RANKING:
                UIManager.Instance.RankModStart();
                break;
            default:
                break;
        }
    }

    public void GameState(GAMESTATE argState)
    {
        m_GameState = argState;
        switch(m_GameState)
        {
            case GAMESTATE.NONE:
                Time.timeScale = 1;
                break;
            case GAMESTATE.STOP:
                Time.timeScale = 0;
                break;
            default:
                break;
        }
    }


    public static GameManager Instance
    {
        get { return g_gameManager; }
    }
}