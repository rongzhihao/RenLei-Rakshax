using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IEnemyState
{
	private Enemy enemy;	
	private float idleTimer;
	private float idleDuration = 5f;

    public void Enter(Enemy enemy)
    {
       this.enemy = enemy;
    }

    public void Execute()
    {
        Patrol();
		enemy.Move();
		if(enemy.Target != null && enemy.InLongRange)
		{
			enemy.ChangeState(new RangedState());
		}
    }

    public void Exit()
    {
         
    }

    public void OnTriggerEnter(Collider2D other)
    {
         if(other.tag == "Edge")
		 {
			enemy.ChangeDirection();
		 }
    }


	private void Patrol()
	{
		idleTimer += Time.deltaTime;
		if(idleTimer >= idleDuration)
		{
			enemy.ChangeState(new IdleState());
		}
	}
}
