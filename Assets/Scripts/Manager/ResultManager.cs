using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    [SerializeField] Text monsterCountText;
    [SerializeField] Text bombItemCountText;
    [SerializeField] Text energyItemCountText;
    [SerializeField] Text heartItemCountText;
    [SerializeField] Text totalResultText;
    int monsterPrice = 0;
    int bombPrice = 0;
    int energyPrice = 0;
    int heartPrice = 0;
    int totalPrice = 0;

    private void Start()
    {
        TotalResult();
        ResultText();
    }

    void TotalResult()
    {
        monsterPrice = ValueManager.Instance.monsterCount * 10;
        bombPrice = ValueManager.Instance.bombCount * 20;
        energyPrice = ValueManager.Instance.energyCount * 30;
        heartPrice = ValueManager.Instance.heartCount * 40;

        totalPrice = monsterPrice + bombPrice + energyPrice + heartPrice;
        ValueManager.Instance.AddGold(totalPrice);
    }

    void ResultText()
    {
        monsterCountText.text = " x" + ValueManager.Instance.monsterCount.ToString() + " ----- " + monsterPrice + " G";
        bombItemCountText.text = " x" + ValueManager.Instance.bombCount.ToString() + " ----- " + bombPrice + " G";
        energyItemCountText.text = " x" + ValueManager.Instance.energyCount.ToString() + " ----- " + energyPrice + " G";
        heartItemCountText.text = " x" + ValueManager.Instance.heartCount.ToString() + " ----- " + heartPrice + " G";
        totalResultText.text = totalPrice + " G";
    }

    public void Enter()
    {
        ValueManager.Instance.GoldSave();
        SceneController.Instance.ChangeScene("Menu");
    }


}
