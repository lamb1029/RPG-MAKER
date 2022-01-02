using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tiled2Unity;

public class Area : MonoBehaviour
{
    public Vector2 center;
    public Vector2 size;
    public TiledMap TM;

    // Start is called before the first frame update
    void Start()
    {
        TM = GetComponent<TiledMap>();
        size = new Vector2(TM.MapWidthInPixels, TM.MapHeightInPixels);
        center = new Vector2(transform.position.x + (size.x / 2), transform.position.y - (size.y / 2));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, size);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
