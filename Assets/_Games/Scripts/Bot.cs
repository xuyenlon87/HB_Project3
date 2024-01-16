using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    private Vector3 targetPos;
    private NavMeshAgent navMesh;
    private float roamRadius = 10f;
    // Start is called before the first frame update
    void Start()
    {
        OnInit();
    }

    // Update is called once per frame
    void Update()
    {
        if (!navMesh.hasPath || navMesh.remainingDistance < 0.1f)
        {
            GetRandomPointInNavMesh();
            Move();
        }
    }
    public override void OnInit()
    {
        base.OnInit();
        navMesh = GetComponent<NavMeshAgent>();
    }
    private void Move()
    {
        targetPos = GetRandomPointInNavMesh();
        navMesh.SetDestination(targetPos);
    }

    private Vector3 GetRandomPointInNavMesh()
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * roamRadius;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, roamRadius, 1);
        return hit.position;
    }
}
