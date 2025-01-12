﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ## DEFUNCT SCRIPT - NO LONGER IN USE ##

[CreateAssetMenu(fileName = "New Character", menuName = "Character")]
public class Character : ScriptableObject {

    public string characterName; // Name of character
    public float speed; // Movement speed of character
    public float jumpHeight; // Jump height of character
    public float jumpLength; // Jump length of character
    public GameObject animatedCharacter;
    public Collider collider; // The collider for the character
    public Rigidbody rigidbody; // The rigidbody for the character
    public Material material; // The material/texture for the character
    public Vector3 colliderOffset;
    public Vector3 colliderSize;
    public int health; // Max HP


    // UI Elements
    public Sprite reticle; // Crosshair


    public MeleeAttack attack1;
    public MeleeAttack attack2;


	public AudioClip MovementSFX;



}
