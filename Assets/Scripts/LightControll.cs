using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightControll : MonoBehaviour
{
    Player thePlayer;
    Vector2 vector;

    Quaternion rotation;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        vector.Set(thePlayer.animator.GetFloat("DirX"), thePlayer.animator.GetFloat("DirY"));

        if(vector.x == 1f)
        {
            rotation = Quaternion.Euler(0, 0, 90);
            transform.rotation = rotation;
        }
        else if(vector.x == -1f)
        {
            rotation = Quaternion.Euler(0, 0, -90);
            transform.rotation = rotation;
        }
        else if (vector.y == 1f)
        {
            rotation = Quaternion.Euler(0, 0, 180);
            transform.rotation = rotation;
        }
        else if (vector.y == -1f)
        {
            rotation = Quaternion.Euler(0, 0, 0);
            transform.rotation = rotation;
        }
    }

    private void LateUpdate()
    {
        transform.position = thePlayer.transform.position;
    }
}
