using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : GameUnit
{
    public Rigidbody rb;
    private float speed = 5f;
    private Character character;
    private Vector3 targetPosition;
    private Vector3 newPosition;
    public void OnInit()
    {
        targetPosition = character.targetPosition.position; 
    }

    public void OnDespawn()
    {
        SimplePool.Despawn(this);
    }


    //private void OnTriggerEnter(Collider other)
    //{
    //    ParticlePool.Play(ParticleType.Hit, transform.position, Quaternion.identity);
    //    OnDespawn();
    //}

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.fixedDeltaTime);
    }

}
