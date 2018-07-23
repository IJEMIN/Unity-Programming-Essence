using UnityEngine;

public abstract class Item : MonoBehaviour {

    public void Start () {
        Destroy (gameObject, 5f);
    }

    public virtual void Use (GameObject target) {
        Destroy (gameObject);
    }
}