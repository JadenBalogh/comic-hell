using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartsPanel : MonoBehaviour
{
    [SerializeField] private Image heartIconPrefab;
    [SerializeField] private float width = 40f;

    private Image[] heartIcons;

    private void Start()
    {
        heartIcons = new Image[GameManager.Player.MaxHealth];
        for (int i = 0; i < GameManager.Player.MaxHealth; i++)
        {
            heartIcons[i] = Instantiate(heartIconPrefab, transform);
            heartIcons[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(-width * i, 0f);
        }
    }

    public void RefreshHearts(int health)
    {
        for (int i = heartIcons.Length - 1; i >= 0; i--)
        {
            heartIcons[i].color = new Color(1, 1, 1, i < health ? 1 : 0.2f);
        }
    }
}
