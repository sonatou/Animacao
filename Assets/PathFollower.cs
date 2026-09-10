using UnityEngine;
using UnityEngine.InputSystem;

public enum CurveMode { Linear, Hermite }

[RequireComponent(typeof(Transform))]
public class PathFollower : MonoBehaviour
{
    [Header("Referências")]
    [SerializeField] private PathControlPoints controlPoints;
    [SerializeField] private HermitePath hermitePath;

    [Header("Configuração")]
    [SerializeField] private float speed = 0.2f;
    [SerializeField] private CurveMode currentMode = CurveMode.Linear;

    private float t = 0f;

    private void Update()
    {
        HandleInput();
        MoveAlongPath();
    }

    private void HandleInput()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            currentMode = currentMode == CurveMode.Linear ? CurveMode.Hermite : CurveMode.Linear;
            Debug.Log($"Modo trocado para: {currentMode}");
        }
    }

    private void MoveAlongPath()
    {
        Vector3[] points = controlPoints.GetControlPoints();
        if (points.Length < 2) return;

        t += speed * Time.deltaTime;
        if (t > 1f) t = 0f; // loop na trajetória

        Vector3 targetPosition = currentMode == CurveMode.Linear
            ? LinearPath.Evaluate(points, t)
            : hermitePath.Evaluate(points, t);

        transform.position = targetPosition;
    }

    public bool CurrentModeIsHermite() => currentMode == CurveMode.Hermite;
}