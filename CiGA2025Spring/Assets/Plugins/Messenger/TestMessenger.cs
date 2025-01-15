using UnityEngine;

public class TestMessenger : MonoBehaviour
{
    void Start()
    {
        Messenger.AddListener(MsgType.SimpleEvent, MySimpleCallback);
        Messenger.AddListener<int>(MsgType.IntEvent, MyIntCallback);

        Messenger.Broadcast(MsgType.SimpleEvent);
        Messenger.Broadcast<int>(MsgType.IntEvent, 123);
    }

    void MySimpleCallback() {
        Debug.Log("Simple event triggered");
    }

    void MyIntCallback(int value) {
        Debug.Log("Event with int triggered: " + value);
    }

    void Remove()
    {
        Messenger.RemoveListener(MsgType.SimpleEvent, MySimpleCallback);
        Messenger.RemoveListener<int>(MsgType.IntEvent, MyIntCallback);
    }
}