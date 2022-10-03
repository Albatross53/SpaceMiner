using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    /// <summary>
    /// 몬스터체력
    /// </summary>
    [SerializeField] int enemyHealth = 100;

    /// <summary>
    /// 몬스터 스피드
    /// </summary>
    float enemySpeed = 4.0f;

    /// <summary>
    /// 데미지효과음
    /// </summary>
    [SerializeField] AudioClip hitSE;

    /// <summary>
    /// 다음 이동할 방향
    /// </summary>
    int nextMove = 0;

    /// <summary>
    /// 플레이어 추격
    /// </summary>
    public bool isChase = false;
    /// <summary>
    /// 플레이어와 거리
    /// </summary>
    float playerDistance;

    GameObject m_player;

    Rigidbody2D m_rigidbody2D;
    AudioSource m_audioSource;

    private void Awake()
    {
        m_player = GameObject.Find("Player");
    }
    private void Start()
    {
        nextMove = 0;
        isChase = false;
        m_audioSource = this.GetComponent<AudioSource>();
        m_rigidbody2D = this.GetComponent<Rigidbody2D>();
        InvokeRepeating("RandomMove", 0f, 2f);
    }

    private void Update()
    {
        if (transform.rotation.y == 0)
        {
            transform.position += Vector3.right * Time.deltaTime * enemySpeed;
        }
        else
        {
            transform.position += Vector3.left * Time.deltaTime * enemySpeed;
        }

        playerDistance = Vector3.Distance(m_player.transform.position, this.transform.position);
        if (playerDistance < 5)
        {
            isChase = true;
        }
        else
        {
            isChase = false;
        }

        if(enemyHealth <= 0)
        {
            ValueManager.Instance.monsterCount++;
            ValueManager.Instance.AddScore(10);
            Destroy(gameObject);
        }
    }

    void RandomMove()
    {
        nextMove = Random.Range(-1, 2);
        if (isChase)
        {
            if(m_player.transform.position.x - gameObject.transform.position.x > 0)
            {
                nextMove = 1;
            }
            else if(m_player.transform.position.x - gameObject.transform.position.x < 0)
            {
                nextMove = -1;
            }            
        }
        if(nextMove > 0)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if(nextMove < 0)
        {
            transform.localRotation = Quaternion.Euler(0, -180, 0);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Shoot")
        {
            m_audioSource.PlayOneShot(hitSE);
            ShootController shootCtrl = other.GetComponent<ShootController>();
            enemyHealth -= shootCtrl.shootAtk;
        }
        else if(other.gameObject.tag == "Lava")
        {
            m_audioSource.PlayOneShot(hitSE);
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Lava")
        {
            m_audioSource.PlayOneShot(hitSE);
            Destroy(gameObject);
        }
    }
}
