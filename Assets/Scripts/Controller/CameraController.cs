using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject m_player;

    void Update()
    {
        Vector3 Pos = m_player.transform.position;
        this.transform.position = new Vector3(Pos.x, Pos.y, -10);
    }
}
