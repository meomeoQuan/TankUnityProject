using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody2D))]
public class TankScript : MonoBehaviour
{
    // public InputActionAsset actionsAsset; // optional; or assign InputActions individually in inspector
    // public InputAction moveAction;
    // public InputAction fireAction; // Button
    public Transform firePoint;
    public float moveSpeed = 10f;
    public float rotateSpeed = 300f;
    public GameObject bulletPrefab;
    // public int playerNumber = 1; // Player1 hoặc Player2
    Rigidbody2D rb;
    Vector2 moveInput;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.gravityScale = 0f;
        }
    }

    // Unity gọi hàm này tự động khi action "Move" thay đổi
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
    void OnFire()
    {
        Fire();
    }

    void FixedUpdate()
    {
        // Clamp moveInput to avoid excessive speed
        float moveAmount = Mathf.Clamp(moveInput.y, -1f, 1f);
        float rotateAmount = Mathf.Clamp(moveInput.x, -1f, 1f);
        // Move forward/backward
        rb.linearVelocity = transform.up * moveInput.y * moveSpeed;

        // Rotate left/right
        float rotation = rb.rotation - moveInput.x * rotateSpeed * Time.fixedDeltaTime;
        rb.MoveRotation(rotation);
    }

    void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        // Thêm lực đẩy cho viên đạn
        Rigidbody2D brb = bullet.GetComponent<Rigidbody2D>();
        brb.linearVelocity = firePoint.up * 12f;
        // Destroy viên đạn sau 3 giây
        Destroy(bullet, 3f);
    }
    
}
