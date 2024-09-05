using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class CharacterController2D : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private Vector2 velocity;
    private bool grounded;
    Rigidbody2D rigidbody_2D;
    public bool IsGrounded()
    {
        return grounded;
    }
    public Vector2 GetVelocity()
    {
        return velocity;
    }

    private void Awake()
    {      
        boxCollider = GetComponent<BoxCollider2D>();
        rigidbody_2D = GetComponent<Rigidbody2D>();
    }
    public void Move(Vector2 velocity)
    {
        this.velocity = velocity;
        Vector2 position = transform.position;
        rigidbody_2D.MovePosition(position + this.velocity);

        grounded = false;

        // レイヤー設定もできるっぽい
        Collider2D[] hits = Physics2D.OverlapBoxAll(position, boxCollider.size, 0);

        foreach (Collider2D hit in hits)
        {
            // Ignore our own collider.
            if (hit == boxCollider)
                continue;
            
            ColliderDistance2D colliderDistance = hit.Distance(boxCollider);

            if (colliderDistance.isOverlapped)
            {
                // transform.Translate(colliderDistance.pointA - colliderDistance.pointB);
                
                if (Vector2.Angle(colliderDistance.normal, Vector2.up) < 90 && this.velocity.y < 0)
                {
                    grounded = true;
                }
            }
        }
    }
}
