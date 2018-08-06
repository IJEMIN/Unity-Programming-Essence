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


    private void Awake() {
        // 사용할 컴포넌트들의 참조를 가져오고, 상태를 초기화
    }

    public void Fire() {
        // 발사를 시도한다
    }

    private void Shot() {
        // 실제 발사처리가 온다
    }

    private IEnumerator ShotEffect(Vector3 hitPosition) {
        // 발사 이펙트와 소리를 재생하고 총알 궤적을 그린다

        // 발사 이펙트 재생
        bulletLineRenderer.enabled = true; // 총알 궤적 랜더러를 켠다

        yield return new WaitForSeconds(0.03f); // 총알 궤적 랜더러를 켠체로 잠시 쉰다

        bulletLineRenderer.enabled = false; // 총알 궤적 랜더러를 끈다
    }

    public bool Reload() {
        // 재장전을 시도한다
        return false;
    }

    private IEnumerator ReloadRoutine() {
        // 실제 재장전 처리가 진행되는 곳

        state = State.Reloading; // 현재 상태를 재장전 상태로 전환

        yield return new WaitForSeconds(reloadTime); // 재장전 소요 시간 만큼 처리를 쉰다

        state = State.Ready; // 총의 현재 상태를 발사 준비된 상태로 변경
    }
}