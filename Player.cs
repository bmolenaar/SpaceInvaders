using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public Projectile laser;
    public Sprite defaultSprite;
    public Sprite hitSprite;

    //Reference in ScoreMaster script, Update() function
    public static int livesLeft = 3;

    public static bool _dead;


    bool _canShoot = true;
    int cnt;


    private void Awake()
    {
        _dead = false;
        cnt = 0;
        defaultSprite = GetComponent<SpriteRenderer>().sprite;
    }

    void Update()
    {
        Vector2 pos = transform.position;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            pos.x -= moveSpeed * Time.deltaTime;
            transform.position = pos;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            pos.x += moveSpeed * Time.deltaTime;
            transform.position = pos;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }

        if (livesLeft == 2 && cnt == 0)
        {
            Destroy(ScoreMaster.playerLives[2]);
            StartCoroutine(ScoreMaster.PauseAndWait());
            cnt++;
        }
        else if (livesLeft == 1 && cnt == 1)
        {
            Destroy(ScoreMaster.playerLives[1]);
            StartCoroutine(ScoreMaster.PauseAndWait());
            cnt++;
        }
        else if (livesLeft == 0 && cnt == 2)
        {
            _dead = true;
            Destroy(ScoreMaster.playerLives[0]);
            StartCoroutine(ScoreMaster.EnableGameOverText());
            cnt++;
        }

    }

    void Shoot()
    {
        if (_canShoot)
        {
            Projectile proj = Instantiate(laser, this.transform);
            proj.projectileDestroyed += DestroyProjectile;
            _canShoot = false;
        }
        
    }

    void DestroyProjectile()
    {
        _canShoot = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("EnemyProjectile"))
        {
            this.GetComponent<SpriteRenderer>().sprite = hitSprite;
            livesLeft -= 1;
            StartCoroutine(ChangeSprite());
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Invader"))
        {
            _dead = true;
            StartCoroutine(ScoreMaster.EnableGameOverText());
        }
    }

    IEnumerator ChangeSprite()
    {
        yield return new WaitForSecondsRealtime(2);
        this.GetComponent<SpriteRenderer>().sprite = defaultSprite;
    }



}
