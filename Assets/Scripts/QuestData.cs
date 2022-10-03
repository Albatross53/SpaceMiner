using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestData", menuName = "ScroptableObj/QuestData", order = int.MaxValue)]

public class QuestData : ScriptableObject
{
    [SerializeField] int questCode;
    public int QuestCode
    {
        get { return questCode; }
    }

    [SerializeField] [TextArea] string  questContent;
    public string QuestContent
    {
        get { return questContent; }
    }

    [SerializeField] int questItemCode;
    public int QuestItemCode
    {
        get { return questItemCode; }
    }

    [SerializeField] int questItemCount;
    public int QuestItemCount
    {
        get { return questItemCount; }
    }
}
