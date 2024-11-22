using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleScript : MonoBehaviour
{

    public static PaddleScript instance;
    public Rigidbody2D rb2d;
    public BoxCollider2D coll2d;
    public int id;
    public float moveSpeed = 2f;

    [Header("CPU")]
    public float cpuDeadZone = 1f;
    public float cpuMoveSpeedMultiplierMin = 0.5f, cpuMoveSpeedMultiplierMax = 1.5f;

    private Vector3 startPosition;
    private int direction = 0;
    private float moveSpeedMultiplier = 1f;

    private const string MovePlayer1InputName = "MovePlayer1";
    private const string MovePlayer2InputName = "MovePlayer2";

    void Awake(){
        instance = this;
    }

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
            float movement = GetInput();
            Move(movement);
        }
        if(Input.GetKeyDown(KeyCode.Escape)){
            transform.position = startPosition;
        }
    }

    private bool IsCPU(){
        bool isPlayer1CPU = IsLeftPaddle() && GameManager.instance.IsPlayer1CPU();
        bool isPlayer2CPU = !IsLeftPaddle() && GameManager.instance.IsPlayer2CPU();
        return isPlayer1CPU || isPlayer2CPU;
    }

    private void MoveCPU(){
        Vector2 ballPos = GameManager.instance.ball.transform.position;
        if(Mathf.Abs(ballPos.y - transform.position.y) > cpuDeadZone){
            direction = ballPos.y > transform.position.y ? 1 : -1;
        }
        if(Random.value < 0.01f){
            moveSpeedMultiplier = Random.Range(cpuMoveSpeedMultiplierMin, cpuMoveSpeedMultiplierMax);
        }
        Move(direction);
    }

    private float GetInput(){
        return IsLeftPaddle() ? Input.GetAxis(MovePlayer1InputName) : Input.GetAxis(MovePlayer2InputName);
    }

    private void Move(float movement){
        Vector2 vel = rb2d.velocity;
        vel.y = moveSpeed * moveSpeedMultiplier * movement;
        rb2d.velocity = vel;
    }

    public float GetHeight(){
        return coll2d.size.y;
    }

    public bool IsLeftPaddle(){
        return id == 1;
    }
}
