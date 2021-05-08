using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriestOffice : MonoBehaviour {
    private int progress = (int) DataStorage.saveValues["progress"];
    [SerializeField] private GameObject headPriest;
    void Start() {
        
    }
}
