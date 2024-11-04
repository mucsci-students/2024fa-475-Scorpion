using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: use NavMesh for pathfinding, or write more complex code
public class NewEnemy : MonoBehaviour
{

    [SerializeField] private float speed = 1f; // in units per second
    [SerializeField] private GameObject attackPrefab;
    [SerializeField] private float range = 5f;
    [SerializeField] private float attackCooldown = 1f; // in seconds
    public List<GameObject> targets = new List<GameObject> ();

    private float retargetCooldown = 0.5f;
    private Rigidbody2D rb;
    private NewEnemyVision vision;
    private List<float> weights; // how much the enemy wants to move in each of the 8 directions

    private float targetDist = float.PositiveInfinity;
    private float timeOfLastAttack = 0f;
    private GameObject currTarget; // null if no current target
    private int directionChoice = 0;
    private float timeOfLastRetarget = 0f;

    public bool drawWeights = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D> ();
        vision = GetComponentInChildren<NewEnemyVision> ();
        weights = new List<float> ();
        for (int i = 0; i < 8; ++i)
        {
            weights.Add (0f);
        }
    }

    void Update()
    {
        // retarget
        if (timeOfLastRetarget + retargetCooldown < Time.time)
        {
            Retarget ();
            timeOfLastRetarget = Time.time;
        }

        // move
        UpdateWeights ();
        directionChoice = IndexOfMaxWeight (weights);
        Vector2 dir;
        if (directionChoice != -1)
        {
            dir = IndexToVector (directionChoice);
            Move (dir);

            // attack the target
            if (currTarget != null)
            {
                targetDist = Mathf.Abs((currTarget.transform.position - transform.position).magnitude);
                if (timeOfLastAttack + attackCooldown < Time.time && targetDist < range)
                {
                    Attack (dir);
                    timeOfLastAttack = Time.time;
                }
            }
        }
        else
        {
            Move (new Vector3 (0f, 0f, 0f));
        }

        if (drawWeights)
                DrawDebugWeights ();
    }

    // choose a new target based on proximity
    public void Retarget ()
    {
        if (!targets.Contains (currTarget))
        {
            currTarget = null;
            targetDist = float.PositiveInfinity;
        }
        foreach (GameObject t in targets)
        {
            if (t != null)
            {
                float tDist = Mathf.Abs((t.transform.position - transform.position).magnitude);
                if (tDist < targetDist)
                {
                    currTarget = t;
                    targetDist = tDist;
                }
            }
        }
    }

    // decide which directions are best to move in based on nearby entities
    private void UpdateWeights ()
    {
        weights = new List<float> {0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f};
        foreach (GameObject g in vision.objects)
        {
            // update each weight
            for (int i = 0; i < 8; ++i)
            {
                Vector2 dir = IndexToVector (i);
                Vector2 dirToObject = (Vector2) (g.transform.position - transform.position).normalized;
                float dist = Vector3.Distance (g.transform.position, transform.position);
                weights[i] += ShapeWeight (g.tag, dir, dirToObject, dist);
            }
        }
    }

    // apply a shaping function to a weight, based on the object's tag
    private float ShapeWeight (string tag, Vector2 dir, Vector2 dirToObject, float distToObject)
    {
        if (currTarget && tag == currTarget.tag) // either Player1 or Player2
        {
            float angle = Vector2.Angle (dir, dirToObject) / 180f * Mathf.PI; // in radians
            return Mathf.Cos (angle / 2f); // / distToObject;
        }
        else if (tag == "Wall Entity" || tag == "Enemy")
        {
            distToObject -= 1f;
            if (distToObject < 0f) distToObject = 0f;
            float dot = Vector2.Dot (dir, dirToObject);
            if (dot < 0f)
                dot = 0f;
            return -1f * dot / distToObject / distToObject / distToObject;
        }
        return 0f;
    }

    // advance in a certain direction
    public void Move (Vector3 dir)
    {
        rb.velocity = dir * speed;
    }

    // attack in a certain direction
    public void Attack (Vector3 dir)
    {
        // Instantiate the arrow prefab
        GameObject arrow = Instantiate(attackPrefab, transform.position + dir, Quaternion.identity);

        // Get the Arrow component and set its direction
        Arrow arrowComponent = arrow.GetComponent<Arrow>();
        arrowComponent.SetDirection(dir);

        // Rotate the arrow to match the shooting direction
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        arrow.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    private int IndexOfMaxWeight (List<float> weights)
    {
        float maxVal = 0f;
        int index = -1;
        for (int i = 0; i < 8; ++i)
        {
            if (weights[i] > maxVal)
            {
                maxVal = weights[i];
                index = i;
            }
        }
        return index;
    }

    private Vector2 IndexToVector (int i)
    {
        float angle = ((float) i) / 8f * 2f * Mathf.PI; // in radians
        return new Vector2 (Mathf.Sin (angle), Mathf.Cos (angle));
    }



    // from ChatGPT, for debug
    void DrawDebugWeights()
    {
        if (weights == null || weights.Count != 8) return;

        // Set the base position of the enemy
        Vector3 position = transform.position;

        // Iterate through each direction (8 directions total)
        for (int i = 0; i < 8; i++)
        {
            // Get the direction vector for this index
            Vector2 direction = IndexToVector(i);

            // Get the weight and determine the color
            float weight = weights[i];
            Color lineColor = weight >= 0 ? Color.green : Color.red;

            // Calculate the endpoint of the line, with length proportional to the absolute weight
            Vector3 endPoint = position + (Vector3)direction * Mathf.Log(1f + Mathf.Abs(weight));

            // Draw the line in the Game view
            Debug.DrawLine(position, endPoint, lineColor);
        }
    }

}
