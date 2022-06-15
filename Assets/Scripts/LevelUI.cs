﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour //TODO: this should probably get rolled into an overall status UI
{
	public LevelUI instance;

	private Text levelText;
	private Image experienceBarImage;

	private void Awake()
	{
		levelText = transform.Find("levelText").GetComponent<Text>();
		experienceBarImage = transform.Find("experienceBar").Find("bar").GetComponent<Image>();
	}

	private void Start() //do things that rely on other objects being populated already
	{
		levelText.text = ExperienceManager.instance.GetLevel().ToString();
		SetExperienceBarSize();
	}

	private void SetExperienceBarSize()
	{
		var expPercentage = ExperienceManager.instance.GetExpPercentage();
		experienceBarImage.fillAmount = expPercentage;
	}

}
