using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private const string IS_RUNNING = "IsRunning";
    private const string IS_DIE = "IsDie";
    private const string WITH_STICK = "WithStick";
    private const string TAKE_HIT = "TakeHit";

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();   
    }

    private void Start()
    {
        Player.Instance.OnPlayerDeath += Player_OnPlayerDeath;
        Player.Instance.OnTakeDamageAnimation += Player_OnTakeDamageAnimation;
        Player.Instance.OnTakeStick += Player_OnTakeStick;
    }

    private void Player_OnTakeDamageAnimation(object sender, System.EventArgs e)
    {
        animator.SetTrigger(TAKE_HIT);
    }

    private void Player_OnTakeStick(object sender, System.EventArgs e)
    {
        animator.SetBool(WITH_STICK, true);
    }

    private void Player_OnPlayerDeath(object sender, System.EventArgs e)
    {
        animator.SetBool(IS_DIE, true);
    }

    private void Update()
    {
        if (!PauseMenu.isPaused)
        {
            animator.SetBool(IS_RUNNING, Player.Instance.IsRunning());
            AdjustPlayerFacingDirection();
        }
    }

    private void AdjustPlayerFacingDirection() {
        if (Player.Instance.IsAlive())
        {
            Vector3 mousePos = GameInput.Instance.GetMousePosition();
            Vector3 playerPosition = GameInput.Instance.GetPlayerScreenPosition();

            if (mousePos.x > playerPosition.x)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }
        }
    }

    private void TriggerEndAttackingAnimation()
    {

    }
}
