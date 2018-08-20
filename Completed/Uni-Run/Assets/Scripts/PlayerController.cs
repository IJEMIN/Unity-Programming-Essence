using UnityEngine;

// PlayerController는 플레이어로서 플레이어 게임 오브젝트를 제어하는 역할을 가진다.
// 사용자 입력을 감지하여 점프를 하고, 물리 충돌을 통해 장애물에 부딫쳤을때 사망 처리를 한다.
// 또한 현재 바닥에 발이 닿아있는지 체크하며, 상황에 알맞는 효과음과 애니메이션을 재생한다.
public class PlayerController : MonoBehaviour {
    public AudioClip deathClip; // 죽었을때 재생할 오디오 클립
    public float jumpForce = 700f; // 위쪽으로 점프하는 힘

    private bool isGrounded; // 플레이어가 바닥에 닿아있는지
    private bool isDead; // 플레이어가 죽었는지

    private Rigidbody2D playerRigidbody; // 플레이어의 리기드바디 컴포넌트, 점프하는 힘을 주기 위해 사용
    private Animator animator; // 플레이어의 애니메이터 컴포넌트, 애니메이션을 재생하기 위해 사용
    private AudioSource playerAudio; // 플레이어의 오디오소스 컴포넌트, 오디오를 재생하기 위해 사용

    private void Start() {
        // 초기화를 합니다
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
    }

    private void Update() {
        // 사용자 입력을 감지하고 점프를 하는 처리를 구현합니다
        if (isDead)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0) && isGrounded)
        {
            playerRigidbody.AddForce(new Vector2(0, jumpForce));
            playerAudio.Play();
        }
        else if (Input.GetMouseButtonUp(0) && playerRigidbody.velocity.y > 0)
        {
            playerRigidbody.velocity = playerRigidbody.velocity * 0.5f;
        }

        animator.SetBool("Grounded", isGrounded);
    }

    private void Die() {
        // 플레이어가 사망했을 때의 처리를 구현합니다
        animator.SetTrigger("Die");

        playerAudio.clip = deathClip;
        playerAudio.Play();

        playerRigidbody.velocity = Vector2.zero;
        isDead = true;

        GameManager.instance.OnPlayerDead();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        // 트리거 충돌체인 장애물에 부딫쳤을때의 처리가 옵니다
        if (other.tag == "Dead" && !isDead)
        {
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        // 바닥에 닿았음을 감지하는 처리가 옵니다
        isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D other) {
        // 점프 등으로 바닥에서 떼어졌음을 감지하는 처리가 옵니다
        isGrounded = false;
    }
}