using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VfxService:MonoBehaviour
{
    [SerializeField] private VfxView _vfxView;
    [SerializeField] private VfxSO _vfxSO;

    private VfxController _vfxController;

    public VfxController GetVfxController() => _vfxController;

    public VfxService()
    {
        _vfxController = new VfxController(_vfxView, _vfxSO);
    }
}
