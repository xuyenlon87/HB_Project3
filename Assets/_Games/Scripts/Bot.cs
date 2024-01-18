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
        //Nếu có target, ko có đạn => di chuyển để lấy đạn => bắn
        //có targett => có đạn => dừng navmesh => đúng hướng => bắn
        //Nếu không có target => di chuyển lung tung
        if (navMesh.velocity.sqrMagnitude > 0)
        {
            transform.rotation = Quaternion.LookRotation(navMesh.velocity);
        }
        if (navMesh.velocity.sqrMagnitude > 1f)
        {
            amountBullet = 1;
        }
        if (target != null)
        {
            transform.rotation = Quaternion.LookRotation(lookTarget);

            if (amountBullet > 0)
            {
                navMesh.isStopped = true;
                if (Quaternion.Angle(transform.rotation, Quaternion.LookRotation(lookTarget)) <= 0.1f)
                {
                    OnShoot();
                }
            }
            else if(amountBullet < 1)
            {
                StartCoroutine(ChangeDestination());
            }
        }
    }
    public override void OnInit()
    {
        base.OnInit();
        navMesh = GetComponent<NavMeshAgent>();
        navMesh.speed = speed;
    }

    private Vector3 GetRandomPointOnNavMesh()
    {
        NavMeshHit hit;
        Vector3 randomPoint = transform.position + Random.insideUnitSphere * 15f;
        float randomRadius = Random.Range(5, 15);
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
            yield return new WaitForSeconds(Random.Range(0.1f, 1f));
        }
    }

    public override void OnShoot()
    {
        base.OnShoot();
    }
}
