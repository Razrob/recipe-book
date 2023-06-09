using System;
using UnityEngine;

public static class ScreenExtensions
{
    public static Quaternion GetScreenRotationFromWorldDirection(Vector3 worldDirection, Camera camera)
    {
        Vector3 screenDirection = Vector3.ProjectOnPlane(worldDirection, camera.transform.forward).normalized;
        screenDirection = new Vector3(screenDirection.x, screenDirection.z);
        screenDirection = Quaternion.Euler(0f, 0f, camera.transform.eulerAngles.y) * screenDirection;
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, screenDirection.normalized);

        return rotation * Quaternion.Euler(0f, 0f, 180f);
    }

    public static Vector2 GetClampedScreenPositionFromWorldPoint(Vector3 worldDirection, Camera camera, float treshold = 0f)
    {
        Vector3 screenDirection = Vector3.ProjectOnPlane(worldDirection, camera.transform.forward).normalized;
        screenDirection = new Vector3(screenDirection.x, screenDirection.z);
        screenDirection = Quaternion.Euler(0f, 0f, camera.transform.eulerAngles.y) * screenDirection;

        float maxScreenSideLength = Mathf.Max(camera.pixelWidth, camera.pixelHeight);
        Vector2 screenPosition = (Vector2)screenDirection * maxScreenSideLength * 1.5f;

        treshold = maxScreenSideLength * treshold;
        Vector2 screenBounds = new Vector2(camera.pixelWidth - treshold, camera.pixelHeight - treshold);

        float ratio = screenBounds.x / screenBounds.y;
        bool isHeight = (Mathf.Abs(screenPosition.y) - screenBounds.y) * ratio >= Mathf.Abs(screenPosition.x) - screenBounds.x;

        float a = (isHeight ? screenBounds.y : screenBounds.x) / 2f;
        float bAngle = Vector2.Angle(Vector2.up, screenPosition) + Convert.ToInt32(!isHeight) * 90f;
        float cos = Mathf.Cos(bAngle * Mathf.Deg2Rad);
        float c = Mathf.Abs(a / cos);

        screenPosition /= screenPosition.magnitude / c;
        screenPosition += new Vector2(camera.pixelWidth / 2f, camera.pixelHeight / 2f);

        return screenPosition;
    }

    public static bool WorldPointInScreenBounds(Vector3 worldPoint, Camera camera, float treshold = 0f)
    {
        Vector3 screenPosition = camera.WorldToViewportPoint(worldPoint);

        return screenPosition.x >= treshold && screenPosition.x <= 1f - treshold
            && screenPosition.y >= treshold && screenPosition.y <= 1f - treshold;
    }
}