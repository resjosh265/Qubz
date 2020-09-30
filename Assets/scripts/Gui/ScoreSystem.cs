using UnityEngine;
using System.Collections;
using Qubz.Enums;

namespace Qubz.Gui{
	public class ScoreSystem : MonoBehaviour {

		public static int score = 0;
		private Texture scoreOnes;
		private Texture scoreTens;
		private Texture scoreHundreds;
		private Texture scoreOneThousands;

		public static int multiplier = 0;

		public static int grayNum, redNum, greenNum, blackNum;

		public Texture scoreMain, score1, score2, score3, score4, score5, score6, score7, score8, score9, score0;


		// Use this for initialization
		void Start () {
		
		}
		
		// Update is called once per frame
		void Update () {
			// 1234 / 1000 = 1
			//(1234 % 1000) / 100 = 2
			//(1234 % 100) / 10 = 3
			// 1234 % 10 = 4

			print (4 % 10);


		}

		public static void CalculateScore () {

			int greenScore = 5;
			int redScore = 5;
			int blackScore = 10;

			score += ((((greenNum * greenScore) + grayNum) + (redNum * redScore)) + (blackNum * blackScore)) * multiplier;

			grayNum = 0;
			redNum = 0;
			greenNum = 0;
			blackNum = 0;
			multiplier = 0;
				

		}

		public static void IncreaseBlockScore(ColorEnum color){
		switch(color){
			case ColorEnum.Red:
				ScoreSystem.redNum++;
				break;
			case ColorEnum.Green:
				ScoreSystem.greenNum++;
				break;
			case ColorEnum.Gray:
				ScoreSystem.grayNum++;
				break;
			case ColorEnum.Black:
				ScoreSystem.blackNum++;
				break;
		}
	}
			

		void OnGUI(){
			int ones = score % 10;
			int tens = (score % 100) / 10;
			int hundreds = (score % 1000) / 100;
			int thousands = score / 1000;

			//ScoreTexturePlacement (ones);
			GUI.Label (new Rect (15, 10, 100, 20), scoreMain);
			GUI.Label (new Rect (50, 30, 100, 20), ScoreTexturePlacement (ones));
			GUI.Label (new Rect (40, 30, 100, 20), ScoreTexturePlacement (tens));
			GUI.Label (new Rect (30, 30, 100, 20), ScoreTexturePlacement (hundreds));
			GUI.Label (new Rect (20, 30, 100, 20), ScoreTexturePlacement (thousands));




		}

		public Texture ScoreTexturePlacement(int unit) {
			

			if (unit == 0) {
				return score0;
			}
			if (unit == 1) {
				return score1;
			}
			if (unit == 2) {
				return score2;
			}
			if (unit == 3) {
				return score3;
			}
			if (unit == 4) {
				return score4;
			}
			if (unit == 5) {
				return score5;
			}
			if (unit == 6) {
				return score6;
			}
			if (unit == 7) {
				return score7;
			}
			if (unit == 8) {
				return score8;
			}
			if (unit == 9) {
				return score9;
			}

			return null;
		}

	}
}