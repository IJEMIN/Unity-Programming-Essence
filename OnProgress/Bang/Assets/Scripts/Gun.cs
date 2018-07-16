using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    public Transform leftHandle;
    public Transform rightHandle;

    public Transform m_FireTransform; // 총구의 위치를 나타내는 트랜스폼
    public ParticleSystem m_ShellEjectEffect; // 탄피 배출 효과 재생기
    public ParticleSystem m_MuzzleFlashEffect; // 총구 화염 효과 재생기

    public AudioSource m_GunAudioPlayer; // 총 소리 재생기
    public AudioClip m_ShotClip; // 발사 소리
    public AudioClip m_ReloadClip; // 재장전 소리

    public LineRenderer m_BulletLineRenderer; // 총알 궤적 랜더러

    public int m_MaxAmmo = 13; // 탄창의 최대 탄약 수
    public float m_TimeBetFire = 0.3f; // 발사와 발사 사이의 시간 간격
    public float m_Damage = 25;
    public float m_ReloadTime = 2.0f;
    public float m_FireDistance = 100f;

    public enum State { Ready, Empty, Reloading };

    public State m_CurrentState { get; private set; } // 현재 총의 상태

    private float m_LastFireTime; // 총을 마지막으로 발사한 시점
    private int m_CurrentAmmo = 0; // 탄창에 남은 현재 탄약 갯수


    // Use this for initialization
    void Start()
    {
        m_CurrentState = State.Empty; // 탄약이 빈 상태로 지정
        m_LastFireTime = 0; // 마지막으로 총을 쏜 시점을 초기화

        m_BulletLineRenderer.positionCount = 2; // 라인랜더러가 사용할 정점을 두개로 지정
        m_BulletLineRenderer.enabled = false; // 라인랜더러를 끔

        UpdateUI(); // UI를 갱신
    }

    // 발사 처리를 시도하는 함수
    public void Fire()
    {
        // 총이 준비된 상태고 AND 현재 시간 >= 마지막 발사 시점 + 연사 간격
        if (m_CurrentState == State.Ready && Time.time >= m_LastFireTime + m_TimeBetFire)
        {
            m_LastFireTime = Time.time; // 마지막으로 총을 쏜 시점이 현재 시점으로 갱신

            Shot();
            UpdateUI();
        }
    }

    // 실제 발사 처리를 하는 부분
    private void Shot()
    {
        RaycastHit hit; // 레이캐스트 정보를 저장하는, 충돌 정보 컨테이너

        // 총을쏴서 총알이 맞은 곳 : 처음값으로는 총구 위치 + 총구 위치로 앞쪽 방향 * 사정거리
        Vector3 hitPosition = m_FireTransform.position + m_FireTransform.forward * m_FireDistance;

        // 레이캐스트(시작지점, 방향, 충돌 정보 컨테이너, 사정거리)
        if (Physics.Raycast(m_FireTransform.position, m_FireTransform.forward, out hit, m_FireDistance))
        {
            // 상대방이 IDamageable 로서 가져와진다면,
            // 상대방의 OnDamage 함수를 실행시켜서 데미지를 쥐어준다

            
            IDamageable target = hit.collider.GetComponent<IDamageable>();

            if (target != null)
            {
                target.OnDamage(m_Damage, hit.point, hit.point - m_FireTransform.position);
            }


            // 충돌 위치를 가져오기
            hitPosition = hit.point;

        }

        // 발사 이펙트 재생 시작
        StartCoroutine(ShotEffect(hitPosition));

        // 남은 탄환의 수를 -1
        m_CurrentAmmo--;

        if (m_CurrentAmmo <= 0)
        {
            m_CurrentState = State.Empty;
        }
    }

    // 발사 이펙트를 재생하고 총알 궤적을 잠시 그렸다가 끄는 함수
    private IEnumerator ShotEffect(Vector3 hitPosition)
    {
        // 총알 궤적 랜더러를 켬
        m_BulletLineRenderer.enabled = true;

        // 선분의 첫번째 점은 총구의 위치 
        m_BulletLineRenderer.SetPosition(0, m_FireTransform.position);

        // 선분의 두번째 점은 입력으로 들어온 피탄 위치
        m_BulletLineRenderer.SetPosition(1, hitPosition);

        // 이펙트들 재생
        m_MuzzleFlashEffect.Play(); // 총구 화염 효과 재생
        m_ShellEjectEffect.Play(); // 탄피 배출 효과 재생

        m_GunAudioPlayer.PlayOneShot(m_ShotClip); // 총격 소리 재생

        yield return new WaitForSeconds(0.03f); // 처리를 '잠시' 쉬는 시간

        m_BulletLineRenderer.enabled = false;
    }

    // 총의 탄약 UI에 남은 탄약수를 갱신해서 띄어줌
    private void UpdateUI()
    {
        if (m_CurrentState == State.Empty)
        {
           // m_AmmoText.text = "EMPTY";
        }
        else if (m_CurrentState == State.Reloading)
        {
           // m_AmmoText.text = "RELOADING";
        }
        else
        {
           // m_AmmoText.text = m_CurrentAmmo.ToString();
        }
    }

    // 재장전을 시도
    public void Reload()
    {
        if (m_CurrentState != State.Reloading)
        {
            StartCoroutine(ReloadRoutin())
;
        }
    }

    // 실제 재장전 처리가 진행되는 곳
    private IEnumerator ReloadRoutin()
    {
        m_CurrentState = State.Reloading; // 현재 상태를 재장전 상태로 전환

        m_GunAudioPlayer.clip = m_ReloadClip; // 오디오 소스의 클립을 재장전 소리로 교체
        m_GunAudioPlayer.Play(); // 재장전 소리 재생

        UpdateUI();

        yield return new WaitForSeconds(m_ReloadTime); // 재장전 시간 만큼 처리를 쉰다

        m_CurrentAmmo = m_MaxAmmo; // 탄약 최대 충전
        m_CurrentState = State.Ready;
        UpdateUI();
    }
}