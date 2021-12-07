using System;
using System.ComponentModel;
using System.Threading;

namespace System.Timers
{
	/// <summary>Generates recurring events in an application.</summary>
	// Token: 0x020004AE RID: 1198
	[System.ComponentModel.DefaultProperty("Interval")]
	[System.ComponentModel.DefaultEvent("Elapsed")]
	public class Timer : System.ComponentModel.Component, System.ComponentModel.ISupportInitialize
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Timers.Timer" /> class, and sets all the properties to their initial values.</summary>
		// Token: 0x06002AF9 RID: 11001 RVA: 0x00093A6C File Offset: 0x00091C6C
		public Timer() : this(100.0)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Timers.Timer" /> class, and sets the <see cref="P:System.Timers.Timer.Interval" /> property to the specified number of milliseconds.</summary>
		/// <param name="interval">The time, in milliseconds, between events. The value must be greater than zero and less than or equal to <see cref="F:System.Int32.MaxValue" />.</param>
		/// <exception cref="T:System.ArgumentException">The value of the <paramref name="interval" /> parameter is less than or equal to zero, or greater than <see cref="F:System.Int32.MaxValue" />. </exception>
		// Token: 0x06002AFA RID: 11002 RVA: 0x00093A80 File Offset: 0x00091C80
		public Timer(double interval)
		{
			if (interval > 2147483647.0)
			{
				throw new ArgumentException("Invalid value: " + interval, "interval");
			}
			this.autoReset = true;
			this.Interval = interval;
		}

