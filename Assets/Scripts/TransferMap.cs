using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransferMap : MonoBehaviour
{
    public Transform target;
    public Area targetArea;

    private Player player;
    private CameraControll cam;
    private FadeManager theFade;
    OrderManager theOrder;

    public Animator anim_1;
    public Animator anim_2;

    public int door_count;

    [Tooltip("UP, DOWN, LEFR, RIGHT")]
    public string dir; //캐릭터가 바라보는 방향
    private Vector2 vector;

    [Tooltip("문이 있으면 true, 문이 없으면 false")]
    public bool door;


    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        cam = FindObjectOfType<CameraControll>();
        theFade = FindObjectOfType<FadeManager>();
        theOrder = FindObjectOfType<OrderManager>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!door)
        {
            if (collision.gameObject.name == "Player")
            {
                StartCoroutine(TransferCoroutine());
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(door)
        {
            if(collision.gameObject.name == "Player")
            {
                if(Input.GetKeyDown(KeyCode.Z))
                {
                    vector.Set(player.animator.GetFloat("DirX"), player.animator.GetFloat("DirY"));
                    switch(dir)
                    {
                        case "UP":
                            if(vector.y == 1f)
                                StartCoroutine(TransferCoroutine());
                            break;
                        case "DOWN":
                            if (vector.y == -1f)
                                StartCoroutine(TransferCoroutine());
                            break;
                        case "RIGHT":
                            if (vector.x == 1f)
                                StartCoroutine(TransferCoroutine());
                            break;
                        case "LEFT":
                            if (vector.x == -1f)
                                StartCoroutine(TransferCoroutine());
                            break;
                        default:
                            StartCoroutine(TransferCoroutine());
                            break;
                    }
                }
            }
        }
    }

    IEnumerator TransferCoroutine()
    {
        theOrder.NotMove();
        theFade.FadeOut();
        if(door)
        {
            anim_1.SetBool("Open", true);
            if (door_count == 2)
                anim_2.SetBool("Open", true);
        }
        yield return new WaitForSeconds(0.5f);

        //theOrder.
        if (door)
        {
            anim_1.SetBool("Open", false);
            if (door_count == 2)
                anim_2.SetBool("Open", false);
        }

        yield return new WaitForSeconds(0.5f);
        cam.SetArea(targetArea);
        player.transform.position = target.transform.position;
        theFade.FadeIn();
        yield return new WaitForSeconds(0.5f);
        theOrder.Move();
    }

}
