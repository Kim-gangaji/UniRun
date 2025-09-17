using UnityEngine;

// NinjaFrog_PlayerController : 플레이어 캐릭터 제어
public class NinjaFrog_PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 8f;     // 이동 속력
    public float jumpForce = 250f; // 점프 힘

    private int jumpCount = 0;    // 누적 점프 횟수
    private bool isGrounded = false; // 바닥에 닿았는지 여부
    private bool isDead = false;     // 사망 여부

    private Rigidbody2D playerRigidbody; // 리지드바디
    private Animator animator;           // 애니메이터

    private Vector3 originalScale;       // 시작 시 캐릭터의 원래 크기 저장

    private void Start()
    {
        // 필요한 컴포넌트 가져오기
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // 현재 오브젝트의 원래 크기를 저장
        originalScale = transform.localScale;
    }

    private void Update()
    {
        if (isDead)
            return;

        // --- 1. 좌우 이동 ---
        float moveInput = Input.GetAxis("Horizontal"); // -1 ~ 1 입력
        playerRigidbody.linearVelocity = new Vector2(moveInput * speed, playerRigidbody.linearVelocity.y);

        // --- 2. 이동 방향에 따라 좌우 반전 ---
        if (moveInput < 0)
        {
            transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
        }
        else if (moveInput > 0)
        {
            transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);
        }

        // --- 3. 점프 처리 ---
        if (Input.GetButtonDown("Jump") && jumpCount < 2)
        {
            if (jumpCount == 0)
            {
                animator.SetTrigger("isJumping"); // 첫 점프
            }
            else
            {
                animator.SetTrigger("isDoubleJumping"); // 더블 점프
            }

            jumpCount++;

            // 점프 직전 Y 속도를 0으로 리셋
            playerRigidbody.linearVelocity = new Vector2(playerRigidbody.linearVelocity.x, 0);

            // 위쪽으로 힘 가하기
            playerRigidbody.AddForce(new Vector2(0, jumpForce));
        }

        // --- 4. 애니메이터 파라미터 업데이트 ---
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
