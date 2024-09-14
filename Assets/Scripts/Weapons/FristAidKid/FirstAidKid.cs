using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAidKid : MonoBehaviour
{
    [SerializeField] public int AddHealth = 1;

    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out Player player))
        {
            if (player.GetMaxHP() != player.GetCurrnetHP())
            {
                player.AddHP(AddHealth);
                Destroy(this.gameObject);
            }
        }
    }
}
