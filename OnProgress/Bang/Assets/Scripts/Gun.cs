using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour {


    public Transform fireTransform; // 총구의 위치를 나타내는 트랜스폼
    public ParticleSystem shellEjectEffect; // 탄피 배출 효과 재생기
    public ParticleSystem muzzleFlashEffect; // 총구 화염 효과 재생기

    private AudioSource gunAudioPlayer; // 총 소리 재생기
    public AudioClip shotClip; // 발사 소리
    public AudioClip reloadClip; // 재장전 소리

    private LineRenderer bulletLineRenderer; // 총알 궤적 랜더러

    public int magCapacity = 13; // 탄창 용량
    public int magAmmo = 0; // 탄창에 남은 현재 탄약 갯수
    public int ammoRemain = 100;

    public float timeBetFire = 0.5f; // 발사와 발사 사이의 시간 간격
    public float damage = 25;
    public float reloadTime = 1.8f;
    private float fireDistance = 50f;

    public enum State { Ready, Empty, Reloading }

    public State state { get; private set; } // 현재 총의 상태

    private float lastFireTime; // 총을 마지막으로 발사한 시점

    void Awake () {
        gunAudioPlayer = GetComponent<AudioSource> ();
        bulletLineRenderer = GetComponent<LineRenderer> ();
    }

    // Use this for initialization
    void Start () {
        state = State.Empty; // 탄약이 빈 상태로 지정
        lastFireTime = 0; // 마지막으로 총을 쏜 시점을 초기화

        bulletLineRenderer.positionCount = 2; // 라인랜더러가 사용할 정점을 두개로 지정
        bulletLineRenderer.enabled = false; // 라인랜더러를 끔
    }

    // 발사 처리를 시도하는 함수
    public void Fire () {
        // 총이 준비된 상태고 AND 현재 시간 >= 마지막 발사 시점 + 연사 간격
        if (state == State.Ready && Time.time >= lastFireTime + timeBetFire) {
            lastFireTime = Time.time; // 마지막으로 총을 쏜 시점이 현재 시점으로 갱신

            Shot ();
        }
    }

    // 실제 발사 처리를 하는 부분
    private void Shot () {
        RaycastHit hit; // 레이캐스트 정보를 저장하는, 충돌 정보 컨테이너

        // 총을쏴서 총알이 맞은 곳 : 처음값으로는 총구 위치 + 총구 위치로 앞쪽 방향 * 사정거리
        Vector3 hitPosition = fireTransform.position + fireTransform.forward * fireDistance;

        // 레이캐스트(시작지점, 방향, 충돌 정보 컨테이너, 사정거리)
        if (Physics.Raycast (fireTransform.position, fireTransform.forward, out hit, fireDistance)) {

            // 상대방이 IDamageable 로서 가져와진다면,
            // 상대방의 OnDamage 함수를 실행시켜서 데미지를 쥐어준다

            IDamageable target = hit.collider.GetComponent<IDamageable> ();
            if (target != null) {
                target.OnDamage (damage, hit.point, hit.normal);
            }

            hitPosition = hit.point;
            // 충돌 위치를 가져오기
        }

        // 발사 이펙트 재생 시작
        StartCoroutine (ShotEffect (hitPosition));

        // 남은 탄환의 수를 -1
        magAmmo--;

        if (magAmmo <= 0) {
            state = State.Empty;
        }
    }

    // 발사 이펙트를 재생하고 총알 궤적을 잠시 그렸다가 끄는 함수
    private IEnumerator ShotEffect (Vector3 hitPosition) {
        // 총알 궤적 랜더러를 켬
        bulletLineRenderer.enabled = true;

        // 선분의 첫번째 점은 총구의 위치
        bulletLineRenderer.SetPosition (0, fireTransform.position);

        // 선분의 두번째 점은 입력으로 들어온 피탄 위치
        bulletLineRenderer.SetPosition (1, hitPosition);

        // 이펙트들 재생
        muzzleFlashEffect.Play (); // 총구 화염 효과 재생
        shellEjectEffect.Play (); // 탄피 배출 효과 재생

        gunAudioPlayer.PlayOneShot (shotClip); // 총격 소리 재생

        yield return new WaitForSeconds (0.03f); // 처리를 '잠시' 쉬는 시간

        bulletLineRenderer.enabled = false;
    }

    // 재장전을 시도
    public void Reload () {
        if (state != State.Reloading && ammoRemain > 0) {
            StartCoroutine (ReloadRoutin ());
        }
    }

    // 실제 재장전 처리가 진행되는 곳
    private IEnumerator ReloadRoutin () {
        state = State.Reloading; // 현재 상태를 재장전 상태로 전환

        gunAudioPlayer.clip = reloadClip; // 오디오 소스의 클립을 재장전 소리로 교체
        gunAudioPlayer.Play (); // 재장전 소리 재생

        yield return new WaitForSeconds (reloadTime); // 재장전 시간 만큼 처리를 쉰다

        // 탄창의 탄약을 임시로 전체 탄약으로 옮기기
        ammoRemain += magAmmo;
        magAmmo = 0;

        if (ammoRemain >= magCapacity) {
            magAmmo = magCapacity; // 탄약 최대 충전
            ammoRemain -= magCapacity;
        } else {
            magAmmo = ammoRemain;
            ammoRemain = 0;
        }

        state = State.Ready;
    }
}