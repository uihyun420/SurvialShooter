using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.iOS;

public class PlayerInput : MonoBehaviour
{
    public static readonly string horizontal = "Horizontal";
    public static readonly string vertical = "Vertical";

    private static readonly int hashMove = Animator.StringToHash("Move");

    private Animator animator;
    public Gun gun;
    private float moveSpeed = 5f;

    private Rigidbody rb;
    private CinemachineFollow characterCamera;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        characterCamera = GetComponent<CinemachineFollow>();
        gun = GetComponent<Gun>();
    }

    private void Update()
    {
        if (Time.timeScale == 0f) return;

        float h = Input.GetAxis(horizontal);
        float v = Input.GetAxis(vertical);

        Vector3 velocity = new Vector3(h, 0f, v);

        velocity *= moveSpeed;
        rb.linearVelocity = velocity;

        animator.SetFloat(hashMove, velocity.magnitude);
        LookMouseCursor();
    }

    private void LookMouseCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit raycastHit;
        if(Physics.Raycast(ray, out raycastHit))
        {
            Vector3 mouseDirection = new Vector3(raycastHit.point.x, transform.position.y, raycastHit.point.z) - transform.position;
            animator.transform.forward = mouseDirection;
        }
    }
}
