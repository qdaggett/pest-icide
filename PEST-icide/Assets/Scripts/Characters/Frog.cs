﻿using UnityEngine;

public class Frog : MonoBehaviour {
    // Data members
    private float f_resources;
    private float f_speed;
    private float f_jumpHeight;
    private float f_jumpLength;
    private float f_invuln;
    private Vector3 f_movementVector;
    Collider f_collider;
    private float f_distToGround;
    Rigidbody f_rigidBody;

    public static Frog instance;

    [SerializeField]
    Transform attackPosition;

    [SerializeField]
    GameObject scratch;

    [SerializeField]
    GameObject bite;

	// Use this for initialization
	void Start () {
        Resources = 0.0f;
        Speed = 3.0f;
        JumpHeight = 5.0f;
        JumpLength = 5.0f;

        // Components
        f_rigidBody = gameObject.GetComponent<Rigidbody>();

	}

    // Movement for the frog
    private void frogMovement()
    {
        f_movementVector.x = Input.GetAxis("LeftJoystickX_P3");
        f_movementVector.z = Input.GetAxis("LeftJoystickY_P3");

        f_movementVector = f_movementVector.normalized * Speed * Time.deltaTime;

        transform.Translate(f_movementVector.x, 0, f_movementVector.z);
    }

    // Jumping for the frog
    private void frogJump()
    {
        if(IsGrounded())
        f_rigidBody.AddForce(0.0f, f_jumpHeight, 0.0f, ForceMode.Impulse);
    }

    private void scratchAttack()
    {
        GameObject tempAttack = Instantiate(scratch, attackPosition.position, attackPosition.rotation);
        Destroy(tempAttack, 0.20f);

    }

    private void biteAttack()
    {
        GameObject tempAttack = Instantiate(bite, attackPosition.position, attackPosition.rotation);
        Destroy(tempAttack, 0.30f);
    }

    private void takeDamage(float dmg)
    {
        if(Invulnerable <= 0.0f)
        {
            Resources -= dmg;
            Invulnerable += 3.0f;
        }
    }

    private void Update()
    {
        if(Invulnerable > 0.0f)
        {
            Invulnerable -= Time.deltaTime;
        }
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, f_distToGround + 0.1f);
    }

    // Awake() runs before any Start() calls
    // Enforces the singleton pattern
    private void Awake()
    {
        // Check if instance exists
        if (instance == null)
        {
            // If not, set the game manager to this
            instance = this;
        }

        else if (instance != this)
        {
            Destroy(gameObject);
        }

        // Ensures that this persists between scenes
        DontDestroyOnLoad(gameObject);
    }



    // Setters and getters
    public float Resources
    {
        get { return f_resources; }
        set { f_resources = value; }
    }

    public float Speed
    {
        get { return f_speed; }
        set { f_speed = value; }
    }

    public float JumpHeight
    {
        get { return f_jumpHeight; }
        set { f_jumpHeight = value; }
    }

    public float JumpLength
    {
        get { return f_jumpLength; }
        set { f_jumpLength = value; }
    }

    public float Invulnerable
    {
        get { return f_invuln; }
        set { f_invuln = value; }
    }
	

}
