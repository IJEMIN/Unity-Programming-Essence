using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : LivingEntity {
    public ParticleSystem hitEffect; // 적이 공격받았을때 재생할 이펙트

    public Renderer enemeyRenderer;

    public LayerMask targetLayer;

    public float damage = 30f; // 적이 폭발하면서 줄 데미지

    public AudioClip hitSound; // 맞았을때 소리
    public AudioClip deathSound; // 죽었을때 재생할 소리

    //[HideInInspector]
    public LivingEntity targetEntity; // 추적 대상으로부터 가져온 '생명' 정보

    public float timeBetAttck = 0.5f; // 공격 간격
    public float attackRanage = 0.6f;
    private float lastAttackTime;

    bool hasTarget {
        get {
            if (targetEntity != null && !targetEntity.dead) {
                return true;
            }
            return false;
        }
    } // 추적할 대상이 있는가?

    private Animator enemyAnimator;
    private AudioSource enemyAudioPlayer; // 사운드 재생기
    private NavMeshAgent pathFinder; // 경로 AI

    private void Awake () {
        enemyAnimator = GetComponent<Animator> ();
        enemyAudioPlayer = GetComponent<AudioSource> ();
        pathFinder = GetComponent<NavMeshAgent> ();
    }

    void Start () {
        StartCoroutine (UpdatePath ()); //추적 루틴 시작
    }

    private void Update () {
        if (hasTarget && !dead) {

            Debug.DrawLine (transform.position + Vector3.up * 0.5f, transform.position + Vector3.up * 0.5f+ transform.forward * attackRanage);

            float distance = Vector3.Distance (transform.position, targetEntity.transform.position);

            if (distance <= attackRanage && Time.time >= lastAttackTime + timeBetAttck) {
                lastAttackTime = Time.time;
                Attack ();
            }
        }
    }

    // 외부에서 Enemy를 만들경우, 이것을 통해 디테일을 설정함
    public void Setup (float moveSpeed, float newDamage, float newHeath, Color skinColor, LivingEntity newTarget) {
        pathFinder.speed = moveSpeed;
        damage = newDamage;
        startingHealth = newHeath;
        enemeyRenderer.material.color = skinColor;
        targetEntity = newTarget;
    }

    public override void OnDamage (float damage, Vector3 hitPoint, Vector3 hitDirection) {
        enemyAudioPlayer.PlayOneShot (hitSound); // 데미지 입는 소리를 재생

        var effectInstance = Instantiate (hitEffect, hitPoint, Quaternion.LookRotation (-hitDirection));
        effectInstance.Play ();

        Destroy (effectInstance.gameObject, effectInstance.main.duration);

        base.OnDamage (damage, hitPoint, hitDirection);
    }

    public override void Die () {
        FindObjectOfType<GameManager>().AddScore(100);
        pathFinder.isStopped = true;

        enemyAnimator.SetTrigger ("Die");
        GetComponent<Collider> ().enabled = false;
        base.Die ();
    }

    IEnumerator UpdatePath () {
        // 추적할 대상이 존재하는 동안 경로 갱신을 무한루프
        while (hasTarget && !dead) {
            pathFinder.SetDestination (targetEntity.transform.position);
            yield return new WaitForSeconds (0.25f);
        }
    }

    void Attack () {

        RaycastHit hit;

        if (Physics.Raycast(transform.position + Vector3.up * 0.5f, transform.forward, out hit, attackRanage, targetLayer)) {
            LivingEntity target = hit.collider.GetComponent<LivingEntity> ();

            if (target != null) {
                target.OnDamage (damage, hit.point, -hit.normal);
            }
        }
    }

}