# 오탈자와 오류
이 곳은 [**레트로의 유니티 게임 프로그래밍 에센스**](http://www.yes24.com/24/goods/69320872)에서 발견된 오탈자와 수정 내용을 확인할 수 있는 페이지입니다.

## 신고 경로
책의 오탈자와 예제 프로젝트의 잘못된 점은 아래 경로 중 한곳에 신고하면 됩니다.

- [한빛미디어 웹페이지](http://www.hanbit.co.kr/store/books/look.php?p_code=B3604463061) | [깃허브 프로젝트 페이지](https://github.com/IJEMIN/Unity-Programming-Essence) | [저자 유튜브](https://youtube.com/c/JeminDev) | [저자 블로그](https://ijemin.com) | [저자 이메일](i_jemin@outlook.com)


## 오탈자

### P341
- **오타**
>string 저장/가져오기<br>- **```PlayerPrefs.SetString(string key, float value);```**<br>- ```PlayerPrefs.GetString(string key);```

- **수정**
>string 저장/가져오기<br>- **```PlayerPrefs.SetString(string key, string value);```**<br>- ```PlayerPrefs.GetString(string key);```

### P432
- **오타**
>**오디오 소스 컴포넌트**가 게임속에서 듣는 소리가 게임에 최종출력되는 소리가 됩니다.

>**오디오 컴포넌트**가 오디오 리스너 컴포넌트를 가진 게임 오브젝트에 가까워질수록 해당 **오디오 컴포넌트**의 소리가 크게 들립니다.


- **수정**
>**오디오 리스너 컴포넌트**가 게임속에서 듣는 소리가 게임에 최종출력되는 소리가 됩니다.

>**오디오 소스 컴포넌트**가 오디오 리스너 컴포넌트를 가진 게임 오브젝트에 가까워질수록 해당 **오디오 소스 컴포넌트**의 소리가 크게 들립니다.

### P529, P537
- **오타**

다른 페이지에서는 poolPosition에 (0, -25)가 할당되어 있지만 위 두 페이지에서는 poolPosition에 (0, -20)을 할당합니다.
>```private Vector2 poolPosition = new Vector2(0, -20);```

- **수정**
>```private Vector2 poolPosition = new Vector2(0, -25);```

poolPosition의 값은 화면에 보이지 않을 정도로 원점에서 멀리 떨어진 위치 값이면 어떠한 값이라도 좋습니다.
따라서 (0, -20)과 (0, -25) 중에서 어느쪽을 사용해도 상관없지만, 통일성을 위해 (0, -25)로 통일합니다.

### P530
- **오타**
>**timeSpawn** 위에 선언된 timeBetSpawnMin, timeBetSpawnMax는 timeBetSpawn의 최솟값과 최댓값을 결정합니다.

- **수정**
>**timeBetSpawn** 위에 선언된 timeBetSpawnMin, timeBetSpawnMax는 timeBetSpawn의 최솟값과 최댓값을 결정합니다.

### P532
- **오타**
> poolPosition의 값은 (0, -25)이므로 복제 생성된 발판은 게임 화면에서 **왼쪽**으로 멀리 벗어나 보이지 않습니다.

- **수정**
> poolPosition의 값은 (0, -25)이므로 복제 생성된 발판은 게임 화면에서 **아래쪽**으로 멀리 벗어나 보이지 않습니다.


### P592, 593
- **오타**

예제 코드 상에서는 ```Vector3 moveDistance;```를 사용했으나, 본문에서 해당 변수를 **move**라는 이름으로 언급했습니다.

- **수정**

P592, 593에서 언급된 **move** 변수는 **moveDistance** 를 가리키는 것입니다.

### P670
- **오타**
~~~
void Start() {
    onClean += CleanRoomA;
    onClean += CleanRoomB;
}
~~~
- **수정**
~~~
void Start() {
    onClean += CleaningRoomA;
    onClean += CleaningRoomB;
}
~~~