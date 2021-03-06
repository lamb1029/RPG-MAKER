using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MovingObject
{
    static public Player instance;
    public string transferMapName;

    public string walkSound_1;

    [SerializeField]
    private float runSpeed;
    private float applyRunSpeed;

    private bool canMove = true;
    private bool running = false;

    public bool notMove = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    void Start()
    {
        queue = new Queue<string>();
        box = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        theAudio = FindObjectOfType<AudioManager>();
    }

    void Update()
    {
        if (canMove && !notMove)
        {
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                canMove = false;
                StartCoroutine("MoveCoroutine");
            }
        }
    }

    void LateUpdate()
    {
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }

    IEnumerator MoveCoroutine()
    {
        while (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0 && !notMove)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                applyRunSpeed = runSpeed;
                running = true;
            }
            else
            {
                applyRunSpeed = 0;
                running = false;
            }


            vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);
            if (vector.x != 0)
                vector.y = 0;


            animator.SetFloat("DirX", vector.x);
            animator.SetFloat("DirY", vector.y);

            bool checkCollsionFlag = base.CheckCollsion();
            if (checkCollsionFlag)
                break;

            animator.SetBool("Walking", true);

            int temp = Random.Range(1, 2);
            switch (temp)
            {
                case 1:
                    theAudio.Play(walkSound_1);
                    break;
                case 2:
                    theAudio.Play(walkSound_1);
                    break;
            }

            box.offset = new Vector2(vector.x * 0.7f * speed * walkCount, vector.y * 0.7f * speed * walkCount);

            while (currentWalkCount < walkCount)
            {
                transform.Translate(vector.x * (speed + applyRunSpeed), vector.y * (speed + applyRunSpeed), 0);
                if (running)
                    currentWalkCount++;
                currentWalkCount++;
                if (currentWalkCount == 12)
                    box.offset = Vector2.zero;
                yield return new WaitForSeconds(0.01f);
            }
            currentWalkCount = 0;
        }
        animator.SetBool("Walking", false);
        canMove = true;
    }
}
