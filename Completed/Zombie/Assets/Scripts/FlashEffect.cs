using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// 화면을 깜박이는 스크립트
public class FlashEffect : MonoBehaviour {
    public static FlashEffect instance; // 싱글톤을 위한 static 변수

    public Image flashImage; // 깜박이는데 사용할 이미지 컴포넌트
    public Color flashColor = new Color (1f, 0f, 0f, 0.3f); // 깜박일때 사용할 컬러
    public float flashSpeed = 5f; // 깜박이는 속도

    private void Awake () {
        // 싱글톤으로 접근 가능하게 설정
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            // 이미 다른 싱글톤 오브젝트가 씬에 존재하면 자신을 파괴
            Destroy (gameObject);
            return;
        }

        // 컬러를 무색으로 초기화
        flashImage.color = Color.clear;
    }

    public void Flash () {
        // 화면을 깜박인다

        StopCoroutine ("FlashRoutine"); // 이전의 플래시 코루틴이 있다면 강제 종료
        StartCoroutine ("FlashRoutine"); // 플래시 코루틴을 새로 시작
    }

    private IEnumerator FlashRoutine () {
        flashImage.color = flashColor; // 처음 컬러로는 깜박임 컬러로 시작
        float progress = 0f; // 진행도를 0으로 초기화

        // 깜박임 컬러에서 무색으로 점점 변하는 처리
        while (progress < 1.0f) { // 진행도가 1.0 (100%)에 도달할때 까지 반복

            // 이미지의 컬러를 진행도를 기준으로 깜빡임~무색 사이에서 결정
            flashImage.color = Color.Lerp (flashColor, Color.clear, progress);

            progress += Time.deltaTime * flashSpeed; // 진행도를 증가
            yield return null; // 한 프레임 쉬기
        }

        flashImage.color = Color.clear; // 깜박임 처리가 끝났다면 무색으로 바꾸고 코루틴 종료
    }

}