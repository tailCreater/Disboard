using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatController : MonoBehaviour
{
    private UIInput input;
    private UITextList chatContext;

    void Awake()
    {
        chatContext = GameObject.Find("UI Root/FunctionBar/Chat/Label").GetComponent<UITextList>();
        input = this.GetComponent<UIInput>();
    }
    public void GetChatContext()
    {
        string chatMessage = input.value;
        chatContext.Add(PlayerStatus.instance.name + ":" + chatMessage);
        input.value = "";
    }
}
