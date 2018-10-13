using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections.Generic;

// 플레이어 캐릭터의 생명체로서의 동작을 담당
public class PlayerHealth : LivingEntity {
    public Slider healthSlider; // 체력을 표시할 UI 슬라이더

    public AudioClip deathClip; // 사망 소리
    public AudioClip hitClip; // 피격 소리
    public AudioClip itemPickupClip; // 아이템 습득 소리

    private AudioSource playerAudioPlayer; // 플레이어 소리 재생기
    private Animator playerAnimator; // 플레이어의 애니메이터

    private PlayerShooter playerShooter;
    private PlayerMovement playerMovement;

    private void Awake() {
        // 사용할 컴포넌트를 가져오고 수치 초기화
        playerAnimator = GetComponent<Animator>();
        playerAudioPlayer = GetComponent<AudioSource>();

        playerShooter = GetComponent<PlayerShooter>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    protected override void OnEnable() {
        base.OnEnable();

        healthSlider.gameObject.SetActive(true);
        // 체력 슬라이더의 최대값을 기본 체력값으로 변경
        healthSlider.maxValue = startingHealth;
        // 체력 슬라이더의 값을 현재 체력값으로 변경
        healthSlider.value = health;

        // 플레이어 관련 컴포넌트들 다시 활성화
        playerShooter.enabled = true;
        playerMovement.enabled = true;
    }

    protected override void OnHealthUpdated(float value) {
        healthSlider.value = value;
    }

    // 데미지 처리
    public override void OnDamage(float damage, Vector3 hitPoint,
        Vector3 hitDirection) {
        // LivingEntity의 OnDamage() 실행(데미지 적용)
        base.OnDamage(damage, hitPoint, hitDirection);

        if (!dead)
        {
            // 사망하지 않은 경우에만 효과음을 재생
            playerAudioPlayer.PlayOneShot(hitClip);
        }
    }

    // 사망 처리
    public override void Die() {
        // LivingEntity의 Die() 실행(사망 적용)
        base.Die();

        // 체력 슬라이더를 비활성화
        healthSlider.gameObject.SetActive(false);

        // 사망음 재생
        playerAudioPlayer.PlayOneShot(deathClip);
        // 애니메이터의 Die 트리거를 발동시켜 사망 애니메이션 재생
        playerAnimator.SetTrigger("Die");

        // 플레이어 슈터 컴포넌트 비활성화
        playerShooter.enabled = false;
        // 플레이어 움직임 컴포넌트 비활성화
        playerMovement.enabled = false;
    }

    private void OnTriggerEnter(Collider other) {
        // 아이템과 충돌한 경우 해당 아이템을 사용하는 처리
        // 사망하지 않은 경우에만 아이템 사용가능
        if (!dead)
        {
            // 충돌한 상대방으로 부터 Item 컴포넌트를 가져오기 시도
            IItem item = other.GetComponent<IItem>();

            // 충돌한 상대방으로부터 Item 컴포넌트가 가져오는데 성공했다면
            if (item != null)
            {
                // Use 메서드를 실행하여 아이템 사용
                item.Use(gameObject);

                // 아이템 습득 소리 재생
                playerAudioPlayer.PlayOneShot(itemPickupClip);
            }
        }
    }
}