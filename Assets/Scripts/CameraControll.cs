using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
    static public CameraControll instance;

    [SerializeField]
    Player player;

    //public BoxCollider2D bound;
    public Area area;

    //박스콜라이더 영역의 최대 최소 값
    //public Vector3 minBound;
    //public Vector3 maxBound;

    //카메라의 반너비, 반높이 값을 지닐 변수
    public float halfWidth;
    public float halfHeight;

    //카메라의 반폰이 값을 구할때 이용할 변수
    //public Camera theCamera;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            player = FindObjectOfType<Player>();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        //theCamera = GetComponent<Camera>();
        //minBound = bound.bounds.min;
        //maxBound = bound.bounds.max;
        halfHeight = Camera.main.orthographicSize;
        halfWidth = halfHeight * Screen.width / Screen.height;
    }

    void LateUpdate()
    {
        float lx = area._size.x * 0.5f - halfWidth;
        float clampX = Mathf.Clamp(player.transform.position.x, -lx + area._center.x, lx + area._center.x);

        float ly = area._size.y * 0.5f - halfHeight;
        float clampY = Mathf.Clamp(player.transform.position.y, -ly + area._center.y, ly + area._center.y);
        //transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);

        //float clampX = Mathf.Clamp(player.transform.position.x, minBound.x + halfWidth, maxBound.x - halfWidth);
        //float clampY = Mathf.Clamp(player.transform.position.y, minBound.x + halfHeight, maxBound.x - halfHeight);

        transform.position = new Vector3(clampX, clampY, transform.position.z);
    }

    public void SetArea(Area newArea)
    {
        area = newArea;
    }
}
