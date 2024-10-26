using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultEnemy : MonoBehaviour
{

    [SerializeField] private float speed = 1f; // in units per second
    [SerializeField] private GameObject attackPrefab;
    [SerializeField] private float range = 5f;
    [SerializeField] private float attackCooldown = 1f; // in seconds
    public GameObject target;

    private float timeOfLastAttack = 0f;

    void Start()
    {
        
    }

    void Update()
    {
        Vector3 desiredDirection = target.transform.position - transform.position;
        float dist = Mathf.Abs(desiredDirection.magnitude);
        
        Move (desiredDirection.normalized);
        if (dist < range && timeOfLastAttack + attackCooldown < Time.time)
        {
            Attack (desiredDirection.normalized);
        }
    }

    // advance in the direction of a vector
    public void Move (Vector3 dir)
    {
        transform.position += dir * speed * Time.deltaTime;
    }

    // attacks in a certain direction
    public void Attack (Vector3 dir)
    {
        Instantiate (attackPrefab, transform.position + dir, Quaternion.identity);
        timeOfLastAttack = Time.time;
    }
}
