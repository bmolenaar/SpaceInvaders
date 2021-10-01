using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Projectile : MonoBehaviour
{
    public float speed;
    public Vector2 direction;
    public Action projectileDestroyed;

    private Vector2 _pos;
    

    private void Awake()
    {
        _pos = this.transform.position;
        transform.parent = null;
    }


    void Update()
    {
        _pos += speed * direction * Time.deltaTime;
        this.transform.position = _pos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
            projectileDestroyed.Invoke();
            Destroy(this.gameObject);                    
    }
}
