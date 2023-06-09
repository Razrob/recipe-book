using UnityEngine;

public static class ObserveVisibleZoneExtensions
{
    public static bool PointInVisibleZone(this Vector3 point, Vector3 observePivot, Vector3 observeDirection,
        float visibleAngle, float visibleDistance, float pointRadius = 0f)
    {
        bool visible;

        Vector3 directionToPlayer = (point - observePivot).XZ().normalized;
        Vector3 right = Vector3.Cross(directionToPlayer, Vector3.up);

        for (int i = 0; i < (pointRadius > 0.05f ? 3 : 1); i++)
        {
            if (i is 1)
                directionToPlayer = ((point - right * pointRadius) - observePivot).XZ().normalized;
            else if (i is 2)
                directionToPlayer = ((point + right * pointRadius) - observePivot).XZ().normalized;

            float angle = Mathf.Abs(Mathf.Acos(Vector3.Dot(observeDirection.XZ(), directionToPlayer)) * Mathf.Rad2Deg);
            visible = angle <= visibleAngle / 2f && Vector3.Distance(observePivot, point) <= visibleDistance + pointRadius;

            if (visible)
                return true;
        }

        return false;
    }
}