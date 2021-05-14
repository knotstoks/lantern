using UnityEngine;

public abstract class ManageScene : MonoBehaviour {
    public int facingDirection;
    protected string reference;
    protected int referenceInt;
    private void Start() {
        referenceInt = (int) DataStorage.saveValues[reference];
    }
}
