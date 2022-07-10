using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Actor : MonoBehaviour
{
    [SerializeField] protected LayerMask opponentLayer;
    [SerializeField] protected float baseMaxHealth = 10;
    [SerializeField] protected AudioClip hurtSound;
    [SerializeField] protected AudioClip shootSound;

    public int MaxHealth { get => (int)baseMaxHealth; }

    protected float maxHealth;
    protected float health;
    protected bool alive = true;

    protected new Collider2D collider2D;
    protected new Rigidbody2D rigidbody2D;
    protected SpriteRenderer spriteRenderer;
    protected AudioSource audioSource;

    protected virtual void Awake()
    {
        collider2D = GetComponent<Collider2D>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        maxHealth = baseMaxHealth;
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

    public abstract float GetDamage(int actionIndex);

    public virtual void UpgradeMaxHealth(float mult)
    {
        maxHealth = baseMaxHealth * mult;
        health = maxHealth;
    }

    public virtual void TakeDamage(Actor source, int actionIndex)
    {
        float damage = source.GetDamage(actionIndex);
        health -= damage;
        audioSource.PlayOneShot(hurtSound);
        if (health <= 0f && alive)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        alive = false;
        rigidbody2D.velocity = Vector2.zero;
        collider2D.enabled = false;
        spriteRenderer.enabled = false;
        enabled = false;
    }

    protected abstract Vector2 GetFacingDir();
}
