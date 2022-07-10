using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Actor
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private int xpOnKill;
    [SerializeField] private BulletAction[] bulletActions;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private GameObject deathEffect;

    private void Start()
    {
        StartCoroutine(_BulletLoop());
    }

    private void Update()
    {
        rigidbody2D.velocity = GetFacingDir() * moveSpeed;
    }

    private IEnumerator _BulletLoop()
    {
        while (alive)
        {
            for (int i = 0; i < bulletActions.Length; i++)
            {
                ShootBullet(bulletActions[i], i);
                yield return new WaitForSeconds(bulletActions[i].duration);
            }
        }
    }

    private void ShootBullet(BulletAction action, int index)
    {
        float actionAngle = action.angle * Mathf.Deg2Rad;
        float facingAngle = action.useFacingDir ? Vector2.SignedAngle(Vector2.right, GetFacingDir()) * Mathf.Deg2Rad : 0;
        float totalAngle = actionAngle + facingAngle;
        Vector2 spawnDir = new Vector2(Mathf.Cos(totalAngle), Mathf.Sin(totalAngle));
        Vector2 spawnPos = (Vector2)transform.position + spawnDir * action.spawnOffset;

        Bullet bullet = Instantiate(action.prefab, spawnPos, Quaternion.identity);
        bullet.Rigidbody2D.velocity = spawnDir * action.speed;
        bullet.SetSource(this);
        bullet.SetActionIndex(index);

        audioSource.PlayOneShot(shootSound);
    }

    public override float GetDamage(int actionIndex)
    {
        return bulletActions[actionIndex].damage;
    }

    protected override void Die()
    {
        audioSource.PlayOneShot(deathSound);
        GameManager.LevelSystem.GainXP(xpOnKill);
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        base.Die();
        Destroy(gameObject, 3f);
    }

    protected override Vector2 GetFacingDir()
    {
        Vector2 playerDir = (GameManager.Player.transform.position - transform.position);
        return playerDir.normalized;
    }

    [System.Serializable]
    private class BulletAction
    {
        public Bullet prefab;
        public float duration = 1f;
        public float angle = 0f;
        public float spawnOffset = 0.6f;
        public float speed = 1f;
        public float damage = 1;
        public bool useFacingDir = false;
    }
}
