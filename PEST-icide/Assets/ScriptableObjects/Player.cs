﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField]
    public Character character; // The character scriptedobject

    [SerializeField]
    public int playerNumber; // The controller number that will be passed to the charactercontroller

    // Variables handled by ScriptedObject
    private string playerName;
    private float speed;
    private float jumpHeight;
    private float jumpLength;
    private Collider playerCollider;
    private Rigidbody playerRigidbody;
    private Mesh playerMesh;
    private Material playerMaterial;
    private Vector3 colliderOffset;
    private Vector3 colliderSize;

    // Internal variables
    private uint resources;
    private float invuln;
    private float stun;
    private Vector3 movementVector;
    private float distToGround;
    private Transform attackPosition;

	// Use this for initialization
	void Start () {
        loadCharacter();

	}

    void loadCharacter()
    {
        // Assigning values from scriptableobject
        playerName = character.characterName;
        speed = character.speed;
        jumpHeight = character.jumpHeight;
        jumpLength = character.jumpLength;
        playerCollider = character.collider;
        playerRigidbody = gameObject.GetComponent<Rigidbody>();
        playerMesh = character.mesh;
        playerMaterial = character.material;
        colliderOffset = character.colliderOffset;
        colliderSize = character.colliderSize;

        // Assigning values to gameobject components where necessary
        gameObject.GetComponent<MeshFilter>().mesh = playerMesh;
        gameObject.GetComponent<MeshRenderer>().material = playerMaterial;
        gameObject.AddComponent<BoxCollider>();
        gameObject.GetComponent<BoxCollider>().center = colliderOffset;
        gameObject.GetComponent<BoxCollider>().size = colliderSize;
        distToGround = gameObject.GetComponent<Collider>().bounds.extents.y;
        attackPosition = gameObject.transform.GetChild(1).gameObject.transform;

        // Setting up the camera viewspace
        setCamera();

    }

    // This sets up the part of the split-screen that this players camera will occupy
    void setCamera()
    {
        // Top left
        if(playerNumber == 1)
        {
            Rect temp = Rect.zero;
            temp.Set(0.0f, 0.5f, 0.5f, 0.5f);
            gameObject.GetComponentInChildren<Camera>().rect = temp;
        }

        // Top right
        else if (playerNumber == 2)
        {
            Rect temp = Rect.zero;
            temp.Set(0.5f, 0.5f, 0.5f, 0.5f);
            gameObject.GetComponentInChildren<Camera>().rect = temp;
        }

        // Bottom left
        else if (playerNumber == 3)
        {
            Rect temp = Rect.zero;
            temp.Set(0.0f, 0.0f, 0.5f, 0.5f);
            gameObject.GetComponentInChildren<Camera>().rect = temp;
        }

        // Bottom right
        else if (playerNumber == 4)
        {
            Rect temp = Rect.zero;
            temp.Set(0.5f, 0.0f, 0.5f, 0.5f);
            gameObject.GetComponentInChildren<Camera>().rect = temp;
        }

        // In case something goes wrong
        else
        {
            Debug.Log("Error dividing screen");
        }
    }

    // Checking if the player is grounded
    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }

    // Movement processing and updating
    public void Movement(int playerNumber)
    {
        if (stun <= 0.0f) // Checking to see if the player is stunned
        {
            // Getting the input of the Xbox controller left joystick on the X and Y axes
            movementVector.x = Input.GetAxis("LeftJoystickX_P" + playerNumber);
            movementVector.z = Input.GetAxis("LeftJoystickY_P" + playerNumber);

            // Updating the movement vector by multiplying the normalized vector by its speed vector and delta time
            movementVector = movementVector.normalized * speed * Time.deltaTime;

            // Translating the attached gameobject by the above movementvector
            transform.Translate(movementVector.x, 0, movementVector.z);
        }
    }
    
    // Jumping
    public void Jump()
    {
        if (stun <= 0.0f) // Checking to see if the player is stunned
        {
            // Making sure the player is grounded so they can't jump on thin air
            if (IsGrounded())
            {
                // Jumping is simply applying force on the y-axis to the rigidbody
                playerRigidbody.AddForce(0.0f, jumpHeight, 0.0f, ForceMode.Impulse);
            }
        }
    }

    // Primary attack function
    public void PrimaryAttack()
    {
        if (stun <= 0.0f) // Stun check
        {
            GameObject tempAttack = Instantiate(character.attack1.prefab, attackPosition.position, attackPosition.rotation);
            Destroy(tempAttack, 0.30f);
        }
    }

    // Secondary attack function
    public void SecondaryAttack()
    {
        if (stun <= 0.0f) // Stun check
        {
            GameObject tempAttack = Instantiate(character.attack2.prefab, attackPosition.position, attackPosition.rotation);
            Destroy(tempAttack, 0.20f);
        }
    }

    // Invincibility
    private void Update()
    {
        if(invuln > 0.0f)
        {
            invuln -= Time.deltaTime;
        }

        if(stun > 0.0f)
        {
            stun -= Time.deltaTime;
        }
    }

    public void TakeDamage(uint dmg)
    {
        if (invuln <= 0.0f)
        {
            if(resources > 0.0f)
            {
                resources -= dmg;
                invuln += 3.0f;
            }

            else
            {
                invuln += 3.0f;
                stun += 1.0f;
            }
            
        }
    }

    // Public wrapper for resources
    public uint Resources
    {
        get { return resources; }
        set { value = resources; }
    }


}