using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]
public class BlockClickable : BlockAbstract
{
    public BoxCollider2D _collider;

    protected void OnMouseUp()
    {
        GridManagerCtrl.Instance.SetNode(ctrl);
    }
}
