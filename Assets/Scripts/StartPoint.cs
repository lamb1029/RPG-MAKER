using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    public string startPoint;
    private Player player;
    //private CameraControll cam;
    //public Area area;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        //cam = FindObjectOfType<CameraControll>();

        if (startPoint == player.transferMapName)
        {
            player.transform.position = this.transform.position;
        }
        //cam.SetArea(area);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
