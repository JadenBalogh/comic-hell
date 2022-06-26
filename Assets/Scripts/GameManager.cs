using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static LevelSystem LevelSystem { get; private set; }

    public static Player Player { get => instance.player; }
    private Player player;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        LevelSystem = GetComponent<LevelSystem>();
    }

    public static void SetPlayer(Player player)
    {
        instance.player = player;
    }
}
