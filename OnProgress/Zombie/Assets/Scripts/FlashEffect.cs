using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FlashEffect : MonoBehaviour {
	public static FlashEffect instance;

	public Image flashImage;
	public Color flashColor = new Color (1f, 0f, 0f, 0.3f);
	public float flashSpeed = 4f;

	private void Awake () {
		instance = this;
	}

	private void OnEnable () {
		flashImage.color = Color.clear;
	}

	public void Flash () {
		StopCoroutine ("FlashRoutine"); // 이전의 플래시 코루틴을 강제 종료
		StartCoroutine ("FlashRoutine"); // 새로 플래시 코루틴을 시작
	}

	private IEnumerator FlashRoutine () {
		flashImage.color = flashColor;

		float progress = 0f;
		while (progress < 1) {

			flashImage.color = Color.Lerp (flashColor, Color.clear, progress);
			progress += Time.time * flashSpeed;
			yield return null;
		}

		flashImage.color = Color.clear;
	}

}