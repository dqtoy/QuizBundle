using InterativaSystem.Controllers;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace InterativaSystem.Views.HUD.Quiz
{
	public class QuizTypesPrepareButton : ButtonView
	{
		public event GenericController.IntEvent OnSelection;


		public int option;
		[Space]
		public bool HasTypes;
		public bool BlockOnReset;
		public List<int> types;

		private QuizController _quizController;

		protected override void OnStart()
		{
			base.OnStart();

			_quizController = _controller as QuizController;

			if (HasTypes && BlockOnReset)
			{
				_quizController.Reset += () =>
				{
					_bt.interactable = false;
				};
			}
		}


		protected override void OnClick()
		{
			base.OnClick();

			RegisterJNController.Instance.AddRegister("option", option.ToString());

			if (HasTypes)
				_quizController.PrepareGame(types);
			else
				_quizController.PrepareGame();
		}
	}
}
