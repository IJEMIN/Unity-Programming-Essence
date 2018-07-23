using System.Collections;
using UnityEngine;

// 총을 구현한다
public class Gun : MonoBehaviour {

    // 총의 상태를 표현하는데 사용할 타입을 선언한다
    public enum State {
        Ready, // 총이 발사될 준비가 됬다
        Empty, // 탄창이 비었다
        Reloading // 재장전 중이다
    }

    public State state { get; private set; } // 현재 총의 상태

    private LineRenderer bulletLineRenderer; // 총알 궤적을 그리기 위한 랜더러
    public Transform fireTransform; // 총알이 발사될 위치

    public ParticleSystem muzzleFlashEffect; // 총구 화염 효과
    public ParticleSystem shellEjectEffect; // 탄피 배출 효과

    private AudioSource gunAudioPlayer; // 총 소리 재생기
    public AudioClip shotClip; // 발사 소리
    public AudioClip reloadClip; // 재장전 소리

    public float damage = 25; // 공격력
    private float fireDistance = 50f; // 사정거리

    public int ammoRemain = 100; // 남은 전체 탄약
    public int magCapacity = 25; // 탄창 용량
    public int magAmmo = 0; // 현재 탄창에 남아있는 탄약

    public float reloadTime = 1.8f; // 재장전 소요 시간
    public float timeBetFire = 0.12f; // 총알 발사 간격
    private float lastFireTime; // 총을 마지막으로 발사한 시점

    private void Awake () {
        // 사용할 컴포넌트들의 참조를 가져오고, 상태를 초기화
        gunAudioPlayer = GetComponent<AudioSource> ();
        bulletLineRenderer = GetComponent<LineRenderer> ();

        bulletLineRenderer.positionCount = 2; // 라인 랜더러가 사용할 정점을 두개로 변경
        bulletLineRenderer.enabled = false; // 라인 랜더러를 비활성화

        state = State.Empty; // 총의 현재 상태를 탄창이 비었음으로 초기화
        lastFireTime = 0; // 마지막으로 총을 쏜 시점을 초기화
    }

    public void Fire () {
        // 발사를 시도하는 메서드

        // 총이 준비된 상태이며, 마지막 발사 시점에서 발사 간격 이상의 시간이 지났다면
        if (state == State.Ready && Time.time >= lastFireTime + timeBetFire) {
            lastFireTime = Time.time; // 마지막으로 총을 쏜 시점이 현재 시점으로 갱신
            Shot (); // 실제 발사를 실행하는 메서드를 실행
        }
    }

    private void Shot () {
        // 실제 발사 처리를 실행한다

        RaycastHit hit; // 레이캐스트에 의한 충돌 정보를 저장하는 컨테이너
        Vector3 hitPosition = Vector3.zero; // 총알이 맞은 곳을 저장할 변수

        // 레이캐스트(시작지점, 방향, 충돌 정보 컨테이너, 사정거리)
        if (Physics.Raycast (fireTransform.position, fireTransform.forward, out hit, fireDistance)) {
            // 레이가 어떤 물체와 충돌한 경우

            // 충돌한 상대방으로부터 IDamageable 오브젝트를 가져오기 시도
            IDamageable target = hit.collider.GetComponent<IDamageable> ();

            // 상대방으로 부터 IDamageable 오브젝트를 가져오는데 성공했다면
            if (target != null) {
                // 상대방의 OnDamage 함수를 실행시켜서 상대방에게 데미지를 준다
                target.OnDamage (damage, hit.point, hit.normal);
            }

            hitPosition = hit.point; // 충돌한 위치를 저장
        } else {

            // 레이가 다른 물체와 충돌하지 않았다면 총알이 최대 사정거리까지 날아갔을때의 위치를 충돌 위치로 삼는다
            // 충돌 위치 = 총구 위치 + 총구 앞쪽 방향 * 사정거리
            hitPosition = fireTransform.position + fireTransform.forward * fireDistance;
        }

        StartCoroutine (ShotEffect (hitPosition)); // 발사 이펙트 재생 시작

        // 남은 탄환의 수를 -1
        magAmmo--;
        if (magAmmo <= 0) {
            // 탄창에 남은 탄약이 없다면, 총의 현재 상태를 Empty으로 갱신
            state = State.Empty;
        }
    }

    private IEnumerator ShotEffect (Vector3 hitPosition) {
        // 발사 이펙트와 소리를 재생하고 총알 궤적을 잠시 그렸다가 끄는 처리가 온다

        // 발사 이펙트 재생
        muzzleFlashEffect.Play (); // 총구 화염 효과 재생
        shellEjectEffect.Play (); // 탄피 배출 효과 재생

        gunAudioPlayer.PlayOneShot (shotClip); // 총격 소리 재생

        bulletLineRenderer.SetPosition (0, fireTransform.position); // 선분의 첫번째 점은 총구의 위치
        bulletLineRenderer.SetPosition (1, hitPosition); // 선분의 끝점은 입력으로 들어온 충돌 위치

        bulletLineRenderer.enabled = true; // 총알 궤적 랜더러를 켠다

        yield return new WaitForSeconds (0.03f); // 총알 궤적 랜더러를 켠체로 잠시 쉰다

        bulletLineRenderer.enabled = false; // 총알 궤적 랜더러를 끈다
    }

    public void Reload () {
        // 재장전을 시도한다

        // 재장전 도중이 아니고, 남은 총알이 존재하는 경우 재장전 처리가 이루어진다
        if (state != State.Reloading && ammoRemain > 0) {
            StartCoroutine (ReloadRoutine ()); // 재장전 처리 시작
        }
    }

    private IEnumerator ReloadRoutine () {
        // 실제 재장전 처리가 진행되는 곳

        state = State.Reloading; // 현재 상태를 재장전 상태로 전환
        gunAudioPlayer.PlayOneShot (reloadClip); // 재장전 소리 재생

        yield return new WaitForSeconds (reloadTime); // 재장전 소요 시간 만큼 처리를 쉰다

        int ammoToFill = magCapacity - magAmmo; // 탄창에 채워야할 탄약을 계산한다

        // 탄창에 채워야할 탄약이 남은 탄약보다 많다면, 채워야할 탄약 수를 남은 탄약 수로 줄인다
        if (ammoRemain < ammoToFill) {
            ammoToFill = ammoRemain;
        }

        magAmmo += ammoToFill; // 탄창을 채운다
        ammoRemain -= ammoToFill; // 남은 탄약에서, 탄창에 채운만큼 탄약을 뺸다

        state = State.Ready; // 총의 현재 상태를 발사 준비된 상태로 변경
    }
}