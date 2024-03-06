//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    public NavMeshAgent navMesh;
    public Vector3 targetPos;
    private IState<Bot> currentState;
    private NavMeshHit hit;
    // Start is called before the first frame update
    void Start()
    {
        OnInit();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            if (currentState != null && !isDead)
            {
                currentState.OnExecute(this);
            }
        }
    }
    public override void OnInit()
    {
        base.OnInit();
        navMesh = GetComponent<NavMeshAgent>();
        navMesh.speed = speed;
        ChangeState(new PatrolState());
        currentWeapon = (WeaponType)Random.Range(0, 3);
        GetWeapon();
    }

    public float SetSpeed(float speed)
    {
        navMesh.speed = speed;
        return navMesh.speed;
    }
    private Vector3 GetRandomPointOnNavMesh()
    {
        Vector3 randomPoint = new Vector3(transform.position.x + Random.Range(-10, 10), 0, transform.position.z + Random.Range(-10, 10));
        if (NavMesh.SamplePosition(randomPoint, out hit, 10f, NavMesh.AllAreas) && !isDead)
        {
            return hit.position;
        }
        else if (Vector3.Distance(transform.position, Vector3.zero) >= 48f)
        {
            return Vector3.zero;
        }
        return transform.position;
    }
    public IEnumerator Move()
    {
        if (!isDead)
        {
            while (true)
            {
                targetPos = GetRandomPointOnNavMesh();
                navMesh.SetDestination(targetPos);
                yield return new WaitForSeconds(Random.Range(1f, 3f));
            }
        }
    }

    public void Stop()
    {
        navMesh.isStopped = true;
        ChangeAnim("IsIdle");
    }
    public void StartMove()
    {
        navMesh.isStopped = false;
    }
    public override void OnShoot()
    {
        Stop();
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
}
