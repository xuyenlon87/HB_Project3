using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState<Bot>
{
    private float timeChangeTarget;
    private float timeChangeState;
    public void OnEnter(Bot bot)
    {
        timeChangeState = Random.Range(0.5f, 0.8f);
        bot.Stop();
        this.timeChangeTarget = 0;
        bot.OnShoot();
    }

    public void OnExecute(Bot bot)
    {
        this.timeChangeTarget += Time.deltaTime;
        if (timeChangeTarget >= timeChangeState && !bot.isDead)
        {
            bot.ChangeState(new PatrolState());
        }
    }

    public void OnExit(Bot bot)
    {

    }
}