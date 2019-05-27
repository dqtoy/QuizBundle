using InterativaSystem.Interfaces;
using UnityEngine;

namespace InterativaSystem.Controllers
{
    [AddComponentMenu("ModularSystem/Controllers/Time Controller")]
    public class TimeController : GenericController
    {
        public float TimeSpeed = 1;


		[HideInInspector]
        public float TimeSinceAppStart, TimeSinceGameStart, RemainingAppTime, RemainingGameTime, TimeSinceLastTimeout;
        [HideInInspector]
        public bool AppTimeup, GameTimeup, Timeup;
        public bool Paused = false;

		public event SimpleEvent AppTimeout, GameTimeout, Timeout, AddQuestionRight;
		public event RegisterEvent RegisterInJson;
        public event StringEvent OnUpdateRemainingGameTime, OnUpdateRemainingAppTime, OnUpdateGameTime, OnUpdateAppTime;

        public float AppTimeLimit = 90f, GameTimeLimit = 30f, TimeoutLimit = 120f;
        [Space]
        public float RemainingTimeoutTime;

        private IController _gameController;
        
        void Awake()
        {
            //Mandatory set for every controller
            Type = ControllerTypes.Time;
		
			if(FindObjectOfType<ConfigQuizController>() != null) 
				ConfigQuizController.Instance.SimpleIntEvent2 += ChangeValueGameLimit;
				
				
		}

		void Update()
        {
            if (Paused)
                return;
			//Debug.Log("asdasdas");
            if (_bootstrap.IsAppRunning && !AppTimeup)
            {
                TimeSinceAppStart += Time.deltaTime;
                RemainingAppTime = AppTimeLimit - TimeSinceAppStart;

                if (OnUpdateAppTime != null)
                    OnUpdateAppTime(TimeSinceAppStart.ToString());

                if (OnUpdateRemainingAppTime != null)
                    OnUpdateRemainingAppTime(RemainingAppTime.ToString());

                if (RemainingAppTime <= 0)
                {
                    if (AppTimeout != null) AppTimeout();
                    AppTimeup = true;
                }
			
            }

            if (!GameTimeup && _gameController != null && _gameController.IsGameRunning)
            {
				
                TimeSinceGameStart += Time.deltaTime;
                RemainingGameTime = GameTimeLimit - TimeSinceGameStart;
				
                if (OnUpdateGameTime != null)
                    OnUpdateGameTime(TimeSinceGameStart.ToString());

                if (OnUpdateRemainingGameTime != null)
                    OnUpdateRemainingGameTime(RemainingGameTime.ToString());

					if (Timeout != null) Timeout();
					Timeup = true;


                if (RemainingGameTime <= 0 )
                {

					if (GameTimeout != null ) GameTimeout();
                    GameTimeup = true;

				}
            }

            if (!Timeup)
            {
                TimeSinceLastTimeout += Time.deltaTime;
                RemainingTimeoutTime = TimeoutLimit - TimeSinceLastTimeout;
                if (RemainingTimeoutTime <= 0)
                {
                    if (Timeout != null) Timeout();
                    Timeup = true;
					
                }
            }

            Time.timeScale = TimeSpeed;
        }

        protected override void OnStart()
        {
            _bootstrap.AppStarted += StartAppTimer;
            _bootstrap.GameControllerStarted += SetGameController;
			
		}

		public void  ChangeValueGameLimit(int value) 
		{
			GameTimeLimit = float.Parse(value.ToString());
		}

        void SetGameController()
        {
            StartGameTimer();
            _gameController = _bootstrap.ActualRunningGameController;
            _gameController.OnGameStart += StartGameTimer;
            _gameController.OnGameEnd += UnsetGameController;

            if (_gameController.GameTime > 0)
                GameTimeLimit = _gameController.GameTime;
        }
        void UnsetGameController()
        {
            _gameController = null;
        }
        
        public void TimeoutReset()
        {
            TimeSinceLastTimeout = 0;
            Timeup = false;
        }
        public void StartAppTimer()
        {
            TimeSinceAppStart = 0;
            AppTimeup = false;
        }
        public void StartGameTimer()
        {
            DebugLog("StartGameTimer");
            TimeSinceGameStart = 0;
            GameTimeup = false;
            Paused = false;
        }

        public void ResetTimeScale()
        {
            TimeSpeed = 1;
        }
        public void AddToTimeScale(float value)
        {
            TimeSpeed += value;
        }
        public void SetToTimeScale(float value)
        {
            TimeSpeed = value;
        }
    }
}