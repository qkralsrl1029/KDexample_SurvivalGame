using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{


    private const float WALKING_FIRE = 0.08f, STANDING_FIRE = 0.04f, CROUCHING_FIRE = 0.02f, FINESIGHT_FIRE = 0.001f;

    [SerializeField]
    private Animator animator;


    // 크로스헤어 상태에 따른 총의 정확도.
    private float gunAccuracy;


    // 크로스 헤어 비활성화를 위한 부모 객체.
    [SerializeField]
    private GameObject go_CrosshairHUD;
    [SerializeField]
    private GunController theGunController;

    public void WalkingAnimation(bool _flag)
    {
        WeaponManager.currentWeaponAnim.SetBool("Walk", _flag);
        animator.SetBool("isWalk", _flag);
    }

    public void RunningAnimation(bool _flag)
    {
        WeaponManager.currentWeaponAnim.SetBool("Run", _flag);
        animator.SetBool("isRun", _flag);
    }

    public void CrouchingAnimation(bool _flag)
    {
        animator.SetBool("isCrouch", _flag);
    }

    public void FineSightAnimation(bool _flag)
    {
        animator.SetBool("Finesight", _flag);
    }


    public void FireAnimation()
    {
        if (animator.GetBool("isWalk"))
            animator.SetTrigger("walkFire");
        else if (animator.GetBool("isCrouch"))
            animator.SetTrigger("crouchFire");
        else
            animator.SetTrigger("idleFire");
    }

    public float GetAccuracy()
    {
        if (animator.GetBool("isWalk"))
            gunAccuracy = 0.06f;
        else if (animator.GetBool("isCrouch"))
            gunAccuracy = 0.015f;
        else if (theGunController.GetFineSightMode())
            gunAccuracy = 0.001f;
        else
            gunAccuracy = 0.035f;

        return gunAccuracy;
    }

}