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

		private States state;
		private long period;

		private Timer timer;

		#endregion

		#region ctors

		public Timer2(double p)
			: this((long)p) { }

		public Timer2(int p)
			: this((long)p) { }

		public Timer2(long p) {
			period = p;
			state = States.Paused;
		}

		#endregion

		#region Accessors

		public int Delay {
			get { return (int)period; }
			set {
				if(null != timer && state == States.Started)
					timer.Change(period, value);
				period = value;
			}
		}

		public States State {
			get { return state; }
			set { state = value == States.StopRequest && state != States.Started ? States.Paused : value; }
		}

		#endregion

		#region Methods

		private void _TC(object stateInfo) {
			if(State == States.Started) {
				OnTick();
			}
			else if(State == States.StopRequest) {
				Stop();
			}
		}

		public virtual void OnTick() {
		}

		public virtual void Start() {
			state = States.Started;
			if(timer == null)
				timer = new Timer(_TC);
			timer.Change(period, period);
		}

		public virtual void Restart() {
			Start();
		}

		public virtual void Stop() {
			state = States.Paused;
			if(timer == null) {
				return;
			}
			timer.Dispose();
			timer = null;
		}

		#endregion
	}
}