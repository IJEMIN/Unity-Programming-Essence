using UnityEngine;

// 플레이어 캐릭터를 사용자 입력에 따라 앞뒤로 움직이거나 좌우로 회전한다
public class PlayerMovement : MonoBehaviour {
    public float moveSpeed = 5f; // 앞뒤 움직임의 속도
    private Animator playerAnimator; // 플레이어 자신의 애니메이터
    private PlayerInput playerInput; // 플레이어의 입력을 전달하는 컴포넌트
    private Rigidbody playerRigidbody; // 플레이어 자신의 리지드바디
    public float rotateSpeed = 180f; // 좌우 회전 속도

    private void Start () {
        // 사용할 컴포넌트들의 참조를 가져온다
        playerInput = GetComponent<PlayerInput>();
        playerAnimator = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void Update () {
        // 매 프레임 실행할 처리들이 온다

        Rotate (); // 회전 실행
        Move (); // 움직임 실행

        // 애니메이터의 Move 파라미터에 사용자 움직임 입력을 넣음
        playerAnimator.SetFloat("Move",playerInput.move);
    }

    private void Move () {
        // 입력값에 따라 캐릭터를 앞뒤로 움직인다
        // 움직일 방향과 거리 = 앞쪽 방향 * 거리 * 시간
        Vector3 moveDistance
            = playerInput.move * transform.forward * moveSpeed * Time.deltaTime;
        
        playerRigidbody.MovePosition(playerRigidbody.position + moveDistance);
    }

    private void Rotate () {
        // 입력값에 따라 캐릭터를 좌우로 회전한다

        float turn = playerInput.rotate * rotateSpeed * Time.deltaTime;

        playerRigidbody.rotation
            = playerRigidbody.rotation * Quaternion.Euler(0f,turn, 0f);
    }
}