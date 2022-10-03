using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ReplyData", menuName = "ScroptableObj/ReplyData", order = int.MaxValue)]

public class ReplyData : ScriptableObject
{
    [SerializeField] int replyCode;
    public int ReplyCode
    {
        get { return replyCode; }
    }

    [SerializeField] [TextArea] string content;
    public string Content
    {
        get { return content; }
    }

    [SerializeField] int replyRewardCode;
    public int ReplyRewardCode
    {
        get { return replyRewardCode; }
    }

    [SerializeField] int replyRewardCount;
    public int ReplyRewardCount
    {
        get { return replyRewardCount; }
    }
}
