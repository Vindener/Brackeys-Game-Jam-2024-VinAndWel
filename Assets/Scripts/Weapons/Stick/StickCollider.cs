using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickCollider : MonoBehaviour
{
    public GameObject InventoryUI;  

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out Player player))
        {
            if (!Player.Instance.GetInfoStick())
            {
                InventoryUI.SetActive(true);
                Player.Instance.TakeStick();
                this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }
}
