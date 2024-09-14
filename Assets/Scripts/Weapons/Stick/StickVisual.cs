using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickVisual : MonoBehaviour
{
    public Animator animator;
    public Animator animator1;
    private const string ATTACK = "Attack";

    [SerializeField] private Stick_ stick;


    private void Start()
    {
        stick.OnStickSwing += Sword_OnSwordSwing;
    }


    private void Sword_OnSwordSwing(object sender, System.EventArgs e)
    {
        animator.SetTrigger(ATTACK);
        animator1.SetTrigger(ATTACK);
    }

    public void TriggerEndAttackingAnimation()
    {
        stick.AttackColliderTurnOff();
    }
}
