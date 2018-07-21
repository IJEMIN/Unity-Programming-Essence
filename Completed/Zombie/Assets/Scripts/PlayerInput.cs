using UnityEngine;

// 플레이어 캐릭터를 조작하기 위한 사용자 입력을 감지하고, 감지된 입력값을 다른 컴포넌트들이 사용할 수 있도록 제공한다.
public class PlayerInput : MonoBehaviour {
    public string moveInputAxisName = "Vertical"; // 앞뒤 움직임을 위한 입력축 이름
    public string rotateInputAxisName = "Horizontal"; // 좌우 회전을 위한 입력축 이름
    public string fireInputButtonName = "Fire1"; // 발사를 위한 입력 버튼 이름

    // 자동 생성 프로퍼티를 사용하여, 값을 가져올때는(get) public으로, 값을 할당할때는(set) private으로 동작한다.
    public float move { get; private set; } // 감지된 move 입력값
    public float rotate { get; private set; } // 감지된 rotate 입력값
    public bool fire { get; private set; } // 감지된 fire 입력값

    // Update 메서드에서 매프레임 사용자 입력을 감지한다
    void Update() {
        // 만약 게임오버인 상태라면 사용자 입력을 감지하지 않는다
        if (GameManager.instance != null && GameManager.instance.isGameover) {
            move = 0;
            rotate = 0;
            fire = false;
            return;
        }

        move = Input.GetAxis(moveInputAxisName); // move에 관한 입력 감지
        rotate = Input.GetAxis(rotateInputAxisName); // rotate에 관한 입력 감지
        fire = Input.GetButton(fireInputButtonName); // fire에 관한 입력 감지
    }
}