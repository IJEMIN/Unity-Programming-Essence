using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    public Transform gunPivot;
    public Gun gun;
    private PlayerInput playerInput;
    private Animator playerAnimator;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {

        playerAnimator.SetBool("Fire", playerInput.fire);

        if (playerInput.fire)
        {
            if (gun.m_CurrentState == Gun.State.Empty)
            {
                playerAnimator.SetTrigger("Reload");
                gun.Reload();
            }
            else
            {
                gun.Fire();
            }
        }
    }

    private void OnAnimatorIK(int layerIndex)
    {


        gunPivot.position = playerAnimator.GetIKHintPosition(AvatarIKHint.RightElbow);
        // playerAnimator.SetLookAtWeight(1.0f);
        //    playerAnimator.SetLookAtPosition(transform.position + transform.forward * 100f);


        playerAnimator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1.0f);
        playerAnimator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1.0f);

        playerAnimator.SetIKPosition(AvatarIKGoal.RightHand, gun.rightHandle.position);
        playerAnimator.SetIKRotation(AvatarIKGoal.RightHand, gun.rightHandle.rotation);

        playerAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1.0f);
        playerAnimator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1.0f);

        playerAnimator.SetIKPosition(AvatarIKGoal.LeftHand, gun.leftHandle.position);
        playerAnimator.SetIKRotation(AvatarIKGoal.LeftHand, gun.leftHandle.rotation);
    }

}
