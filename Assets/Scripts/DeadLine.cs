using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadLine : MonoBehaviour
{
    public Action gameOver;

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Unit"))
        {
            Rigidbody2D rigid = collision.GetComponent<Rigidbody2D>();
            if (rigid.velocity.y >= 0)
            {
                //gameover
                Debug.Log("Game Over");
                gameOver?.Invoke();
                GameManager.Instance.GameOver();
            }
        }

    }
}
