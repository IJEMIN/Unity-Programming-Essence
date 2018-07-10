using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 발판으로서, 플레이어가 닿았을때 점수를 추가
// 발판 위에 있는 장애물들 중 활성화 할 것을 랜덤으로 선택
public class Platform : MonoBehaviour
{
    public GameObject[] obstacles; // 사용할 장애물 오브젝트들
    private bool stepped = false; // 플레이어가 이미 한번 닿았었는가

    private void OnEnable()
    {
        // 컴포넌트가 활성화될때 마다 매번 실행된다
        // 발판이 재배치되었을때 장애물의 위치와 플레이어와 닿았는지에 대한 정보를 리셋

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 플레이어가 닿았을때(충돌했을때) 점수를 추가하는 처리

    }
}
