using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exp : MonoBehaviour
{
    public int level;
    // Start is called before the first frame update
    void Start()
    {
        OnInit();
    }

    // Update is called once per frame
    private void OnInit()
    {
        level = 1;
        Invoke(nameof(OnDestroy), 5f);
    }

    private void OnDestroy()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Bot") || other.CompareTag("Player"))
        {
            Character character = other.GetComponent<Character>();
            //character.Upgrade(level);
            OnDestroy();
        }
    }
}
