﻿using UnityEngine;
using System.Collections;

public class Popup : MonoBehaviour {
  
  public enum Type {
		POPUP_RESULT,
		POPUP_LEAVE_GAME,
		POPUP_CREATE_ROOM,
		POPUP_USER_INFO,
		POPUP_FRIENDS,
		POPUP_SLOT_MACHINE,
		POPUP_INVITE_TO_GAME
  }
  
  private Vector3 maxScale = new Vector3(1f, 1f, 1.0f);
  private Vector3 minScale = new Vector3(0.5f, 0.5f, 1.0f);
  private bool isAnimating = false;
	private float animateTime = 0.4f;
  
  public UIPanel panel;
  public UIButton btnClose;
  public Type type;
  
  public virtual void Init() {
    BaseInit();
  }
  
  public virtual void Init(object[] data) {
    BaseInit();
  }
  
  public virtual void BaseInit() {
    EventDelegate.Set(btnClose.onClick, Close);
  }
  
  public virtual void OpenPopupNoAnimation() {
    gameObject.SetActive(true);
    PopupManager.Instance.ShowDim();
    HandleOpenPopupCallback();
  }
  
  public virtual void Open() {
    if (!isAnimating) {
      if (panel.alpha > 0) {
  			panel.alpha = 0;
  		}
  		transform.localScale = minScale;
  		gameObject.SetActive(true);
  		RunBeforeOpen();
  		BounceUpAnimation();
  		PopupManager.Instance.ShowDim();
  		isAnimating = true;
  		Global.isAnimatingPopup = true;
    }
  }
  
  public virtual void RunBeforeOpen() {}
  
  void BounceUpAnimation() {
	  LeanTween.cancel(gameObject);
    // transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
		LeanTween.scale(gameObject, maxScale, animateTime).setEase(LeanTweenType.easeOutBack).setOnComplete(HandleOpenPopupCallback);
		LeanTween.value(panel.gameObject, DoUpdatePanelAlpha, panel.alpha, 1, animateTime);
	}
  
  void DoUpdatePanelAlpha(float updateVal) {
		panel.alpha = updateVal;
	}
	 
  void BounceDownAnimation() {
		LeanTween.scale(gameObject, minScale, animateTime).setEase(LeanTweenType.easeInBack).setOnComplete(HandleClosePopupCallback);
		LeanTween.value(panel.gameObject, DoUpdatePanelAlpha, panel.alpha, 0, animateTime);
	}
  
  public virtual void HandleOpenPopupCallback() {
    isAnimating = false;
    Global.isAnimatingPopup = false;
  }
  
  public virtual void HandleClosePopupCallback() {
    isAnimating = false;
    Global.isAnimatingPopup = false;
    PopupManager.Instance.ClosePopup(this);
    Destroy(gameObject);
  }
  
  public virtual void Close() {
    if (!isAnimating) {
      isAnimating = true;
      Global.isAnimatingPopup = true;
      BounceDownAnimation();
      PopupManager.Instance.HideDim();
    }
  }
  
  public virtual void CloseNoAnimation() {
    HandleClosePopupCallback();
    PopupManager.Instance.HideDimNoAnimation();
  }
}
