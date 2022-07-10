using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public static void SetPlayer(Player player)
    {
        instance.player = player;
    }
}
