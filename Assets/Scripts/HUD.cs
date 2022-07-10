using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    private static HUD instance;

    public static XPComponent XPComponent { get => instance.xpComponent; }
    [SerializeField] private XPComponent xpComponent;

    public static LevelComponent[] LevelComponents { get => instance.levelComponents; }
    [SerializeField] private LevelComponent[] levelComponents;

    public static TextMeshProUGUI AvailablePoints { get => instance.availablePoints; }
    [SerializeField] private TextMeshProUGUI availablePoints;

    public static HeartsPanel HeartsPanel { get => instance.heartsPanel; }
    [SerializeField] private HeartsPanel heartsPanel;

    public static GameObject DeathPanel { get => instance.deathPanel; }
    [SerializeField] private GameObject deathPanel;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public static void RefreshSkills(int availablePoints)
    {
        foreach (LevelComponent levelComponent in LevelComponents)
        {
            levelComponent.RefreshSkillButton(availablePoints);
        }

        AvailablePoints.text = "Available Points: " + availablePoints;
    }
}
