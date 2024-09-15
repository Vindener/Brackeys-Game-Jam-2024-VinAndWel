using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public GameObject EndPanel;
    public int waitTime = 5;


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out Player player))
        {
            EndPanel.SetActive(true);
            StartCoroutine(GoToMenu());

        }
    }

    IEnumerator GoToMenu()
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(0);
    }
}
