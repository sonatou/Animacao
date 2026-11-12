using UnityEngine;

[RequireComponent(typeof(Animator))]
public class SaitamaController : MonoBehaviour
{
    [Header("Blend Tree (valores do parâmetro Speed)")]
    [SerializeField] private float walkValue = 0.5f;
    [SerializeField] private float runValue = 1f;
    [SerializeField] private float dampTime = 0.12f;

    [Header("Movimento (opcional)")]
    [Tooltip("Desligado: o personagem fica parado no lugar (use animações Mixamo 'In Place').")]
    [SerializeField] private bool moveTransform = false;
    [SerializeField] private float walkSpeed = 1.5f;
    [SerializeField] private float runSpeed = 4f;
    [SerializeField] private float turnSpeed = 120f;

    [Header("Interface")]
    [SerializeField] private bool showHelp = true;

    private Animator anim;
    private bool dancing;

    private static readonly int SpeedParam = Animator.StringToHash("Speed");
    private static readonly int PunchParam = Animator.StringToHash("Punch");
    private static readonly int DanceParam = Animator.StringToHash("Dance");

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");
        bool run = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        float target = 0f;
        if (v > 0.1f)
            target = run ? runValue : walkValue;

        if (Input.GetKeyDown(KeyCode.Alpha2))
            dancing = !dancing;

        if (target > 0f)
            dancing = false;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            dancing = false;
            anim.SetTrigger(PunchParam);
        }

        anim.SetBool(DanceParam, dancing);
        anim.SetFloat(SpeedParam, target, dampTime, Time.deltaTime);

        if (moveTransform && !dancing)
        {
            transform.Rotate(0f, h * turnSpeed * Time.deltaTime, 0f);

            float speed = target > 0f ? (run ? runSpeed : walkSpeed) : 0f;
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }

    private void OnGUI()
    {
        if (!showHelp) return;

        GUILayout.BeginArea(new Rect(10, 10, 320, 120), GUI.skin.box);
        GUILayout.Label("Controles do Saitama");
        GUILayout.Label("W / Seta cima: andar   (+Shift: correr)");
        GUILayout.Label("1: soco");
        GUILayout.Label("2: dançar (liga/desliga)");
        GUILayout.EndArea();
    }
}