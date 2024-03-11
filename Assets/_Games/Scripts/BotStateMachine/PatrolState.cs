using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState<Bot>
{
    public void OnEnter(Bot bot)
    {
        bot.target = null;
        bot.StartMove();
        bot.StartCoroutine(bot.Move());
    }

    public void OnExecute(Bot bot)
    {
        if (bot.navMesh.velocity.sqrMagnitude > 1f)
        {
            bot.charaterImg.transform.rotation = Quaternion.LookRotation(bot.navMesh.velocity);
            bot.ChangeAnim("IsRun");
            bot.amountBullet = 1;
        }
        else if (bot.target == null && bot.navMesh.velocity.sqrMagnitude < 1f)
        {
            bot.ChangeAnim("IsIdle");
        }
        if (bot.target != null && Vector3.Distance(bot.transform.position, bot.targetPos) <= 5f)
        {
            bot.Stop();
            bot.StopCoroutine(bot.Move());
            bot.ChangeState(new AttackState());
        }
    }

    public void OnExit(Bot bot)
    {
        bot.StopCoroutine(bot.Move());
    }
}