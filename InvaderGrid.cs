using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvaderGrid : MonoBehaviour
{
    public Invader[] invaders;
    public int rows = 5;
    public int columns = 11;
    public int spacing = 2;
    public AnimationCurve speed;
    public float amtKilled { get; private set; }
    public Projectile missile;
    public float attackRate = 1f;

    int totalInvaders => rows * columns;
    float pct => (float)amtKilled / (float)totalInvaders;
    Vector3 dir = Vector2.right;
    bool _canShoot = true;
    Vector3 _startPos;

    private void Awake()
    {
        _startPos = transform.position;
        CreateGrid(rows, columns);
    }

    private void Start()
    {
        InvokeRepeating(nameof(ShootMissile), attackRate, attackRate);
    }

    private void Update()
    {
        Vector2 leftEdge = Camera.main.ViewportToWorldPoint(Vector2.zero);
        Vector2 rightEdge = Camera.main.ViewportToWorldPoint(Vector2.right);

        transform.position += dir * speed.Evaluate(pct) * Time.deltaTime;

        foreach (Transform invader in this.transform)
        {
            
            if (!invader.gameObject.activeInHierarchy)
            {
                continue;
            }            

            if (dir == Vector3.right && invader.position.x >= rightEdge.x - 1)
            {
                dir = Vector3.left;
                Vector2 pos = this.transform.position;
                pos.y -= 1;
                this.transform.position = pos;
            }
            else if (dir == Vector3.left && invader.position.x <= leftEdge.x + 1)
            {
                dir = Vector3.right;
                Vector2 pos = this.transform.position;
                pos.y -= 1;
                this.transform.position = pos;
            }           
        }

        if (amtKilled >= totalInvaders)
        {
            ResetGrid();
        }
    }

    void CreateGrid(int numRows, int numCols)
    {
        for (int i = 0; i < numRows; i++)
        {
            float gridWidth = spacing * (numCols - 1);
            float gridHeight = spacing * (numRows - 1);
            Vector2 centerPoint = new Vector2(-gridWidth / 2, -gridHeight / 2);
            Vector2 rowPos = new Vector2(centerPoint.x, centerPoint.y + (i * spacing));

            for (int j = 0; j < numCols; j++)
            {
                Invader invader = Instantiate(invaders[i], this.transform);
                invader.died += Die;
                Vector2 colPos = rowPos;
                colPos.x += (j * spacing);
                invader.transform.localPosition = colPos;
            }
        }
    }

    void Die()
    {
        amtKilled++;
    }

    void DestroyProjectile()
    {
        _canShoot = true;
    }

    void ShootMissile()
    {
        foreach (Transform invader in this.transform)
        {

            if (!invader.gameObject.activeInHierarchy)
            {
                continue;
            }

            int spawnChance = Random.Range(1, ((int)totalInvaders - (int)amtKilled) * 2);
            if (spawnChance < 2)
            {
                Projectile proj = Instantiate(missile, invader.transform);
                proj.projectileDestroyed += DestroyProjectile;
                _canShoot = false;
            }
        }
    }

    void ResetGrid()
    {
        transform.position = _startPos;
        CreateGrid(rows, columns);
        amtKilled = 0;
    }

}
