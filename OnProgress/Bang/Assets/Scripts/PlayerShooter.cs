using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShooter : MonoBehaviour {

    public Text ammoText;
    public Transform gunPivot;
    public Gun gun;
    private PlayerInput playerInput;
    private Animator playerAnimator;

    public Transform leftIKMount; // IK에 사용할 왼손 마운트 위치
    public Transform rightIKMount; // IK에 사용할 오른손 마운트 위치

    private void Start () {
        playerInput = GetComponent<PlayerInput> ();
        playerAnimator = GetComponent<Animator> ();
    }


	void OnDisable()
	{
        gun.gameObject.SetActive(false);
	}


    private void Update () {

        if (playerInput.fire) {
            if (gun.state == Gun.State.Empty) {
                playerAnimator.SetTrigger ("Reload");
                gun.Reload ();
            } else {
                gun.Fire ();
            }
        }

        UpdateUI ();
    }

    private void OnAnimatorIK (int layerIndex) {

        gunPivot.position = playerAnimator.GetIKHintPosition (AvatarIKHint.RightElbow);

        playerAnimator.SetIKPositionWeight (AvatarIKGoal.RightHand, 1.0f);
        playerAnimator.SetIKRotationWeight (AvatarIKGoal.RightHand, 1.0f);

        playerAnimator.SetIKPosition (AvatarIKGoal.RightHand, rightIKMount.position);
        playerAnimator.SetIKRotation (AvatarIKGoal.RightHand, rightIKMount.rotation);

        playerAnimator.SetIKPositionWeight (AvatarIKGoal.LeftHand, 1.0f);
        playerAnimator.SetIKRotationWeight (AvatarIKGoal.LeftHand, 1.0f);

        playerAnimator.SetIKPosition (AvatarIKGoal.LeftHand, leftIKMount.position);
        playerAnimator.SetIKRotation (AvatarIKGoal.LeftHand, leftIKMount.rotation);
    }

    public void UpdateUI () {
        if (gun != null) {
            ammoText.text = gun.magAmmo + "/" + gun.ammoRemain;
        }
    }
}