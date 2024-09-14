using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class KnockBack : MonoBehaviour
{
    [SerializeField] private float knockBackForce = 3f;
    [SerializeField] private float knockBackMovingTimerMax=0.3f;

    private float knockBackMovingTimer;

    private Rigidbody2D rb;
    public bool IsGettingBack { get; private set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        knockBackMovingTimer -= Time.deltaTime;
        if(knockBackMovingTimer < 0)
        {
            StopKnockMovement();
        }
    }

    public void GetKnockedBack(Transform damageSource)
    {
        IsGettingBack = true;
        knockBackMovingTimer = knockBackMovingTimerMax;
        Vector2 difference = (transform.position - damageSource.position).normalized * knockBackForce / rb.mass;
        rb.AddForce(difference, ForceMode2D.Force);
    }

    public void StopKnockMovement()
    {
        rb.velocity = Vector2.zero;
        IsGettingBack = false;
    }

}
