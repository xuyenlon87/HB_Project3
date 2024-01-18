using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState<Bot>
{
    private int state;
    public void OnEnter(Bot bot)
    {
        state = Random.Range(0, 2);
        switch (state)
        {
            case 0: 
                bot.ChangeState(new PatrolState());
                break;
            case 1:
                bot.StartCoroutine(bot.ShootAndMove());
                break;
            //case 2:         
        }
        Debug.Log(state);

    }

    public void OnExecute(Bot bot)
    {

    }

    public void OnExit(Bot bot)
    {

    }
}
