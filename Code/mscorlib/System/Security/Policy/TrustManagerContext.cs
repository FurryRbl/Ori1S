using System;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
	/// <summary>Represents the context for the trust manager to consider when making the decision to run an application, and when setting up the security on a new <see cref="T:System.AppDomain" /> in which to run an application.</summary>
	// Token: 0x02000655 RID: 1621
	[ComVisible(true)]
	public class TrustManagerContext
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.TrustManagerContext" /> class. </summary>
		// Token: 0x06003DC0 RID: 15808 RVA: 0x000D4C48 File Offset: 0x000D2E48
		public TrustManagerContext() : this(TrustManagerUIContext.Run)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.TrustManagerContext" /> class using the specified <see cref="T:System.Security.Policy.TrustManagerUIContext" /> object. </summary>
		/// <param name="uiContext">One of the enumeration values that specifies the type of trust manager user interface to use. </param>
		// Token: 0x06003DC1 RID: 15809 RVA: 0x000D4C54 File Offset: 0x000D2E54
		public TrustManagerContext(TrustManagerUIContext uiContext)
		{
			this._ignorePersistedDecision = false;
			this._noPrompt = false;
			this._keepAlive = false;
			this._persist = false;
			this._ui = uiContext;
		}

		/// <summary>Gets or sets a value indicating whether the application security manager should ignore any persisted decisions and call the trust manager.</summary>
		/// <returns>true to call the trust manager; otherwise, false. </returns>
		// Token: 0x17000BA8 RID: 2984
		// (get) Token: 0x06003DC2 RID: 15810 RVA: 0x000D4C80 File Offset: 0x000D2E80
		// (set) Token: 0x06003DC3 RID: 15811 RVA: 0x000D4C88 File Offset: 0x000D2E88
		public virtual bool IgnorePersistedDecision
		{
			get
			{
				return this._ignorePersistedDecision;
			}
			set
			{
				this._ignorePersistedDecision = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the trust manager should cache state for this application, to facilitate future requests to determine application trust.</summary>
		/// <returns>true to cache state data; otherwise, false. The default is false.</returns>
		// Token: 0x17000BA9 RID: 2985
		// (get) Token: 0x06003DC4 RID: 15812 RVA: 0x000D4C94 File Offset: 0x000D2E94
		// (set) Token: 0x06003DC5 RID: 15813 RVA: 0x000D4C9C File Offset: 0x000D2E9C
		public virtual bool KeepAlive
		{
			get
			{
				return this._keepAlive;
			}
			set
			{
				this._keepAlive = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the trust manager should prompt the user for trust decisions.</summary>
		/// <returns>true to not prompt the user; false to prompt the user. The default is false.</returns>
		// Token: 0x17000BAA RID: 2986
		// (get) Token: 0x06003DC6 RID: 15814 RVA: 0x000D4CA8 File Offset: 0x000D2EA8
		// (set) Token: 0x06003DC7 RID: 15815 RVA: 0x000D4CB0 File Offset: 0x000D2EB0
		public virtual bool NoPrompt
		{
			get
			{
				return this._noPrompt;
			}
			set
			{
				this._noPrompt = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the user's response to the consent dialog should be persisted. </summary>
		/// <returns>true to cache state data; otherwise, false. The default is true.</returns>
		// Token: 0x17000BAB RID: 2987
		// (get) Token: 0x06003DC8 RID: 15816 RVA: 0x000D4CBC File Offset: 0x000D2EBC
		// (set) Token: 0x06003DC9 RID: 15817 RVA: 0x000D4CC4 File Offset: 0x000D2EC4
		public virtual bool Persist
		{
			get
			{
				return this._persist;
			}
			set
			{
				this._persist = value;
			}
		}

		/// <summary>Gets or sets the identity of the previous application identity.</summary>
		/// <returns>An <see cref="T:System.ApplicationIdentity" /> object representing the previous <see cref="T:System.ApplicationIdentity" />.</returns>
		// Token: 0x17000BAC RID: 2988
		// (get) Token: 0x06003DCA RID: 15818 RVA: 0x000D4CD0 File Offset: 0x000D2ED0
		// (set) Token: 0x06003DCB RID: 15819 RVA: 0x000D4CD8 File Offset: 0x000D2ED8
		public virtual ApplicationIdentity PreviousApplicationIdentity
		{
			get
			{
				return this._previousId;
			}
			set
			{
				this._previousId = value;
			}
		}

		/// <summary>Gets or sets the type of user interface the trust manager should display.</summary>
		/// <returns>One of the enumeration values. The default is <see cref="F:System.Security.Policy.TrustManagerUIContext.Run" />. </returns>
		// Token: 0x17000BAD RID: 2989
		// (get) Token: 0x06003DCC RID: 15820 RVA: 0x000D4CE4 File Offset: 0x000D2EE4
		// (set) Token: 0x06003DCD RID: 15821 RVA: 0x000D4CEC File Offset: 0x000D2EEC
		public virtual TrustManagerUIContext UIContext
		{
			get
			{
				return this._ui;
			}
			set
			{
				this._ui = value;
			}
		}

		// Token: 0x04001A9E RID: 6814
		private bool _ignorePersistedDecision;

		// Token: 0x04001A9F RID: 6815
		private bool _noPrompt;

		// Token: 0x04001AA0 RID: 6816
		private bool _keepAlive;

		// Token: 0x04001AA1 RID: 6817
		private bool _persist;

		// Token: 0x04001AA2 RID: 6818
		private ApplicationIdentity _previousId;

		// Token: 0x04001AA3 RID: 6819
		private TrustManagerUIContext _ui;
	}
}
