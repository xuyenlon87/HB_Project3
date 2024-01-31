using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Vector3 offsetStart;
    [SerializeField] private float speed;
    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<Player>().transform;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {

        if (target != null && GameManager.Ins.currentState == GameState.Play)
        {
            transform.position = Vector3.Lerp(transform.position, target.position + offset, Time.deltaTime * speed);
        }
        else if (target != null)
        {
            transform.position = Vector3.Lerp(transform.position, target.position + offsetStart, Time.deltaTime * speed);
        }
    }
}
