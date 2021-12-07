using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Proxies;
using System.Runtime.Serialization;

namespace System.Runtime.Remoting.Messaging
{
	/// <summary>Implements the <see cref="T:System.Runtime.Remoting.Activation.IConstructionCallMessage" /> interface to create a request message that constitutes a constructor call on a remote object.</summary>
	// Token: 0x02000493 RID: 1171
	[CLSCompliant(false)]
	[ComVisible(true)]
	[Serializable]
	public class ConstructionCall : MethodCall, IConstructionCallMessage, IMessage, IMethodCallMessage, IMethodMessage
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Messaging.ConstructionCall" /> class by copying an existing message. </summary>
		/// <param name="m">A remoting message.</param>
		// Token: 0x06002FAB RID: 12203 RVA: 0x0009D8A0 File Offset: 0x0009BAA0
		public ConstructionCall(IMessage m) : base(m)
		{
			this._activationTypeName = this.TypeName;
			this._isContextOk = true;
		}

		// Token: 0x06002FAC RID: 12204 RVA: 0x0009D8BC File Offset: 0x0009BABC
		internal ConstructionCall(Type type)
		{
			this._activationType = type;
			this._activationTypeName = type.AssemblyQualifiedName;
			this._isContextOk = true;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Messaging.ConstructionCall" /> class from an array of remoting headers. </summary>
		/// <param name="headers">An array of remoting headers that contain key-value pairs. This array is used to initialize <see cref="T:System.Runtime.Remoting.Messaging.ConstructionCall" /> fields for those headers that belong to the namespace "http://schemas.microsoft.com/clr/soap/messageProperties".</param>
		// Token: 0x06002FAD RID: 12205 RVA: 0x0009D8EC File Offset: 0x0009BAEC
		public ConstructionCall(Header[] headers) : base(headers)
		{
		}

		// Token: 0x06002FAE RID: 12206 RVA: 0x0009D8F8 File Offset: 0x0009BAF8
		internal ConstructionCall(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002FAF RID: 12207 RVA: 0x0009D904 File Offset: 0x0009BB04
		internal override void InitDictionary()
		{
			ConstructionCallDictionary constructionCallDictionary = new ConstructionCallDictionary(this);
			this.ExternalProperties = constructionCallDictionary;
			this.InternalProperties = constructionCallDictionary.GetInternalProperties();
		}

		// Token: 0x17000897 RID: 2199
		// (get) Token: 0x06002FB0 RID: 12208 RVA: 0x0009D92C File Offset: 0x0009BB2C
		// (set) Token: 0x06002FB1 RID: 12209 RVA: 0x0009D934 File Offset: 0x0009BB34
		internal bool IsContextOk
		{
			get
			{
				return this._isContextOk;
			}
			set
			{
				this._isContextOk = value;
			}
		}

		/// <summary>Gets the type of the remote object to activate. </summary>
		/// <returns>The <see cref="T:System.Type" /> of the remote object to activate.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000898 RID: 2200
		// (get) Token: 0x06002FB2 RID: 12210 RVA: 0x0009D940 File Offset: 0x0009BB40
		public Type ActivationType
		{
			get
			{
				if (this._activationType == null)
				{
					this._activationType = Type.GetType(this._activationTypeName);
				}
				return this._activationType;
			}
		}

		/// <summary>Gets the full type name of the remote object to activate. </summary>
		/// <returns>A <see cref="T:System.String" /> containing the full type name of the remote object to activate.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000899 RID: 2201
		// (get) Token: 0x06002FB3 RID: 12211 RVA: 0x0009D970 File Offset: 0x0009BB70
		public string ActivationTypeName
		{
			get
			{
				return this._activationTypeName;
			}
		}

		/// <summary>Gets or sets the activator that activates the remote object. </summary>
		/// <returns>The <see cref="T:System.Runtime.Remoting.Activation.IActivator" /> that activates the remote object.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x1700089A RID: 2202
		// (get) Token: 0x06002FB4 RID: 12212 RVA: 0x0009D978 File Offset: 0x0009BB78
		// (set) Token: 0x06002FB5 RID: 12213 RVA: 0x0009D980 File Offset: 0x0009BB80
		public IActivator Activator
		{
			get
			{
				return this._activator;
			}
			set
			{
				this._activator = value;
			}
		}

		/// <summary>Gets the call site activation attributes for the remote object. </summary>
		/// <returns>An array of type <see cref="T:System.Object" /> containing the call site activation attributes for the remote object.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x1700089B RID: 2203
		// (get) Token: 0x06002FB6 RID: 12214 RVA: 0x0009D98C File Offset: 0x0009BB8C
		public object[] CallSiteActivationAttributes
		{
			get
			{
				return this._activationAttributes;
			}
		}

		// Token: 0x06002FB7 RID: 12215 RVA: 0x0009D994 File Offset: 0x0009BB94
		internal void SetActivationAttributes(object[] attributes)
		{
			this._activationAttributes = attributes;
		}

		/// <summary>Gets a list of properties that define the context in which the remote object is to be created. </summary>
		/// <returns>A <see cref="T:System.Collections.IList" /> that contains a list of properties that define the context in which the remote object is to be created.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x1700089C RID: 2204
		// (get) Token: 0x06002FB8 RID: 12216 RVA: 0x0009D9A0 File Offset: 0x0009BBA0
		public IList ContextProperties
		{
			get
			{
				if (this._contextProperties == null)
				{
					this._contextProperties = new ArrayList();
				}
				return this._contextProperties;
			}
		}

		// Token: 0x06002FB9 RID: 12217 RVA: 0x0009D9C0 File Offset: 0x0009BBC0
		internal override void InitMethodProperty(string key, object value)
		{
			switch (key)
			{
			case "__Activator":
				this._activator = (IActivator)value;
				return;
			case "__CallSiteActivationAttributes":
				this._activationAttributes = (object[])value;
				return;
			case "__ActivationType":
				this._activationType = (Type)value;
				return;
			case "__ContextProperties":
				this._contextProperties = (IList)value;
				return;
			case "__ActivationTypeName":
				this._activationTypeName = (string)value;
				return;
			}
			base.InitMethodProperty(key, value);
		}

		// Token: 0x06002FBA RID: 12218 RVA: 0x0009DAA4 File Offset: 0x0009BCA4
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			IList list = this._contextProperties;
			if (list != null && list.Count == 0)
			{
				list = null;
			}
			info.AddValue("__Activator", this._activator);
			info.AddValue("__CallSiteActivationAttributes", this._activationAttributes);
			info.AddValue("__ActivationType", null);
			info.AddValue("__ContextProperties", list);
			info.AddValue("__ActivationTypeName", this._activationTypeName);
		}

		/// <summary>Gets an <see cref="T:System.Collections.IDictionary" /> interface that represents a collection of the remoting message's properties. </summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> interface that represents a collection of the remoting message's properties.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x1700089D RID: 2205
		// (get) Token: 0x06002FBB RID: 12219 RVA: 0x0009DB20 File Offset: 0x0009BD20
		public override IDictionary Properties
		{
			get
			{
				return base.Properties;
			}
		}

		// Token: 0x1700089E RID: 2206
		// (get) Token: 0x06002FBC RID: 12220 RVA: 0x0009DB28 File Offset: 0x0009BD28
		// (set) Token: 0x06002FBD RID: 12221 RVA: 0x0009DB30 File Offset: 0x0009BD30
		internal RemotingProxy SourceProxy
		{
			get
			{
				return this._sourceProxy;
			}
			set
			{
				this._sourceProxy = value;
			}
		}

		// Token: 0x0400144C RID: 5196
		private IActivator _activator;

		// Token: 0x0400144D RID: 5197
		private object[] _activationAttributes;

		// Token: 0x0400144E RID: 5198
		private IList _contextProperties;

		// Token: 0x0400144F RID: 5199
		private Type _activationType;

		// Token: 0x04001450 RID: 5200
		private string _activationTypeName;

		// Token: 0x04001451 RID: 5201
		private bool _isContextOk;

		// Token: 0x04001452 RID: 5202
		[NonSerialized]
		private RemotingProxy _sourceProxy;
	}
}
