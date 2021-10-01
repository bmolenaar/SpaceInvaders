using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryShip : MonoBehaviour
{

    public float speed;
    public float spawnRate;
    public int scoreGiven;

    Vector2 dir = Vector2.right;
    Vector2 startPos;
    bool go;

    void Start()
    {
        startPos = transform.position;
        InvokeRepeating(nameof(CanMove), spawnRate, spawnRate);
    }

    // Update is called once per frame
    void Update()
    {
        MoveAcrossScreen();
    }

    void MoveAcrossScreen()
    {
        if (go)
        {
            Vector2 leftEdge = Camera.main.ViewportToWorldPoint(Vector2.zero);
            Vector2 rightEdge = Camera.main.ViewportToWorldPoint(Vector2.right);

            Vector2 pos;
            pos = transform.position;

            pos += speed * dir * Time.deltaTime;
            transform.position = pos;

            if (pos.x >= rightEdge.x + 1)
            {
                transform.position = startPos;
                go = false;
            }
        }

    }

    void CanMove()
    {
        if (!go)
        {
            go = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("PlayerProjectile"))
        {
            this.gameObject.SetActive(false);
            ScoreMaster.curScore += scoreGiven;
        }
    }
}
