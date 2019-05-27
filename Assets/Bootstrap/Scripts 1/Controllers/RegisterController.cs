using System;
using System.Collections.Generic;
using System.Xml.Linq;
using HeathenEngineering.OSK.v2;
using InterativaSystem.Models;
using InterativaSystem.Views.HUD;
using InterativaSystem.Views.HUD.Register;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.UI;
using DG.Tweening;

//TODO Yuri: CORRIGIR ENTRADAS EM BRANCO

namespace InterativaSystem.Controllers
{
	public enum CheckType {
		Generic = 0,
		Mail = 1,
		CPF = 2,
		Phone = 3,
		CRM = 4
	}
	[AddComponentMenu("ModularSystem/Controllers/Register Controller")]
    public class RegisterController : GenericController
    {
        private RegistrationData _data;

        public event SimpleEvent OnSubmit, OnRegistryUpdate;

        public OnScreenKeyboard OSKeyboard;
        
#if HAS_SERVER
        public event SimpleEvent OnRegistryReceiveEnded;
#endif

        [HideInInspector]
        public RegisterInput ActiveField;

        [HideInInspector]
        public List<bool> FieldsBools;

        #region Initialization
        void Awake()
        {
            //Mandatory set for every controller
            Type = ControllerTypes.Register;

            FieldsBools = new List<bool>();
        }
        protected override void OnStart()
        {
            base.OnStart();

			#region JN
			if (FindObjectOfType<ConfigQuizController>() != null) 
			{
				
				ConfigQuizController.Instance.SimpleIntEventCall += (int index) => {
					Debug.Log(index);
					CallAction(index);
				};
			}

			if (FindObjectOfType<RegisterJNController>() != null)
			{
				RegisterJNController.Instance.OnRegister += AddRegisterValue;
			}
			#endregion

		}

		protected override void GetReferences()
        {
            base.GetReferences();

            _data = _bootstrap.GetModel(ModelTypes.Register) as RegistrationData;

#if HAS_SERVER
            _data.OnRegistryReceiveEnded += RegistryReceiveEnd;
#endif
            _data.OnNewValue += RegistryUpdate;
            _data.OnNewRegstry += RegistryUpdate;
            _data.OnRegstryUpdate += RegistryUpdate;

            if (OSKeyboard != null)
            {
                OSKeyboard.KeyPressed += new KeyboardEventHandler(KeyboardKeyPressed);
               EventSystem.current.SetSelectedGameObject(OSKeyboard.ActiveKey.gameObject);
            }

			
        }
        #endregion
        
#region GetSetters
        public int GetFilteredRegistryCount()
        {
            return _data.GetFilteredRegistryCount();
        }
        public int GetRegistryCount()
        {
            return _data.GetRegistryCount();
        }
        public string GetRegistryValue(int index, string fieldName)
        {
            return _data.GetRegistryValue(index, fieldName);
        }
        public string GetRegistryValue(string uid, string fieldName)
        {
            return _data.GetRegistryValue(uid, fieldName);
        }
        public bool TryGetRegistryValue(string fieldName, out string value, bool isActual)
        {
            return _data.TryGetRegistryValue(fieldName, out value, isActual);
        }
        public bool TryGetRegistryValue(string fieldName, out string value)
        {
            return _data.TryGetRegistryValue(fieldName, out value);
        }
        public bool TryGetRegistryValue(string uid, string fieldName, out string value)
        {
            return _data.TryGetRegistryValue(uid, fieldName, out value);
        }
        public string GetActualRegistry(string field)
        {
            return _data.GetActualRegistry(field);
        }

        
        [Obsolete("AddData is deprecated, please use AddRegisterValue instead.")]
        public void AddData(string key, string value)
        {
            _data.NewRegistryValue(key, value, false);
        }
        public void AddRegisterValue(string key, string value, bool updateTime)
        {
            _data.NewRegistryValue(key, value, updateTime);
        }

