using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor
{
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private float bulletOffsetFwd = 1f;
    [SerializeField] private float bulletOffsetSide = 1f;
    [SerializeField] protected float moveSpeed = 3f;
    [SerializeField] private float bulletSpeed = 6f;
    [SerializeField] private int bulletDamage = 1;
    [SerializeField] private float bulletCooldown = 0.2f;
    [SerializeField] private BaseSkill[] baseSkills;

    [SerializeField] private Dictionary<Skill, int> skillUpgrades = new Dictionary<Skill, int>();

    private bool canShoot = true;

    protected override void Awake()
    {
        base.Awake();

        GameManager.SetPlayer(this);

        Skill[] skillOptions = (Skill[])System.Enum.GetValues(typeof(Skill));
        foreach (Skill skill in skillOptions)
        {
            skillUpgrades.Add(skill, 0);
        }
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

        rigidbody2D.velocity = inputVector * moveSpeed * GetSkill(Skill.MoveSpeed);
    }

    private void ShootBullet()
    {
        Vector3 mouseDir = GetFacingDir();
        Vector2 spawnPos = transform.position + mouseDir * bulletOffsetFwd;
        Quaternion spawnRot = Quaternion.FromToRotation(Vector2.right, mouseDir);
        Vector2 bulletVel = mouseDir * bulletSpeed * GetSkill(Skill.BulletSpeed);

        Bullet bulletL = Instantiate(bulletPrefab, spawnPos - Vector2.Perpendicular(mouseDir) * bulletOffsetSide, spawnRot);
        bulletL.Rigidbody2D.velocity = bulletVel;
        bulletL.SetSource(this);

        Bullet bulletR = Instantiate(bulletPrefab, spawnPos + Vector2.Perpendicular(mouseDir) * bulletOffsetSide, spawnRot);
        bulletR.Rigidbody2D.velocity = bulletVel;
        bulletR.SetSource(this);

        audioSource.PlayOneShot(shootSound);
    }

    public void UpgradeSkill(Skill skill)
    {
        skillUpgrades[skill]++;
    }

    public int GetSkillCount(Skill skill)
    {
        return skillUpgrades[skill];
    }

    public float GetSkill(Skill skill)
    {
        float baseValue = 0f;
        float upgradeValue = 0f;

        foreach (BaseSkill baseSkill in baseSkills)
        {
            if (baseSkill.skill == skill)
            {
                baseValue = baseSkill.baseValue;
                upgradeValue = baseSkill.upgradeValue;
            }
        }

        return baseValue + upgradeValue * skillUpgrades[skill];
    }

    public override float GetDamage(int actionIndex)
    {
        return bulletDamage * GetSkill(Skill.BulletDamage);
    }

    public override void TakeDamage(Actor source, int actionIndex)
    {
        base.TakeDamage(source, actionIndex);

        HUD.HeartsPanel.RefreshHearts((int)health);
    }

    protected override void Die()
    {
        base.Die();

        HUD.DeathPanel.SetActive(true);
    }

    private IEnumerator _BulletCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(bulletCooldown / GetSkill(Skill.BulletReload));
        canShoot = true;
    }

    protected override Vector2 GetFacingDir()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return (mousePos - (Vector2)transform.position).normalized;
    }

    public enum Skill
    {
        MoveSpeed,
        BulletSpeed,
        BulletDamage,
        BulletReload
    }

    [System.Serializable]
    public struct BaseSkill
    {
        public Skill skill;
        public float baseValue;
        public float upgradeValue;
    }
}
