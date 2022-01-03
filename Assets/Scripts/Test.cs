using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TestMove
{
    public string name;
    public string dir;
}

public class Test : MonoBehaviour
{
    //public TestMove[] move;
    public string dir;
    private OrderManager theOrder;

    // Start is called before the first frame update
    void Start()
    {
        theOrder = FindObjectOfType<OrderManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.gameObject.name == "Player")
        {
            theOrder.PreLoadCharacter();
            //for(int i = 0; i < move.Length; i++)
            //{
            //    theOrder.Move(move[i].name, move[i].dir);
            //}
            theOrder.Turn("NPC1", dir);
        }
    }
}
