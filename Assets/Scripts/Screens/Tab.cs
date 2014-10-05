﻿using UnityEngine;
using System.Collections;

public class Tab : MonoBehaviour {

  public enum TabType {
    NULL = -1,
		LOGIN_TAB,
		REGISTER_TAB,
		POLICY_TAB,
		FRIEND_LIST_TAB
  }

  public TabType type;

  public virtual void Init() {}
  
  public virtual void Open() {
    gameObject.SetActive(true);
  }

	public virtual void Close(bool shouldRemove = true) {
		if (shouldRemove) {
			Destroy(gameObject);
		} else {
			gameObject.SetActive(false);
		}
	}
}
