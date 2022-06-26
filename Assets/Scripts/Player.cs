using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor
{
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private float bulletOffsetFwd = 1f;
    [SerializeField] private float bulletOffsetSide = 1f;
    [SerializeField] private float bulletSpeed = 6f;
    [SerializeField] private int bulletDamage = 1;
    [SerializeField] private float bulletCooldown = 0.2f;
    [SerializeField] private AudioClip shootSound;

    private bool canShoot = true;
    private WaitForSeconds bulletCooldownWait;

    protected override void Awake()
    {
        base.Awake();

        bulletCooldownWait = new WaitForSeconds(bulletCooldown);

        GameManager.SetPlayer(this);
    }

    private void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");
        Vector2 inputVector = new Vector2(inputX, inputY);

        if (canShoot && Input.GetButton("Fire1"))
        {
            ShootBullet();
            StartCoroutine(_BulletCooldown());
        }

        rigidbody2D.velocity = inputVector * moveSpeed;
    }

    private void ShootBullet()
    {
        Vector3 mouseDir = GetFacingDir();
        Vector2 spawnPos = transform.position + mouseDir * bulletOffsetFwd;
        Quaternion spawnRot = Quaternion.FromToRotation(Vector2.right, mouseDir);

        Bullet bulletL = Instantiate(bulletPrefab, spawnPos - Vector2.Perpendicular(mouseDir) * bulletOffsetSide, spawnRot);
        bulletL.Rigidbody2D.velocity = mouseDir * bulletSpeed;
        bulletL.SetSource(this);

        Bullet bulletR = Instantiate(bulletPrefab, spawnPos + Vector2.Perpendicular(mouseDir) * bulletOffsetSide, spawnRot);
        bulletR.Rigidbody2D.velocity = mouseDir * bulletSpeed;
        bulletR.SetSource(this);

        audioSource.PlayOneShot(shootSound);
    }

    public override int GetDamage(int actionIndex)
    {
        return bulletDamage;
    }

    private IEnumerator _BulletCooldown()
    {
        canShoot = false;
        yield return bulletCooldownWait;
        canShoot = true;
    }

    protected override Vector2 GetFacingDir()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return (mousePos - (Vector2)transform.position).normalized;
    }
}
