using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinItem : Item {

    public int score = 200;

    public override void Use (GameObject target) {

        GameManager.instance.AddScore (score);

        base.Use (target);
    }

}