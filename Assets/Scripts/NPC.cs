using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NPCmove
{
    [Tooltip("NPCMove를 체크하면 NPC가 움직임")]
    public bool NPCMove;

    public string[] dir; //npc가 움직일 방향 설정.

    [Range(1, 5)][Tooltip("1=천천히, 2=조금 천천히, 3=보통, 4=빠르게, 5 =연속적으로")]
    public int frequency; //npc속도
}


public class NPC : MovingObject
{
    [SerializeField]
    private NPCmove npc;

    // Start is called before the first frame update
    void Start()
    {
        box = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        queue = new Queue<string>();
        StartCoroutine(MoveCoroutine());
    }

    public void SetMove()
    {

    }

    public void SetNotMove()
    {
        StopAllCoroutines();
    }

    IEnumerator MoveCoroutine()
    {
        if(npc.dir.Length != 0)
        {
            for(int i = 0; i < npc.dir.Length; i++)
            {
                yield return new WaitUntil(() => queue.Count < 2);

                base.Move(npc.dir[i], npc.frequency);

                if (i == npc.dir.Length - 1)
                    i = -1;
            }
        }
    }
}
