using UnityEngine;

public class HermitePath : MonoBehaviour
{
    [Range(0f, 1f)]
    [Tooltip("0 = curva mais 'esticada', 1 = curva mais 'solta/arredondada'")]
    public float tension = 0.5f;

    public Vector3 Evaluate(Vector3[] points, float t)
    {
        if (points == null || points.Length < 2) return points != null && points.Length == 1 ? points[0] : Vector3.zero;

        t = Mathf.Clamp01(t);

        float scaledT = t * (points.Length - 1);
        int i = Mathf.Min(Mathf.FloorToInt(scaledT), points.Length - 2);
        float localT = scaledT - i;

        Vector3 p0 = points[i];
        Vector3 p1 = points[i + 1];

        Vector3 tangent0 = ComputeTangent(points, i);
        Vector3 tangent1 = ComputeTangent(points, i + 1);

        return HermiteFormula(p0, tangent0, p1, tangent1, localT);
    }

    private Vector3 ComputeTangent(Vector3[] points, int index)
    {
        Vector3 prev = points[Mathf.Max(index - 1, 0)];
        Vector3 next = points[Mathf.Min(index + 1, points.Length - 1)];

        return (next - prev) * tension;
    }

    private Vector3 HermiteFormula(Vector3 p0, Vector3 t0, Vector3 p1, Vector3 t1, float t)
    {
        float t2 = t * t;
        float t3 = t2 * t;

        float h00 = 2 * t3 - 3 * t2 + 1;
        float h10 = t3 - 2 * t2 + t;
        float h01 = -2 * t3 + 3 * t2;
        float h11 = t3 - t2;

        return h00 * p0 + h10 * t0 + h01 * p1 + h11 * t1;
    }
}