using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class CharacterMovement : MonoBehaviour
{
//tanımlamalar

    [SerializeField] float speed;
    Rigidbody2D rb;


    [SerializeField]Camera camera;

    Vector2 input;

 public int rotate;


   
    void Start()
    {
        //rb yi karakterdeki rbye eşitler
        rb=GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        move();
        Rotate();
    }
    private void FixedUpdate() {
        movement();
    }


    public void move(){
        //klavyedeki wasd yada ok tüşları girdisi alınır
        input.x=Input.GetAxisRaw("Horizontal");
        input.y=Input.GetAxisRaw("Vertical");

        //alınan girdiler x y olarak olduğu için  1 1 1kök2 olur çapraza hızlı gider
        //engellemek için normalızed kullanılır o da 1kök2 yi 1 e eşitler  
              input.Normalize();

    }
    public void movement(){
        //girdiyi hizla çarpar ve hareketi sağlar
        rb.velocity = input*speed;
    }
    private void Rotate()
    {
       // Debug.Log(Input.mousePosition);
        Vector3 mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log(mousePos);
        Vector2 direction = (mousePos - transform.position).normalized;
        if (direction.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            rotate = -1;
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            rotate = 1;
        }
    }
}
