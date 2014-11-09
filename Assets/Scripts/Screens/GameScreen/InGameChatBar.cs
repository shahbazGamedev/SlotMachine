﻿using UnityEngine;
using System.Collections;
using Boomlagoon.JSON;
using Sfs2X.Requests;
using Sfs2X.Entities;
using Sfs2X.Entities.Data;

public class InGameChatBar : MonoBehaviour {
	// Ipad keyboard height: 634
	// Iphone keyboard height: 528
	// Editor: -534f
	public UITextList textList;
	public UIInput chatInput;
	public UIEventTriggerExtent bgEventListener;
	public UIEventTriggerExtent textListEventListener;
	public UISprite background;
	public UIAnchorExtent anchor;

	private bool isOpen = false;
	private Vector3 closePos;
	private Vector3 openPos = Vector3.zero;

	// TEST
	void Start() {
		Init();
	}

	public void Init() {
    EventDelegate.Set(chatInput.onSubmit, SendChat);
    EventDelegate.Set(bgEventListener.onClick, Open);
    EventDelegate.Set(textListEventListener.onClick, Open);
		anchor.Reset();
		closePos = transform.localPosition;
	}
	
	public void SendChat() {
    if (chatInput.value != string.Empty) {
      string escapedString = Utils.ChatEscape(chatInput.value);
      // DisplayBubbleChat(escapedString, currentScreen.FindUserSlot(AccountManager.Instance.username));
      JSONObject data = new JSONObject();
      data.Add("message", escapedString);
      data.Add("senderId", AccountManager.Instance.username);
      data.Add("senderName", AccountManager.Instance.displayName);
      SmartfoxClient.Instance.HandleServerRequest(CreatePublicMessageRequest(Command.USER.CHAT_IN_ROOM, string.Empty, data));
			AddChatToList(data);
      chatInput.value = string.Empty;
			chatInput.isSelected = true;		
    }
	}
	
	public void AddChatToList(JSONObject jsonData) {
		string message = jsonData.GetString("senderName") + ":" + Utils.ChatUnescape(jsonData.GetString("message"));
		textList.Add(message);
	}
	
	public void Open() {
		chatInput.isSelected = true;		
		// isOpen = true;
		#if UNITY_EDITOR
		if (openPos == Vector3.zero) {
			float pos = Mathf.Abs(transform.localPosition.y * 2) * 634f / 2048f;
			openPos = new Vector3(transform.localPosition.x, pos - Mathf.Abs(transform.localPosition.y) + 150f, transform.localPosition.z);
		}
		TweenPosition tween = TweenPosition.Begin(gameObject, 0.3f, openPos, false);
		#else
		// TweenPosition tween = TweenPosition.Begin(gameObject, 0.3f, new Vector3(transform.localPosition.x, transform.localPosition.y + TouchScreenKeyboard.area.height, 0), false);
		#endif 
		// static public TweenPosition Begin (GameObject go, float duration, Vector3 pos, bool worldSpace);
		
	}
	
	void Update() {
    #if UNITY_IPHONE || UNITY_ANDROID
			if (chatInput.isSelected && !isOpen && TouchScreenKeyboard.area.height > 0) {
				isOpen = true;
				if (openPos == Vector3.zero) {
					float pos = Mathf.Abs(transform.localPosition.y * 2) * TouchScreenKeyboard.area.height / Screen.height;
					openPos = new Vector3(transform.localPosition.x, pos - Mathf.Abs(transform.localPosition.y) + 150f, transform.localPosition.z);
				}
				Debug.Log("## " + openPos + " " + TouchScreenKeyboard.area);
				TweenPosition tween = TweenPosition.Begin(gameObject, 0.3f, openPos, false);
			}
			if (!chatInput.isSelected && isOpen) {
				isOpen = false;
				TweenPosition tween = TweenPosition.Begin(gameObject, 0.3f, closePos, false);
			}
		#endif 
	}
	
	public void Close() {
		isOpen = false;
	}
	
  private ISFSObject CreatePublicMessageObject(JSONObject jsonData, string commandId) {
    ISFSObject objOut = new SFSObject();
    objOut.PutByteArray("jsonData", Utils.ToByteArray(jsonData.ToString()));
    objOut.PutUtfString("message", jsonData.GetString("message"));
    objOut.PutUtfString("cmd", commandId);
    return objOut;
  }
  
  private ServerRequest CreatePublicMessageRequest(string commandId, string successCallback, JSONObject jsonData = null) {
   if (jsonData == null) {
     jsonData = new JSONObject();
   }
  
   ISFSObject requestData = CreatePublicMessageObject(jsonData, commandId);
   ServerRequest serverRequest = new ServerRequest(ServerRequest.Type.PUBLIC_MESSAGE,
                           Command.Create(GameId.USER, commandId),
                           requestData,
                           gameObject,
                           successCallback);
   return serverRequest;
  }
}
