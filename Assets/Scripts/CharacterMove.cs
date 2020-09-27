using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    Rigidbody2D rgb;

    public float speed;
    Animator anim;

    public GameObject knob;

    float holdMove = 0;
    float holdAtt = 0;

    private AudioSource playerAudio;
    public AudioClip[] clip;

    void Start()
    {
        rgb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
    }

    public Transform attackPos;
    public LayerMask enemyLayer;
    public float attackRange;

    void onDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    bool isArmored = false;
    public int enemyCount;

    void Update()
    {
        if (GameControl.instance.inGame)
        {
            if (isArmored)
            {
                Collider2D[] enemy = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemyLayer);
                if (Input.GetKey(KeyCode.Space) && holdAtt > 30)
                {
                    holdAtt = 0;
                    anim.SetTrigger("Attack");
                    playerAudio.PlayOneShot(clip[0]);
                    for (int i = 0; i < enemy.Length; i++)
                        enemy[i].GetComponent<EnemyMove>().Death(enemyCount--);
                }
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                transform.Translate(Vector3.up * Time.deltaTime * speed);
                if (holdMove > 17)
                {
                    playerAudio.PlayOneShot(clip[1]);
                    anim.SetTrigger("UpMove");
                    holdMove = 0;
                }
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                if (holdMove > 17)
                {
                    playerAudio.PlayOneShot(clip[1]);
                    anim.SetTrigger("DownMove");
                    holdMove = 0;
                }
                transform.Translate(Vector3.down * Time.deltaTime * speed);
            }
            else
            {
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    if (holdMove > 17)
                    {
                        playerAudio.PlayOneShot(clip[1]);
                        anim.SetTrigger("RightMove");
                        holdMove = 0;
                    }
                    transform.Translate(Vector3.right * Time.deltaTime * speed);
                }
                else if (Input.GetKey(KeyCode.LeftArrow))
                {
                    if (holdMove > 17)
                    {
                        playerAudio.PlayOneShot(clip[1]);
                        anim.SetTrigger("LeftMove");
                        holdMove = 0;
                    }
                    transform.Translate(Vector3.left * Time.deltaTime * speed);
                }
                else
                    rgb.velocity = new Vector2(0, 0);
            }
            holdMove += Time.deltaTime * 60;
            holdAtt += Time.deltaTime * 60;
        }
    }

    public void TakeKnob()
    {
        knob.gameObject.SetActive(true);
        isArmored = true;
    }

}
