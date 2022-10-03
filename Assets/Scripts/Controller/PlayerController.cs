using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// 플레이어 체력
    /// </summary>
    [HideInInspector]public float health = 100;

    /// <summary>
    /// 총알 프리팹
    /// </summary>
    [SerializeField] GameObject[] m_ShootPrefab;
    /// <summary>
    /// 총알 타입
    /// </summary>
    int m_ShootType = 0;
    /// <summary>
    /// 총알발사위치
    /// </summary>
    [SerializeField] Transform m_ShootPos;

    /// <summary>
    /// 공격타입
    /// </summary>
    [HideInInspector] public int shootType = 0;

    /// <summary>
    /// 플레이어 스피드
    /// </summary>
    [SerializeField] float Speed = 5.0f;
    /// <summary>
    /// 플레이어점프력
    /// </summary>
    [SerializeField] float JumpForce = 5.0f;

    /// <summary>
    /// 효과음 총알, 점프, 게임오버
    /// </summary>
    [SerializeField] AudioClip shootSE, jumpSE, damageSE, gameOverSE;

    AudioSource m_audioSource;
    Animator m_animator;
    Rigidbody2D m_rigidbody2D;

    static PlayerController g_playerController;

    private void Awake()
    {
        if(Instance == null)
        {
            g_playerController = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        m_animator = this.GetComponent<Animator>();
        m_rigidbody2D = this.GetComponent<Rigidbody2D>();
        m_audioSource = this.GetComponent<AudioSource>();
        health = 100;
        shootType = 0;
    }

    void Update()
    {
        if (ValueManager.isStart)
        {
            //이동
            float PosX = Input.GetAxisRaw("Horizontal") * Time.deltaTime * Speed;
            if (PosX > 0)
            {
                m_animator.SetBool("Walk", true);
                this.GetComponent<SpriteRenderer>().flipX = false;
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            else if (PosX < 0)
            {
                m_animator.SetBool("Walk", true);
                transform.localRotation = Quaternion.Euler(0, -180, 0);
            }
            else
            {
                m_animator.SetBool("Walk", false);
            }
            transform.position += new Vector3(PosX, 0, 0);

            //점프
            if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(m_rigidbody2D.velocity.y) < 0.1f)
            {
                m_animator.SetTrigger("Jump");
                m_rigidbody2D.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
                m_audioSource.PlayOneShot(jumpSE);
            }

            //공격
            if (Input.GetKeyDown(KeyCode.E))
            {
                m_animator.SetTrigger("Shoot");
                Shoot();
                m_audioSource.PlayOneShot(shootSE);
            }

            //플레이어 죽음
            if (health <= 0)
            {
                StartCoroutine("PlayerDeath");
            }
        }
    }

    /// <summary>
    /// 플레이어 죽음
    /// </summary>
    /// <returns></returns>
    IEnumerator PlayerDeath()
    {
        m_animator.SetTrigger("Death");
        m_audioSource.PlayOneShot(gameOverSE);
        yield return new WaitForSeconds(1.0f);
        UIManager.Instance.GameEnd();
    }

    /// <summary>
    /// 공격
    /// </summary>
    void Shoot()
    {
        GameObject Shoot;
        Shoot = Instantiate(m_ShootPrefab[m_ShootType], m_ShootPos.position, transform.rotation) as GameObject;
    }

    public void ChageShoot(int argType)
    {
        StartCoroutine("ChageShootType", argType);
    }

    IEnumerator ChageShootType(int argType)
    {
        m_ShootType = argType;
        yield return new WaitForSeconds(5.0f);
        m_ShootType = 0;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Monster")
        {
            health -= 20;
            UIManager.Instance.GetPlayerHealth();
            m_audioSource.PlayOneShot(damageSE);
        }  
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Lava")
        {
            health -= 1000;
            UIManager.Instance.GetPlayerHealth();
            m_audioSource.PlayOneShot(damageSE);
        }
    }

    public static PlayerController Instance
    {
        get { return g_playerController; }
    }
}
