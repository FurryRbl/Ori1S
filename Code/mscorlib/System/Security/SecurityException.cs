using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Security.Policy;
using System.Text;

namespace System.Security
{
	/// <summary>The exception that is thrown when a security error is detected.</summary>
	// Token: 0x02000548 RID: 1352
	[ComVisible(true)]
	[Serializable]
	public class SecurityException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.SecurityException" /> class with default properties.</summary>
		// Token: 0x06003536 RID: 13622 RVA: 0x000AFD40 File Offset: 0x000ADF40
		public SecurityException() : base(Locale.GetText("A security error has been detected."))
		{
			base.HResult = -2146233078;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.SecurityException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		// Token: 0x06003537 RID: 13623 RVA: 0x000AFD60 File Offset: 0x000ADF60
		public SecurityException(string message) : base(message)
		{
			base.HResult = -2146233078;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.SecurityException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info " />is null.</exception>
		// Token: 0x06003538 RID: 13624 RVA: 0x000AFD74 File Offset: 0x000ADF74
		protected SecurityException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			base.HResult = -2146233078;
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Name == "PermissionState")
				{
					this.permissionState = (string)enumerator.Value;
					break;
				}
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.SecurityException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06003539 RID: 13625 RVA: 0x000AFDD8 File Offset: 0x000ADFD8
		public SecurityException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2146233078;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.SecurityException" /> class with a specified error message and the permission type that caused the exception to be thrown.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="type">The type of the permission that caused the exception to be thrown. </param>
		// Token: 0x0600353A RID: 13626 RVA: 0x000AFDF0 File Offset: 0x000ADFF0
		public SecurityException(string message, Type type) : base(message)
		{
			base.HResult = -2146233078;
			this.permissionType = type;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.SecurityException" /> class with a specified error message, the permission type that caused the exception to be thrown, and the permission state.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="type">The type of the permission that caused the exception to be thrown. </param>
		/// <param name="state">The state of the permission that caused the exception to be thrown. </param>
		// Token: 0x0600353B RID: 13627 RVA: 0x000AFE0C File Offset: 0x000AE00C
		public SecurityException(string message, Type type, string state) : base(message)
		{
			base.HResult = -2146233078;
			this.permissionType = type;
			this.permissionState = state;
		}

		// Token: 0x0600353C RID: 13628 RVA: 0x000AFE3C File Offset: 0x000AE03C
		internal SecurityException(string message, PermissionSet granted, PermissionSet refused) : base(message)
		{
			base.HResult = -2146233078;
			this._granted = granted.ToString();
			this._refused = refused.ToString();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.SecurityException" /> class for an exception caused by a Deny on the stack.  </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="deny">The denied permission or permission set.</param>
		/// <param name="permitOnly">The permit-only permission or permission set.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> that identifies the method that encountered the exception.</param>
		/// <param name="demanded">The demanded permission, permission set, or permission set collection.</param>
		/// <param name="permThatFailed">An <see cref="T:System.Security.IPermission" /> that identifies the permission that failed.</param>
		// Token: 0x0600353D RID: 13629 RVA: 0x000AFE74 File Offset: 0x000AE074
		public SecurityException(string message, object deny, object permitOnly, MethodInfo method, object demanded, IPermission permThatFailed) : base(message)
		{
			base.HResult = -2146233078;
			this._denyset = deny;
			this._permitset = permitOnly;
			this._method = method;
			this._demanded = demanded;
			this._firstperm = permThatFailed;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.SecurityException" /> class for an exception caused by an insufficient grant set. </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="assemblyName">An <see cref="T:System.Reflection.AssemblyName" /> that specifies the name of the assembly that caused the exception.</param>
		/// <param name="grant">A <see cref="T:System.Security.PermissionSet" /> that represents the permissions granted the assembly.</param>
		/// <param name="refused">A <see cref="T:System.Security.PermissionSet" /> that represents the refused permission or permission set.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> that represents the method that encountered the exception.</param>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values.</param>
		/// <param name="demanded">The demanded permission, permission set, or permission set collection.</param>
		/// <param name="permThatFailed">An <see cref="T:System.Security.IPermission" /> that represents the permission that failed.</param>
		/// <param name="evidence">The <see cref="T:System.Security.Policy.Evidence" /> for the assembly that caused the exception.</param>
		// Token: 0x0600353E RID: 13630 RVA: 0x000AFEBC File Offset: 0x000AE0BC
		public SecurityException(string message, AssemblyName assemblyName, PermissionSet grant, PermissionSet refused, MethodInfo method, SecurityAction action, object demanded, IPermission permThatFailed, Evidence evidence) : base(message)
		{
			base.HResult = -2146233078;
			this._assembly = assemblyName;
			this._granted = ((grant != null) ? grant.ToString() : string.Empty);
			this._refused = ((refused != null) ? refused.ToString() : string.Empty);
			this._method = method;
			this._action = action;
			this._demanded = demanded;
			this._firstperm = permThatFailed;
			if (this._firstperm != null)
			{
				this.permissionType = this._firstperm.GetType();
			}
			this._evidence = evidence;
		}

		/// <summary>Gets or sets the security action that caused the exception.</summary>
		/// <returns>One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values.</returns>
		// Token: 0x170009F1 RID: 2545
		// (get) Token: 0x0600353F RID: 13631 RVA: 0x000AFF60 File Offset: 0x000AE160
		// (set) Token: 0x06003540 RID: 13632 RVA: 0x000AFF68 File Offset: 0x000AE168
		[ComVisible(false)]
		public SecurityAction Action
		{
			get
			{
				return this._action;
			}
			set
			{
				this._action = value;
			}
		}

		/// <summary>Gets or sets the denied security permission, permission set, or permission set collection that caused a demand to fail.</summary>
		/// <returns>A permission, permission set, or permission set collection object.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x170009F2 RID: 2546
		// (get) Token: 0x06003541 RID: 13633 RVA: 0x000AFF74 File Offset: 0x000AE174
		// (set) Token: 0x06003542 RID: 13634 RVA: 0x000AFF7C File Offset: 0x000AE17C
		[ComVisible(false)]
		public object DenySetInstance
		{
			[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"ControlEvidence, ControlPolicy\"/>\n</PermissionSet>\n")]
			get
			{
				return this._denyset;
			}
			set
			{
				this._denyset = value;
			}
		}

		/// <summary>Gets or sets information about the failed assembly.</summary>
		/// <returns>An <see cref="T:System.Reflection.AssemblyName" /> that identifies the failed assembly.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x170009F3 RID: 2547
		// (get) Token: 0x06003543 RID: 13635 RVA: 0x000AFF88 File Offset: 0x000AE188
		// (set) Token: 0x06003544 RID: 13636 RVA: 0x000AFF90 File Offset: 0x000AE190
		[ComVisible(false)]
		public AssemblyName FailedAssemblyInfo
		{
			[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"ControlEvidence, ControlPolicy\"/>\n</PermissionSet>\n")]
			get
			{
				return this._assembly;
			}
			set
			{
				this._assembly = value;
			}
		}

		/// <summary>Gets or sets the information about the method associated with the exception.</summary>
		/// <returns>A <see cref="T:System.Reflection.MethodInfo" /> object describing the method.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x170009F4 RID: 2548
		// (get) Token: 0x06003545 RID: 13637 RVA: 0x000AFF9C File Offset: 0x000AE19C
		// (set) Token: 0x06003546 RID: 13638 RVA: 0x000AFFA4 File Offset: 0x000AE1A4
		[ComVisible(false)]
		public MethodInfo Method
		{
			[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"ControlEvidence, ControlPolicy\"/>\n</PermissionSet>\n")]
			get
			{
				return this._method;
			}
			set
			{
				this._method = value;
			}
		}

		/// <summary>Gets or sets the permission, permission set, or permission set collection that is part of the permit-only stack frame that caused a security check to fail.</summary>
		/// <returns>A permission, permission set, or permission set collection object.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x170009F5 RID: 2549
		// (get) Token: 0x06003547 RID: 13639 RVA: 0x000AFFB0 File Offset: 0x000AE1B0
		// (set) Token: 0x06003548 RID: 13640 RVA: 0x000AFFB8 File Offset: 0x000AE1B8
		[ComVisible(false)]
		public object PermitOnlySetInstance
		{
			[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"ControlEvidence, ControlPolicy\"/>\n</PermissionSet>\n")]
			get
			{
				return this._permitset;
			}
			set
			{
				this._permitset = value;
			}
		}

