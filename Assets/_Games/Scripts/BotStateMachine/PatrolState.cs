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

    }

    public void OnExit(Bot bot)
    {

    }
}
