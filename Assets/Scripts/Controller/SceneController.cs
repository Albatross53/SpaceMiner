using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    static SceneController g_sceneController;

    private void Awake()
    {
        if(Instance == null)
        {
            g_sceneController = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Title")
        {
            LoadMain();   
        }
    }

    /// <summary>
    /// ���� Ÿ��Ʋ���̶�� main������ �̵�
    /// </summary>
    public void LoadMain()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeScene("Menu");
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitGame();
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// ���̵� �Լ�
    /// </summary>
    /// <param name="argSceneName">�ٲܾ��̸�</param>
    public void ChangeScene(string argSceneName)
    {
        SceneManager.LoadScene(argSceneName);
    }

    public static SceneController Instance
    {
        get{ return g_sceneController; }
    }
}
