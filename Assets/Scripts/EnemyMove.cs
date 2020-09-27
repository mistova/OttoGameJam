using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float speed;

    Transform enemySpawn;
    public GameObject enemy;

    Rigidbody2D rb2d;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == GameObject.FindGameObjectWithTag("Player"))
        {
            GameControl.instance.LevelFailed();
            rb2d.velocity = new Vector2(0, 0);
            GetComponent<Animator>().SetTrigger("EnemyAttack");
        }
    }
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        enemySpawn = GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<Transform>();
        rb2d.velocity = new Vector2(0, speed);
    }

    public void Death(int i)
    {
        if (i > 0)
        {
            Vector3 vector3 = new Vector3(enemySpawn.position.x, enemySpawn.position.y, 0);
            Instantiate(enemy, vector3, enemySpawn.rotation);
        }
        else
            GameControl.instance.CompleteMission(GameControl.instance.missions.Length - 1);
        Destroy(this.gameObject);
    }
}
