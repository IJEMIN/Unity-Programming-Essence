using UnityEngine;

// 게임 점수를 증가시키는 아이템
public class CoinItem : Item {
    public int score = 200; // 증가할 점수

    public override void Use (GameObject target) {
        // 게임 매니저로 접근하여 점수를 추가한다
        GameManager.instance.AddScore (score);

        base.Use (target);
    }
}