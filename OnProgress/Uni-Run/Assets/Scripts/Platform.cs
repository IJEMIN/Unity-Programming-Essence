using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 발판으로서, 플레이어가 닿았을때 무엇을 할지 결정하는 스크립트
// 플레이어가 자신에게 닿으면 점수를 추가한다.
// 그리고 매번 활성화 될때마다 자신의 장애물 중 어떠한 것들을 활성화 시킬지 결정한다.
public class Platform : MonoBehaviour {
    public GameObject[] obstacles; // 사용할 장애물 오브젝트들
    private bool stepped = false; // 플레이어가 이미 한번 닿았었는가

    private void OnEnable () {
        // 컴포넌트가 활성화될때 마다 매번 실행된다
        // 발판이 재배치되었을때 장애물의 위치와 플레이어와 닿았는지에 대한 정보를 리셋
        stepped = false;

        for (int i = 0; i < obstacles.Length; i++) {

            if (Random.Range (0, 3) == 0) {
                obstacles[i].SetActive (true);
            } else {
                obstacles[i].SetActive (false);
            }
        }
    }

    void OnCollisionEnter2D (Collision2D collision) {
        // 플레이어가 닿았을때(충돌했을때) 점수를 추가하는 처리
        if (collision.collider.tag == "Player" && !stepped) {
            stepped = true;
            GameManager.instance.AddScore (1);
        }
    }
}