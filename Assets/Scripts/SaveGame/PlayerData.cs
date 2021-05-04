public class PlayerData {
    public int maxHealth;
    public float[] position;

    public PlayerData(Player player) {
        this.maxHealth = player.getMaxHP();
        this.position = new float[2];
        this.position[0] = player.transform.position.x;
        this.position[1] = player.transform.position.y;
    }
}