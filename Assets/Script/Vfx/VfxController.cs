using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VfxController
{
    private VfxView _vfxView;
    private VfxSO _vfxSO;

    public VfxController(VfxView vfxView, VfxSO vfxSO)
    {
        _vfxView = vfxView;
        _vfxSO = vfxSO;
    }
}
