using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] private List<GameObject> enemyPrefabs;
    [SerializeField] private List<int> probabilityWeights; // how likely each enemy prefab is to be spawned (0 is never, 1 is average, 2 is twice as likely, etc.)
    [SerializeField] private int chanceOfNoSpawn; // how likely it is for no enemy to spawn
    [SerializeField] private List<GameObject> targets; // list of targets to pass to spawned enemies
    [SerializeField] private GameObject cam;

    private int totalProbabilities = 0;
    private float triggerRadius = 15f; // how close the camera should be to trigger the spawner

    void Start()
    {
        // sum all of the probabilities
        foreach (int p in probabilityWeights)
        {
            totalProbabilities += p;
        }
        totalProbabilities += chanceOfNoSpawn;
    }

    void Update()
    {
        if (Mathf.Abs(((Vector2)(cam.transform.position - transform.position)).magnitude) < triggerRadius)
        {
            // instantiate a random enemy
            GameObject enemyPrefab = RandomEnemyPrefab ();
            if (enemyPrefab)
            {
                GameObject enemyInst = Instantiate (enemyPrefab, transform.position, Quaternion.identity);
                enemyInst.GetComponent<NewEnemy> ().targets = targets;
            }
            Destroy (gameObject);
        }
    }

    // returns a random enemy prefab using their weighted probability
    private GameObject RandomEnemyPrefab ()
    {
        int choice = Random.Range (1, totalProbabilities);
        int index = 0;
        for (int i = 0; i < probabilityWeights.Count; ++i)
        {
            index += probabilityWeights[i];
            if (choice <= index)
                return enemyPrefabs[i];
        }
        return null;
    }
}
