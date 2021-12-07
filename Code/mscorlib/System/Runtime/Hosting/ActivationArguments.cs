using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Hosting
{
	/// <summary>Provides data for manifest-based activation of an application. This class cannot be inherited. </summary>
	// Token: 0x0200034C RID: 844
	[ComVisible(true)]
	[Serializable]
	public sealed class ActivationArguments
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Hosting.ActivationArguments" /> class with the specified activation context. </summary>
		/// <param name="activationData">An <see cref="T:System.ActivationContext" /> object identifying the manifest-based activation application.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="activationData" /> is null.</exception>
		// Token: 0x060028BC RID: 10428 RVA: 0x00091EBC File Offset: 0x000900BC
		public ActivationArguments(ActivationContext activationData)
		{
			if (activationData == null)
			{
				throw new ArgumentNullException("activationData");
			}
			this._context = activationData;
			this._identity = activationData.Identity;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Hosting.ActivationArguments" /> class with the specified application identity.</summary>
		/// <param name="applicationIdentity">An <see cref="T:System.ApplicationIdentity" />  object identifying the manifest-based activation application.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="applicationIdentity" /> is null.</exception>
		// Token: 0x060028BD RID: 10429 RVA: 0x00091EF4 File Offset: 0x000900F4
		public ActivationArguments(ApplicationIdentity applicationIdentity)
		{
			if (applicationIdentity == null)
			{
				throw new ArgumentNullException("applicationIdentity");
			}
			this._identity = applicationIdentity;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Hosting.ActivationArguments" /> class with the specified activation context and activation data.</summary>
		/// <param name="activationContext">An <see cref="T:System.ActivationContext" /> object identifying the manifest-based activation application.</param>
		/// <param name="activationData">An array of strings containing host-provided activation data.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="activationContext" /> is null.</exception>
		// Token: 0x060028BE RID: 10430 RVA: 0x00091F14 File Offset: 0x00090114
		public ActivationArguments(ActivationContext activationContext, string[] activationData)
		{
			if (activationContext == null)
			{
				throw new ArgumentNullException("activationContext");
			}
			this._context = activationContext;
			this._identity = activationContext.Identity;
			this._data = activationData;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Hosting.ActivationArguments" /> class with the specified application identity and activation data.</summary>
		/// <param name="applicationIdentity">An <see cref="T:System.ApplicationIdentity" /> object identifying the manifest-based activation application.</param>
		/// <param name="activationData">An array of strings containing host-provided activation data.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="applicationIdentity" /> is null.</exception>
		// Token: 0x060028BF RID: 10431 RVA: 0x00091F48 File Offset: 0x00090148
		public ActivationArguments(ApplicationIdentity applicationIdentity, string[] activationData)
		{
			if (applicationIdentity == null)
			{
				throw new ArgumentNullException("applicationIdentity");
			}
			this._identity = applicationIdentity;
			this._data = activationData;
		}

		/// <summary>Gets the activation context for manifest-based activation of an application.</summary>
		/// <returns>An <see cref="T:System.ActivationContext" /> object identifying a manifest-based activation application.</returns>
		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x060028C0 RID: 10432 RVA: 0x00091F70 File Offset: 0x00090170
		public ActivationContext ActivationContext
		{
			get
			{
				return this._context;
			}
		}

		/// <summary>Gets activation data from the host.</summary>
		/// <returns>An array of strings containing host-provided activation data.</returns>
		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x060028C1 RID: 10433 RVA: 0x00091F78 File Offset: 0x00090178
		public string[] ActivationData
		{
			get
			{
				return this._data;
			}
		}

		/// <summary>Gets the application identity for a manifest-activated application.</summary>
		/// <returns>An <see cref="T:System.ApplicationIdentity" /> object identifying an application for manifest-based activation.</returns>
		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x060028C2 RID: 10434 RVA: 0x00091F80 File Offset: 0x00090180
		public ApplicationIdentity ApplicationIdentity
		{
			get
			{
				return this._identity;
			}
		}

		// Token: 0x040010A8 RID: 4264
		private ActivationContext _context;

		// Token: 0x040010A9 RID: 4265
		private ApplicationIdentity _identity;

		// Token: 0x040010AA RID: 4266
		private string[] _data;
	}
}
