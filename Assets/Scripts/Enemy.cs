using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public int hp;
    private int currentHp;
    private Rigidbody rb;

    public int CurrentHp 
    { 
        get
        {
            return currentHp;
        }
        set
        {
            currentHp = value;
            if(currentHp <= 0)
                DestroyObject();
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        CurrentHp = (int)(hp * GameManager.instance.hpMultiplyer);
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Damage(int dmg)
    {
        CurrentHp -= dmg;
    }

    protected virtual void Move()
    {
        rb.AddForce(Vector3.back * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "DeathZone")
        {
            DestroyObject();
            GameManager.instance.GameOver();
        }
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
        GameManager.instance.Score(1);
        GameManager.instance.enemysOnField.Remove(this);
    }
}
