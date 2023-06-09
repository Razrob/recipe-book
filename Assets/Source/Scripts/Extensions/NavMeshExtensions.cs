using UnityEngine;
using UnityEngine.AI;

public static class NavMeshExtensions
{
    public static Vector3 GetNavMeshPoint(Vector3 origin, float maxOriginDistance, float navMeshRadius)
    {
        const int maxIterationCount = 100;
        int iterationNumber = 0;

        while (true)
        {
            if (iterationNumber > maxIterationCount)
                return origin;

            NavMeshHit navMeshHit;
            Vector2 circle = Random.insideUnitCircle;
            Vector3 offcet = new Vector3(circle.x, 0f, circle.y);

            float maxOriginDistanceMultiplier = (iterationNumber / 5 + 1) * 0.25f;

            if (NavMesh.SamplePosition(origin + offcet * maxOriginDistance * maxOriginDistanceMultiplier, 
                out navMeshHit, navMeshRadius, 1 << 0))
                return navMeshHit.position;

            iterationNumber++;
        }
    }
}