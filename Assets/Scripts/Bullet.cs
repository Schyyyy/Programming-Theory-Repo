using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 30f;
    public Tower parent;
    public Vector3 direction;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(direction * speed, ForceMode.Impulse);
    }

    private void Update()
    {
        if(transform.position.x < -20 || transform.position.x > 20 || transform.position.y < -20 || transform.position.y > 20)
            Destroy(gameObject);    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "enemy")
        {
            Enemy e = other.gameObject.GetComponent<Enemy>();
            e.Damage(parent.dmg);
        }
    }
}
