using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public int cost;
    public int dmg;
    public float relaodTime;
    public float range;
    public GameObject bulletPrefab;
    public GameObject bulletPoint;
    public bool active = false;

    protected float lastShoot = 0;

    protected Enemy currentEnemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            if (currentEnemy == null)
            {
                currentEnemy = GetClosestEnemy();
            }
            else
            {
                transform.LookAt(currentEnemy.transform.position);
                if (lastShoot + relaodTime < Time.time)
                {
                    Shoot();
                }
            }
        }
    }

    Enemy GetClosestEnemy()
    {
        if (GameManager.instance.enemysOnField.Count > 0)
        {
            float minDist = 999;
            int curr = -1;
            for (int i = 0; i < GameManager.instance.enemysOnField.Count; i++)
            {
                float dist = Vector3.Distance(transform.position, GameManager.instance.enemysOnField[i].transform.position);
                if (dist < minDist && dist <= range)
                {
                    minDist = dist;
                    curr = i;
                }
            }
            if(curr >= 0) return GameManager.instance.enemysOnField[curr];
        }
        return null;
    }

    void Shoot()
    {
        lastShoot = Time.time;
        GameObject b = Instantiate(bulletPrefab, bulletPoint.transform.position, transform.rotation);
        Vector3 dir = currentEnemy.transform.position - bulletPoint.transform.position;
        b.GetComponent<Bullet>().direction = dir.normalized;
        b.GetComponent<Bullet>().parent = this;
    }
}
