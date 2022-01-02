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
        animator = GetComponent<Animator>();
        StartCoroutine("MoveCoroutine");
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
                 switch(npc.frequency)
                {
                    case 1:
                        yield return new WaitForSeconds(4f);
                        break;
                    case 2:
                        yield return new WaitForSeconds(3f);
                        break;
                    case 3:
                        yield return new WaitForSeconds(2f);
                        break;
                    case 4:
                        yield return new WaitForSeconds(1f);
                        break;
                    case 5:
                        break;
                }

                yield return new WaitUntil(() => NPCcanMove);
                base.Move(npc.dir[i], npc.frequency);

                if (i == npc.dir.Length - 1)
                    i = -1;
            }
        }
    }
}
