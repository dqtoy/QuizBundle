using InterativaSystem.Controllers;
using InterativaSystem.Models;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Text.RegularExpressions;

namespace InterativaSystem.Views.HUD.Register
{
    public class RegisterInput : InputView
    {
        protected RegisterController _registerController;
        public Text Label;
        [Space]
        public string DataName;
        public CheckType CheckType;
        public bool IsFirstSelected;

        [Space]
        public bool IsUnique;

        [HideInInspector]
        public bool _isOk;

        private bool _isReseting;

        public GameObject CheckFeddback;
        private int _id;


		protected override void OnStart()
        {
            _registerController = _controller as RegisterController;
            _registerController.OnSubmit += Submit;

			//CheckFeddback.SetActive(CheckType != CheckType.Generic && !_isOk);
			CheckFeddback.SetActive(!_isOk);

			_registerController.FieldsBools.Add(_isOk);
            _id = _registerController.FieldsBools.Count - 1;
			//modify by jonathan
			

			_registerController.Reset += Reset;

            if (IsFirstSelected)
            {
                _registerController.ActiveField = this;
            }

			_timeController.RegisterInJson += AddNewValues; 
            //input.onEndEdit.AddListener(delegate { _registerController.CloseKeyboard(); });
            //input.OnSelect.AddListener(delegate { _registerController.CloseKeyboard(); });
        }

        void Reset()
        {
            if (input == null) return;

            _isReseting = true;
            input.text = "";
            ChackString(input.text);
            _registerController.FieldsBools[_id] = _isOk;

			CheckFeddback.SetActive(true);

			if (IsFirstSelected)
            {
                _registerController.ActiveField = this;
            }
            _isReseting = false;
        }

		void ChackString(string value) {
			if (IsUnique) {
				string reg;
				if (_registerController.TryGetRegistryFromValue(DataName, value,out reg)) {

					UnityEngine.Debug.Log(reg);
					UnityEngine.Debug.Log(value);

					if (reg.Equals(value))
					{
						RegisterJNController.Instance.OnPageOpenId(2);
					}

				
					switch (CheckType) {
						case CheckType.Generic:
							_isOk = !string.IsNullOrEmpty(value) && reg != null && reg != value ||
								!string.IsNullOrEmpty(value) && reg == null;
							break;
						case CheckType.CPF:
							_isOk = Utils.CpfValid(value) && reg != null && reg != value ||
								!string.IsNullOrEmpty(value) && reg == null;
							break;
						case CheckType.Mail:
							_isOk = Utils.EmailIsValid(value) && reg != null && reg != value ||
								!string.IsNullOrEmpty(value) && reg == null;
							break;
						case CheckType.CRM:
							Match match = Regex.Match(value, @"[A-Za-z]{2}[0-9]{7}");

							_isOk = !string.IsNullOrEmpty(value) && reg != null && reg != value ||
								!string.IsNullOrEmpty(value) && reg == null && match.Success;
							break;
					}
					return;
				}
			}

			UnityEngine.Debug.Log(value);
			//UnityEngine.Debug.Log(value);

			switch (CheckType) {
				case CheckType.Generic:
					_isOk = !string.IsNullOrEmpty(value);
					break;
				case CheckType.CPF:
					_isOk = Utils.CpfValid(value);
					break;
				case CheckType.Mail:
					_isOk = Utils.EmailIsValid(value);
					break;
				case CheckType.Phone:
					string t = value.Replace("(", "").Replace(")", "");
					_isOk = t.Length >= 10;
					break;
				case CheckType.CRM:
					Match match = Regex.Match(value, @"[A-Za-z]{2}[0-9]{7}");
					_isOk = match.Success;
					break;
			}
		}
		public override void OnSelect(BaseEventData eventData)
        {
            if (_isReseting) return;
            base.OnSelect(eventData);

            if(_bootstrap.IsMobile) return;

            _registerController.ShowKeyboard();
            _registerController.ActiveField = this;
        }

        protected override void ValueChanged(string value)
        {
			UnityEngine.Debug.Log("ValueCHANGED :" + _isReseting);
			if (_isReseting) return;

			ChackString(value);
            _registerController.FieldsBools[_id] = _isOk;
        }
        public void AddValues()
        {
			UnityEngine.Debug.Log("AddValues :" + _isOk);

			if (_isReseting) return;

            if (_isOk && !IsUnique)
                _registerController.AddRegisterValue(DataName, input.text, true);

            if (CheckFeddback != null)
            {
                CheckFeddback.SetActive(!_isOk);
            }
        }

		public void AddValueButton()
		{
			if (_isOk )
				_registerController.AddRegisterValue(DataName, input.text, true);
		}

		public void AddNewValues(string DataName,string Value) 
		{

			_registerController.AddRegisterValue(DataName, Value, true);

		}

        protected override void EndEdit(string value)
        {
			UnityEngine.Debug.Log("EndEdit :" + _isReseting);

			if (_isReseting) return;

            if (_isOk)
			{
				UnityEngine.Debug.Log("EndEdit");
				_registerController.AddRegisterValue(DataName, value, true);
			}

            if (CheckFeddback != null)
            {
                CheckFeddback.SetActive(!_isOk);
            }
        }
        void Submit()
        {
            if (_isReseting) return;
        }
    }
}