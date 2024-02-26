using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBot : GameUnit
{
    [SerializeField] Renderer rd;
    // Start is called before the first frame update
    void Start()
    {
        rd = GetComponent<Renderer>();
        Color rdColor = new Color(Random.value, Random.value, Random.value, 1f);
        rd.material.color = rdColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
