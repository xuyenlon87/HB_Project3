using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState<Bot>
{
    public void OnEnter(Bot bot)
    {
        bot.target = null;
        bot.navMesh.isStopped = false;
        bot.StartCoroutine(bot.Move());
    }

    public void OnExecute(Bot bot)
    {
        if (bot.target != null)
        {
            bot.ChangeState(new AttackState());
        }
    }

    public void OnExit(Bot bot)
    {
        bot.StopCoroutine(bot.Move());
    }
}