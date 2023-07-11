using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockAbstract : SaiMonoBehaviour
{
    public BlockCtrl ctrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadModel();
    }

    protected virtual void LoadModel()
    {
        if (ctrl != null) return;
        ctrl = transform.parent.GetComponent<BlockCtrl>();
        Debug.Log(transform.name + "LoadCtrl", gameObject);
    }
}
