using UnityEngine;

// 게임 아이템들은 반드시 Item을 상속해서 구현되야 한다
// Item은 추상 클래스이므로 상속없이 Item 자체로 사용할순 없다
public interface IItem {
    // 게임 아이템들은 Use 메서드를 오버라이드하여 자신만의 기능을 구현해야 한다
    // 입력으로 자신을 사용하려는 게임 오브젝트를 전달 받는다
    void Use(GameObject target);
}