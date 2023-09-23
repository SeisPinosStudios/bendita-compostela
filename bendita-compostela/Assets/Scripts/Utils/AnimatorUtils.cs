using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorUtils : MonoBehaviour
{
    public static IEnumerator WaitAnimationLength(Animator animator, int layer) {
        var clip = animator.GetCurrentAnimatorClipInfo(layer)[0].clip;
        yield return new WaitForSeconds(clip.length);
    }

    public static float ClipLength(Animator animator, int layer) {
        return animator.GetCurrentAnimatorClipInfo(layer)[0].clip.length;
    }
}
