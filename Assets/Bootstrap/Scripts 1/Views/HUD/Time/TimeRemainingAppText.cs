﻿using Assets.Scripts.Views.HUD;
using InterativaSystem.Controllers;
using InterativaSystem.Views.HUD;

namespace InterativaSystem.Views.HUD.Time
{
    public class TimeRemainingAppText : DynamicText
    {
        public ConvertTimeType TimeFormat = ConvertTimeType.ms;

        protected override void OnStart()
        {
            _timeController = _controller as TimeController;
            _timeController.OnUpdateRemainingAppTime += UpdateData;
        }

        protected override void UpdateData(string value)
        {
            string nText = Utils.ConvertRealTime(float.Parse(value), TimeFormat);
            UpdateText(nText);
        }
    }
}