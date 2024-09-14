using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{

    public static ActiveWeapon Instance { get; private set; }

   // [SerializeField] private Sword sword;
    [SerializeField] private Stick_ sword;
    [SerializeField] private GameObject ActiveObject;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (!PauseMenu.isPaused && Player.Instance.IsAlive())
        {
            FollowMousePosition();
        }

        if (Player.Instance.GetInfoStick())
        {
            ActiveObject.SetActive(true);
        }
    }

    public Stick_ GetActiveWeapon()
    {
        return sword;
    }

    private void FollowMousePosition ()
    {
        Vector3 mousePos = GameInput.Instance.GetMousePosition();
        Vector3 playerPosition = GameInput.Instance.GetPlayerScreenPosition();

        if (mousePos.x < playerPosition.x)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }


}
