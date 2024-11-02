using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pitfall : MonoBehaviour
{

    [SerializeField] private List<Vector2> tileLoc; // the locations of the tiles to destroy (use the lower lefthand corner of the tiles)
    [SerializeField] private float minDestroyRadius = 8f; // min & max distance at which the tiles will fall
    [SerializeField] private float maxDestroyRadius = 12f; // 8 and 12 are good standard distances
    [SerializeField] private FloorDestroyer floorDestroyer;
    [SerializeField] private Camera cam;

    private float destroyRadius;

    void Start()
    {
        destroyRadius = Random.Range (minDestroyRadius, maxDestroyRadius);
    }

    void Update()
    {
        if (transform.position.y - destroyRadius < cam.transform.position.y)
        {
            floorDestroyer.DestroyTiles (tileLoc);
            Destroy (gameObject);
        }
    }
}
