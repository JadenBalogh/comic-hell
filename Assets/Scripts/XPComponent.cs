using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class XPComponent : MonoBehaviour
{
    [SerializeField] private RectTransform barFill;
    [SerializeField] private TextMeshProUGUI text;

    public void UpdateDisplay(int xp, int xpToLevel, int level)
    {
        barFill.anchorMax = new Vector2((float)xp / xpToLevel, 1f);
        text.text = "Level " + level + " - " + xp + " / " + xpToLevel;
    }
}
