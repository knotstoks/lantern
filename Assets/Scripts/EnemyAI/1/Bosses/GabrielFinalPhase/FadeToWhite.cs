using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeToWhite : MonoBehaviour {
    [SerializeField] private Image imageWhite;
    public IEnumerator FadeNow() {
        imageWhite.color = new Color(255, 255, 255, 0.04f);
        yield return new WaitForSeconds(0.06f);
        imageWhite.color = new Color(255, 255, 255, 0.08f);
        yield return new WaitForSeconds(0.06f);
        imageWhite.color = new Color(255, 255, 255, 0.12f);
        yield return new WaitForSeconds(0.06f);
        imageWhite.color = new Color(255, 255, 255, 0.16f);
        yield return new WaitForSeconds(0.06f);
        imageWhite.color = new Color(255, 255, 255, 0.2f);
        yield return new WaitForSeconds(0.06f);
        imageWhite.color = new Color(255, 255, 255, 0.24f);
        yield return new WaitForSeconds(0.06f);
        imageWhite.color = new Color(255, 255, 255, 0.28f);
        yield return new WaitForSeconds(0.06f);
        imageWhite.color = new Color(255, 255, 255, 0.32f);
        yield return new WaitForSeconds(0.06f);
        imageWhite.color = new Color(255, 255, 255, 0.36f);
        yield return new WaitForSeconds(0.06f);
        imageWhite.color = new Color(255, 255, 255, 0.4f);
        yield return new WaitForSeconds(0.06f);
        imageWhite.color = new Color(255, 255, 255, 0.44f);
        yield return new WaitForSeconds(0.06f);
        imageWhite.color = new Color(255, 255, 255, 0.48f);
        yield return new WaitForSeconds(0.06f);
        imageWhite.color = new Color(255, 255, 255, 0.52f);
        yield return new WaitForSeconds(0.06f);
        imageWhite.color = new Color(255, 255, 255, 0.56f);
        yield return new WaitForSeconds(0.06f);
        imageWhite.color = new Color(255, 255, 255, 0.6f);
        yield return new WaitForSeconds(0.06f);
        imageWhite.color = new Color(255, 255, 255, 0.64f);
        yield return new WaitForSeconds(0.06f);
        imageWhite.color = new Color(255, 255, 255, 0.68f);
        yield return new WaitForSeconds(0.06f);
        imageWhite.color = new Color(255, 255, 255, 0.72f);
        yield return new WaitForSeconds(0.06f);
        imageWhite.color = new Color(255, 255, 255, 0.76f);
        yield return new WaitForSeconds(0.06f);
        imageWhite.color = new Color(255, 255, 255, 0.8f);
        yield return new WaitForSeconds(0.06f);
        imageWhite.color = new Color(255, 255, 255, 0.84f);
        yield return new WaitForSeconds(0.06f);
        imageWhite.color = new Color(255, 255, 255, 0.88f);
        yield return new WaitForSeconds(0.06f);
        imageWhite.color = new Color(255, 255, 255, 0.92f);
        yield return new WaitForSeconds(0.06f);
        imageWhite.color = new Color(255, 255, 255, 0.96f);
        yield return new WaitForSeconds(0.06f);
        imageWhite.color = new Color(255, 255, 255, 1f);
    }
}
