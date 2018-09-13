using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedState : IEnemyState
{
	private Enemy enemy;
	private float longAttackTimer;
	private float attackCoolDown = 0.8f;
	private bool canLongAttack = true;

    public void Enter(Enemy enemy)
    {
		this.enemy = enemy;
    }

    public void Execute()
    {
		LongAttack();
		if(enemy.InMeleeRange)
		{
			enemy.ChangeState(new MeleeState());
		}
		else if(enemy.Target != null)
		{
			enemy.Move();
		}
		else
		{
			enemy.ChangeState(new IdleState());
		}
         
    }

    public void Exit()
    {
         
    }

    public void OnTriggerEnter(Collider2D other)
    {
         
    }

	private void LongAttack()
	{
		longAttackTimer += Time.deltaTime;
		if(longAttackTimer >= attackCoolDown)
		{
			longAttackTimer = 0;
			canLongAttack = true;
		}
		if(canLongAttack)
		{
			canLongAttack = false;
			enemy.LongAttack();
		}
	}
}
