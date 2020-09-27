using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMission : MonoBehaviour
{
    public int missionCount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == GameObject.FindGameObjectWithTag("Player"))
        {
            if (GameControl.instance.missionCheck(missionCount))
                Destroy(this.gameObject);
        }
    }
}
