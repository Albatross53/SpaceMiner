using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource), typeof(CircleCollider2D))]
public class ItemController : MonoBehaviour
{
    [SerializeField] int itemType;
    [SerializeField] AudioClip pickSE;
    AudioSource m_audioSource;

    private void Start()
    {
        m_audioSource = this.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            m_audioSource.PlayOneShot(pickSE);
            GetItemType();
            ValueManager.Instance.AddScore(20);
            UIManager.Instance.ItemText();
            Destroy(gameObject);
        }
    }

    void GetItemType()
    {
        switch (itemType)
        {
            case 0:
                ValueManager.Instance.bombCount++;
                break;
            case 1:
                ValueManager.Instance.energyCount++;
                break;
            case 2:
                ValueManager.Instance.heartCount++;
                break;
            case 3:
                PlayerController.Instance.ChageShoot(1);
                break;
            case 4:
                PlayerController.Instance.ChageShoot(2);
                break;
            default:
                break;
        }
    }
}
