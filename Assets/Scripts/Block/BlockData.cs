using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BlockData : BlockAbstract
{
    
    public Node node;
    public TextMeshPro text;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadTextMeshPro();
    }

    protected virtual void LoadTextMeshPro()
    {
        if (text != null) return;
        text = transform.GetComponentInChildren<TextMeshPro>();
        Debug.Log(transform.name + "LoadTextMeshPro", gameObject);
    }
    public virtual void SetNode(Node node)
    {
        this.node = node;
        text.text = node.x.ToString() + "\n" + node.y.ToString();
    }

    public virtual void SetSprite(Sprite sprite)
    {
        ctrl.sprite.sprite = sprite;
    }
}
