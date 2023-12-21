using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using DefaultNamespace.Utils;
using Elements;
using Elements.ElementsConfig;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    [SerializeField] private LevelGenerator levelGenerator;

    

    void Start()
    {
        levelGenerator.Generate();
    }
    
}
