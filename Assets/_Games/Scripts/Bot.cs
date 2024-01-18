//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    private NavMeshAgent navMesh;
    private Vector3 targetPos;
    private IState<Bot> currentState;
    // Start is called before the first frame update
    void Start()
    {
        OnInit();
    }

    // Update is called once per frame
    void Update()
    {
        ////Nếu có target, ko có đạn => di chuyển để lấy đạn => bắn
        ////có targett => có đạn => dừng navmesh => đúng hướng => bắn
        ////Nếu không có target => di chuyển lung tung
        if (navMesh.velocity.sqrMagnitude > 0)
        {
            transform.rotation = Quaternion.LookRotation(navMesh.velocity);
        }
        if (navMesh.velocity.sqrMagnitude > 1f)
        {
            amountBullet = 1;
        }

        if (currentState != null && !isDead)
        {
            currentState.OnExecute(this);
        }
    }
    public override void OnInit()
    {
        base.OnInit();
        navMesh = GetComponent<NavMeshAgent>();
        navMesh.speed = speed;
        ChangeState(new PatrolState());

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
    public IEnumerator Move()
    {
        while (true)
        {
            targetPos = GetRandomPointOnNavMesh();
            navMesh.SetDestination(targetPos);
            yield return new WaitForSeconds(Random.Range(0.1f, 1f));
        }
    }

    public IEnumerator ShootAndMove()
    {
        while (true)
        {
            OnShoot();
            yield return new WaitForSeconds(0.1f);
            transform.position = Vector3.MoveTowards(transform.position, GetRandomPointOnNavMesh(), speed*Time.deltaTime);
            yield return new WaitForSeconds(1.4f);
        }
    }
    public override void OnShoot()
    {
        transform.rotation = Quaternion.LookRotation(lookTarget);
        base.OnShoot();
    }
    public void ChangeState(IState<Bot> newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = newState;
        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bot") || other.CompareTag("Player"))
        {
            target = other.transform;
            lookTarget = new Vector3(target.position.x, 0, target.position.z);
            ChangeState(new AttackState());
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Bot") || other.CompareTag("Player"))
        {
            target = null;
            ChangeState(new PatrolState());
        }
    }
}
