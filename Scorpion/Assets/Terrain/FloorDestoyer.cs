using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FloorDestoyer : MonoBehaviour
{
    
    [SerializeField] GameObject fallingTilePrefab;

    private Tilemap floor;

    void Start()
    {
        floor = GetComponent<Tilemap> ();
    }

    void Update()
    {
        // TODO: destroy tiles as the camera scrolls above them
    }

    public void DestroyTiles (List<Vector2> locations)
    {
        foreach (Vector2 loc in locations)
        {
            Vector3Int tileCoord = floor.WorldToCell (loc);
            floor.SetTile (tileCoord, null);
            Instantiate (fallingTilePrefab, loc, Quaternion.identity);
        }
    }
}
