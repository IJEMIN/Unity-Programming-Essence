using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;

public class Enemy : LivingEntity
{
    public LayerMask explosionTarget;
    public float explosionForce = 1000f; //폭심지에서 발생하는 힘

    public enum State { Idle, Chasing, Attacking };
    State currentState; // 현재 AI 상태

    public ParticleSystem deathEffect; // 적이 죽었을때 재생할 이펙트
    public ParticleSystem hitEffect; // 적이 공격받았을때 재생할 이펙트

    public float damage = 50f; // 적이 폭발하면서 줄 데미지
    public float explosionDistance = 5; // 폭발 반경
    public float explosionSpeed = 1.0f; // 폭발하는 속도

    AudioSource audioPlayer; // 사운드 재생기
    public AudioClip hitSound; // 맞았을때 소리

    private Animator enemyAnimator;

    NavMeshAgent pathFinder; // 경로 AI
    Transform target; // 추적 대상
    LivingEntity targetEntity; // 추적 대상으로부터 가져온 '생명' 정보 
    Material skinMaterial; // 표면의 색깔을 결정하는 애셋

    public Color attackColor = Color.red; // 공격 컬러
    private Color originColor; // 원래 머티리얼의 컬러

    float attackDistanceThreshold = 3.5f; // 공격을 시도하는 거리
    bool hasTarget; // 추적할 대상이 있는가?


    private void Awake()
    {
        enemyAnimator = GetComponent<Animator>();
        audioPlayer = GetComponent<AudioSource>();
        pathFinder = GetComponent<NavMeshAgent>();

        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            hasTarget = true;

            target = GameObject.FindGameObjectWithTag("Player").transform;
            targetEntity = target.GetComponent<LivingEntity>();

            targetEntity.OnDeath += OnTargetDeath;
        }
    }

    protected override void Start()
    {
        base.Start();

        if (hasTarget)
        {
            currentState = State.Chasing;
            StartCoroutine("UpdatePath"); //추적 루틴 시작
        }
    }

    // 타겟이 죽었을때 체인으로 실행됨
    void OnTargetDeath()
    {
        // 타겟이 죽었으니 더이상 추적안함
        hasTarget = false;
        currentState = State.Idle;
    }

    private void Update()
    {
        if (hasTarget)
        {
            float distance = Vector3.Distance(transform.position, target.position);

            if (distance < attackDistanceThreshold && currentState != State.Attacking)
            {
               // StartCoroutine("Attack");
            }
        }
    }


    // 외부에서 Enemy를 만들경우, 이것을 통해 디테일을 설정함
    public void Setup(float moveSpeed, float newDamage,
        float newHeath, Color skinColor)
    {
        pathFinder.speed = moveSpeed;
        damage = newDamage;

        startingHealth = newHeath;

        // 이펙트와 표면의 색깔을 설정
        deathEffect.startColor = skinColor;

        skinMaterial = GetComponent<Renderer>().material;
        skinMaterial.color = skinColor;
        originColor = skinMaterial.color;
    }

    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitDirection)
    {
        audioPlayer.clip = hitSound; // 클립을 교체하고
        audioPlayer.Play(); // 데미지 입는 소리를 재생

        var effectInstance = Instantiate(hitEffect, hitPoint,
            Quaternion.FromToRotation(Vector3.forward, hitDirection));

        effectInstance.Play();
        //아래 코드는 파티클 시스템 컴포넌트 측의 StopAction을 Destroy를 사용함으로써 대체할수도 있음
        Destroy(effectInstance.gameObject, effectInstance.duration);

        base.OnDamage(damage, hitPoint, hitDirection);
    }

    public override void Die()
    {
        enemyAnimator.SetTrigger("Die");
        GetComponent<Collider>().enabled = false;
        pathFinder.isStopped = true;

        var effectInstance
            = Instantiate(deathEffect, transform.position, transform.rotation);

        var effectAudio = effectInstance.GetComponent<AudioSource>();

        if (effectAudio != null)
        {
            effectAudio.Play();
        }

        effectInstance.Play();

       // Destroy(effectInstance.gameObject, effectInstance.duration);



        base.Die();
    }

    IEnumerator UpdatePath()
    {
        float refreshRate = 0.25f;

        // 추적할 대상이 존재하는 동안 경로 갱신을 무한루프
        while (hasTarget)
        {
            if (currentState == State.Chasing)
            {
                if (!dead)
                {
                    pathFinder.SetDestination(target.position);
                }
            }
            yield return new WaitForSeconds(refreshRate);
        }
    }

    IEnumerator Attack()
    {
        currentState = State.Attacking; // 공격하는 상태로 전환
        pathFinder.isStopped = true; // AI 경로 추적을 중지

        Color startColor = skinMaterial.color; // 시작 
        Color targetColor = attackColor; // 변환 완료로 될 컬러

        float percent = 0f; // 공격 진행도

        while (percent <= 1f)
        {
            percent += Time.deltaTime * explosionSpeed;

            skinMaterial.color = Color.Lerp(startColor, targetColor, percent);

            yield return null;
        }

        Debug.Log("공격을 했다! " + damage);

        // transform.positino을 중심으로 구를 그려서
        // 겹치는 모든 콜라이더를 가져옴
        Collider[] attackTargets
            = Physics.OverlapSphere(transform.position,
            explosionDistance, explosionTarget);

        for (int i = 0; i < attackTargets.Length; i++)
        {
            LivingEntity targetLivingEntity
                = attackTargets[i].GetComponent<LivingEntity>();

            if (targetLivingEntity != null)
            {
                Transform targetTransform = targetLivingEntity.transform;

                targetLivingEntity.OnDamage(damage,
                    targetTransform.position, transform.forward);

                Debug.Log("공격당한 상대방 체력: "
                    + targetLivingEntity.health);
            }

            Rigidbody targetRigidbody = attackTargets[i].GetComponent<Rigidbody>();

            if (targetRigidbody != null)
            {
                targetRigidbody.AddExplosionForce(explosionForce,
                    transform.position, explosionDistance);
            }
        }

        Die(); // 자폭이라서 공격하면 스스로 죽음
    }
}