using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// PlayerController는 플레이어 캐릭터로서 Player 게임 오브젝트를 제어한다.
public class PlayerController : MonoBehaviour {
   public AudioClip deathClip; // 사망시 재생할 오디오 클립
   public float jumpForce = 700f; // 점프 힘

   private bool isGrounded = false; // 바닥에 닿았는지 나타냄
   private bool isDead = false; // 사망 상태

   private Rigidbody2D playerRigidbody; // 사용할 리지드바디 컴포넌트
   private Animator animator; // 사용할 애니메이터 컴포넌트
   private AudioSource playerAudio; // 사용할 오디오 소스 컴포넌트

   private void Start() {
       // 초기화
   }


   private void Update() {
       // 사용자 입력을 감지하고 점프하는 처리
   }

   private void Die() {
       // 사망 처리
   }

   private void OnTriggerEnter2D(Collider2D other) {
       // 트리거 콜라이더를 가진 장애물과의 충돌을 감지
   }

   private void OnCollisionEnter2D(Collision2D other) {
       // 바닥에 닿았음을 감지하는 처리
   }

   private void OnCollisionExit2D(Collision2D other) {
       // 바닥에서 벗어났음을 감지하는 처리
   }
}