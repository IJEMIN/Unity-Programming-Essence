# 오류와 오탈자

이 문서는 『레트로의 유니티 게임 프로그래밍 에센스』 초판의 오류와 수정 사항을 정리합니다.

- 최종 갱신: 2026-07-13
- 오탈자 제보: [GitHub Issues](https://github.com/IJEMIN/Unity-Programming-Essence/issues) 또는 [한빛미디어 홈페이지](https://www.hanbit.co.kr/store/books/look.php?p_code=B3604463061)

| 페이지 | 위치 및 비고 | 오탈자 | 수정 |
| ---: | --- | --- | --- |
| 043 | 우상단 캡쳐화면 | **Projects** 탭 | **Installs** 탭 |
| 117 | 마지막 줄 | 브로드케이팅 | 브로드캐스팅 |
| 193 | 5.2.4 멤버와 접근 제한자 | \[도표\] 접근 제한자의 동작<br>**punlic** name; | \[도표\] 접근 제한자의 동작<br>**public** name; |
| 289 |  | 수정사항이 적용했으니 | 수정사항을 적용했으니 |
| 294 |  | Random.Range() 메서드는 입력으로 **최댓값과 최솟값**을 순서대로 | Random.Range() 메서드는 입력으로 **최솟값과 최댓값**을 순서대로 |
| 295 | 마지막 줄 | 누적된 시간을 저장하는 변수 **timeSpawnRate**를 체크합니다. | 누적된 시간을 저장하는 변수 **timeAfterSpawn**를 체크합니다. |
| 338 | 위에서 3번째 줄 | **SampleManager**.LoadScene(0); | **SceneManager**.LoadScene(0); |
| 341 | 페이지 하단에 있는 함수 정의 부분 | string 저장/가져오기<br>- **PlayerPrefs.SetString(string key, float value);**<br>- PlayerPrefs.GetString(string key); | string 저장/가져오기<br>- **PlayerPrefs.SetString(string key, string value);**<br>- PlayerPrefs.GetString(string key); |
| 367 | 1권 마지막 줄 | B - A = (-3, 5)를 공간상에서 보면 **B에서 A로** 가는 방향과 거리를 표현 | B - A = (-3, 5)를 공간상에서 보면 **A에서 B로** 가는 방향과 거리를 표현 |
| 369 | 1권 가장 위 그림 | A • B = \|B\| cos θ | A • B = \|A\| \|B\| cos θ |
| 374 | 맨 마지막 줄 | Vector3 c = a - b // c는 (-1, **2**, -1)이 됨 | Vector3 c = a - b // c는 (-1, **-2**, -1)이 됨 |
| 407 | 10.2.5 회전 | 왼쪽 방향키나 **아래쪽** 방향키 | 왼쪽 방향키나 **오른쪽** 방향키 |
| 432 |  | **오디오 소스 컴포넌트**가 게임속에서 듣는 소리가 게임에 최종출력되는 소리가 됩니다. | **오디오 리스너 컴포넌트**가 게임속에서 듣는 소리가 게임에 최종출력되는 소리가 됩니다. |
| 432 |  | **오디오 컴포넌트**가 오디오 리스너 컴포넌트를 가진 게임 오브젝트에 가까워질수록 해당 **오디오 컴포넌트**의 소리가 크게 들립니다. | **오디오 소스 컴포넌트**가 오디오 리스너 컴포넌트를 가진 게임 오브젝트에 가까워질수록 해당 **오디오 소스 컴포넌트**의 소리가 크게 들립니다. |
| 459 | 맨 위에서 3번째 줄 | 플레이어 캐릭터의 발이 바닥에 닿아 있어 isGrounded의 값이 **false**라면 | 플레이어 캐릭터의 발이 바닥에 닿아 있어 isGrounded의 값이 **true**라면 |
| 529 | **비고:** 다른 페이지에서는 poolPosition에 (0, -25)가 할당되어 있지만 위 두 페이지에서는 poolPosition에 (0, -20)을 할당합니다. | private Vector2 poolPosition = new Vector2(0, **-20**); | private Vector2 poolPosition = new Vector2(0, **-25**); |
| 530 | 페이지 중간, 코드 제외하고 설명 8번째줄 | **timeSpawn** 위에 선언된 timeBetSpawnMin, timeBetSpawnMax는 timeBetSpawn의 최솟값과 최댓값을 결정합니다. | **timeBetSpawn** 위에 선언된 timeBetSpawnMin, timeBetSpawnMax는 timeBetSpawn의 최솟값과 최댓값을 결정합니다. |
| 532 | 2번째 줄 (주석) | 생성된 발판을 **platform** 배열에 할당 | 생성된 발판을 **platforms** 배열에 할당 |
| 532 |  | poolPosition의 값은 (0, -25)이므로 복제 생성된 발판은 게임 화면에서 **왼쪽으로** 멀리 벗어나 보이지 않습니다. | poolPosition의 값은 (0, -25)이므로 복제 생성된 발판은 게임 화면에서 **아래쪽**으로 멀리 벗어나 보이지 않습니다. |
| 537 | **비고:** 다른 페이지에서는 poolPosition에 (0, -25)가 할당되어 있지만 위 두 페이지에서는 poolPosition에 (0, -20)을 할당합니다. | private Vector2 poolPosition = new Vector2(0, **-20**); | private Vector2 poolPosition = new Vector2(0, **-25**); |
| 592 | 페이지 전반<br><br>**비고:** P592, 593에서 언급된 move 변수는 moveDistance를 가리키는 것입니다.<br>move를 모두 moveDistance로 바꿔야 합니다. | move | moveDistance |
| 593 | 페이지 전반<br><br>**비고:** P592, 593에서 언급된 move 변수는 moveDistance를 가리키는 것입니다.<br>move를 모두 moveDistance로 바꿔야 합니다. | move | moveDistance |
| 632 | **비고:** 슬래시 두개 --가 —로 인쇄됨 | // 남은 탄알 수를 -1<br>magAmmo—; | // 남은 탄알 수를 -1<br>magAmmo--; |
| 634 | 밑에서 3번째 줄 | OnDamabe() | OnDamage() |
| 635 | **비고:** 슬래시 두개 --가 —로 인쇄됨 | // 남은 탄알 수를 -1<br>magAmmo—; | // 남은 탄알 수를 -1<br>magAmmo--; |
| 641 | **비고:** 슬래시 두개 --가 —로 인쇄됨 | // 남은 탄알 수를 -1<br>magAmmo—; | // 남은 탄알 수를 -1<br>magAmmo--; |
| 649 | 15.4.4의 OnDisable() 설명 부분 | 컴포넌트가 **비할성화**될 때 | 컴포넌트가 **비활성화**될 때 |
| 653 | 위에서 2번째 줄 | **AvatarIKHit** 타입으로 | **AvatarIKHint** 타입으로 |
| 662 | 아래에서 9번째 줄 | 상속한 자식 클래스는 **LivignEntity**의 구현을 | 상속한 자식 클래스는 **LivingEntity**의 구현을 |
| 670 | 하단 소스코드 | void Start() \{<br>&nbsp;&nbsp;&nbsp;&nbsp;onClean += **CleanRoomA;**<br>&nbsp;&nbsp;&nbsp;&nbsp;onClean += **CleanRoomB;**<br>\} | void Start() \{<br>&nbsp;&nbsp;&nbsp;&nbsp;**onClean += CleaningRoomA;**<br>&nbsp;&nbsp;&nbsp;&nbsp;**onClean += CleaningRoomB;**<br>\} |
| 683 | 과정 01의 2 | **Heath** Circle | **Health** Circle |
| 724 | 아래에서 세번째 줄 | attackTarget.OnDamage(damage, hitPoint, **hitNoraml**); | attackTarget.OnDamage(damage, hitPoint, **hitNormal**); |
| 801 | 아래에서 6번째 줄 | 클라이언들 | 클라이언트들 |
| 872 | 아래에서 두번째 줄 | Destroy() 대신 **Photon.Network**.Destroy() 메서드 사용 | Destroy() 대신 **PhotonNetwork**.Destroy() 메서드 사용 |
| 876 | 과정 01의 3 | **Resource** 폴더 옮김 | **Resources** 폴더 옮김 |
| 886 | 아래에서 8번째 줄 | AddScore() 메서드가 **실행도록** | AddScore() 메서드가 **실행되도록** |
| 893 | 19.7.2 웨이브 정보 동기화 제목 바로 윗줄 | Awake() 메서드에서 **Photon.Peer**.RegisterType() 실행 | Awake() 메서드에서 **PhotonPeer**.RegisterType() 실행 |
| 894 |  | Enemy **Spawenr** | Enemy **Spawner** |
