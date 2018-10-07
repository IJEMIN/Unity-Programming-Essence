using UnityEngine;

// 플레이어 캐릭터를 사용자 입력에 따라 움직이는 스크립트
public class PlayerMovement : MonoBehaviour {
    public float moveSpeed = 5f; // 앞뒤 움직임 속력
    public float rotateSpeed = 180f; // 좌우 회전 속력

    private Rigidbody playerRigidbody; // 플레이어 캐릭터의 리지드바디
    private Animator playerAnimator; // 플레이어 캐릭터의 애니메이터
    private PlayerInput playerInput; // 플레이어 입력을 알려주는 컴포넌트

    private void Start() {
        // 사용할 컴포넌트들의 참조 가져오기
    }

    private void Update() {
        // 매 프레임마다 움직임, 회전, 애니메이션 처리 실행
    }

    // 입력값에 따라 캐릭터를 앞뒤로 움직임
    private void Move() {
        
    }

    // 입력값에 따라 캐릭터를 좌우로 회전
    private void Rotate() {
        
    }
}