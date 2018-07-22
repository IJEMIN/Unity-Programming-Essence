using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float moveSpeed = 5f; // 앞뒤 움직임의 속도
    public float rotateSpeed = 180f; // 좌우 회전 속도
    private PlayerInput playerInput; // 플레이어의 입력을 전달하는 컴포넌트
    private Rigidbody playerRigidbody; // 플레이어 자신의 리지드바디
    private Animator playerAnimator; // 플레이어 자신의 애니메이터

    void Start() {
        // 사용할 컴포넌트들의 참조를 가져온다
    }

    void Update() {
        // 매 프레임 실행할 처리들이 온다

        Rotate(); // 회전 실행
        Move(); // 움직임 실행
    }

    private void Move() {
        // 입력값에 따라 캐릭터를 앞뒤로 움직인다

    }

    private void Rotate() {
        // 입력값에 따라 캐릭터를 좌우로 회전한다

    }
}