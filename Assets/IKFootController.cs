using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKFootController : MonoBehaviour
{
    
    Animator animator;

    public LayerMask playerLayer;

    [Range(0f, 1f)]
    public float distanceToGround;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {

    }
    private void OnAnimatorIK(int layerIndex)
    {
        if (animator)
        {
            animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1f);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1f);

            RaycastHit hit;
            Ray ray = new Ray(animator.GetIKPosition(AvatarIKGoal.LeftFoot) + Vector3.up, Vector3.down);
            if (Physics.Raycast(ray, out hit, distanceToGround + 1f, playerLayer))
            {
                if (hit.transform.tag == "Floor")
                {
                    Vector3 footPos = hit.point;
                    footPos.y += distanceToGround;
                    animator.SetIKPosition(AvatarIKGoal.LeftFoot, footPos);
                    animator.SetIKRotation(AvatarIKGoal.LeftFoot, Quaternion.LookRotation(transform.forward, hit.normal));
                }
            }

            animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1f);
            animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1f);

            ray = new Ray(animator.GetIKPosition(AvatarIKGoal.RightFoot) + Vector3.up, Vector3.down);
            if (Physics.Raycast(ray, out hit, distanceToGround + 1f, playerLayer))
            {
                if (hit.transform.tag == "Floor")
                {
                    Vector3 footPos = hit.point;
                    footPos.y += distanceToGround;
                    animator.SetIKPosition(AvatarIKGoal.RightFoot, footPos);
                    animator.SetIKRotation(AvatarIKGoal.RightFoot, Quaternion.LookRotation(transform.forward, hit.normal));
                }
            }
        }
    }
}
