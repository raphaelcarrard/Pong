using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleScript : MonoBehaviour
{

    public Rigidbody2D rb2d;
    public int id;
    public float moveSpeed = 2f;
    public float cpuDeadZone = 1f;

    private Vector3 startPosition;
    private int direction = 0;
    private float moveSpeedMultiplier = 1f;

    private void Start(){
        startPosition = transform.position;
        GameManager.instance.onReset += ResetPosition;
    }

    private void ResetPosition(){
        transform.position = startPosition;
    }

    private void Update(){
        if(IsCPU()){
            MoveCPU();
        }
        else 
        {
            float movement = ProcessInput();
            Move(movement);
        }
    }

    private bool IsCPU(){
        bool isPlayer1CPU = id == 1 && GameManager.instance.IsPlayer1CPU();
        bool isPlayer2CPU = id == 2 && GameManager.instance.IsPlayer2CPU();
        return isPlayer1CPU || isPlayer2CPU;
    }

    private void MoveCPU(){
        Vector2 ballPos = GameManager.instance.ball.transform.position;
        if(Mathf.Abs(ballPos.y - transform.position.y) > cpuDeadZone){
            direction = ballPos.y > transform.position.y ? 1 : -1;
        }
        if(Random.value < 0.01f){
            moveSpeedMultiplier = Random.Range(0.5f, 1.5f);
        }
        Move(direction);
    }

    private float ProcessInput(){
        float movement = 0f;
        switch(id){
            case 1:
                movement = Input.GetAxis("MovePlayer1");
                break;
            case 2:
                movement = Input.GetAxis("MovePlayer2");
                break;
        }
        return movement;
    }

    private void Move(float movement){
        Vector2 vel = rb2d.velocity;
        vel.y = moveSpeed * moveSpeedMultiplier * movement;
        rb2d.velocity = vel;
    }

    public float GetHeight(){
        return transform.localScale.y;
    }

    public bool IsLeftPaddle(){
        return id == 1;
    }
}
