using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SceneManger : MonoBehaviour {
    [SerializeField] protected string reference;
    protected int referenceInt;

    private void Start() {
        referenceInt = (int) DataStorage.saveValues["reference"];
    }
}
