using UnityEngine;

public class AllyAgent : SteeringAgent
{
	private Attack.AttackType attackType = Attack.AttackType.AllyGun;
	private Seek allySeekBehaviour;
	private AllyAttack allyAttackBehaviour;
	private Seperation allySeperationBehaviour;	
	private Cohesion allyCohesionBehaviour;	

    protected float attackRange = 100f;


    protected enum State
	{
		Seeking,
		Attacking,
		Fleeing
	}

	private State currentState;
	
	public SteeringAgent getNearestEnemy; 

	protected override void InitialiseFromAwake()
	{
		allySeekBehaviour = gameObject.AddComponent<Seek>();
		allyAttackBehaviour = gameObject.AddComponent<AllyAttack>();
		allySeperationBehaviour = gameObject.AddComponent<Seperation>();
        allyCohesionBehaviour = gameObject.AddComponent<Cohesion>();	
    }

	protected override void CooperativeArbitration()
	{
        getNearestEnemy = GetNearestAgent(transform.position, GameData.Instance.enemies);

		State previousState = currentState;

        base.CooperativeArbitration();

		switch (currentState)
		{
			case State.Seeking:
				if (getNearestEnemy != null)
				{
					allySeekBehaviour.enabled = true;
					allySeperationBehaviour.enabled = true;
                    allyCohesionBehaviour.enabled = true;
                    Debug.Log("Seeking");				
				}
                break;
			case State.Attacking:
				allyAttackBehaviour.enabled = true;
				break;
				
				

        }



	}

	protected override void UpdateDirection()
	{
		

			var mouseInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			mouseInWorld.z = 0.0f;
			transform.up = Vector3.Normalize(mouseInWorld - transform.position);
		
	}
	/*protected bool ChangeStateifEnemyInRange()
	{
		if ()
        {
            
        }
        return ChangeState(State.Attacking);
	;
	}*/
	protected bool ChangeState(State newState)
	{
		currentState = newState;
		return true;
	}
}
