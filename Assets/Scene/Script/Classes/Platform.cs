using UnityEngine;
public class Platform
{
    public Vector2 position;
    public float length;
    public float height;

    public bool isTouched = false; // Indicates if the platform has been touched by the player

    // Platform types
    // Normal, Vanishing, Moving
    public bool isVanishing;
    public bool isMoving;

    public Platform(Vector2 position, float length, float height, bool isVanishing = false, bool isMoving = false)
    {
        this.position = position;
        this.length = length;
        this.height = height;
        this.isVanishing = isVanishing;
        this.isMoving = isMoving;
    }
}