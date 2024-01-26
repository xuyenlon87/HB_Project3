using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState<Bot>
{
    public void OnEnter(Bot bot)
    {
        Debug.Log("patrol");
        bot.target = null;
        bot.navMesh.isStopped = false;
        bot.StartCoroutine(bot.Move());
        bot.StopCoroutine(bot.MoveAroundAndShoot());
        bot.StopCoroutine(bot.AvoidAndShoot());
    }

    public void OnExecute(Bot bot)
    {
        if(bot.target != null)
        {
            bot.ChangeState(new AttackState());
        }
    }

    public void OnExit(Bot bot)
    {

    }
}
