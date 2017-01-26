using UnityEngine;
using System.Collections;

public class AnimationController : Entity {

    private bool isAttack = false;

    private Animator animator;

	void Start () {
        animator = this.GetComponent<Animator>();
    }

	void Update () {
        if (isAttack) {
            animator.SetBool("isAttack", true);
            isAttack = false;
        }
        else {
            animator.SetBool("isAttack", false);
            isAttack = false;
        }
    }

    public void Attack () {
        isAttack = true;
    }
}
