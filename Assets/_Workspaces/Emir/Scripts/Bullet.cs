using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        // Burada merminin çarptığı nesneye zarar verebilirsiniz.
        if(hitInfo.CompareTag("Enemy") || hitInfo.CompareTag("Ground"))
        {
            //siviller icinde yapılıcak
             Destroy(gameObject); // Mermiyi yok et
        }

       
    }

    
    private void Update()
    {
        
    }


}
