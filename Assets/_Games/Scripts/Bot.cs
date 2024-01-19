﻿//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    public NavMeshAgent navMesh;
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
        if (navMesh.velocity.sqrMagnitude > 1f)
        {
            transform.rotation = Quaternion.LookRotation(navMesh.velocity);
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

    public float SetSpeed(float speed)
    {
        navMesh.speed = speed;
        return navMesh.speed;
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
            yield return new WaitForSeconds(Random.Range(1f, 3f));
        }
    }

    public IEnumerator ShootAndMove()
    {
        if (target != null)
        {
            while (true)
            {
                OnShoot();
                transform.position = Vector3.MoveTowards(transform.position, GetRandomPointOnNavMesh(), navMesh.speed * Time.deltaTime);
                yield return new WaitForSeconds(timeResetAttack-0.1f);
            }
        }
    }
    public IEnumerator MoveAroundAndShoot()
    {
        if (target != null)
        {
            OnShoot();
            while (true)
            {
                int random = Random.Range(-3,3);
                Vector3 targetPosMoveAround = new Vector3(target.position.x + random, target.position.y, target.position.z + random);
                transform.position = Vector3.MoveTowards(transform.position,targetPosMoveAround, navMesh.speed * Time.deltaTime);
                yield return new WaitForSeconds(timeResetAttack - 0.1f);
                OnShoot();
            }
        }     
    }
    public IEnumerator AvoidAndShoot()
    {
        if (target != null)
        {
            OnShoot();
            while (true)
            {
                int random = Random.Range(-3, 3);
                Vector3 targetAvoidAndShoot = new Vector3(transform.position.x + random, transform.position.y, transform.position.z + random);
                transform.position = Vector3.MoveTowards(transform.position, targetAvoidAndShoot, navMesh.speed * Time.deltaTime);
                yield return new WaitForSeconds(timeResetAttack - 0.1f);
                OnShoot();
            }
        }
    }
    public override void OnShoot()
    {
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
