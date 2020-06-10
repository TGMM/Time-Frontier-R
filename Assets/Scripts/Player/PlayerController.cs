using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Camera _mainCamera;
    [SerializeField] public float movementSpeed = 100;

    private void Awake()
    {
        SetUpRb();

        _mainCamera = Camera.main;
    }

    private void FixedUpdate()
    {
        // Handle user input
        var targetVelocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        MovePlayer(targetVelocity);
        RotateToMouse();
    }

    private void SetUpRb()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.isKinematic = false;
        _rb.angularDrag = 0.0f;
        _rb.gravityScale = 0.0f;
    }

    private void MovePlayer(Vector2 targetVelocity)
    {
        // Set rigidbody velocity
        _rb.velocity = targetVelocity * (movementSpeed * Time.deltaTime); // Multiply the target by deltaTime to make movement speed consistent across different framerates
    }

    private void RotateToMouse()
    {
        var dir = Input.mousePosition - _mainCamera.WorldToScreenPoint(transform.position);
        var angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg) - 90;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
