using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: use NavMesh for pathfinding, or write more complex code
public class DefaultEnemy : MonoBehaviour
{

    [SerializeField] private float speed = 1f; // in units per second
    [SerializeField] private GameObject attackPrefab;
    [SerializeField] private float range = 5f;
    [SerializeField] private float attackCooldown = 1f; // in seconds
    public List<GameObject> targets; // TODO: make list of Vector3 instead...?

    private float timeOfLastAttack = 0f;
    private GameObject currTarget; // == null if no current target
    private float retargetCooldown = 0.5f;
    private float timeOfLastRetarget = 0f;

    void Start()
    {
        
    }

    void Update()
    {
        // retarget and get the current distance to target
        float dist = currTarget ? Mathf.Abs((currTarget.transform.position - transform.position).magnitude) : float.PositiveInfinity;
        if (timeOfLastRetarget + retargetCooldown < Time.time)
        {
            dist = Retarget (dist);
            timeOfLastRetarget = Time.time;
        }

        if (currTarget != null)
        {
            // advance towards target and attack
            Vector3 desiredDirection = currTarget.transform.position - transform.position;
            
            Move (desiredDirection.normalized);
            if (timeOfLastAttack + attackCooldown < Time.time && dist < range)
            {
                Attack (desiredDirection.normalized);
                timeOfLastAttack = Time.time;
            }
        }
        
    }

    // choose a new target based on proximity
    // returns the new distance to the current target
    public float Retarget (float dist)
    {
        foreach (GameObject t in targets)
        {
            float tDist = Mathf.Abs((t.transform.position - transform.position).magnitude);
            if (tDist < dist)
            {
                currTarget = t;
                dist = tDist;
            }
        }
        return dist;
    }

    // advance in a certain direction
    public void Move (Vector3 dir)
    {
        transform.position += dir * speed * Time.deltaTime;
    }

    // attack in a certain direction
    public void Attack (Vector3 dir)
    {
        Instantiate (attackPrefab, transform.position + dir, Quaternion.identity);
    }

}
