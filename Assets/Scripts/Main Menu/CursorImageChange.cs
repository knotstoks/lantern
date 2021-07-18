using UnityEngine;

public class CursorImageChange : MonoBehaviour {
    public Texture2D cursor0, cursor1;
    private void Start () {
        Cursor.visible = true;
    }
    private void OnMouseOver() {
        Cursor.SetCursor(cursor1, Vector2.zero, CursorMode.Auto);
    }
    private void OnMouseExit() {
        Cursor.SetCursor(cursor0, Vector2.zero,CursorMode.Auto);
    }
}