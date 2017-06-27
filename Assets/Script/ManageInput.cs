using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine;

//专门管理输入
public class ManageInput : MonoBehaviour
{
	CharacterControl Character_01;

	//技能对应按键是否正被按下，传递给角色控制类再判断是否触发技能
	//序列号0为冲锋，1为小闪避，2为大闪避，3为主动技能
	bool[] Character_01SkillPress;
	//控制移动的轴
	float Chara1Horizon;
	float Chara1Vertical;
	float Chara2Horizon;
	float Chara2Vertical;

	// Use this for initialization
	void Start ()
	{
		Character_01 = gameObject.GetComponent<CharacterControl> ();
		Character_01SkillPress = new bool[]{ false, false, false, false };
	}
	
	// Update is called once per frame
	void Update ()
	{
		/////////////////////////////////
		if (!Character_01SkillPress [0]) {
			Character_01SkillPress [0] = CrossPlatformInputManager.GetButtonDown ("Character1_dash");
		}
		if (Character_01SkillPress [0]) {
			Character_01SkillPress [0] = !CrossPlatformInputManager.GetButtonUp ("Character1_dash");
		}
		/////////////////////////////////
		if (!Character_01SkillPress [1]) {
			Character_01SkillPress [1] = CrossPlatformInputManager.GetButtonDown ("Jump");
		}
		if (Character_01SkillPress [1]) {
			Character_01SkillPress [1] = !CrossPlatformInputManager.GetButtonUp ("Jump");
		}
		/////////////////////////////////
		if (!Character_01SkillPress [2]) {
			Character_01SkillPress [2] = CrossPlatformInputManager.GetButtonDown ("Character1_dodge_l");
		}
		if (Character_01SkillPress [2]) {
			Character_01SkillPress [2] = !CrossPlatformInputManager.GetButtonUp ("Character1_dodge_l");
		}
		/////////////////////////////////
		if (!Character_01SkillPress [3]) {
			Character_01SkillPress [3] = CrossPlatformInputManager.GetButtonDown ("Character1_skill_01");
		}
		if (Character_01SkillPress [3]) {
			Character_01SkillPress [3] = !CrossPlatformInputManager.GetButtonUp ("Character1_skill_01");
		}
		/////////////////////////////////
		Chara1Horizon= CrossPlatformInputManager.GetAxis ("Horizontal");
		Chara1Vertical= CrossPlatformInputManager.GetAxis ("Vertical");

		Character_01.Move (Chara1Horizon, Chara1Vertical, Character_01SkillPress);
		//Character_02.Move (Chara2Horizon, Chara2Vertical, Character_02SkillPress);
	}
}
