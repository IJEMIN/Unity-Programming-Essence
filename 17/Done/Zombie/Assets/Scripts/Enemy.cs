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
       enemyAnimator = GetComponent<Animator>();
       enemyAudioPlayer = GetComponent<AudioSource>();
       pathFinder = GetComponent<NavMeshAgent>();

       // 랜더러 컴포넌트는 자식 게임 오브젝트에게 있으므로, GetComponentInChildren() 메서드를 사용한다
       enemyRenderer = GetComponentInChildren<Renderer>();
   }

   public void Setup(float newHealth, float newDamage, float newSpeed, Color skinColor, LivingEntity newTarget) {
       // 적 AI의 능력치와 추적대상을 설정하는 메서드
       targetEntity = newTarget; // AI가 추적할 대상을 지정

       pathFinder.speed = newSpeed; // 내비메쉬 에이전트의 이동 속도 설정
       damage = newDamage; // 공격력 설정
       startingHealth = newHealth; // 체력 설정
       enemyRenderer.material.color = skinColor; // 랜더러의 머테리얼 컬러 변경, 표면색이 변한다
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
           // 살아있는 동안 무한 루프

           if (hasTarget)
           {
               // 추적 대상이 존재 : 경로를 갱신하고 AI 이동을 계속 진행
               pathFinder.isStopped = false;
               pathFinder.SetDestination(targetEntity.transform.position);
           }
           else
           {
               // 추적 대상이 없으면 AI 이동을 중지
               pathFinder.isStopped = true;
           }

           yield return new WaitForSeconds(0.25f); // 0.25초 주기로 처리 반복
       }
   }


   public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal) {
       // 데미지를 입었을때 실행할 처리
       if (!dead)
       {
           // 공격 받은 지점과 방향으로 파티클 효과를 재생
           hitEffect.transform.position = hitPoint;
           hitEffect.transform.rotation = Quaternion.LookRotation(hitNormal);
           hitEffect.Play();

           enemyAudioPlayer.PlayOneShot(hitSound); // 데미지 입는 소리를 재생
       }

       base.OnDamage(damage, hitPoint, hitNormal); // LivingEntity의 OnDamage()를 실행하여 데미지 적용
   }

   public override void Die() {
       // 사망 처리를 구현
       base.Die(); // LivingEntity의 Die()를 실행하여 기본 사망 처리 실행

       // 다른 오브젝트들을 방해하지 않도록 자신의 모든 콜라이더들을 비활성화
       Collider[] enemyColliders = GetComponents<Collider>();
       for (int i = 0; i < enemyColliders.Length; i++)
       {
           enemyColliders[i].enabled = false;
       }

       // AI 추적을 중지하고 내바메쉬 컴포넌트를 비활성화
       pathFinder.isStopped = true;
       pathFinder.enabled = false;

       enemyAnimator.SetTrigger("Die"); // 사망 애니메이션 재생
       enemyAudioPlayer.PlayOneShot(deathSound); // 사망 효과음 재생
   }

   private void OnTriggerStay(Collider other) {
       // 트리거 충돌한 상대방 게임 오브젝트가 추적 대상이라면 공격 실행
       if (!dead && Time.time >= lastAttackTime + timeBetAttack)
       {
           // 자신이 사망하지 않았고, 최근 공격 시점에서 timeBetAttack 이상 시간이 흘렀다면 공격 가능

           // 상대방으로부터 LivingEntity 타입을 가져오기 시도
           LivingEntity attackTarget = other.GetComponent<LivingEntity>();

           if (attackTarget != null && attackTarget == targetEntity)
           {
               // 상대방으로부터 가져온 LivingEntity 오브젝트가 추적 대상이 맞다면
               lastAttackTime = Time.time; // 최근 공격 시간을 갱신

               // 상대방이 공격 받는 위치와 공격 받는 표면의 방향을 근삿값으로 설정
               Vector3 hitPoint = other.ClosestPoint(transform.position);
               Vector3 hitNoraml = transform.position - other.transform.position;

               attackTarget.OnDamage(damage, hitPoint, hitNoraml); // 상대방을 공격
           }
       }
   }
}