using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class PathVisualizer : MonoBehaviour
{
    [SerializeField] private PathControlPoints controlPoints;
    [SerializeField] private HermitePath hermitePath;
    [SerializeField] private PathFollower follower;
    [SerializeField] private int resolution = 50;

    private LineRenderer line;

    private void Awake()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = resolution + 1;
    }

    private void Update()
    {
        DrawCurrentCurve();
    }

    private void DrawCurrentCurve()
    {
        Vector3[] points = controlPoints.GetControlPoints();
        if (points.Length < 2) return;

        bool useHermite = follower != null && follower.CurrentModeIsHermite();

        for (int i = 0; i <= resolution; i++)
        {
            float t = i / (float)resolution;
            Vector3 pos = useHermite
                ? hermitePath.Evaluate(points, t)
                : LinearPath.Evaluate(points, t);

            line.SetPosition(i, pos);
        }
    }
}