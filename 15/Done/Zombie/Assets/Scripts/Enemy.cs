using System.Collections;
using UnityEngine;
using UnityEngine.AI;

// 적 AI로서 목표 추적과 공격과 체력을 구현
public class Enemy : LivingEntity {
   private NavMeshAgent pathFinder; // 경로계산 AI 에이전트

   public LivingEntity targetEntity; // 추적할 대상
   private Animator enemyAnimator; // 자신의 애니메이터
   private Renderer enemyRenderer; // 자신의 랜더러

   public ParticleSystem hitEffect; // 피격시 재생할 파티클 효과

   private AudioSource enemyAudioPlayer; // 자신의 소리 재생기
   public AudioClip deathSound; // 사망시 재생할 소리
   public AudioClip hitSound; // 피격시 재생할 소리

   public float damage = 20f; // 공격력

   public float timeBetAttack = 0.5f; // 공격 간격
   private float lastAttackTime; // 마지막 공격 시점

   // 추적할 대상이 있는지 알려주는 프로퍼티
   private bool hasTarget
   {
       get
       {
           if (targetEntity != null && !targetEntity.dead)
           {
               // 추적할 대상이 존재하며, 대상이 사망하지 않았다면 true 반환
               return true;
           }

           // 그렇지 않다면 false 반환
           return false;
       }
   }

   private void Awake() {
       // 게임 오브젝트가 활성화되었을때 실행할 초기화
   }

   public void Setup(float newHealth, float newDamage, float newSpeed, Color skinColor, LivingEntity newTarget) {
       // 적 AI의 능력치와 추적대상을 설정하는 메서드
   }

   private void Start() {
       // 게임 오브젝트 활성화와 동시에 AI의 추적 루틴을 시작
       StartCoroutine(UpdatePath());
   }

   private void Update() {
       // 추적할 대상의 존재 여부에 따라 알맞은 애니메이션을 재생
       enemyAnimator.SetBool("HasTarget", hasTarget);
   }

   private IEnumerator UpdatePath() {
       // 주기적으로 추적할 대상의 위치를 찾아 경로를 계산
       while (!dead)
       {
           yield return new WaitForSeconds(0.25f); // 0.25초 주기로 처리 반복
       }
   }


   public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal) {
       // 데미지를 입었을때 실행할 처리
       base.OnDamage(damage, hitPoint, hitNormal); // LivingEntity의 OnDamage()를 실행하여 데미지 적용
   }

   public override void Die() {
       // 사망 처리를 구현
       base.Die(); // LivingEntity의 Die()를 실행하여 기본 사망 처리 실행
   }

   private void OnTriggerStay(Collider other) {
       // 트리거 충돌한 상대방 게임 오브젝트가 추적 대상이라면 공격 실행
   }
}