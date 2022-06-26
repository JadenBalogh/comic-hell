using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Actor Source { get; private set; }
    public int ActionIndex { get; private set; }
    public Rigidbody2D Rigidbody2D { get; private set; }

    private void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void SetSource(Actor source)
    {
        Source = source;
    }

    public void SetActionIndex(int index)
    {
        ActionIndex = index;
    }
}