		/// <summary>Occurs when the interval elapses.</summary>
		// Token: 0x1400005E RID: 94
		// (add) Token: 0x06002AFB RID: 11003 RVA: 0x00093AD8 File Offset: 0x00091CD8
		// (remove) Token: 0x06002AFC RID: 11004 RVA: 0x00093AF4 File Offset: 0x00091CF4
		[System.ComponentModel.Category("Behavior")]
		[TimersDescription("Occurs when the Interval has elapsed.")]
		public event ElapsedEventHandler Elapsed;

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Timers.Timer" /> should raise the <see cref="E:System.Timers.Timer.Elapsed" /> event each time the specified interval elapses or only after the first time it elapses.</summary>
		/// <returns>true if the <see cref="T:System.Timers.Timer" /> should raise the <see cref="E:System.Timers.Timer.Elapsed" /> event each time the interval elapses; false if it should raise the <see cref="E:System.Timers.Timer.Elapsed" /> event only once, after the first time the interval elapses. The default is true.</returns>
		// Token: 0x17000BCB RID: 3019
		// (get) Token: 0x06002AFD RID: 11005 RVA: 0x00093B10 File Offset: 0x00091D10
		// (set) Token: 0x06002AFE RID: 11006 RVA: 0x00093B18 File Offset: 0x00091D18
		[System.ComponentModel.Category("Behavior")]
		[System.ComponentModel.DefaultValue(true)]
		[TimersDescription("Indicates whether the timer will be restarted when it is enabled.")]
		public bool AutoReset
		{
			get
			{
				return this.autoReset;
			}
			set
			{
				this.autoReset = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Timers.Timer" /> should raise the <see cref="E:System.Timers.Timer.Elapsed" /> event.</summary>
		/// <returns>true if the <see cref="T:System.Timers.Timer" /> should raise the <see cref="E:System.Timers.Timer.Elapsed" /> event; otherwise, false. The default is false.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This property cannot be set because the timer has been disposed.</exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Timers.Timer.Interval" /> property was set to a value greater than <see cref="F:System.Int32.MaxValue" /> before the timer was enabled.  </exception>
		// Token: 0x17000BCC RID: 3020
		// (get) Token: 0x06002AFF RID: 11007 RVA: 0x00093B24 File Offset: 0x00091D24
		// (set) Token: 0x06002B00 RID: 11008 RVA: 0x00093B7C File Offset: 0x00091D7C
		[TimersDescription("Indicates whether the timer is enabled to fire events at a defined interval.")]
		[System.ComponentModel.Category("Behavior")]
		[System.ComponentModel.DefaultValue(false)]
		public bool Enabled
		{
			get
			{
				object @lock = this._lock;
				bool result;
				lock (@lock)
				{
					result = (this.timer != null);
				}
				return result;
			}
			set
			{
				object @lock = this._lock;
				lock (@lock)
				{
					bool flag = this.timer != null;
					if (flag != value)
					{
						if (value)
						{
							this.timer = new Timer(new TimerCallback(Timer.Callback), this, (int)this.interval, (!this.autoReset) ? 0 : ((int)this.interval));
						}
						else
						{
							this.timer.Dispose();
							this.timer = null;
						}
					}
				}
			}
		}

		/// <summary>Gets or sets the interval at which to raise the <see cref="E:System.Timers.Timer.Elapsed" /> event.</summary>
		/// <returns>The time, in milliseconds, between <see cref="E:System.Timers.Timer.Elapsed" /> events. The value must be greater than zero, and less than or equal to <see cref="F:System.Int32.MaxValue" />. The default is 100 milliseconds.</returns>
		/// <exception cref="T:System.ArgumentException">The interval is less than or equal to zero.-or-The interval is greater than <see cref="F:System.Int32.MaxValue" />, and the timer is currently enabled. (If the timer is not currently enabled, no exception is thrown until it becomes enabled.)  </exception>
		// Token: 0x17000BCD RID: 3021
		// (get) Token: 0x06002B01 RID: 11009 RVA: 0x00093C2C File Offset: 0x00091E2C
		// (set) Token: 0x06002B02 RID: 11010 RVA: 0x00093C34 File Offset: 0x00091E34
		[System.ComponentModel.DefaultValue(100)]
		[TimersDescription("The number of milliseconds between timer events.")]
		[System.ComponentModel.RecommendedAsConfigurable(true)]
		[System.ComponentModel.Category("Behavior")]
		public double Interval
		{
			get
			{
				return this.interval;
			}
			set
			{
				if (value <= 0.0)
				{
					throw new ArgumentException("Invalid value: " + value);
				}
				object @lock = this._lock;
				lock (@lock)
				{
					this.interval = value;
					if (this.timer != null)
					{
						this.timer.Change((int)this.interval, (!this.autoReset) ? 0 : ((int)this.interval));
					}
				}
			}
		}

		/// <summary>Gets or sets the site that binds the <see cref="T:System.Timers.Timer" /> to its container in design mode.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.ISite" /> interface representing the site that binds the <see cref="T:System.Timers.Timer" /> object to its container.</returns>
		// Token: 0x17000BCE RID: 3022
		// (get) Token: 0x06002B03 RID: 11011 RVA: 0x00093CD8 File Offset: 0x00091ED8
		// (set) Token: 0x06002B04 RID: 11012 RVA: 0x00093CE0 File Offset: 0x00091EE0
		public override System.ComponentModel.ISite Site
		{
			get
			{
				return base.Site;
			}
			set
			{
				base.Site = value;
			}
		}

		/// <summary>Gets or sets the object used to marshal event-handler calls that are issued when an interval has elapsed.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.ISynchronizeInvoke" /> representing the object used to marshal the event-handler calls that are issued when an interval has elapsed. The default is null.</returns>
		// Token: 0x17000BCF RID: 3023
		// (get) Token: 0x06002B05 RID: 11013 RVA: 0x00093CEC File Offset: 0x00091EEC
		// (set) Token: 0x06002B06 RID: 11014 RVA: 0x00093CF4 File Offset: 0x00091EF4
		[System.ComponentModel.Browsable(false)]
		[System.ComponentModel.DefaultValue(null)]
		[TimersDescription("The object used to marshal the event handler calls issued when an interval has elapsed.")]
		public System.ComponentModel.ISynchronizeInvoke SynchronizingObject
		{
			get
			{
				return this.so;
			}
			set
			{
				this.so = value;
			}
		}

		/// <summary>Begins the run-time initialization of a <see cref="T:System.Timers.Timer" /> that is used on a form or by another component.</summary>
		// Token: 0x06002B07 RID: 11015 RVA: 0x00093D00 File Offset: 0x00091F00
		public void BeginInit()
		{
		}

		/// <summary>Releases the resources used by the <see cref="T:System.Timers.Timer" />.</summary>
		// Token: 0x06002B08 RID: 11016 RVA: 0x00093D04 File Offset: 0x00091F04
		public void Close()
		{
			this.Enabled = false;
		}

		/// <summary>Ends the run-time initialization of a <see cref="T:System.Timers.Timer" /> that is used on a form or by another component.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002B09 RID: 11017 RVA: 0x00093D10 File Offset: 0x00091F10
		public void EndInit()
		{
		}

		/// <summary>Starts raising the <see cref="E:System.Timers.Timer.Elapsed" /> event by setting <see cref="P:System.Timers.Timer.Enabled" /> to true.</summary>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="T:System.Timers.Timer" /> is created with an interval equal to or greater than <see cref="F:System.Int32.MaxValue" /> + 1, or set to an interval less than zero.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002B0A RID: 11018 RVA: 0x00093D14 File Offset: 0x00091F14
		public void Start()
		{
			this.Enabled = true;
		}

		/// <summary>Stops raising the <see cref="E:System.Timers.Timer.Elapsed" /> event by setting <see cref="P:System.Timers.Timer.Enabled" /> to false.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002B0B RID: 11019 RVA: 0x00093D20 File Offset: 0x00091F20
		public void Stop()
		{
			this.Enabled = false;
		}

		/// <summary>Releases all resources used by the current <see cref="T:System.Timers.Timer" />.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06002B0C RID: 11020 RVA: 0x00093D2C File Offset: 0x00091F2C
		protected override void Dispose(bool disposing)
		{
			this.Close();
			base.Dispose(disposing);
		}

		// Token: 0x06002B0D RID: 11021 RVA: 0x00093D3C File Offset: 0x00091F3C
		private static void Callback(object state)
		{
			Timer timer = (Timer)state;
			if (!timer.Enabled)
			{
				return;
			}
			ElapsedEventHandler elapsed = timer.Elapsed;
			if (!timer.autoReset)
			{
				timer.Enabled = false;
			}
			if (elapsed == null)
			{
				return;
			}
			ElapsedEventArgs elapsedEventArgs = new ElapsedEventArgs(DateTime.Now);
			if (timer.so != null && timer.so.InvokeRequired)
			{
				timer.so.BeginInvoke(elapsed, new object[]
				{
					timer,
					elapsedEventArgs
				});
			}
			else
			{
				try
				{
					elapsed(timer, elapsedEventArgs);
				}
				catch
				{
				}
			}
		}

		// Token: 0x04001B1F RID: 6943
		private double interval;

		// Token: 0x04001B20 RID: 6944
		private bool autoReset;

		// Token: 0x04001B21 RID: 6945
		private Timer timer;

		// Token: 0x04001B22 RID: 6946
		private object _lock = new object();

		// Token: 0x04001B23 RID: 6947
		private System.ComponentModel.ISynchronizeInvoke so;
	}
}
