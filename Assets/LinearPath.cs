using UnityEngine;

public class LinearPath : MonoBehaviour
{

    public static Vector3 Evaluate(Vector3[] points, float t)
    {
        if (points == null || points.Length < 2) return points != null && points.Length == 1 ? points[0] : Vector3.zero;

        t = Mathf.Clamp01(t);

        float scaledT = t * (points.Length - 1);
        int segmentIndex = Mathf.Min(Mathf.FloorToInt(scaledT), points.Length - 2);
        float localT = scaledT - segmentIndex;

        Vector3 p0 = points[segmentIndex];
        Vector3 p1 = points[segmentIndex + 1];

        return Vector3.Lerp(p0, p1, localT);
    }
}