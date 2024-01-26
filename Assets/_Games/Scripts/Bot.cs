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
        currentBullet = BulletType.Bullet;
        GetBullet();
    }

    // Update is called once per frame
    void Update()
    {
        if (navMesh.velocity.sqrMagnitude > 1f)
        {
            transform.rotation = Quaternion.LookRotation(navMesh.velocity);
            ChangeAnim("IsRun");
            amountBullet = 1;
        }
        if (navMesh.velocity.sqrMagnitude <= 0.1f)
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

    private void Stop()
    {
        navMesh.isStopped = true;
    }
    public override void OnShoot()
    {
        Stop();
        base.OnShoot();

    }
    public IEnumerator MoveAroundAndShoot()
    {
        if (target != null)
        {
            navMesh.isStopped = true;
            OnShoot();
            while (target != null)
            {
                navMesh.isStopped = false;
                int random = Random.Range(-3,3);
                Vector3 targetPosMoveAround = new Vector3(target.position.x + random, 0f, target.position.z + random);
                navMesh.SetDestination(targetPosMoveAround);
                OnShoot();
                yield return new WaitForSeconds(timeResetAttack - 0.1f);
            }
        }     
    }
    public IEnumerator AvoidAndShoot()
    {
        if (target != null)
        {
            navMesh.isStopped = true;
            OnShoot();
            while (target != null)
            {
                navMesh.isStopped = false;
                int random = Random.Range(-3, 3);
                Vector3 targetAvoidAndShoot = new Vector3(transform.position.x + random, 0f, transform.position.z + random);
                navMesh.SetDestination(targetAvoidAndShoot);
                OnShoot();
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
}
