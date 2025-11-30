using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UIElements;

public class Seperation : SteeringBehaviour
{

    protected float neighbourRadius = 50f;
    public SteeringAgent allyCheck;

    public override Vector3 UpdateBehaviour(SteeringAgent steeringAgent)
    {
        Vector3 steeringVelocity = Vector3.zero;
        int totalAllies = 0;

        foreach (var ally in GameData.Instance.allies)
        {
            if (ally == steeringAgent) continue; // Skip self

            if (Vector3.Distance(steeringAgent.transform.position, ally.transform.position) <= neighbourRadius)
            {
                Vector3 separation = steeringAgent.transform.position - ally.transform.position;
                steeringVelocity += separation;
                totalAllies++;
            }
        }

        if (totalAllies > 0)
        {
            steeringVelocity /= totalAllies; // Average
            steeringVelocity = steeringVelocity.normalized * SteeringAgent.MaxCurrentSpeed; // Scale
        }

        Debug.Log("Total Allies within neighbour radius: " + totalAllies);

        return steeringVelocity;
    }

}
