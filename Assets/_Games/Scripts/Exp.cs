using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exp : MonoBehaviour
{
    private int level;
    // Start is called before the first frame update
    void Start()
    {
        OnInit();
    }

    // Update is called once per frame
    private void OnInit()
    {
        Invoke("OnDestroy", 5f);
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
            level = Random.Range(1, character.level);
            character.Upgrade(level);
            OnDestroy();
        }
    }
}
