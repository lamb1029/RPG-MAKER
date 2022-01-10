using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    private OrderManager theorder;
    private NumberSystem thenumber;

    public int correct;
    public bool flag;

    private void Start()
    {
        thenumber = FindObjectOfType<NumberSystem>();
        theorder = FindObjectOfType<OrderManager>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(!flag)
        {
            StartCoroutine(A());
        }
    }

    IEnumerator A()
    {
        flag = true;
        theorder.NotMove();
        thenumber.ShowNumber(correct);
        yield return new WaitUntil(() => !thenumber.activated);
        theorder.Move();
    }

}
