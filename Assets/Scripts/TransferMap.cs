using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransferMap : MonoBehaviour
{
    public Transform target;
    public Area targetArea;

    private Player player;
    private CameraControll cam;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        cam = FindObjectOfType<CameraControll>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            cam.SetArea(targetArea);
            player.transform.position = target.transform.position;
        }
    }

}
