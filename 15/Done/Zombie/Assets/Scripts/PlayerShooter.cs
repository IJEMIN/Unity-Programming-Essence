using UnityEngine;
using UnityEngine.UI;

// 주어진 Gun 오브젝트를 쏘거나 재장전한다
// 알맞은 애니메이션을 재생하고, IK를 통해 3D모델의 양손이 총의 손잡이에 맞춰 위치하도록 조정한다
public class PlayerShooter : MonoBehaviour {

    public Gun gun; // 사용할 총
    public Transform gunPivot; // 총 배치의 기준점
    public Transform leftHandMount; // 총의 왼쪽 손잡이, 3D 모델의 왼손이 위치할 지점
    public Transform rightHandMount; // 총의 오른쪽 손잡이, 3D 모델의 오른손이 위치할 지점

    public Text ammoText; // 탄약을 표시할 UI 텍스트

    private PlayerInput playerInput; // 플레이어의 입력을 전달하는 컴포넌트
    private Animator playerAnimator; // 플레이어의 애니메이터 컴포넌트

    private void Start () {
        // 사용할 컴포넌트들을 가져온다
        playerInput = GetComponent<PlayerInput> ();
        playerAnimator = GetComponent<Animator> ();
    }

    private void OnDisable () {
        // 컴포넌트가 비활성화되었을때 총 게임 오브젝트를 비활성화한다
        gun.gameObject.SetActive (false);
    }

    private void Update () {
        // 발사 입력을 감지하고 총을 발사한다

    }

    private void UpdateUI () {

    }

    private void OnAnimatorIK (int layerIndex) {
        // 애니메이터의 IK를 갱신한다

    }

}