using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    [SerializeField]
    protected float speed;
    [SerializeField]
    protected int walkCount;
    protected Vector3 vector;
    protected int currentWalkCount;
    protected Animator animator;
    public LayerMask layerMask;
    protected bool NPCcanMove = true;

    protected void Move(string _dir, int _frequency)
    {
        StartCoroutine(MoveCoroutine(_dir, _frequency));
    }
    IEnumerator MoveCoroutine(string _dir, int _frequency)
    {
        NPCcanMove = false;
        vector.Set(0, 0, vector.z);

        switch (_dir)
        {
            case "UP":
                vector.y = 1f;
                break;
            case "DOWN":
                vector.y = -1f;
                break;
            case "RIGHT":
                vector.x = 1f;
                break;
            case "LEFT":
                vector.x = -1f;
                break;
        }

        animator.SetFloat("DirX", vector.x);
        animator.SetFloat("DirY", vector.y);
        animator.SetBool("Walking", true);

        while (currentWalkCount < walkCount)
        {
            transform.Translate(vector.x * speed, vector.y * speed, 0);
            currentWalkCount++;
            yield return new WaitForSeconds(0.01f);
        }
        currentWalkCount = 0;
        if(_frequency != 5)
            animator.SetBool("Walking", false);
        NPCcanMove = true;
    }

    protected bool CheckCollsion()
    {
        RaycastHit2D hit;
        Vector2 start = transform.position; //캐릭터의 현재 위치
        Vector2 end = start + new Vector2(vector.x * speed * walkCount, vector.y * speed * walkCount); // 캐릭터가 이동하고자 하는 위치
        hit = Physics2D.Linecast(start, end, layerMask);

        if (hit.transform != null)
            return true;
        return false;
    }
}

