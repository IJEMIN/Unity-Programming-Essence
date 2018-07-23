using UnityEngine;

public class Rotator : MonoBehaviour {
    public float rotationSpeed = 60f;

    private void Update () {
        transform.Rotate (0f, rotationSpeed * Time.deltaTime, 0f);
    }
}