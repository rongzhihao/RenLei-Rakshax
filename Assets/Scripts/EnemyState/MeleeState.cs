using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeState : IEnemyState
{
	private float shortAttackTimer;
	private float shortAttackCoolDown = 0.3f;
	private bool canAttack = true;
	private Enemy enemy;
    public void Enter(Enemy enemy)
    {
		this.enemy = enemy;
        
    }

    public void Execute()
    {
       ShortAttack();
	   if(enemy.InLongRange && ! enemy.InMeleeRange )
	   {
		   enemy.ChangeState(new RangedState());
	   }
    }

    public void Exit()
    {
        
    }

    public void OnTriggerEnter(Collider2D other)
    {
        
    }

	private void ShortAttack()
	{
		shortAttackTimer += Time.deltaTime;
		if(shortAttackTimer >= shortAttackCoolDown)
		{
			shortAttackTimer = 0;
			canAttack = true;
		}
		if(canAttack)
		{
			canAttack = false;
			enemy.ShortAttack();
		}
	}
}
