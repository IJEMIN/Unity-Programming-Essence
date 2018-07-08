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
        stepped = false;
       
        for (int i = 0; i < obstacles.Length; i++)
        {
            bool active = false;

            int randomNumber = Random.Range(0, 3);
            if (randomNumber == 1)
            {
                active = true;
            }

            obstacles[i].SetActive(active);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 플레이어가 닿았을때(충돌했을때) 점수를 추가하는 처리
        if(collision.collider.tag == "Player" && !stepped)
        {
            stepped = true;
            // 게임 매니저를 통해 점수를 1점 추가하는 코드가 온다
            GameManager.instance.AddScore(1);
        }
    }
}
