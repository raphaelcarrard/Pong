using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndroidControls : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool moveUp;
    private bool moveDown;
    private float verticalMove;
    public float speed = 2;

    void Start()
    {
       rb = GetComponent<Rigidbody2D>();
       moveUp = false;
       moveDown = false; 
    }
 
    public void PointerDownUp()
    {
        moveUp = true;
    }
 
    public void PointerUpUp()
    {
        moveUp = false;
    }
 
    public void PointerDownDown()
    {
        moveDown = true;
    }
 
    public void PointerUpDown()
    {
        moveDown = false;
    }
 
    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }
 
    private void MovePlayer()
    {
        if (moveDown)
        {
            verticalMove = -speed;
        }
        else if (moveUp)
        {
            verticalMove = speed;
        }
        else
        {
            verticalMove = 0;
        }
    }
 
    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, verticalMove);
    }
}
