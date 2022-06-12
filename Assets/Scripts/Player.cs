using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [SerializeField]
    float speed = 10f;

    [SerializeField]
    float jumpSpeed = 20f;

    Rigidbody2D myRigidBody2D;
    Animator myAnimator;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        Jump();
    }

    private void Jump()
    {
        bool isjumping = CrossPlatformInputManager.GetButtonDown("Jump");

        if (isjumping)
        {
            Vector2 jumpVelocity = new Vector2(myRigidBody2D.velocity.x, jumpSpeed);
            myRigidBody2D.velocity = jumpVelocity;
        }
    }

    private void Run()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal");

        Vector2 playerVelocity = new Vector2(controlThrow * speed, myRigidBody2D.velocity.y);
        myRigidBody2D.velocity = playerVelocity;

        FlipSprite();
        ChangingToRunningState();
    }

    private void ChangingToRunningState()
    {
        bool runningHorizontaly = Mathf.Abs(myRigidBody2D.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("Running", runningHorizontaly);
    }

    private void FlipSprite()
    {
        bool runningHorizontaly = Mathf.Abs(myRigidBody2D.velocity.x) > Mathf.Epsilon;

        if (runningHorizontaly)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody2D.velocity.x), 1f);
        }
    }
}
