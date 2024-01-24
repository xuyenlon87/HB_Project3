using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState<Bot>
{
    public void OnEnter(Bot bot)
    {
        bot.navMesh.isStopped = false;
        bot.StartCoroutine(bot.Move());

    }

    public void OnExecute(Bot bot)
    {
        if(Vector3.Distance(bot.transform.position, Vector3.zero) >= 48f)
        {
            bot.targetPos = Vector3.zero;
        }
    }

    public void OnExit(Bot bot)
    {

    }
}
