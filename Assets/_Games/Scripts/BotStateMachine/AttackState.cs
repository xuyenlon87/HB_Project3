using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState<Bot>
{
    private float timeChangeTarget;
    public void OnEnter(Bot bot)
    {
        bot.Stop();
        this.timeChangeTarget = 0;
        bot.OnShoot();
    }

    public void OnExecute(Bot bot)
    {
        this.timeChangeTarget += Time.deltaTime;
        if (timeChangeTarget >= 1f || bot.target == null)
        {
            bot.ChangeState(new PatrolState());
        }
    }

    public void OnExit(Bot bot)
    {

    }
}