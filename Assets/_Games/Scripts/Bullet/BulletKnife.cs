using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletKnife : BulletMain
{
    [SerializeField] private Renderer bulletKnifeRen;
    [SerializeField] private Material[] bulletKnifeMat;

    private void Start()
    {
        OnInit();
    }
    private void Update()
    {
        Move();
    }
    public override void Move()
    {
        if(target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            if (Vector3.Distance(startPos, transform.position) >= rangeSize || Vector3.Distance(transform.position, target) <= 0.1f)
            {
                Destroy(gameObject);
            }
        }
    }
    public void ChangeMaterial(int materialIndex)
    {
        if (materialIndex >= 0 && materialIndex < bulletKnifeMat.Length)
        {
            bulletKnifeRen.material = bulletKnifeMat[materialIndex];
        }
        else
        {
            bulletKnifeRen.material = bulletKnifeMat[0];
        }
    }
}
