using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NPCmove
{
    [Tooltip("NPCMove�� üũ�ϸ� NPC�� ������")]
    public bool NPCMove;

    public string[] dir; //npc�� ������ ���� ����.

    [Range(1, 5)][Tooltip("1=õõ��, 2=���� õõ��, 3=����, 4=������, 5 =����������")]
    public int frequency; //npc�ӵ�
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
