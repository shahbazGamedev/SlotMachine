﻿using UnityEngine;
using System.Collections;
using Boomlagoon.JSON;

public class SlotZombieScreen : BaseSlotMachineScreen {
	
	public UIPanel mainPanel;
	public GameObject freeSpinLeft;
	public UILabel freeSpinLeftLabel;
	public GameObject[] lampGlows;
	
	private Transform fallingLeave;
	
	public override void SpawnSkill(int type, int level, int damage) {
		GameObject tempGameObject;
		SkillFireBall skill;
		switch (type) {
			case SlotItemZombie.ITEM_CHOPPER:
				tempGameObject = MyPoolManager.Instance.Spawn("SkillBite", skillCamera.transform).gameObject;
				// tempGameObject = NGUITools.AddChild(skillCamera, Resources.Load(Global.SCREEN_PATH + "/GameScreen/SlotMachine/SlotPirateScreen/SkillBite", typeof(GameObject)) as GameObject);
				SkillBite skillBite = tempGameObject.GetComponent<SkillBite>();
				skillBite.Init(level, damage, bossManager);
			break;
			case SlotItemZombie.ITEM_USOOP:
				tempGameObject = MyPoolManager.Instance.Spawn("SkillFireBall", skillCamera.transform).gameObject;
				// tempGameObject = NGUITools.AddChild(skillCamera, Resources.Load(Global.SCREEN_PATH + "/GameScreen/SlotMachine/SlotPirateScreen/SkillFireBall", typeof(GameObject)) as GameObject);
				skill = tempGameObject.GetComponent<SkillFireBall>();
				skill.Init(level, damage, bossManager);
			break;
			case SlotItemZombie.ITEM_NAMI:
				tempGameObject = MyPoolManager.Instance.Spawn("SkillThunder", skillCamera.transform).gameObject;
				// tempGameObject = NGUITools.AddChild(skillCamera, Resources.Load(Global.SCREEN_PATH + "/GameScreen/SlotMachine/SlotPirateScreen/SkillThunder", typeof(GameObject)) as GameObject);
				SkillThunder thunderSkill = tempGameObject.GetComponent<SkillThunder>();
				thunderSkill.Init(level, damage, bossManager);
			break;
			case SlotItemZombie.ITEM_PISTOL:
				tempGameObject = MyPoolManager.Instance.Spawn("SkillPistol", skillCamera.transform).gameObject;
				// tempGameObject = NGUITools.AddChild(skillCamera, Resources.Load(Global.SCREEN_PATH + "/GameScreen/SlotMachine/SlotPirateScreen/SkillFireBall", typeof(GameObject)) as GameObject);
				SkillPistol skillPistol = tempGameObject.GetComponent<SkillPistol>();
				skillPistol.Init(level, damage, bossManager);
			break;
			case SlotItemZombie.ITEM_BROOK:
				tempGameObject = MyPoolManager.Instance.Spawn("SkillDagger", skillCamera.transform).gameObject;
				// tempGameObject = NGUITools.AddChild(skillCamera, Resources.Load(Global.SCREEN_PATH + "/GameScreen/SlotMachine/SlotPirateScreen/SkillDagger", typeof(GameObject)) as GameObject);
				SkillDagger skillSword = tempGameObject.GetComponent<SkillDagger>();
				skillSword.Init(level, damage, bossManager);
			break;
			case SlotItemZombie.ITEM_NICO:
				tempGameObject = MyPoolManager.Instance.Spawn("SkillDagger", skillCamera.transform).gameObject;
				// tempGameObject = NGUITools.AddChild(skillCamera, Resources.Load(Global.SCREEN_PATH + "/GameScreen/SlotMachine/SlotPirateScreen/SkillDagger", typeof(GameObject)) as GameObject);
				SkillDagger skillDagger = tempGameObject.GetComponent<SkillDagger>();
				skillDagger.Init(level, damage, bossManager);
			break;
			case SlotItemZombie.ITEM_MACHINEGUN:
				tempGameObject = MyPoolManager.Instance.Spawn("SkillMachineGun", skillCamera.transform).gameObject;
				// tempGameObject = NGUITools.AddChild(skillCamera, Resources.Load(Global.SCREEN_PATH + "/GameScreen/SlotMachine/SlotPirateScreen/SkillSanji", typeof(GameObject)) as GameObject);
				SkillMachineGun skillMachineGun = tempGameObject.GetComponent<SkillMachineGun>();
				skillMachineGun.Init(level, damage, bossManager);
			break;
			case SlotItemZombie.ITEM_ZORO:
				tempGameObject = MyPoolManager.Instance.Spawn("SkillSwordBlue", skillCamera.transform).gameObject;
				// tempGameObject = NGUITools.AddChild(skillCamera, Resources.Load(Global.SCREEN_PATH + "/GameScreen/SlotMachine/SlotPirateScreen/SkillSwordBlue", typeof(GameObject)) as GameObject);
				SkillSwordBlue skillSwordBlue = tempGameObject.GetComponent<SkillSwordBlue>();
				skillSwordBlue.Init(level, damage, bossManager);
			break;
			case SlotItemZombie.ITEM_LUFFY:
				tempGameObject = MyPoolManager.Instance.Spawn("SkillLuffy", skillCamera.transform).gameObject;
				// tempGameObject = NGUITools.AddChild(skillCamera, Resources.Load(Global.SCREEN_PATH + "/GameScreen/SlotMachine/SlotPirateScreen/SkillLuffy", typeof(GameObject)) as GameObject);
				SkillLuffy skillLuffy = tempGameObject.GetComponent<SkillLuffy>();
				skillLuffy.Init(level, damage, bossManager);
			break;
			case SlotItemZombie.ITEM_RALLY:
				tempGameObject = MyPoolManager.Instance.Spawn("SkillFireBall", skillCamera.transform).gameObject;
				// tempGameObject = NGUITools.AddChild(skillCamera, Resources.Load(Global.SCREEN_PATH + "/GameScreen/SlotMachine/SlotPirateScreen/SkillFireBall", typeof(GameObject)) as GameObject);
				skill = tempGameObject.GetComponent<SkillFireBall>();
				skill.Init(level, damage, bossManager);
			break;
		}
		// tempGameObject = MyPoolManager.Instance.Spawn("SkillMachineGun", skillCamera.transform).gameObject;
		// SkillMachineGun skill = tempGameObject.GetComponent<SkillMachineGun>();
		// skill.Init(level, damage, bossManager);
	}
	
	public override void ShowFreeSpinAnimation() {
		Utils.SetActive(freeSpinLeft, true);
		fallingLeave = MyPoolManager.Instance.Spawn("FallingLeaves", skillCamera.transform);
		fallingLeave.localScale = Vector3.one * 525f;
		fallingLeave.position = new Vector3(0, mainPanel.worldCorners[1].y, 0);
		for (int i = 0; i < lampGlows.Length; i++) {
			NGUITools.SetActive(lampGlows[i], true);
		}
	}

	public override void StopFreeSpinAnimation() {
		Utils.SetActive(freeSpinLeft, false);
		if (fallingLeave != null) {
			MyPoolManager.Instance.Despawn(fallingLeave);
			fallingLeave = null;
		}
		for (int i = 0; i < lampGlows.Length; i++) {
			NGUITools.SetActive(lampGlows[i], false);
		}
	}
	
	public override void UpdateFreeSpinText(int numb) {
		freeSpinLeftLabel.text = Localization.Format("FreeSpinCountText", numb.ToString());
	}	
}