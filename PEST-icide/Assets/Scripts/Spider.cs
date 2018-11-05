﻿using UnityEngine;
using UnityEngine.Events;

public class Spider : MonoBehaviour {
    // Data members
    private float sp_resources;
    private float sp_speed;
    private float sp_jumpHeight;
    private float sp_jumpLength;
    private Vector3 sp_movementVector;
    Collider sp_collider;
    private float sp_distToGround;
    Rigidbody sp_rigidBody;

    // For events
    private UnityAction spiderMoveEvent;
    private UnityAction spiderJumpEvent;


	// Use this for initialization
	void Start ()
    {
        Resources = 0.0f;
        Speed = 10.0f;
        JumpHeight = 1.5f;
        JumpLength = 1.5f;

        // Components
        sp_rigidBody = gameObject.GetComponent<Rigidbody>();

        // Enables the listeners for spider-related events
        EventManager.instance.StartListening("spiderMoveEvent", spiderMoveEvent);
        EventManager.instance.StartListening("spiderJumpEvent", spiderJumpEvent);
	}

    public void OnDisable()
    {
        EventManager.instance.StopListening("spiderMoveEvent", spiderMoveEvent);
        EventManager.instance.StopListening("spiderJumpEvent", spiderJumpEvent);
    }

    private void Awake()
    {
        spiderMoveEvent = new UnityAction(spiderMovement);
        spiderJumpEvent = new UnityAction(spiderJump);
    }

    private void spiderMovement()
    {
        sp_movementVector.x = Input.GetAxis("Horizontal");
        sp_movementVector.z = Input.GetAxis("Vertical");

        sp_movementVector = sp_movementVector.normalized * Speed * Time.deltaTime;

        transform.Translate(sp_movementVector.x, 0, sp_movementVector.z);
    }

    private void spiderJump()
    {
        sp_rigidBody.AddForce(0.0f, sp_jumpHeight, 0.0f, ForceMode.Impulse);
    }

    // Getters and setters
    public float Resources
    {
        get { return sp_resources; }
        set { sp_resources = value; }
    }

    public float Speed
    {
        get { return sp_speed; }
        set { sp_speed = value; }
    }

    public float JumpHeight
    {
        get { return sp_jumpHeight; }
        set { sp_jumpHeight = value; }
    }

    public float JumpLength
    {
        get { return sp_jumpLength; }
        set { sp_jumpLength = value; }
    }
	
}