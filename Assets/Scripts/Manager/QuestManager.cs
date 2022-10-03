using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class QuestManager : MonoBehaviour
{
    [SerializeField] QuestData[] questDatas;
    [SerializeField] ReplyData[] replyDatas;

    int m_curQuestNum = 0;
    int m_curReplyNum = 0;

    QuestSetting _questSetting;

    [SerializeField] GameObject questObj;

    [Header("quest")]
    [SerializeField] GameObject questPanel;
    [SerializeField] Text questcodeText;
    [SerializeField] Text questContentText;
    [SerializeField] Image questItemImage;
    [SerializeField] Text questCountText;
    [SerializeField] Button questBtn;

    [Header("reply")]
    [SerializeField] GameObject replyPanel;
    [SerializeField] Text replycodeText;
    [SerializeField] Text replyContentText;
    [SerializeField] Image replyItemImage;
    [SerializeField] Text replyCountText;

    string QuestSettingFilePath = null;

    private void Start()
    {
        QuestSettingFilePath = Application.persistentDataPath + "/QuestSetting.json";

        QuestSettingLoad();

        if (m_curQuestNum > m_curReplyNum)
        {
            getReply();
        }
        else
        {
            getQuest();
        }
    }

    public void QuestView()
    {
        questObj.SetActive(true);

        try
        {
            int questIndex = 
                ValueManager.Instance.inventoryList.FindIndex(x => x.itemCode == questDatas[m_curQuestNum].QuestItemCode);

            if (questDatas[m_curQuestNum].QuestItemCount <= 
                ValueManager.Instance.inventoryList[questIndex].itemCount)
            {
                questBtn.interactable = true;
            }
        }
        catch (System.ArgumentOutOfRangeException)
        {
            return;
        }

    }

    void getQuest()
    {
        questPanel.SetActive(true);
        questcodeText.text = "요청 #" + (questDatas[m_curQuestNum].QuestCode + 1);
        questContentText.text = questDatas[m_curQuestNum].QuestContent;
        questItemImage.sprite = ItemManager.Instance.itemDatas[questDatas[m_curQuestNum].QuestItemCode].itemSprite;
        questCountText.text = "x" + questDatas[m_curQuestNum].QuestItemCount;
        QuestSettingSave();
    }

    void getReply()
    {
        replyPanel.SetActive(true);
        replycodeText.text = "보답 #" + (replyDatas[m_curReplyNum].ReplyCode + 1);
        replyContentText.text = replyDatas[m_curQuestNum].Content;
        replyItemImage.sprite = ItemManager.Instance.itemDatas[replyDatas[m_curQuestNum].ReplyRewardCode].itemSprite;
        replyCountText.text = "x" + replyDatas[m_curQuestNum].ReplyRewardCount;
        QuestSettingSave();
    }

    public void Send()
    {
        questPanel.SetActive(false);
        ValueManager.Instance.InventorySave(questDatas[m_curQuestNum].QuestItemCode, -questDatas[m_curQuestNum].QuestItemCount);
        m_curQuestNum++;
        QuestSettingSave();
        questBtn.interactable = false;
    }

    public void Take()
    {
        replyPanel.SetActive(false);
        ValueManager.Instance.InventorySave(replyDatas[m_curQuestNum].ReplyRewardCode, replyDatas[m_curQuestNum].ReplyRewardCount);
        m_curReplyNum++;
        QuestSettingSave();
    }

    void QuestSettingSave()
    {
        _questSetting = new QuestSetting(m_curQuestNum, m_curReplyNum);
        string jdata = JsonUtility.ToJson(_questSetting);
        File.WriteAllText(QuestSettingFilePath, jdata);
        QuestSettingLoad();
    }

    void QuestSettingLoad()
    {
        if (!File.Exists(QuestSettingFilePath))
        {
            SettingReset();
            QuestSettingSave();
            return;
        }
        string jdata = File.ReadAllText(QuestSettingFilePath);
        _questSetting = JsonUtility.FromJson<QuestSetting>(jdata);
        m_curQuestNum = _questSetting.curQuestNum;
        m_curReplyNum = _questSetting.curReplyNum;
    }

    void SettingReset()
    {
        m_curQuestNum = 0;
        m_curReplyNum = 0;
        QuestSettingSave();
    }
}

class QuestSetting
{
    public int curQuestNum;
    public int curReplyNum;

    public QuestSetting(int curQuestNum, int curReplyNum)
    {
        this.curQuestNum = curQuestNum;
        this.curReplyNum = curReplyNum;
    }
}
