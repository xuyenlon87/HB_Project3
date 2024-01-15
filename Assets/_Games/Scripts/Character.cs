using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float speed;
    public int amountBullet;
    public int hp;
    public float radiusSize;
    public Transform playerGun;
    public LayerMask targetLayer;
    public Transform targetPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public virtual void OnInit()
    {
        radiusSize = 5;
        hp = 1;
        amountBullet = 0;
    }
}
