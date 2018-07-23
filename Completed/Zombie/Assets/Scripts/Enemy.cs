using System.Collections;
using UnityEngine;
using UnityEngine.AI;

// 적 AI를 구현한다
public class Enemy : LivingEntity {

    private NavMeshAgent pathFinder; // 경로계산 AI 에이전트

    public LivingEntity targetEntity; // 추적할 대상
    private Animator enemyAnimator; // 적 자신의 애니메이터
    private Renderer enemeyRenderer; // 적 자신의 랜더러

    public ParticleSystem hitEffect; // 공격 받았을때 재생할 파티클 효과

    private AudioSource enemyAudioPlayer; // 적 자신의 소리 재생기
    public AudioClip deathSound; // 사망시 재생할 소리
    public AudioClip hitSound; // 공격 받았을때 재생할 소리

    public float damage = 20f; // 공격력
    public int score = 100; // 사망시 플레이어가 흭득할 점수

    public float timeBetAttck = 0.5f; // 공격 간격
    private float lastAttackTime; // 마지막으로 공격을 한 시간

    // 추적할 대상이 있는지 알려주는 프로퍼티
    private bool hasTarget {
        get {
            // 추적할 대상이 존재하고, 대상이 사망하지 않았다면 참 true
            if (targetEntity != null && !targetEntity.dead) {
                return true;
            }

            // 그렇지 않다면 거짓 false
            return false;
        }
    }

    private void Awake () {
        // 게임 오브젝트가 활성화되었을때 실행할 초기화들
        enemyAnimator = GetComponent<Animator> ();
        enemyAudioPlayer = GetComponent<AudioSource> ();
        pathFinder = GetComponent<NavMeshAgent> ();

        // 랜더러 컴포넌트는 자식 게임 오브젝트에게 있으므로, GetComponentInChildren() 메서드를 사용한다
        enemeyRenderer = GetComponentInChildren<Renderer> ();
    }

    public void Setup (float newHealth, float newDamage, float newSpeed, Color skinColor, LivingEntity newTarget) {
        // 적 AI의 생성할때 스펙을 결정하기 위해 사용할 메서드

        targetEntity = newTarget; // 적 AI가 추적할 대상을 지정

        pathFinder.speed = newSpeed; // AI 에이전트의 이동 속도 설정
        damage = newDamage; // 공격력 설정
        startingHealth = newHealth; // 체력 설정
        enemeyRenderer.material.color = skinColor; // 랜더러의 머테리얼 컬러 변경, 표면색이 변한다
    }

    private void Start () {
        // 게임 오브젝트 활성화와 동시에 적 AI의 추적 루틴을 시작한다
        StartCoroutine (UpdatePath ()); //추적 루틴 시작
    }

    private void Update () {
        // 추적할 대상이 있는지 없는지에 따라 알맞은 애니메이션을 재생한다
        enemyAnimator.SetBool ("HasTarget", hasTarget);
    }

    private IEnumerator UpdatePath () {
        // 주기적으로 추적할 대상의 위치를 찾아 경로를 계산한다

        while (!dead) { // 살아있는 동안만 반복한다

            if (hasTarget) {
                // 추적 대상이 존재하면 경로를 갱신하고 AI 이동을 시작(계속)한다
                pathFinder.isStopped = false;
                pathFinder.SetDestination (targetEntity.transform.position);
            } else {
                // 추적 대상이 없으면 AI 이동을 중지한다
                pathFinder.isStopped = true;
            }

            yield return new WaitForSeconds (0.25f); // 0.25초 주기로 경로를 갱신한다
        }
    }

    public override void OnDamage (float damage, Vector3 hitPoint, Vector3 hitNormal) {
        // 데미지를 입었을때 처리할 것

        // 부모의 OnDamage 메서드부터 실행한다
        base.OnDamage (damage, hitPoint, hitNormal);

        // 데미지를 입는 파티클 효과를 공격 당한 지점에, 공격이 들어온 방향을 바라보도록 생성한다
        ParticleSystem effectInstance = Instantiate (hitEffect, hitPoint, Quaternion.LookRotation (hitNormal));
        effectInstance.Play (); // 파티클 효과 재생

        // 파티클 효과 재생이 끝나면 자동 파괴되게 한다
        Destroy (effectInstance.gameObject, effectInstance.main.duration);

        enemyAudioPlayer.PlayOneShot (hitSound); // 데미지 입는 소리를 재생
    }

    public override void Die () {
        // 사망 처리를 구현한다

        base.Die (); // 부모의 사망 메서드부터 실행한다

        // 다른 물체들을 방해하지 않도록 자신의 모든 충돌체들을 찾아 비활성화 한다
        Collider[] enemyColliders = GetComponents<Collider> ();

        for (int i = 0; i < enemyColliders.Length; i++) {
            enemyColliders[i].enabled = false;
        }

        // AI 추적을 중지하고, 컴포넌트를 완전히 비활성화 한다
        pathFinder.isStopped = true;
        pathFinder.enabled = false;

        enemyAnimator.SetTrigger ("Die"); // 사망 애니메이션을 재생한다
        enemyAudioPlayer.PlayOneShot (deathSound); // 사망 효과음을 재생한다

        GameManager.instance.AddScore (score); // 점수를 추가한다
    }

    private void OnTriggerStay (Collider other) {
        // 플레이어와 접촉하고 있는 동안 주기적으로 데미지를 주는 처리가 온다

        // 충돌한 상대방 게임 오브젝트가 Player 태그를 가지고 있는지 검사한다
        if (other.tag == "Player") {

            // 죽지 않았고, 마지막 공격 시점에서 공격 주기 이상으로 시간이 지났을때 공격한다
            if (!dead && Time.time >= lastAttackTime + timeBetAttck) {

                // 마지막 공격 시점을 갱신한다
                lastAttackTime = Time.time;

                // 상대방 게임오브젝트로 부터 LivingEntity 컴포넌트를 가져온다
                LivingEntity target = other.GetComponent<LivingEntity> ();

                // LivingEntity 컴포넌트가 성공적으로 가져와졌다면 데미지를 준다
                if (target != null) {

                    // OnDmage 메서드를 호출하여 데미지를 준다
                    // 공격을 맞은 부위는 상대방의 충돌체 표면 위의 정점중에서 적의 위치와 가장 가까운 곳으로 지정한다
                    // 공격을 맞은 표면의 방향은 적의 앞쪽 방향의 반대로 결정 한다
                    target.OnDamage (damage, other.ClosestPoint (transform.position), -transform.forward);
                }
            }
        }
    }
}