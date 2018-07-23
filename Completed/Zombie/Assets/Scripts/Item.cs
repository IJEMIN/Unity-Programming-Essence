using UnityEngine;

// 게임 아이템들은 반드시 Item을 상속해서 구현되야 한다
// Item은 추상 클래스이므로 상속없이 Item 자체로 사용할순 없다
public abstract class Item : MonoBehaviour {

    public void Start () {
        // 아이템은 생성되고 5초 뒤에 파괴된다
        Destroy (gameObject, 5f);
    }

    public virtual void Use (GameObject target) {
        // 게임 아이템들은 Use 메서드를 오버라이드하여 자신만의 기능을 구현해야 한다
        // 입력으로는 아이템을 사용하려는 게임 오브젝트가 온다

        // 아이템은 사용된 순간 파괴된다
        Destroy (gameObject);
    }
}