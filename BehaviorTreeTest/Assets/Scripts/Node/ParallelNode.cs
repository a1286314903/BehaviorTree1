﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ParallelNode : ControllerNode
{
    private int nPlayNum;

    public ParallelNode() { }

    public ParallelNode(TreeNode Parent, int Priority) { NodeType = NodeType.ParallelNode; this.Priority = Priority; }


    public override int IsPlay()
    {
        return nPlayNum;
    }

    public override bool Play(bool IsOverride = false)
    {
        if (Child != null && CheckSelf())
        {
            for (int i = 0; i < Child.Count; i++)
            {
                mIsPlay = true;
                Child[i].Play();
            }
            return mIsPlay;
        }
        return false;
    }

    public override void Stop()
    {
        base.Stop();
        nPlayNum = 0;
        mIsPlay = false;
    }
}
