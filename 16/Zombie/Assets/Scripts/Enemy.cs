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

    }

    public void Setup (float newHealth, float newDamage, float newSpeed, Color skinColor, LivingEntity newTarget) {
        // 적 AI의 생성할때 스펙을 결정하기 위해 사용할 메서드


    }

    private void Start () {
        // 게임 오브젝트 활성화와 동시에 적 AI의 추적 루틴을 시작한다
        StartCoroutine (UpdatePath ()); //추적 루틴 시작
    }

    private void Update () {
        // 추적할 대상이 있는지 없는지에 따라 알맞은 애니메이션을 재생한다
    }

    private IEnumerator UpdatePath () {
        // 주기적으로 추적할 대상의 위치를 찾아 경로를 계산한다

        while (!dead) { // 살아있는 동안만 반복한다

            yield return new WaitForSeconds (0.25f); // 0.25초 주기로 경로를 갱신한다
        }
    }

    public override void OnDamage (float damage, Vector3 hitPoint, Vector3 hitNormal) {
        // 데미지를 입었을때 처리할 것

        // 부모의 OnDamage 메서드부터 실행한다
        base.OnDamage (damage, hitPoint, hitNormal);

    }

    public override void Die () {
        // 사망 처리를 구현한다

        base.Die (); // 부모의 사망 메서드부터 실행한다

    }

    private void OnTriggerStay (Collider other) {
        // 플레이어와 접촉하고 있는 동안 주기적으로 데미지를 주는 처리가 온다

        // 충돌한 상대방 게임 오브젝트가 Player 태그를 가지고 있는지 검사한다
        if (other.tag == "Player") {

        }
    }
}