        public void Submit()
        {
            _data.Save();
#if HAS_SERVER
            _data.NetworkSendRegistryToServer();
#endif
            if (OnSubmit != null) OnSubmit();
        }
#endregion

        void RegistryUpdate()
        {
            if (OnRegistryUpdate != null) OnRegistryUpdate();
        }
#if HAS_SERVER
        private void RegistryReceiveEnd()
        {
            if (OnRegistryReceiveEnded != null) OnRegistryReceiveEnded();
        }
#endif
        protected void KeyboardKeyPressed(OnScreenKeyboard sender, OnScreenKeyboardArguments args) {
			if (ActiveField) {
				int caretPos;

				InputField outputText = ActiveField.input;
				switch (args.KeyPressed.type) {
					case KeyClass.Backspace:
						if (outputText.text.Length > 0 || outputText.caretPosition > 0) {
							caretPos = outputText.caretPosition;

							outputText.text = outputText.text.Remove(outputText.caretPosition - 1, 1);

							outputText.caretPosition = caretPos - 1;
						}
						break;
					case KeyClass.Return:
						outputText.text += args.KeyPressed.ToString();
						break;
					case KeyClass.Shift:
						//No need to do anything here as the keyboard will sort that on its own
						break;
					case KeyClass.String:
						if (outputText.characterLimit > 0 && outputText.text.Length >= outputText.characterLimit) break;

						caretPos = outputText.caretPosition;
						string s = outputText.text;
						outputText.text = outputText.text.Insert(outputText.caretPosition, args.KeyPressed.ToString());
						outputText.caretPosition = caretPos + 1;
						break;
					case KeyClass.ModifySix:
						if (outputText.characterLimit > 0 && outputText.text.Length >= outputText.characterLimit) break;

						caretPos = outputText.caretPosition;
						string x = outputText.text;
						outputText.text = outputText.text.Insert(outputText.caretPosition, args.KeyPressed.ToString());
						outputText.caretPosition = caretPos + 6;
						break;
					case KeyClass.ModifyEight:
						if (outputText.characterLimit > 0 && outputText.text.Length >= outputText.characterLimit) break;

						caretPos = outputText.caretPosition;
						string z = outputText.text;
						outputText.text = outputText.text.Insert(outputText.caretPosition, args.KeyPressed.ToString());
						outputText.caretPosition = caretPos + 8;
						break;
					case KeyClass.ModifyFor:
						if (outputText.characterLimit > 0 && outputText.text.Length >= outputText.characterLimit) break;

						caretPos = outputText.caretPosition;
						string w = outputText.text;
						outputText.text = outputText.text.Insert(outputText.caretPosition, args.KeyPressed.ToString());
						outputText.caretPosition = caretPos + 4;
						break;
					case KeyClass.ModifyTree:
						if (outputText.characterLimit > 0 && outputText.text.Length >= outputText.characterLimit) break;

						caretPos = outputText.caretPosition;
						string o = outputText.text;
						outputText.text = outputText.text.Insert(outputText.caretPosition, args.KeyPressed.ToString());
						outputText.caretPosition = caretPos + 3;
						break;
				}

				ActiveField.AddValues();
			}
		}
        public void ShowKeyboard()
        {
            /*if(OSKeyboard != null)
                OSKeyboard.GetComponent<Transform>().DOLocalMoveY(-230f, 0.5f).SetEase(Ease.OutCubic).Play();*/
        }

        public void CloseKeyboard()
        {
            /*if (OSKeyboard != null)
                OSKeyboard.GetComponent<Transform>().DOLocalMoveY(-230f, 0.5f).SetEase(Ease.InCubic).Play();*/
        }

        public RegistryData GetRegistry(string uid)
        {
            return _data.GetRegistry(uid);
        }

        public bool TryGetRegistryFromValue(string dataName, string value, out string reg)
        {
            return _data.TryGetRegistryFromValue(dataName, value, out reg);
        }
    }
}