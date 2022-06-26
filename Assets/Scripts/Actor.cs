using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Actor : MonoBehaviour
{
    [SerializeField] protected float moveSpeed = 3f;
    [SerializeField] protected LayerMask opponentLayer;
    [SerializeField] protected int baseMaxHealth = 10;

    private int health;

    protected new Rigidbody2D rigidbody2D;
    protected AudioSource audioSource;

    protected virtual void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        health = baseMaxHealth;
    }

    private void FixedUpdate()
    {
        transform.rotation = Quaternion.FromToRotation(Vector2.right, GetFacingDir());
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (opponentLayer == (opponentLayer | (1 << col.gameObject.layer)))
        {
            if (col.TryGetComponent<Bullet>(out Bullet bullet))
            {
                TakeDamage(bullet.Source, bullet.ActionIndex);
                Destroy(bullet.gameObject);
            }
        }
    }

    public abstract int GetDamage(int actionIndex);

    public virtual void TakeDamage(Actor source, int actionIndex)
    {
        int damage = source.GetDamage(actionIndex);
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        // TODO: spawn death effect
        Destroy(gameObject);
    }

    protected abstract Vector2 GetFacingDir();
}
