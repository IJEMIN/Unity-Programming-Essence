using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal {
    // 동물에 대한 변수
    public string name;
    public string sound;

    // 동물에 대한 메서드
    public void PlaySound() // 동물이 울음소리를 내는 기능
    {
        Debug.Log(name + " : " + sound);
    }
}