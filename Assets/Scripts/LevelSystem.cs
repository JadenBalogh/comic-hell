using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    [SerializeField] private int baseXPToLevel = 100;
    [SerializeField] private int levelXPIncrement = 20;
    [SerializeField] private AudioClip xpGainSound;
    [SerializeField] private AudioClip levelUpSound;

    private int currXP = 0;
    private int level = 1;
    private int availablePoints = 0;
    private int xpToLevel = 0;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        xpToLevel = baseXPToLevel;
    }

    private void Start()
    {
        HUD.XPComponent.UpdateDisplay(currXP, xpToLevel, level);
        HUD.RefreshSkills(availablePoints);
    }

    public void GainXP(int xp)
    {
        currXP += xp;
        HUD.XPComponent.UpdateDisplay(currXP, xpToLevel, level);
        audioSource.PlayOneShot(xpGainSound);

        if (currXP >= xpToLevel)
        {
            currXP = currXP % xpToLevel;
            xpToLevel += levelXPIncrement;
            LevelUp();
        }
    }

    public void SpendPoint()
    {
        availablePoints--;
        HUD.RefreshSkills(availablePoints);
    }

    private void LevelUp()
    {
        level++;
        availablePoints++;
        HUD.RefreshSkills(availablePoints);
        HUD.XPComponent.UpdateDisplay(currXP, xpToLevel, level);
        audioSource.PlayOneShot(levelUpSound);
    }
}
