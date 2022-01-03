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

    //�ڽ��ݶ��̴� ������ �ִ� �ּ� ��
    //public Vector3 minBound;
    //public Vector3 maxBound;

    //ī�޶��� �ݳʺ�, �ݳ��� ���� ���� ����
    public float halfWidth;
    public float halfHeight;

    //ī�޶��� ������ ���� ���Ҷ� �̿��� ����
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
