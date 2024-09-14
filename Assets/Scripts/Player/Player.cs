using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    public event EventHandler OnPlayerDeath;
    public event EventHandler OnTakeStick;
    public event EventHandler OnTakeDamageAnimation;

    [SerializeField] private float movingSpeed = 10f;
    [SerializeField] private int maxHealth = 10;
    [SerializeField] private float damagerecoveryTime = 1.5f;
    Vector2 inputVector;

    private Rigidbody2D rb;
    private KnockBack knockback;

    private float minMovingSpeed = 0.1f;
    private bool isRunning = false;

    private int currentHealth;
    private bool canTakeDamage;

    private bool isAlive = true;

    public Inventory inventory;

    public bool WithStick =false;


    private void Awake()
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
        knockback = GetComponent<KnockBack>();

    }

    private void Start()
    {
        GameInput.Instance.OnPlayerAttack += Player_OnPlayerAttack;
        canTakeDamage = true;
        currentHealth = maxHealth;
    }

    private void Player_OnPlayerAttack(object sender, System.EventArgs e)
    {
        ActiveWeapon.Instance.GetActiveWeapon().Attack();
    }

    private void Update()
    {
        if (!PauseMenu.isPaused)
        {
            inputVector = GameInput.Instance.GetMovementVector();
        }

    }

    private IEnumerator DamageRecoveryRoutine()
    {
        yield return new WaitForSeconds(damagerecoveryTime);
        canTakeDamage = true;

    }

    private void FixedUpdate()
    {
        if (knockback.IsGettingBack)
            return;

        HandelMovement();
    }

    public void TakeDamage(Transform damageTransform, int damage)
    {
        if (canTakeDamage && isAlive)
        {
            canTakeDamage = false;
            currentHealth = Mathf.Max(0, currentHealth -= damage);
            Debug.Log(currentHealth);

            OnTakeDamageAnimation?.Invoke(this, EventArgs.Empty);

            knockback.GetKnockedBack(damageTransform);

            StartCoroutine(DamageRecoveryRoutine());
        }
        DetectDeath();
    }

    private void DetectDeath()
    {
        if(currentHealth == 0)
        {
            isAlive = false;
            //Destroy(this.gameObject);
            knockback.StopKnockMovement();
            GameInput.Instance.DisableMovement();

            OnPlayerDeath?.Invoke(this, EventArgs.Empty);
        }
    }

    public void TakeStick() { if (!WithStick) { OnTakeStick?.Invoke(this, EventArgs.Empty);  WithStick = true; } }

    public bool GetInfoStick() { return WithStick; }

    public bool IsAlive() { return isAlive; }
    public int GetCurrnetHP() { return currentHealth; }
    public int GetMaxHP() { return maxHealth; }

    public void AddHP(int amount)
    {
        currentHealth += amount;
    }

    private void HandelMovement() {
        rb.MovePosition(rb.position + inputVector * (movingSpeed * Time.fixedDeltaTime));
        if (Mathf.Abs(inputVector.x) > minMovingSpeed || Mathf.Abs(inputVector.y) > minMovingSpeed)
        {
            isRunning = true;
        }
        else {
            isRunning = false;
        }
    }

    public bool IsRunning() {
        return isRunning;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        IInventoryItem item = hit.collider.GetComponent<IInventoryItem>();
        if(item != null)
        {
            inventory.AddItem(item);
        }
    }
}
