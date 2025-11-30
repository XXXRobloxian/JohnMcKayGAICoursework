using JetBrains.Annotations;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Seek : SteeringBehaviour
{
    public SteeringAgent nearestEnemy;

    protected float arrivalRadius = 200f;
    public override Vector3 UpdateBehaviour(SteeringAgent steeringAgent)
    {
        Vector3  targetPosition = steeringAgent.transform.position;
        nearestEnemy = AllyAgent.GetNearestAgent(transform.position, GameData.Instance.enemies);
        if (nearestEnemy != null)
        {
           targetPosition = nearestEnemy.transform.position;
        }

        float distance = (targetPosition - transform.position).magnitude;
        if (distance < arrivalRadius)
        {
            desiredVelocity *= SteeringAgent.MaxCurrentSpeed * (distance / arrivalRadius);
        }
        else
        {
            desiredVelocity *= SteeringAgent.MaxCurrentSpeed;
        }


        desiredVelocity = Vector3.Normalize(targetPosition - transform.position) * SteeringAgent.MaxCurrentSpeed;
        steeringVelocity = desiredVelocity - steeringAgent.CurrentVelocity;

        return steeringVelocity;
    }

}
