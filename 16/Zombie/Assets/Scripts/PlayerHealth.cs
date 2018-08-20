using UnityEngine;
using UnityEngine.UI;

// 플레이어 캐릭터의 생명체로서의 동작을 담당
public class PlayerHealth : LivingEntity {
    public Slider healthSlider; // 체력을 표시할 UI 슬라이더

    private Animator playerAnimator; // 플레이어의 애니메이터

    private AudioSource playerAudioPlayer; // 플레이어 소리 재생기
    public AudioClip deathClip; // 사망 소리
    public AudioClip hitClip; // 피격 소리
    public AudioClip itemPickupClip; // 아이템 습득 소리


    private void Start() {
        // 초기 설정을 하고 사용할 컴포넌트들의 참조를 가져오기
    }

    public override void RestoreHealth(float newHealth) {
        // 체력을 회복하는 처리
        base.RestoreHealth(newHealth); // LivingEntity의 RestoreHealth() 실행 (체력 증가)
    }

    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitDirection) {
        // 데미지를 입었을때의 처리
        base.OnDamage(damage, hitPoint, hitDirection); // LivingEntity의 OnDamage() 실행(데미지 적용)
    }

    public override void Die() {
        // 죽었을때의 처리
        base.Die(); // LivingEntity의 Die() 실행(사망 적용)
    }

    private void OnTriggerEnter(Collider other) {
        // 아이템과 충돌한 경우 해당 아이템을 사용하는 처리
    }
}