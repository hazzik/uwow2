using System;
using System.Threading;

namespace Hazzik {
	public class Timer2 {
		#region Nested Types

		public enum States {
			Started = 0,
			Paused = 1,
			StopRequest = 2,
		}

		#endregion

		#region Fields

		private long _period;
		private States _state;

		private Timer _timer;

		#endregion

		#region ctors

		public Timer2(double p)
			: this((long)p) {
		}

		public Timer2(int p)
			: this((long)p) {
		}

		public Timer2(long p) {
			_period = p;
			_state = States.Paused;
		}

		#endregion

		#region Accessors

		public int Delay {
			get { return (int)_period; }
			set {
				if(null != _timer && _state == States.Started) {
					_timer.Change(_period, value);
				}
				_period = value;
			}
		}

		public States State {
			get { return _state; }
			set { _state = value == States.StopRequest && _state != States.Started ? States.Paused : value; }
		}

		#endregion

		#region Methods

		private void Tc(object stateInfo) {
			switch(State) {
			case States.Started:
				OnTick();
				break;
			case States.StopRequest:
				Stop();
				break;
			}
		}

		public virtual void OnTick() {
		}

		public virtual void Start() {
			_state = States.Started;
			if(_timer == null) {
				_timer = new Timer(Tc);
			}
			_timer.Change(_period, _period);
		}

		public virtual void Restart() {
			Start();
		}

		public virtual void Stop() {
			_state = States.Paused;
			if(_timer == null) {
				return;
			}
			_timer.Dispose();
			_timer = null;
		}

		#endregion
	}
}