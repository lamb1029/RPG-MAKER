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
        animator = GetComponent<Animator>();
        StartCoroutine("MoveCoroutine");
        layerMask = (1 << 8) | (1 << 10);
    }

    public void SetMove()
    {

    }

    public void SetNotMove()
    {

    }

    IEnumerator MoveCoroutine()
    {
        if(npc.dir.Length != 0)
        {
            for(int i = 0; i < npc.dir.Length; i++)
            {
                 

                yield return new WaitUntil(() => NPCcanMove);
                base.Move(npc.dir[i], npc.frequency, layerMask);

                if (i == npc.dir.Length - 1)
                    i = -1;
            }
        }
    }
}
