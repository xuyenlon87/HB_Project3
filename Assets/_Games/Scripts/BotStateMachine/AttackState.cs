using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState<Bot>
{
    private int state;
    private float timeChangeTarget;
    public void OnEnter(Bot bot)
    {
        timeChangeTarget = 0;
        state = Random.Range(0, 2);
        switch (state)
        {
            case 0:
                bot.StartCoroutine(bot.MoveAroundAndShoot());
                break;
            case 1:
                bot.StartCoroutine(bot.AvoidAndShoot());
                break;
        }
    }

    public void OnExecute(Bot bot)
    {
        timeChangeTarget += Time.deltaTime;
        if (bot.target == null || timeChangeTarget >= Random.Range(5,15))
        {
            bot.ChangeState(new PatrolState());
        }
    }

    public void OnExit(Bot bot)
    {
        bot.navMesh.isStopped = false;
    }
}
