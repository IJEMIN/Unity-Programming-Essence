using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : LivingEntity {

    private NavMeshAgent pathFinder; // 경로 AI

    public LivingEntity targetEntity; // 추적 대상으로부터 가져온 '생명' 정보
    private Animator enemyAnimator;
    private Renderer enemeyRenderer; // 적의 모습을 그리는 랜더러

    public ParticleSystem hitEffect; // 적이 공격받았을때 재생할 이펙트
    private AudioSource enemyAudioPlayer; // 사운드 재생기
    public AudioClip deathSound; // 죽었을때 재생할 소리
    public AudioClip hitSound; // 맞았을때 소리

    public float damage = 20f; // 적의 공격력
    public int score = 100; // 적을 죽였을때 얻을 점수

    public float timeBetAttck = 0.5f; // 공격 간격
    private float lastAttackTime;

    // 추적할 대상이 있는가?
    private bool hasTarget {
        get {
            if (targetEntity != null && !targetEntity.dead) {
                return true;
            }
            return false;
        }
    }

    private void Awake () {
        enemyAnimator = GetComponent<Animator> ();
        enemyAudioPlayer = GetComponent<AudioSource> ();
        pathFinder = GetComponent<NavMeshAgent> ();

        enemeyRenderer = GetComponentInChildren<Renderer> ();
    }

    // 외부에서 Enemy를 만들경우, 이것을 통해 디테일을 설정함
    public void Setup (float newHealth, float newDamage, float newSpeed, Color skinColor, LivingEntity newTarget) {
        pathFinder.speed = newSpeed;
        damage = newDamage;
        startingHealth = newHealth;
        enemeyRenderer.material.color = skinColor;
        targetEntity = newTarget;
    }

    private void Start () {
        StartCoroutine (UpdatePath ()); //추적 루틴 시작
    }

    private void Update () {
        enemyAnimator.SetBool ("HasTarget", hasTarget);
    }

    private IEnumerator UpdatePath () {

        while (!dead) {

            if (hasTarget) {
                pathFinder.isStopped = false;
                pathFinder.SetDestination (targetEntity.transform.position);
            } else {
                pathFinder.isStopped = true;
            }

            yield return new WaitForSeconds (0.25f);
        }
    }

    public override void OnDamage (float damage, Vector3 hitPoint, Vector3 hitNormal) {
        base.OnDamage (damage, hitPoint, hitNormal);

        ParticleSystem effectInstance = Instantiate (hitEffect, hitPoint, Quaternion.LookRotation (hitNormal));
        effectInstance.Play ();
        Destroy (effectInstance.gameObject, effectInstance.main.duration);

        enemyAudioPlayer.PlayOneShot (hitSound); // 데미지 입는 소리를 재생
    }

    public override void Die () {
        base.Die ();

        // 다른 물체들을 방해하지 않도록 자신의 충돌체들을 모두 해제
        Collider[] enemyColliders = GetComponents<Collider> ();

        for (int i = 0; i < enemyColliders.Length; i++) {
            enemyColliders[i].enabled = false;
        }

        pathFinder.isStopped = true;
        pathFinder.enabled = false;

        enemyAnimator.SetTrigger ("Die");
        enemyAudioPlayer.PlayOneShot (deathSound);
        GameManager.instance.AddScore (score);
    }

    private void OnTriggerStay (Collider other) {
        if (!dead && Time.time >= lastAttackTime + timeBetAttck)
            if (other.tag == "Player") {
                lastAttackTime = Time.time;
                LivingEntity target = other.GetComponent<LivingEntity> ();

                if (target != null) {
                    target.OnDamage (damage, other.ClosestPoint (transform.position), -transform.forward);
                }
            }
    }
}