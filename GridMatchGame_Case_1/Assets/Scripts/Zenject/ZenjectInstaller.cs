using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ZenjectInstaller : MonoInstaller
{
    [SerializeField] private CameraManager cameraManager;
    //[SerializeField] private GridManager gridManager;
    public override void InstallBindings()
    {
        Container.BindInstances(cameraManager);
        //Container.BindInstances(gridManager);
    }

}
