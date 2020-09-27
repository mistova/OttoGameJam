using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelActivater : MonoBehaviour
{
    public int sceneIndex;
    public GameObject lck;

    void Start()
    {
        if (sceneIndex - 1 <= PlayerPrefs.GetInt("Level", 0))
            lck.gameObject.SetActive(false);
    }
}
