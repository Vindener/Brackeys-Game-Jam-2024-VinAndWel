using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP_UI : MonoBehaviour
{
    [SerializeField] private Image HpPanel;
    public Sprite[] sprites;
    public int value;

    public GameObject gameoverMenu;
    private bool activeCor = false;

    void Update()
    {
        if (value >= 0 && value < sprites.Length)
        {
            HpPanel.sprite = sprites[Player.Instance.GetCurrnetHP()];

            bool isPlayerAlive = Player.Instance.IsAlive();
            if (!isPlayerAlive && !activeCor) 
            {
                activeCor = true;
                StartCoroutine(CheckAndActivate());
            }
        }
    }

    IEnumerator CheckAndActivate()
    {
        yield return new WaitForSeconds(2f);

        if (activeCor)
        {
            gameoverMenu.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
