using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
public class CharacterController2D : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private Vector2 velocity;
    private bool grounded;
    private bool collisionWall;
    private bool collisionCeiling;
    private ContactFilter2D contactFilter2D = default;
    Rigidbody2D rigidbody_2D;
    // 地面にいるならtrue
    public bool IsGrounded()
    {
        return grounded;
    }
    // 壁と衝突したらtrue
    public bool CollisionWall()
    {
        return collisionWall;
    }
    // 天井と衝突したらtrue
    public bool CollisionCeiling()
    {
        return collisionCeiling;
    }
    // 移動速度を取得
    public Vector2 GetVelocity()
    {
        return velocity;
    }
    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rigidbody_2D = GetComponent<Rigidbody2D>();
    }
    // 横方向の当たり判定
    private void CollisionSide(Vector2 velocity)
    {
        Vector2 sideVelocity = velocity;
        sideVelocity.y = 0.0f;
        Vector2 position = transform.position;
        // TODO レイヤー設定もできるようにする
        List<Collider2D> hits = new List<Collider2D>(5);
        Physics2D.OverlapBox(position + sideVelocity, boxCollider.size, 0, contactFilter2D, hits);
        foreach (Collider2D hit in hits)
        {
            // Ignore our own collider.
            if (hit == boxCollider)
                continue;

            ColliderDistance2D colliderDistance = hit.Distance(boxCollider);

            if (colliderDistance.isOverlapped)
            {
                float angle = Vector2.Angle(colliderDistance.normal, Vector2.up);
                // 壁に衝突
                if (70.0f < angle && angle < 110.0f)
                {
                    collisionWall = true;
                    this.velocity.x = 0.0f;
                }
            }
        }
    }
    // 縦方向の当たり判定
    private void CollisionVertical(Vector2 velocity)
    {
        Vector2 verticalVelocity = velocity;
        if (verticalVelocity.y <= 0.0f)
        {
            verticalVelocity.y -= 0.01f;
        }
        else
        {
            verticalVelocity.y += 0.01f;
        }
        Vector2 position = transform.position;

        // TODO レイヤー設定もできるようにする
        List<Collider2D> hits = new List<Collider2D>(5);
        Physics2D.OverlapBox(position + verticalVelocity, boxCollider.size, 0, contactFilter2D, hits);
        foreach (Collider2D hit in hits)
        {
            // Ignore our own collider.
            if (hit == boxCollider)
                continue;

            ColliderDistance2D colliderDistance = hit.Distance(boxCollider);
            if (colliderDistance.isOverlapped)
            {
                float angle = Vector2.Angle(colliderDistance.normal, Vector2.up);
                // 地面に衝突
                if (angle < 20.0f)
                {
                    grounded = true;
                    this.velocity.y = 0.0f;
                }
                // 天井に衝突
                else if (angle > 160.0f)
                {
                    collisionCeiling = true;
                    this.velocity.y = 0.0f;
                }
            }
        }
    }
    // 移動処理
    public void Move(Vector2 velocity)
    {
        this.velocity = velocity;

        grounded = false;
        collisionWall = false;
        collisionCeiling = false;

        CollisionSide(velocity);
        CollisionVertical(velocity);

        Vector2 position = transform.position;
        rigidbody_2D.MovePosition(position + velocity);
    }
}
