using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
	//需要在其他脚本调用该脚本函数使用单例模式
	private static GameController _instance;
	public static GameController Instance
	{
		get { return _instance; }
	}

	private void Awake()
	{
		_instance = this;
	}
	
	public int score=0;
	public int length=0;
	public Text msgText;
	public Text scoreText;
	public Text lengthText;
	public Image bgImage;
	private Color bgColor;

	private void Update()
	{
		switch (score/100)
		{
			case 0:
			case 1:
				break;
			case 2:
			case 3:
				ColorUtility.TryParseHtmlString("#CCEEFFFF", out bgColor);
				bgImage.color = bgColor;
				msgText.text = "阶段" + 2;
				break;
			case 5:
			case 6:
				ColorUtility.TryParseHtmlString("#ccffdbff", out bgColor);
				bgImage.color = bgColor;
				msgText.text = "阶段" + 3;
				break;
			case 7:
			case 8:
				ColorUtility.TryParseHtmlString("#ebffccff", out bgColor);
				bgImage.color = bgColor;
				msgText.text = "阶段" + 4;
				break;
			case 9:
			case 10:
				ColorUtility.TryParseHtmlString("#fff3ccff", out bgColor);
				bgImage.color = bgColor;
				msgText.text = "阶段" + 5;
				break;
			default:
				ColorUtility.TryParseHtmlString("#ffdaccff", out bgColor);
				bgImage.color = bgColor;
				msgText.text = "无尽阶段";
				break;
			
		}
	}

	public void UpdateUI(int s = 5, int l= 1)
	{
		score += s;
		length += l;
		scoreText.text = "得分：\n" + score;
		lengthText.text = "长度：\n" + length;
	}
}
