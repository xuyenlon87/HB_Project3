using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState<Bot>
{
    private int state;
    public void OnEnter(Bot bot)
    {
        if(bot.amountBullet >=1)
        {
            bot.navMesh.isStopped = true;
            state = Random.Range(0, 3);
            switch (state)
            {
                case 0:
                    bot.StartCoroutine(bot.ShootAndMove());
                    break;
                case 1:
                    bot.StartCoroutine(bot.MoveAroundAndShoot());
                    break;
                case 2:
                    bot.StartCoroutine(bot.AvoidAndShoot());
                    break;
            }
        }
    }

    public void OnExecute(Bot bot)
    {
        if(bot.target == null)
        {
            bot.ChangeState(new PatrolState());
        }
    }

    public void OnExit(Bot bot)
    {
        bot.navMesh.isStopped = false;
    }
}
