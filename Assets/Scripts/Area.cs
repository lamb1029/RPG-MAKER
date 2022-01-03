using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tiled2Unity;

public class Area : MonoBehaviour
{
    public Vector2 _center;
    public Vector2 _size;
    public TiledMap TM;
    public BoxCollider2D box;
    public CameraControll cam;
    public Area area;

    private void Awake()
    {
        TM = GetComponent<TiledMap>();
        cam = FindObjectOfType<CameraControll>();
        area = GetComponent<Area>();
        box = GetComponent<BoxCollider2D>();
        if (box == null)
        {
            box = this.gameObject.AddComponent<BoxCollider2D>();

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _size = new Vector2(TM.MapWidthInPixels, TM.MapHeightInPixels);
        _center = new Vector2(transform.position.x + (_size.x / 2), transform.position.y - (_size.y / 2));
        SetBoxCollider2D(_size);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_center, _size);
    }

    void SetBoxCollider2D(Vector2 size)
    {
        box.size = size;
        box.offset = new Vector2((size.x / 2), -(size.y / 2));
        box.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            cam.SetArea(area);
        }
    }
}
