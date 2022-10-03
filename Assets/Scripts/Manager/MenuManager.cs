using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Ã¤±¤ÇÏ±â
public class MenuManager : MonoBehaviour
{
    public void SetMiningMod()
    {
        GameManager.Instance.changeMod(GameManager.GAMEMOD.MINING);
    }

    public void SetRankingMod()
    {
        GameManager.Instance.changeMod(GameManager.GAMEMOD.RANKING);
    }

    public void CaveLoad()
    {
        SceneController.Instance.ChangeScene("Map_Cave");
    }

    public void LavaLoad()
    {
        SceneController.Instance.ChangeScene("Map_Lava");
    }

    public void GameExit()
    {
        Application.Quit();
    }
}
