using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Text info;

    public void Play()
    {
        GetComponent<Animator>().SetTrigger("Play");
        GetComponent<AudioSource>().Play();
    }
    public void HowToPlay()
    {
        GetComponent<Animator>().SetTrigger("HTP");
        GetComponent<AudioSource>().Play();
    }
    public void Back()
    {
        GetComponent<Animator>().SetTrigger("Back");
        GetComponent<AudioSource>().Play();
    }
    public void LevelPlay(int sceneIndex)
    {
        GetComponent<AudioSource>().Play();
        if (sceneIndex == 0)
            info.text = "Coming Soon";
        else if(sceneIndex - 1 <= PlayerPrefs.GetInt("Level", 0))
            StartCoroutine(LoadLevel(sceneIndex));
    }
    IEnumerator LoadLevel(int sceneIndex)
    {
        info.text = "Loading Level";
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(sceneIndex);
    }
}
