using UnityEngine;

// NinjaFrog_PlayerController : �÷��̾� ĳ���� ����
public class NinjaFrog_PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 8f;     // �̵� �ӷ�
    public float jumpForce = 250f; // ���� ��

    private int jumpCount = 0;    // ���� ���� Ƚ��
    private bool isGrounded = false; // �ٴڿ� ��Ҵ��� ����
    private bool isDead = false;     // ��� ����

    private Rigidbody2D playerRigidbody; // ������ٵ�
    private Animator animator;           // �ִϸ�����

    private Vector3 originalScale;       // ���� �� ĳ������ ���� ũ�� ����

    private void Start()
    {
        // �ʿ��� ������Ʈ ��������
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // ���� ������Ʈ�� ���� ũ�⸦ ����
        originalScale = transform.localScale;
    }

    private void Update()
    {
        if (isDead)
            return;

        // --- 1. �¿� �̵� ---
        float moveInput = Input.GetAxis("Horizontal"); // -1 ~ 1 �Է�
        playerRigidbody.linearVelocity = new Vector2(moveInput * speed, playerRigidbody.linearVelocity.y);

        // --- 2. �̵� ���⿡ ���� �¿� ���� ---
        if (moveInput < 0)
        {
            transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
        }
        else if (moveInput > 0)
        {
            transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);
        }

        // --- 3. ���� ó�� ---
        if (Input.GetButtonDown("Jump") && jumpCount < 2)
        {
            if (jumpCount == 0)
            {
                animator.SetTrigger("isJumping"); // ù ����
            }
            else
            {
                animator.SetTrigger("isDoubleJumping"); // ���� ����
            }

            jumpCount++;

            // ���� ���� Y �ӵ��� 0���� ����
            playerRigidbody.linearVelocity = new Vector2(playerRigidbody.linearVelocity.x, 0);

            // �������� �� ���ϱ�
            playerRigidbody.AddForce(new Vector2(0, jumpForce));
        }

        // --- 4. �ִϸ����� �Ķ���� ������Ʈ ---
        animator.SetFloat("moveSpeed", Mathf.Abs(moveInput));
        animator.SetBool("isGrounded", isGrounded);
    }

    private void Die()
    {
        animator.SetTrigger("Die");

        playerRigidbody.linearVelocity = Vector2.zero;
        playerRigidbody.bodyType = RigidbodyType2D.Kinematic;

        isDead = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "DEAD" && !isDead)
        {
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0.7f)
        {
            isGrounded = true;
            jumpCount = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }
}
