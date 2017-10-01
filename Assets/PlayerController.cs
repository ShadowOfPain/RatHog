using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int speed = 0;
    public int maxSpeed = 200;
    public int speedup = 2;

    private int scaler = 1000;

    private Rigidbody _body;
    private Collider _collider;
    private Animator _animator;


    private int intDirection = 0;

    // Use this for initialization
    void Start()
    {
        _body = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame


    public states currentState = states.idle;

    [Flags]
    public enum states
    {
        idle = 0,
        right = 1,
        left = 2,
        waiting = 4,
        low_right = 8,
        low_left = 16,
        jump    = 32
    }


    public bool IsTurning
    {
        get { return false; }
        set { _animator.SetBool("Turning", value); }
    }
    
    
    public bool IsRunning
    {
        get { return true; }
        set { _animator.SetBool("Running", value); }
    }

    public bool IsIdle
    {
        get { return true; }
        set { _animator.SetBool("Idle", value); }
    }

    public bool IsJumping
    {
        get { return true; }
        set { _animator.SetBool("Jumping", value); }
    }


    public bool IsFlipped
    {
        get { return true; }
        set { _animator.SetBool("Flipped", value); }
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            currentState = currentState | states.right;
        }
        if ((currentState & states.right) != 0 & Input.GetKeyUp(KeyCode.RightArrow))
        {
            currentState = currentState ^ states.right;
        }



        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            currentState = currentState | states.left;
        }
        if ((currentState & states.left) != 0 & Input.GetKeyUp(KeyCode.LeftArrow))
        {
            currentState = currentState ^ states.left;
        }
        
        if ((currentState & states.left) == 0 & Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentState = currentState | states.jump;
        }


        if (Physics.Raycast(transform.position, Vector3.down,1))
        {
            Debug.Log("Hit");
            /* jump ends here */
        }
        
        
        
        

        
        if ((currentState & states.right) != 0)
        {
            _body.AddForce(new Vector3(maxSpeed, 0, 0));
            
            transform.position.Scale(Vector3.right);
        }
        
        
        if ((currentState & states.left) != 0)
        {
            _body.AddForce(new Vector3(-maxSpeed, 0, 0));
            transform.position.Scale(Vector3.left);
            
        }


        IsIdle    =  (((currentState & states.left) | (currentState & states.right)) == 0);
        IsRunning = !(((currentState & states.left) | (currentState & states.right)) == 0);
        
    }
}