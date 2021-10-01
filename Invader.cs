using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Invader : MonoBehaviour
{
    SpriteRenderer _spRend;
    GameObject sm;
    int curFrame = 0;

    public Sprite[] frames;
    public float animSpeed = 1.0f;

    public Action died;

    public int score;

    private void Awake()
    {
        _spRend = GetComponent<SpriteRenderer>();
        sm = GameObject.FindGameObjectWithTag("ScoreMaster");

        if (_spRend.name.Contains("Invader1"))
        {
            score = 30;
        }
        else if (_spRend.name.Contains("Invader2"))
        {
            score = 20;
        }
        else
        {
            score = 10;
        }
    }

    void Start()
    {
        InvokeRepeating(nameof(Animate), animSpeed, animSpeed);
    }


    void Animate()
    {
        curFrame++;

        if (curFrame >= frames.Length)
        {
            curFrame = 0;
        }

        _spRend.sprite = frames[curFrame];
   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("PlayerProjectile"))
        {
            died.Invoke();
            this.gameObject.SetActive(false);
            ScoreMaster.curScore += score;
        }

    }
}
