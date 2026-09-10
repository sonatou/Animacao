using UnityEngine;

public class PathControlPoints : MonoBehaviour
{
    [Header("Configuração visual (Gizmos)")]
    [SerializeField] private float pointRadius = 0.2f;
    [SerializeField] private Color pointColor = Color.yellow;


    public Vector3[] GetControlPoints()
    {
        int count = transform.childCount;
        Vector3[] points = new Vector3[count];

        for (int i = 0; i < count; i++)
        {
            points[i] = transform.GetChild(i).position;
        }

        return points;
    }

    public int PointCount => transform.childCount;

    private void OnDrawGizmos()
    {
        Gizmos.color = pointColor;

        for (int i = 0; i < transform.childCount; i++)
        {
            Vector3 pos = transform.GetChild(i).position;
            Gizmos.DrawSphere(pos, pointRadius);

            if (i < transform.childCount - 1)
            {
                Vector3 nextPos = transform.GetChild(i + 1).position;
                Gizmos.DrawLine(pos, nextPos);
            }
        }
    }
}