using UnityEngine;

// 게임 오브젝트를 지속적으로 회전한다
public class Rotator : MonoBehaviour {
    public float rotationSpeed = 60f;

    private void Update () {
        transform.Rotate (0f, rotationSpeed * Time.deltaTime, 0f);
    }
}