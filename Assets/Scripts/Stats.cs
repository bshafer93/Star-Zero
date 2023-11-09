using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A basic stats struct to keep track of universal stats between enemies and player.
/// Such as health and move speed.
/// </summary>
struct Stats 
{
    public float health;
    public float baseHealth;
    [Range(1, 10)] public float speed;
    public float baseSpeed;

    public Stats(float h, float s)
    {
        health = h;
        baseHealth = h;
        speed = s;
        baseSpeed = s;
    }

    public override string ToString()
    {
        return $"Health: {health}\nspeed: {speed}";
    }

    public bool checkHealth(float healthModifier) {

        health += healthModifier;
        if (health > baseHealth) {
            health = baseHealth;
        }
        if (health <= 0) {
            health = 0;
            return false;
        }

        return true;
    
    }


}