		/// <summary>Gets or sets the URL of the assembly that caused the exception.</summary>
		/// <returns>A URL that identifies the location of the assembly.</returns>
		// Token: 0x170009F6 RID: 2550
		// (get) Token: 0x06003549 RID: 13641 RVA: 0x000AFFC4 File Offset: 0x000AE1C4
		// (set) Token: 0x0600354A RID: 13642 RVA: 0x000AFFCC File Offset: 0x000AE1CC
		public string Url
		{
			[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"ControlEvidence, ControlPolicy\"/>\n</PermissionSet>\n")]
			get
			{
				return this._url;
			}
			set
			{
				this._url = value;
			}
		}

		/// <summary>Gets or sets the zone of the assembly that caused the exception.</summary>
		/// <returns>One of the <see cref="T:System.Security.SecurityZone" /> values that identifies the zone of the assembly that caused the exception.</returns>
		// Token: 0x170009F7 RID: 2551
		// (get) Token: 0x0600354B RID: 13643 RVA: 0x000AFFD8 File Offset: 0x000AE1D8
		// (set) Token: 0x0600354C RID: 13644 RVA: 0x000AFFE0 File Offset: 0x000AE1E0
		public SecurityZone Zone
		{
			get
			{
				return this._zone;
			}
			set
			{
				this._zone = value;
			}
		}

		/// <summary>Gets or sets the demanded security permission, permission set, or permission set collection that failed.</summary>
		/// <returns>A permission, permission set, or permission set collection object.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x170009F8 RID: 2552
		// (get) Token: 0x0600354D RID: 13645 RVA: 0x000AFFEC File Offset: 0x000AE1EC
		// (set) Token: 0x0600354E RID: 13646 RVA: 0x000AFFF4 File Offset: 0x000AE1F4
		[ComVisible(false)]
		public object Demanded
		{
			[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"ControlEvidence, ControlPolicy\"/>\n</PermissionSet>\n")]
			get
			{
				return this._demanded;
			}
			set
			{
				this._demanded = value;
			}
		}

		/// <summary>Gets or sets the first permission in a permission set or permission set collection that failed the demand.</summary>
		/// <returns>An <see cref="T:System.Security.IPermission" /> object representing the first permission that failed.</returns>
		// Token: 0x170009F9 RID: 2553
		// (get) Token: 0x0600354F RID: 13647 RVA: 0x000B0000 File Offset: 0x000AE200
		// (set) Token: 0x06003550 RID: 13648 RVA: 0x000B0008 File Offset: 0x000AE208
		public IPermission FirstPermissionThatFailed
		{
			[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"ControlEvidence, ControlPolicy\"/>\n</PermissionSet>\n")]
			get
			{
				return this._firstperm;
			}
			set
			{
				this._firstperm = value;
			}
		}

		/// <summary>Gets or sets the state of the permission that threw the exception.</summary>
		/// <returns>The state of the permission at the time the exception was thrown.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x170009FA RID: 2554
		// (get) Token: 0x06003551 RID: 13649 RVA: 0x000B0014 File Offset: 0x000AE214
		// (set) Token: 0x06003552 RID: 13650 RVA: 0x000B001C File Offset: 0x000AE21C
		public string PermissionState
		{
			[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"ControlEvidence, ControlPolicy\"/>\n</PermissionSet>\n")]
			get
			{
				return this.permissionState;
			}
			set
			{
				this.permissionState = value;
			}
		}

		/// <summary>Gets or sets the type of the permission that failed.</summary>
		/// <returns>The type of the permission that failed.</returns>
		// Token: 0x170009FB RID: 2555
		// (get) Token: 0x06003553 RID: 13651 RVA: 0x000B0028 File Offset: 0x000AE228
		// (set) Token: 0x06003554 RID: 13652 RVA: 0x000B0030 File Offset: 0x000AE230
		public Type PermissionType
		{
			get
			{
				return this.permissionType;
			}
			set
			{
				this.permissionType = value;
			}
		}

		/// <summary>Gets or sets the granted permission set of the assembly that caused the <see cref="T:System.Security.SecurityException" />.</summary>
		/// <returns>The XML representation of the granted set of the assembly.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x170009FC RID: 2556
		// (get) Token: 0x06003555 RID: 13653 RVA: 0x000B003C File Offset: 0x000AE23C
		// (set) Token: 0x06003556 RID: 13654 RVA: 0x000B0044 File Offset: 0x000AE244
		public string GrantedSet
		{
			[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"ControlEvidence, ControlPolicy\"/>\n</PermissionSet>\n")]
			get
			{
				return this._granted;
			}
			set
			{
				this._granted = value;
			}
		}

		/// <summary>Gets or sets the refused permission set of the assembly that caused the <see cref="T:System.Security.SecurityException" />.</summary>
		/// <returns>The XML representation of the refused permission set of the assembly.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x170009FD RID: 2557
		// (get) Token: 0x06003557 RID: 13655 RVA: 0x000B0050 File Offset: 0x000AE250
		// (set) Token: 0x06003558 RID: 13656 RVA: 0x000B0058 File Offset: 0x000AE258
		public string RefusedSet
		{
			[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"ControlEvidence, ControlPolicy\"/>\n</PermissionSet>\n")]
			get
			{
				return this._refused;
			}
			set
			{
				this._refused = value;
			}
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with information about the <see cref="T:System.Security.SecurityException" />.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> parameter is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x06003559 RID: 13657 RVA: 0x000B0064 File Offset: 0x000AE264
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			try
			{
				info.AddValue("PermissionState", this.PermissionState);
			}
			catch (SecurityException)
			{
			}
		}

		/// <summary>Returns a representation of the current <see cref="T:System.Security.SecurityException" />.</summary>
		/// <returns>A string representation of the current <see cref="T:System.Security.SecurityException" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x0600355A RID: 13658 RVA: 0x000B00B4 File Offset: 0x000AE2B4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString());
			try
			{
				if (this.permissionType != null)
				{
					stringBuilder.AppendFormat("{0}Type: {1}", Environment.NewLine, this.PermissionType);
				}
				if (this._method != null)
				{
					string text = this._method.ToString();
					int startIndex = text.IndexOf(" ") + 1;
					stringBuilder.AppendFormat("{0}Method: {1} {2}.{3}", new object[]
					{
						Environment.NewLine,
						this._method.ReturnType.Name,
						this._method.ReflectedType,
						text.Substring(startIndex)
					});
				}
				if (this.permissionState != null)
				{
					stringBuilder.AppendFormat("{0}State: {1}", Environment.NewLine, this.PermissionState);
				}
				if (this._granted != null && this._granted.Length > 0)
				{
					stringBuilder.AppendFormat("{0}Granted: {1}", Environment.NewLine, this.GrantedSet);
				}
				if (this._refused != null && this._refused.Length > 0)
				{
					stringBuilder.AppendFormat("{0}Refused: {1}", Environment.NewLine, this.RefusedSet);
				}
				if (this._demanded != null)
				{
					stringBuilder.AppendFormat("{0}Demanded: {1}", Environment.NewLine, this.Demanded);
				}
				if (this._firstperm != null)
				{
					stringBuilder.AppendFormat("{0}Failed Permission: {1}", Environment.NewLine, this.FirstPermissionThatFailed);
				}
				if (this._evidence != null)
				{
					stringBuilder.AppendFormat("{0}Evidences:", Environment.NewLine);
					foreach (object obj in this._evidence)
					{
						if (!(obj is Hash))
						{
							stringBuilder.AppendFormat("{0}\t{1}", Environment.NewLine, obj);
						}
					}
				}
			}
			catch (SecurityException)
			{
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0400165A RID: 5722
		private string permissionState;

		// Token: 0x0400165B RID: 5723
		private Type permissionType;

		// Token: 0x0400165C RID: 5724
		private string _granted;

		// Token: 0x0400165D RID: 5725
		private string _refused;

		// Token: 0x0400165E RID: 5726
		private object _demanded;

		// Token: 0x0400165F RID: 5727
		private IPermission _firstperm;

		// Token: 0x04001660 RID: 5728
		private MethodInfo _method;

		// Token: 0x04001661 RID: 5729
		private Evidence _evidence;

		// Token: 0x04001662 RID: 5730
		private SecurityAction _action;

		// Token: 0x04001663 RID: 5731
		private object _denyset;

		// Token: 0x04001664 RID: 5732
		private object _permitset;

		// Token: 0x04001665 RID: 5733
		private AssemblyName _assembly;

		// Token: 0x04001666 RID: 5734
		private string _url;

		// Token: 0x04001667 RID: 5735
		private SecurityZone _zone;
	}
}
