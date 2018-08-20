using UnityEngine;

// 게임 아이템들은 반드시 Item을 상속해서 구현되야 한다
// Item은 추상 클래스이므로 상속없이 Item 자체로 사용할순 없다
public interface IItem {
    void Use(GameObject target);
}