using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndMission : MonoBehaviour
{
    public int missionCount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == GameObject.FindGameObjectWithTag("Player"))
            GameControl.instance.CompleteMission(missionCount);
    }
}
