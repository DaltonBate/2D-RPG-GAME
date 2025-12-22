using System;
using UnityEngine;

public class Enemy : Entity
{
    [Header("Movement details")]
    [SerializeField] protected float moveSpeed = 3.5f;

    private bool playerDetected;

    // Event invoked when an enemy dies. Other systems can subscribe to track kills.
    public static event Action<Enemy> OnEnemyKilled;

    protected override void Update()
    {
        base.Update();
        HandleAttack();
    }

    protected override void HandleAttack()
    {
        if (playerDetected)
        {
            anim.SetTrigger("attack");
        }
    }

    // Default simple movement for generic enemies
    protected override void HandleMovement()
    {
        if (canMove)
            rb.linearVelocity = new Vector2(facingDir * moveSpeed, rb.linearVelocity.y);
        else
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
    }

    protected override void HandleCollision()
    {
        base.HandleCollision();
        playerDetected = Physics2D.OverlapCircle(attackPoint.position, attackRadius, whatIsTarget);
    }

    protected override void Die()
    {
        base.Die();

        // existing UI increment
        UI.Instance?.AddKillCount();

        // notify subscribers
        OnEnemyKilled?.Invoke(this);
    }
}