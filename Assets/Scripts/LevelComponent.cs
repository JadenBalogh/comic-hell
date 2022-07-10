using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelComponent : MonoBehaviour
{
    [SerializeField] private Button skillButton;
    [SerializeField] private RectTransform skillBubbleParent;
    [SerializeField] private RectTransform skillIndicatorPrefab;
    [SerializeField] private Player.Skill skill;

    public void SpendPoint()
    {
        GameManager.LevelSystem.SpendPoint();
        GameManager.Player.UpgradeSkill(skill);
        AddSkillBubble();
    }

    public void AddSkillBubble()
    {
        RectTransform skillBubble = Instantiate(skillIndicatorPrefab, skillBubbleParent);
        skillBubble.anchoredPosition = new Vector2(25f * (GameManager.Player.GetSkillCount(skill) - 1), 0);
    }

    public void RefreshSkillButton(int availablePoints)
    {
        skillButton.interactable = availablePoints > 0;
    }
}
