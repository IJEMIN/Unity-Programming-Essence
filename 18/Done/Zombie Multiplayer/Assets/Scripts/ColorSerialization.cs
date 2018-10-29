using UnityEngine;
using WebSocketSharp;

public class ColorSerialization {
    public static byte[] SerializeColor(object targetObject) {
        Color color = (Color) targetObject;

        return color.ToByteArray(ByteOrder.Big);
    }

    public static object DeserializeColor(byte[] bytes) {
        Color color = Ext.To<Color>(bytes, ByteOrder.Big);

        return color;
    }
}