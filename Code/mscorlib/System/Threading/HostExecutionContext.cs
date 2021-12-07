using System;

namespace System.Threading
{
	/// <summary>Encapsulates and propagates the host execution context across threads. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200069F RID: 1695
	[MonoTODO("Useless until the runtime supports it")]
	public class HostExecutionContext
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.HostExecutionContext" /> class. </summary>
		// Token: 0x06004082 RID: 16514 RVA: 0x000DF1D4 File Offset: 0x000DD3D4
		public HostExecutionContext()
		{
			this._state = null;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.HostExecutionContext" /> class using the specified state. </summary>
		/// <param name="state">An object representing the host execution context state.</param>
		// Token: 0x06004083 RID: 16515 RVA: 0x000DF1E4 File Offset: 0x000DD3E4
		public HostExecutionContext(object state)
		{
			this._state = state;
		}

		/// <summary>Creates a copy of the current host execution context.</summary>
		/// <returns>A <see cref="T:System.Threading.HostExecutionContext" /> object representing the host context for the current thread.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06004084 RID: 16516 RVA: 0x000DF1F4 File Offset: 0x000DD3F4
		public virtual HostExecutionContext CreateCopy()
		{
			return new HostExecutionContext(this._state);
		}

		/// <summary>Gets or sets the state of the host execution context.</summary>
		/// <returns>An object representing the host execution context state.</returns>
		// Token: 0x17000C20 RID: 3104
		// (get) Token: 0x06004085 RID: 16517 RVA: 0x000DF204 File Offset: 0x000DD404
		// (set) Token: 0x06004086 RID: 16518 RVA: 0x000DF20C File Offset: 0x000DD40C
		protected internal object State
		{
			get
			{
				return this._state;
			}
			set
			{
				this._state = value;
			}
		}

		// Token: 0x04001BB3 RID: 7091
		private object _state;
	}
}
