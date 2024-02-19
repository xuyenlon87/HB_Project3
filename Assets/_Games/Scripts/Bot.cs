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
        currentWeapon = (WeaponType)Random.Range(0, 3);
        GetWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        if (navMesh.velocity.sqrMagnitude > 1f)
        {
            charaterImg.transform.rotation = Quaternion.LookRotation(navMesh.velocity);
            ChangeAnim("IsRun");
            amountBullet = 1;
        }
        if (navMesh.velocity.sqrMagnitude <= 1f)
        {
            ChangeAnim("IsIdle");
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
        Vector3 randomPoint = new Vector3(transform.position.x + Random.Range(-10, 10), 0, transform.position.z + Random.Range(-10, 10));
        if (NavMesh.SamplePosition(randomPoint, out hit, 10f, NavMesh.AllAreas))
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
        while (true)
        {
            targetPos = GetRandomPointOnNavMesh();
            navMesh.SetDestination(targetPos);
            yield return new WaitForSeconds(Random.Range(1f, 3f));
        }
    }

    public void Stop()
    {
        navMesh.isStopped = true;
    }
    public void StartMove()
    {
        navMesh.isStopped = false;
    }
    public override void OnShoot()
    {
        Stop();
        base.OnShoot();
        Invoke(nameof(StartMove), 1f);
    }
    public IEnumerator MoveAroundAndShoot()
    {
        if (target != null)
        {
            navMesh.velocity = Vector3.zero;
            while (target != null)
            {
                OnShoot();
                yield return new WaitForSeconds(0.2f);
                int random = Random.Range(-3,3);
                Vector3 targetPosMoveAround = new Vector3(target.position.x + random, 0f, target.position.z + random);
                navMesh.SetDestination(targetPosMoveAround);
                yield return new WaitForSeconds(timeResetAttack - 0.1f);
            }
        }     
    }
    public IEnumerator AvoidAndShoot()
    {
        if (target != null)
        {
            navMesh.velocity = Vector3.zero;

            while (target != null)
            {
                OnShoot();
                yield return new WaitForSeconds(0.2f);
                int random = Random.Range(-3, 3);
                Vector3 targetAvoidAndShoot = new Vector3(transform.position.x + random, 0f, transform.position.z + random);
                navMesh.SetDestination(targetAvoidAndShoot);
                yield return new WaitForSeconds(timeResetAttack - 0.1f);
            }
        }
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
        }
    }
    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Bot") || other.CompareTag("Player"))
    //    {
    //        target = null;
    //    }
    //}
}
