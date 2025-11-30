using System.Threading;
using UnityEngine;

public class Cohesion : SteeringBehaviour
{

    protected float neighbourRadius = 50f;
    public SteeringAgent allyCheck;

    public override Vector3 UpdateBehaviour(SteeringAgent steeringAgent)
    {
        Vector3 sumPositions = Vector3.zero;
        int totalAllies = 0;


        foreach (var ally in GameData.Instance.allies)
        {
            if (ally == steeringAgent) continue; // Skip self

            if (Vector3.Distance(steeringAgent.transform.position, ally.transform.position) <= neighbourRadius)
            {
               sumPositions += ally.transform.position;    
               totalAllies++;
            }
        }

        if (totalAllies > 0)
        {
            Vector3 averagePosition = sumPositions / totalAllies;
            Debug.Log("Average Position " + averagePosition);

            steeringVelocity = Vector3.Normalize(averagePosition - steeringAgent.transform.position) * SteeringAgent.MaxCurrentSpeed;
        }

        return steeringVelocity;
    }
}
