//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    private NavMeshAgent navMesh;
    private Vector3 targetPos;
    // Start is called before the first frame update
    void Start()
    {
        OnInit();
        StartCoroutine(ChangeDestination());

    }

    // Update is called once per frame
    void Update()
    {

    }
    public override void OnInit()
    {
        base.OnInit();
        navMesh = GetComponent<NavMeshAgent>();
    }

    private Vector3 GetRandomPointOnNavMesh()
    {
        NavMeshHit hit;
        Vector3 randomPoint = transform.position + Random.insideUnitSphere * 15f;
        float randomRadius = Random.Range(5, 10);
        if (NavMesh.SamplePosition(randomPoint, out hit, randomRadius, NavMesh.AllAreas))
        {
            return hit.position;
        }
        return transform.position;
    }
    private IEnumerator ChangeDestination()
    {
        while (true)
        {
            targetPos = GetRandomPointOnNavMesh();
            navMesh.SetDestination(targetPos);

            yield return new WaitForSeconds(Random.Range(0.01f, 1.5f));
        }
    }
}
