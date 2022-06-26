using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    [SerializeField] private int baseXPToLevel = 100;
    [SerializeField] private int levelXPIncrement = 20;

    private int currXP = 0;
    private int level = 1;
    private int availablePoints = 0;
    private int xpToLevel = 0;

    private void Awake()
    {
        xpToLevel = baseXPToLevel;
    }

    private void Start()
    {
        HUD.XPComponent.UpdateDisplay(currXP, xpToLevel, level);
    }

    public void GainXP(int xp)
    {
        currXP += xp;
        HUD.XPComponent.UpdateDisplay(currXP, xpToLevel, level);

        if (currXP >= xpToLevel)
        {
            currXP = currXP % xpToLevel;
            xpToLevel += levelXPIncrement;
            LevelUp();
        }
    }

    private void LevelUp()
    {
        level++;
        availablePoints++;
        HUD.XPComponent.UpdateDisplay(currXP, xpToLevel, level);
    }
}
