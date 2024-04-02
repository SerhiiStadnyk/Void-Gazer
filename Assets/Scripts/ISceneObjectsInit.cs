using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISceneObjectsInit
{
    public void InitObject(GameObject obj);

    public void DisposeObject(GameObject obj);
}
