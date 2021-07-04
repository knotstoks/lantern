using UnityEngine;

public class CameraFollow : MonoBehaviour {
    private Transform playerTransform;
    private Player player;
    [SerializeField] private float cameraDistance;
    private float blackOutResetTime = 12f;
    private float blackOutTime;
    private bool inBlackOut;
    public bool blackOut; //Controls if blackouts occur
    private void Start() {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if ((int) DataStorage.saveValues["blackOut"] == 1) {
            blackOut = true;
        } else {
            blackOut = false;
        }
    }
    private void FixedUpdate() {
        if (!inBlackOut) {
            transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, transform.position.z);
        } else {
            transform.position = new Vector3(-500, -500, transform.position.z);
        }
    }
    private void Awake() {
        GetComponent<UnityEngine.Camera>().orthographicSize = ((Screen.height / 2) / cameraDistance);
    }
    private void Update() {
        if (blackOut && !player.inDialogue) {
            if (blackOutTime <= 0) {
                blackOutTime = blackOutResetTime;
            } else {
                blackOutTime -= Time.deltaTime;
            }
        }

        if (blackOutTime > 0 && blackOutTime <= 2) {
            inBlackOut = true;
        } else {
            inBlackOut = false;
        }
    }
}