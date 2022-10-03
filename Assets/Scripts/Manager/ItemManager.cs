using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 창고와 상점 관리
/// </summary>
public class ItemManager : MonoBehaviour
{
    public ItemData[] itemDatas;

    [SerializeField] Text goldText;

    [Header("storage")]
    [SerializeField] GameObject inventory;
    [SerializeField] Image[] SlotIamges;
    [SerializeField] Text[] SlotCountTexts;


    [Header("shelf")]
    [SerializeField] Button[] shelfs;
    [SerializeField] Image[] shelfItemImages;
    [SerializeField] Text[] shelfPriceTexts;

    [SerializeField] Text cartCountText;
    [SerializeField] Text cartPriceText;
    [SerializeField] Text pocketText;
    [SerializeField] Image CartSprite;

    [SerializeField] GameObject errorPanel;

    /// <summary>
    /// 구매할 상품 코드
    /// </summary>
    int cartItem = 0;
    /// <summary>
    /// 구매할 상품 갯수
    /// </summary>
    int cartCount = 0;
    /// <summary>
    /// 총합가격
    /// </summary>
    int totalPrice = 0;

    static ItemManager g_ItemManager;
    public static ItemManager Instance
    {
        get { return g_ItemManager; }
    }
    private void Awake()
    {
        if(Instance == null)
        {
            g_ItemManager = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        NewShelf();
        GetGold();
    }

    void GetGold()
    {
        ValueManager.Instance.GoldLoad();
        goldText.text = "소지금: " + ValueManager.Instance.m_gold + "G";
        pocketText.text = "소지금: " + ValueManager.Instance.m_gold + "G";
    }

    void NewShelf()
    {
        int shelfCount = Random.Range(1, shelfs.Length);
        for (int i = 0; i < shelfCount; i++)
        {
            shelfs[i].gameObject.SetActive(true);
            int ranItemCode = Random.Range(0, itemDatas.Length);

            shelfItemImages[i].sprite = itemDatas[ranItemCode].itemSprite;
            shelfPriceTexts[i].text = itemDatas[ranItemCode].Price.ToString() + "G";
            shelfs[i].onClick.AddListener(() => SetCart(ranItemCode));
        }

        CartSprite.sprite = null;
    }

    public void InventoryView()
    {
        ValueManager.Instance.InventoryLoad();
        inventory.SetActive(true);

        for (int i = 0; i < ValueManager.Instance.inventoryList.Count; i++)
        {
            SlotIamges[i].sprite = itemDatas[ValueManager.Instance.inventoryList[i].itemCode].itemSprite;
            SlotCountTexts[i].text = ValueManager.Instance.inventoryList[i].itemCount.ToString();
        }
    }

    public void BuyItem()
    {
        if (cartCount > 0)
        {
            if (ValueManager.Instance.m_gold >= totalPrice)
            {
                ValueManager.Instance.m_gold -= totalPrice;
                ValueManager.Instance.GoldSave();
                GetGold();

                ValueManager.Instance.InventorySave(cartItem, cartCount);
                GetGold();

                ResetCart();
            }
            else
            {
                errorPanel.SetActive(true);
                ResetCart();
            }
        }
    }

    void ResetCart()
    {
        cartCount = 0;
        cartCountText.text = "x" + cartCount.ToString("00");
        cartPriceText.text = "가격: 0G";
    }

    void SetCart(int argCode)
    {
        cartCount = 0;
        cartCountText.text = "x" + cartCount.ToString("00");
        cartItem = argCode;
        CartItem();
    }

    public void AddCart()
    {
        if(cartCount < 100)
        {
            cartCount++;
            cartCountText.text = "x" + cartCount.ToString("00");
            totalPrice = itemDatas[cartItem].Price * cartCount;
            cartPriceText.text = "가격: " + totalPrice + "G";
        }
        return;
    }
    public void RemoveCart()
    {
        if(cartCount > 0)
        {
            cartCount--;
            cartCountText.text = cartCount.ToString("00");
            totalPrice = itemDatas[cartItem].Price * cartCount;
            cartPriceText.text = "가격: " + totalPrice + "G";
        }
        return;
    }

    void CartItem()
    {
        CartSprite.sprite = itemDatas[cartItem].itemSprite;
    }
}
