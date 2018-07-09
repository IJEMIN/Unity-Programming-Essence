using System.Collections;
using UnityEngine;

// 배경의 위치를 검사하고, 배경이 너무 많이 왼쪽으로 이동한 경우
// 주기적으로 오른쪽으로 재배치하는 스크립트
public class BackgroundLoop : MonoBehaviour {
    private float width; // 배경 오브젝트의 가로 길이, BoxCollider2D의 Size의 X값을 통해 알수 있다.

    private void Awake () {
        // 가로길이를 측정하는 처리
    }

    private void Update () {
        // 현재 위치를 검사하여 원점에서 왼쪽으로 width 이상 이동했을때, 위치를 리셋하는 메서드 실행

    }

    private void Reposition () {
        // 위치를 오른쪽 방향으로 리셋

    }
}