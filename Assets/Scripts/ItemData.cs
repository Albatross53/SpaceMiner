using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "ScroptableObj/ItemData", order = int.MaxValue)]
public class ItemData : ScriptableObject
{
    [SerializeField] int itemCode;

    public int ItemCode
    {
        get { return itemCode; }
    }

    [SerializeField] int price;

    public int Price
    {
        get { return price; }
    }

    public Sprite itemSprite;
}
