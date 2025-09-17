using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Jump")]
    public float jumpForce = 0.2f;             // 若继续用 AddForce
    public float jumpSpeed = 6f;             // 若用“直接设速度”，用这个
    public int maxJumpCount = 2;
    public LayerMask groundMask;
    public Transform groundCheck;
    [SerializeField] float groundProbe = 0.15f;
    [SerializeField] float groundOffset = 0.01f;
    // 或者改成 Physics2D.CircleCast/OverlapCircle 来检测


    [Header("Refs")]
    public GameManager gameManager;

    Rigidbody2D rb;
    int jumpCount;
    bool isGrounded;
    Animator animator;
    void Awake() => rb = GetComponent<Rigidbody2D>();
    void Start()
    {
        animator = GetComponent<Animator>();
        
        animator.speed = 0.5f;
    }
    void Update()
    {
        if (gameManager == null || gameManager.State != GameState.Playing)
        {
            Debug.LogWarning($"[{System.DateTime.Now:HH:mm:ss}] gameManager 未绑定或游戏未开始");
            return;
        }
        if (!groundCheck)
        {
            Debug.LogWarning("groundCheck 未绑定");
            return;
        }

        // ——稳定的探地：从脚底稍下方打一根短射线，只命中 Ground——
        Vector2 origin = (Vector2)groundCheck.position + Vector2.down * groundOffset;
        var hit = Physics2D.Raycast(origin, Vector2.down, groundProbe, groundMask);
        Debug.DrawRay(origin, Vector2.down * groundProbe, hit.collider ? Color.green : Color.red);
        Debug.Log($"Raycast hit: {hit.collider?.name}");
        isGrounded = hit.collider != null;
        Debug.Log($"isGrounded: {isGrounded}");

        if (isGrounded) jumpCount = 0;

        // 可视化（运行时在 Scene 里看得到）
        Debug.DrawLine(origin, origin + Vector2.down * groundProbe, isGrounded ? Color.green : Color.red);

        // 输入
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            TryJump();
    }

    private void TryJump()
    {
        if (!(isGrounded || jumpCount < maxJumpCount)) return;

        // 方案A：直接设速度（推荐，最可控）
        // rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpSpeed);

        // 方案B：如果你坚持用 AddForce，请改为以下两行（别同时用A和B）
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        jumpCount++;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Obstacle"))
            gameManager.GameOver();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Obstacle"))
            gameManager.GameOver();
    }

    void OnDrawGizmosSelected()
    {
        if (!groundCheck) return;
        Gizmos.color = Color.yellow;
        Vector3 o = groundCheck.position + Vector3.down * groundOffset;
        Gizmos.DrawLine(o, o + Vector3.down * groundProbe);
    }
}
