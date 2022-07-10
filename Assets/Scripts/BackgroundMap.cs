using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMap : MonoBehaviour
{
    [SerializeField] private GridTransform[] backgroundTiles;
    [SerializeField] private int gridSize = 20;

    private Vector2 prevGridPos = Vector2.zero;

    private void Start()
    {
        Vector2 playerPos = GameManager.Player.transform.position;
        Vector2 playerGridPos = new Vector2(Mathf.RoundToInt(playerPos.x / 20f), Mathf.RoundToInt(playerPos.y / 20f));
        PlaceTiles(playerGridPos);
    }

    private void Update()
    {
        Vector2 playerPos = GameManager.Player.transform.position;
        Vector2 playerGridPos = new Vector2(Mathf.RoundToInt(playerPos.x / 20f), Mathf.RoundToInt(playerPos.y / 20f));

        if (playerGridPos != prevGridPos)
        {
            PlaceTiles(playerGridPos);
        }
    }

    private void PlaceTiles(Vector2 playerGridPos)
    {
        for (int i = 0; i < backgroundTiles.Length; i++)
        {
            GridTransform gridTile = backgroundTiles[i];
            gridTile.tile.position = (gridTile.gridPos + playerGridPos) * gridSize;
        }

        prevGridPos = playerGridPos;
    }

    [System.Serializable]
    public struct GridTransform
    {
        public Transform tile;
        public Vector2 gridPos;
    }
}
