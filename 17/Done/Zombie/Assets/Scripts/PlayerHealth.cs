using UnityEngine;
using UnityEngine.UI;

// 플레이어 캐릭터의 생명체로서의 동작을 담당
public class PlayerHealth : LivingEntity {
    public AudioClip deathClip; // 사망 소리
    public Slider healthSlider; // 체력을 표시할 UI 슬라이더
    public AudioClip hitClip; // 피격 소리
    public AudioClip itemPickupClip; // 아이템 습득 소리

    private Animator playerAnimator; // 플레이어의 애니메이터

    private AudioSource playerAudioPlayer; // 플레이어 소리 재생기


    private void Start() {
        // 초기 설정을 하고 사용할 컴포넌트들의 참조 가져오기
        playerAnimator = GetComponent<Animator>();
        playerAudioPlayer = GetComponent<AudioSource>();

        healthSlider.maxValue = startingHealth; // 체력 슬라이더의 최대값을 기본 체력값으로 변경
        healthSlider.value = health; // 체력 슬라이더의 값을 현재 체력값으로 변경
    }

    public override void RestoreHealth(float newHealth) {
        // 체력을 회복하는 처리
        base.RestoreHealth(newHealth); // LivingEntity의 RestoreHealth() 실행 (체력 증가)
        healthSlider.value = health; // 갱신된 체력으로 체력 슬라이더를 갱신
    }

    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitDirection) {
        // 데미지를 입었을때의 처리
        if (!dead)
        {
            // 사망하지 않은 경우에만 효과음을 재생
            playerAudioPlayer.PlayOneShot(hitClip);
        }

        base.OnDamage(damage, hitPoint, hitDirection); // LivingEntity의 OnDamage() 실행(데미지 적용)
        healthSlider.value = health; // 갱신된 체력을 체력 슬라이더에 반영
    }

    public override void Die() {
        // 죽었을때의 처리
        base.Die(); // LivingEntity의 Die() 실행(사망 적용)

        healthSlider.gameObject.SetActive(false); // 체력바 게임 오브젝트를 비활성화

        playerAudioPlayer.PlayOneShot(deathClip); // 사망음 재생
        playerAnimator.SetTrigger("Die"); // 애니메이터의 Die 트리거를 발동시켜 사망 애니메이션 재생

        GetComponent<PlayerShooter>().enabled = false; // 플레이어 슈터 해제
        GetComponent<PlayerMovement>().enabled = false; // 플레이어 움직임 해제
    }

    private void OnTriggerEnter(Collider other) {
        // 아이템과 충돌한 경우 해당 아이템을 사용하는 처리
        if (!dead)
        {
            // 사망하지 않은 경우에만 아이템 사용가능
            var item = other.GetComponent<IItem>(); // 충돌한 상대방으로 부터 Item 컴포넌트를 가져오기 시도

            if (item != null)
            {
                // 충돌한 상대방으로부터 Item 컴포넌트가 가져오는데 성공했다면
                playerAudioPlayer.PlayOneShot(itemPickupClip); // 아이템 습득 소리 재생
                item.Use(gameObject); // Use 메서드를 실행해 아이템 사용
            }
        }
    }
}