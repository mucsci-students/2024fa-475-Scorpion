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

    private float retargetCooldown = 0.5f;

    private float targetDist = float.PositiveInfinity;
    private float timeOfLastAttack = 0f;
    private GameObject currTarget; // null if no current target
    private float timeOfLastRetarget = 0f;

    void Start()
    {
        
    }

    void Update()
    {
        // retarget and get the current distance to target
        if (timeOfLastRetarget + retargetCooldown < Time.time)
        {
            Retarget ();
            timeOfLastRetarget = Time.time;
        }

        if (currTarget != null)
        {
            // advance towards target and attack
            Vector3 desiredDirection = (currTarget.transform.position - transform.position).normalized;
            Move (desiredDirection);
            targetDist = Mathf.Abs((currTarget.transform.position - transform.position).magnitude);
            if (timeOfLastAttack + attackCooldown < Time.time && targetDist < range)
            {
                Attack (desiredDirection);
                timeOfLastAttack = Time.time;
            }
        }
        
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
            float tDist = Mathf.Abs((t.transform.position - transform.position).magnitude);
            if (tDist < targetDist)
            {
                currTarget = t;
                targetDist = tDist;
            }
        }
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
