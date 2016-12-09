using UnityEngine;
using System.Collections;

public class AnimationController : Entity {

    Animator animator;

	void Start () {
        animator = this.GetComponent<Animator>();
    }

	void Update () {
        animator.SetBool("isAttack", false);
    }

    public void Attack () {
        animator.SetBool("isAttack", true);
    }
}
