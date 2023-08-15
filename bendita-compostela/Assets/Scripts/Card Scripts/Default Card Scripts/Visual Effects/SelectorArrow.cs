using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorArrow : MonoBehaviour
{
    public static SelectorArrow Instance { get; private set; }
    [SerializeField] Vector3 from;
    [SerializeField] Vector3 to;
    [SerializeField] SpriteRenderer sprite;
    public bool showArrow;

    private void Awake()
    {
        from = transform.position;
        DragCardScript.onUsing += () => showArrow = true;
        DragCardScript.onReturning += () => showArrow = false;
    }

    private void Update()
    {
        if (showArrow)
        {
            to = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            to = new Vector3(to.x, to.y, 0.0f);
            sprite.enabled = true;

            //transform.position = from;
            transform.up = to - transform.position;

            var size = (from - to).magnitude;
            sprite.size = new Vector2(0.8f, size);
        }
        else
        {
            sprite.enabled = false;
        }
    }
}
