using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class VictoryPlatform : MonoBehaviour
{
    public TextMeshProUGUI victoryText;
    public string sceneToLoad = "Stage1";

    private void Start()
    {
        victoryText.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(HandleVictory());
        }
    }

    private IEnumerator HandleVictory()
    {

        victoryText.color = Color.green;
        victoryText.text = "Victory";
        victoryText.gameObject.SetActive(true);

        yield return new WaitForSeconds(2);

        SceneManager.LoadScene(sceneToLoad);
    }
}