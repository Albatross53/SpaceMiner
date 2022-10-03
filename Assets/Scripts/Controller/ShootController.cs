using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    float ShootSpeed = 9.0f;
    float ShootLimit = 1.0f;
    float ShootTimer = 0f;
    public int shootAtk;

    void Update()
    {
        ShootTimer += Time.deltaTime;
        if(ShootTimer > ShootLimit)
        {
            ShootTimer = 0.0f;
            Destroy(gameObject);
        }

        if(transform.rotation.y == 0)
        {
            transform.position += Vector3.right * Time.deltaTime * ShootSpeed;
        }
        else
        {
            transform.position += Vector3.left * Time.deltaTime * ShootSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Monster")
        {
            Destroy(gameObject);
        }
    }
}
