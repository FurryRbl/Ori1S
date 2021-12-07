﻿using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Runtime.Remoting.Messaging
{
	/// <summary>Implements the <see cref="T:System.Runtime.Remoting.Messaging.IMethodCallMessage" /> interface to create a request message that acts as a method call on a remote object.</summary>
	// Token: 0x020004A5 RID: 1189
	[ComVisible(true)]
	[CLSCompliant(false)]
	[Serializable]
	public class MethodCall : ISerializable, IInternalMessage, IMessage, IMethodCallMessage, IMethodMessage, ISerializationRootObject
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Messaging.MethodCall" /> class from an array of remoting headers.</summary>
		/// <param name="h1">An array of remoting headers that contains key/value pairs. This array is used to initialize <see cref="T:System.Runtime.Remoting.Messaging.MethodCall" /> fields for headers that belong to the namespace "http://schemas.microsoft.com/clr/soap/messageProperties".</param>
		// Token: 0x06003010 RID: 12304 RVA: 0x0009E160 File Offset: 0x0009C360
		public MethodCall(Header[] h1)
		{
			this.Init();
			if (h1 == null || h1.Length == 0)
			{
				return;
			}
			foreach (Header header in h1)
			{
				this.InitMethodProperty(header.Name, header.Value);
			}
			this.ResolveMethod();
		}

		// Token: 0x06003011 RID: 12305 RVA: 0x0009E1BC File Offset: 0x0009C3BC
		internal MethodCall(SerializationInfo info, StreamingContext context)
		{
			this.Init();
			foreach (SerializationEntry serializationEntry in info)
			{
				this.InitMethodProperty(serializationEntry.Name, serializationEntry.Value);
			}
		}

		// Token: 0x06003012 RID: 12306 RVA: 0x0009E208 File Offset: 0x0009C408
		internal MethodCall(CADMethodCallMessage msg)
		{
			this._uri = string.Copy(msg.Uri);
			ArrayList arguments = msg.GetArguments();
			this._args = msg.GetArgs(arguments);
			this._callContext = msg.GetLogicalCallContext(arguments);
			if (this._callContext == null)
			{
				this._callContext = new LogicalCallContext();
			}
			this._methodBase = msg.GetMethod();
			this.Init();
			if (msg.PropertiesCount > 0)
			{
				CADMessageBase.UnmarshalProperties(this.Properties, msg.PropertiesCount, arguments);
			}
		}

		/// <summary>Initializes  a new instance of the <see cref="T:System.Runtime.Remoting.Messaging.MethodCall" /> class by copying an existing message.</summary>
		/// <param name="msg">A remoting message.</param>
		// Token: 0x06003013 RID: 12307 RVA: 0x0009E294 File Offset: 0x0009C494
		public MethodCall(IMessage msg)
		{
			if (msg is IMethodMessage)
			{
				this.CopyFrom((IMethodMessage)msg);
			}
			else
			{
				foreach (object obj in msg.Properties)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					this.InitMethodProperty((string)dictionaryEntry.Key, dictionaryEntry.Value);
				}
				this.Init();
			}
		}

		// Token: 0x06003014 RID: 12308 RVA: 0x0009E340 File Offset: 0x0009C540
		internal MethodCall(string uri, string typeName, string methodName, object[] args)
		{
			this._uri = uri;
			this._typeName = typeName;
			this._methodName = methodName;
			this._args = args;
			this.Init();
			this.ResolveMethod();
		}

		// Token: 0x06003015 RID: 12309 RVA: 0x0009E374 File Offset: 0x0009C574
		internal MethodCall()
		{
		}

		// Token: 0x170008C3 RID: 2243
		// (get) Token: 0x06003016 RID: 12310 RVA: 0x0009E37C File Offset: 0x0009C57C
		// (set) Token: 0x06003017 RID: 12311 RVA: 0x0009E384 File Offset: 0x0009C584
		string IInternalMessage.Uri
		{
			get
			{
				return this.Uri;
			}
			set
			{
				this.Uri = value;
			}
		}

		// Token: 0x170008C4 RID: 2244
		// (get) Token: 0x06003018 RID: 12312 RVA: 0x0009E390 File Offset: 0x0009C590
		// (set) Token: 0x06003019 RID: 12313 RVA: 0x0009E398 File Offset: 0x0009C598
		Identity IInternalMessage.TargetIdentity
		{
			get
			{
				return this._targetIdentity;
			}
			set
			{
				this._targetIdentity = value;
			}
		}

		// Token: 0x0600301A RID: 12314 RVA: 0x0009E3A4 File Offset: 0x0009C5A4
		internal void CopyFrom(IMethodMessage call)
		{
			this._uri = call.Uri;
			this._typeName = call.TypeName;
			this._methodName = call.MethodName;
			this._args = call.Args;
			this._methodSignature = (Type[])call.MethodSignature;
			this._methodBase = call.MethodBase;
			this._callContext = call.LogicalCallContext;
			this.Init();
		}

		// Token: 0x0600301B RID: 12315 RVA: 0x0009E410 File Offset: 0x0009C610
		internal virtual void InitMethodProperty(string key, object value)
		{
			switch (key)
			{
			case "__TypeName":
				this._typeName = (string)value;
				return;
			case "__MethodName":
				this._methodName = (string)value;
				return;
			case "__MethodSignature":
				this._methodSignature = (Type[])value;
				return;
			case "__Args":
				this._args = (object[])value;
				return;
			case "__CallContext":
				this._callContext = (LogicalCallContext)value;
				return;
			case "__Uri":
				this._uri = (string)value;
				return;
			case "__GenericArguments":
				this._genericArguments = (Type[])value;
				return;
			}
			this.Properties[key] = value;
		}

		/// <summary>The <see cref="M:System.Runtime.Remoting.Messaging.MethodCall.GetObjectData(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext)" /> method is not implemented. </summary>
		/// <param name="info">The data for serializing or deserializing the remote object.</param>
		/// <param name="context">The context of a certain serialized stream.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter, Infrastructure" />
		/// </PermissionSet>
		// Token: 0x0600301C RID: 12316 RVA: 0x0009E534 File Offset: 0x0009C734
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("__TypeName", this._typeName);
			info.AddValue("__MethodName", this._methodName);
			info.AddValue("__MethodSignature", this._methodSignature);
			info.AddValue("__Args", this._args);
			info.AddValue("__CallContext", this._callContext);
			info.AddValue("__Uri", this._uri);
			info.AddValue("__GenericArguments", this._genericArguments);
			if (this.InternalProperties != null)
			{
				foreach (object obj in this.InternalProperties)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					info.AddValue((string)dictionaryEntry.Key, dictionaryEntry.Value);
				}
			}
		}

		/// <summary>Gets the number of arguments passed to a method. </summary>
		/// <returns>A <see cref="T:System.Int32" /> that represents the number of arguments passed to a method.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170008C5 RID: 2245
		// (get) Token: 0x0600301D RID: 12317 RVA: 0x0009E638 File Offset: 0x0009C838
		public int ArgCount
		{
			get
			{
				return this._args.Length;
			}
		}

		/// <summary>Gets an array of arguments passed to a method. </summary>
		/// <returns>An array of type <see cref="T:System.Object" /> that represents the arguments passed to a method.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170008C6 RID: 2246
		// (get) Token: 0x0600301E RID: 12318 RVA: 0x0009E644 File Offset: 0x0009C844
		public object[] Args
		{
			get
			{
				return this._args;
			}
		}

		/// <summary>Gets a value that indicates whether the method can accept a variable number of arguments. </summary>
		/// <returns>true if the method can accept a variable number of arguments; otherwise, false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170008C7 RID: 2247
		// (get) Token: 0x0600301F RID: 12319 RVA: 0x0009E64C File Offset: 0x0009C84C
		public bool HasVarArgs
		{
			get
			{
				return (this.MethodBase.CallingConvention | CallingConventions.VarArgs) != (CallingConventions)0;
			}
		}

		/// <summary>Gets the number of arguments in the method call that are not marked as out parameters.</summary>
		/// <returns>A <see cref="T:System.Int32" /> that represents the number of arguments in the method call that are not marked as out parameters.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170008C8 RID: 2248
		// (get) Token: 0x06003020 RID: 12320 RVA: 0x0009E664 File Offset: 0x0009C864
		public int InArgCount
		{
			get
			{
				if (this._inArgInfo == null)
				{
					this._inArgInfo = new ArgInfo(this._methodBase, ArgInfoType.In);
				}
				return this._inArgInfo.GetInOutArgCount();
			}
		}

		/// <summary>Gets an array of arguments in the method call that are not marked as out parameters. </summary>
		/// <returns>An array of type <see cref="T:System.Object" /> that represents arguments in the method call that are not marked as out parameters.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170008C9 RID: 2249
		// (get) Token: 0x06003021 RID: 12321 RVA: 0x0009E69C File Offset: 0x0009C89C
		public object[] InArgs
		{
			get
			{
				if (this._inArgInfo == null)
				{
					this._inArgInfo = new ArgInfo(this._methodBase, ArgInfoType.In);
				}
				return this._inArgInfo.GetInOutArgs(this._args);
			}
		}

		/// <summary>Gets the <see cref="T:System.Runtime.Remoting.Messaging.LogicalCallContext" /> for the current method call. </summary>
		/// <returns>The <see cref="T:System.Runtime.Remoting.Messaging.LogicalCallContext" /> for the current method call.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170008CA RID: 2250
		// (get) Token: 0x06003022 RID: 12322 RVA: 0x0009E6D8 File Offset: 0x0009C8D8
		public LogicalCallContext LogicalCallContext
		{
			get
			{
				if (this._callContext == null)
				{
					this._callContext = new LogicalCallContext();
				}
				return this._callContext;
			}
		}

		/// <summary>Gets the <see cref="T:System.Reflection.MethodBase" /> of the called method. </summary>
		/// <returns>The <see cref="T:System.Reflection.MethodBase" /> of the called method.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170008CB RID: 2251
		// (get) Token: 0x06003023 RID: 12323 RVA: 0x0009E6F8 File Offset: 0x0009C8F8
		public MethodBase MethodBase
		{
			get
			{
				if (this._methodBase == null)
				{
					this.ResolveMethod();
				}
				return this._methodBase;
			}
		}

		/// <summary>Gets the name of the invoked method. </summary>
		/// <returns>A <see cref="T:System.String" /> that contains the name of the invoked method.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170008CC RID: 2252
		// (get) Token: 0x06003024 RID: 12324 RVA: 0x0009E714 File Offset: 0x0009C914
		public string MethodName
		{
			get
			{
				if (this._methodName == null)
				{
					this._methodName = this._methodBase.Name;
				}
				return this._methodName;
			}
		}

		/// <summary>Gets an object that contains the method signature. </summary>
		/// <returns>A <see cref="T:System.Object" /> that contains the method signature.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170008CD RID: 2253
		// (get) Token: 0x06003025 RID: 12325 RVA: 0x0009E744 File Offset: 0x0009C944
		public object MethodSignature
		{
			get
			{
				if (this._methodSignature == null && this._methodBase != null)
				{
					ParameterInfo[] parameters = this._methodBase.GetParameters();
					this._methodSignature = new Type[parameters.Length];
					for (int i = 0; i < parameters.Length; i++)
					{
						this._methodSignature[i] = parameters[i].ParameterType;
					}
				}
				return this._methodSignature;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.IDictionary" /> interface that represents a collection of the remoting message's properties. </summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> interface that represents a collection of the remoting message's properties.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170008CE RID: 2254
		// (get) Token: 0x06003026 RID: 12326 RVA: 0x0009E7AC File Offset: 0x0009C9AC
		public virtual IDictionary Properties
		{
			get
			{
				if (this.ExternalProperties == null)
				{
					this.InitDictionary();
				}
				return this.ExternalProperties;
			}
		}

		// Token: 0x06003027 RID: 12327 RVA: 0x0009E7C8 File Offset: 0x0009C9C8
		internal virtual void InitDictionary()
		{
			MethodCallDictionary methodCallDictionary = new MethodCallDictionary(this);
			this.ExternalProperties = methodCallDictionary;
			this.InternalProperties = methodCallDictionary.GetInternalProperties();
		}

		/// <summary>Gets the full type name of the remote object on which the method call is being made. </summary>
		/// <returns>A <see cref="T:System.String" /> that contains the full type name of the remote object on which the method call is being made.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170008CF RID: 2255
		// (get) Token: 0x06003028 RID: 12328 RVA: 0x0009E7F0 File Offset: 0x0009C9F0
		public string TypeName
		{
			get
			{
				if (this._typeName == null)
				{
					this._typeName = this._methodBase.DeclaringType.AssemblyQualifiedName;
				}
				return this._typeName;
			}
		}

		/// <summary>Gets or sets the Uniform Resource Identifier (URI) of the remote object on which the method call is being made.</summary>
		/// <returns>The URI of a remote object.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170008D0 RID: 2256
		// (get) Token: 0x06003029 RID: 12329 RVA: 0x0009E81C File Offset: 0x0009CA1C
		// (set) Token: 0x0600302A RID: 12330 RVA: 0x0009E824 File Offset: 0x0009CA24
		public string Uri
		{
			get
			{
				return this._uri;
			}
			set
			{
				this._uri = value;
			}
		}

		/// <summary>Gets a method argument, as an object, at a specified index. </summary>
		/// <returns>The method argument as an object.</returns>
		/// <param name="argNum">The index of the requested argument.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x0600302B RID: 12331 RVA: 0x0009E830 File Offset: 0x0009CA30
		public object GetArg(int argNum)
		{
			return this._args[argNum];
		}

		/// <summary>Gets the name of a method argument at a specified index. </summary>
		/// <returns>The name of the method argument.</returns>
		/// <param name="index">The index of the requested argument.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x0600302C RID: 12332 RVA: 0x0009E83C File Offset: 0x0009CA3C
		public string GetArgName(int index)
		{
			return this._methodBase.GetParameters()[index].Name;
		}

		/// <summary>Gets a method argument at a specified index that is not marked as an out parameter. </summary>
		/// <returns>The method argument that is not marked as an out parameter.</returns>
		/// <param name="argNum">The index of the requested argument.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x0600302D RID: 12333 RVA: 0x0009E850 File Offset: 0x0009CA50
		public object GetInArg(int argNum)
		{
			if (this._inArgInfo == null)
			{
				this._inArgInfo = new ArgInfo(this._methodBase, ArgInfoType.In);
			}
			return this._args[this._inArgInfo.GetInOutArgIndex(argNum)];
		}

		/// <summary>Gets the name of a method argument at a specified index that is not marked as an out parameter.</summary>
		/// <returns>The name of the method argument that is not marked as an out parameter.</returns>
		/// <param name="index">The index of the requested argument. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x0600302E RID: 12334 RVA: 0x0009E890 File Offset: 0x0009CA90
		public string GetInArgName(int index)
		{
			if (this._inArgInfo == null)
			{
				this._inArgInfo = new ArgInfo(this._methodBase, ArgInfoType.In);
			}
			return this._inArgInfo.GetInOutArgName(index);
		}

		/// <summary>Initializes an internal serialization handler from an array of remoting headers that are applied to a method. </summary>
		/// <returns>An internal serialization handler.</returns>
		/// <param name="h">An array of remoting headers that contain key/value pairs. This array is used to initialize <see cref="T:System.Runtime.Remoting.Messaging.MethodCall" /> fields for headers that belong to the namespace "http://schemas.microsoft.com/clr/soap/messageProperties".</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x0600302F RID: 12335 RVA: 0x0009E8BC File Offset: 0x0009CABC
		[MonoTODO]
		public virtual object HeaderHandler(Header[] h)
		{
			throw new NotImplementedException();
		}

		/// <summary>Initializes a <see cref="T:System.Runtime.Remoting.Messaging.MethodCall" />. </summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06003030 RID: 12336 RVA: 0x0009E8C4 File Offset: 0x0009CAC4
		public virtual void Init()
		{
		}

		/// <summary>Sets method information from previously initialized remoting message properties. </summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06003031 RID: 12337 RVA: 0x0009E8C8 File Offset: 0x0009CAC8
		public void ResolveMethod()
		{
			if (this._uri != null)
			{
				Type serverTypeForUri = RemotingServices.GetServerTypeForUri(this._uri);
				if (serverTypeForUri == null)
				{
					string str = (this._typeName == null) ? string.Empty : (" (" + this._typeName + ")");
					throw new RemotingException("Requested service not found" + str + ". No receiver for uri " + this._uri);
				}
				Type type = this.CastTo(this._typeName, serverTypeForUri);
				if (type == null)
				{
					throw new RemotingException(string.Concat(new string[]
					{
						"Cannot cast from client type '",
						this._typeName,
						"' to server type '",
						serverTypeForUri.FullName,
						"'"
					}));
				}
				this._methodBase = RemotingServices.GetMethodBaseFromName(type, this._methodName, this._methodSignature);
				if (this._methodBase == null)
				{
					throw new RemotingException(string.Concat(new object[]
					{
						"Method ",
						this._methodName,
						" not found in ",
						type
					}));
				}
				if (type != serverTypeForUri && type.IsInterface && !serverTypeForUri.IsInterface)
				{
					this._methodBase = RemotingServices.GetVirtualMethod(serverTypeForUri, this._methodBase);
					if (this._methodBase == null)
					{
						throw new RemotingException(string.Concat(new object[]
						{
							"Method ",
							this._methodName,
							" not found in ",
							serverTypeForUri
						}));
					}
				}
			}
			else
			{
				this._methodBase = RemotingServices.GetMethodBaseFromMethodMessage(this);
				if (this._methodBase == null)
				{
					throw new RemotingException("Method " + this._methodName + " not found in " + this.TypeName);
				}
			}
			if (this._methodBase.IsGenericMethod && this._methodBase.ContainsGenericParameters)
			{
				if (this.GenericArguments == null)
				{
					throw new RemotingException("The remoting infrastructure does not support open generic methods.");
				}
				this._methodBase = ((MethodInfo)this._methodBase).MakeGenericMethod(this.GenericArguments);
			}
		}

		// Token: 0x06003032 RID: 12338 RVA: 0x0009EAD0 File Offset: 0x0009CCD0
		private Type CastTo(string clientType, Type serverType)
		{
			clientType = MethodCall.GetTypeNameFromAssemblyQualifiedName(clientType);
			if (clientType == serverType.FullName)
			{
				return serverType;
			}
			for (Type baseType = serverType.BaseType; baseType != null; baseType = baseType.BaseType)
			{
				if (clientType == baseType.FullName)
				{
					return baseType;
				}
			}
			Type[] interfaces = serverType.GetInterfaces();
			foreach (Type type in interfaces)
			{
				if (clientType == type.FullName)
				{
					return type;
				}
			}
			return null;
		}

		// Token: 0x06003033 RID: 12339 RVA: 0x0009EB60 File Offset: 0x0009CD60
		private static string GetTypeNameFromAssemblyQualifiedName(string aqname)
		{
			int num = aqname.IndexOf("]]");
			int num2 = aqname.IndexOf(',', (num != -1) ? (num + 2) : 0);
			if (num2 != -1)
			{
				aqname = aqname.Substring(0, num2).Trim();
			}
			return aqname;
		}

		/// <summary>Sets method information from serialization settings. </summary>
		/// <param name="info">The data for serializing or deserializing the remote object.</param>
		/// <param name="ctx">The context of a given serialized stream.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06003034 RID: 12340 RVA: 0x0009EBAC File Offset: 0x0009CDAC
		[MonoTODO]
		public void RootSetObjectData(SerializationInfo info, StreamingContext ctx)
		{
			throw new NotImplementedException();
		}

		// Token: 0x170008D1 RID: 2257
		// (get) Token: 0x06003035 RID: 12341 RVA: 0x0009EBB4 File Offset: 0x0009CDB4
		private Type[] GenericArguments
		{
			get
			{
				if (this._genericArguments != null)
				{
					return this._genericArguments;
				}
				return this._genericArguments = this.MethodBase.GetGenericArguments();
			}
		}

		// Token: 0x04001461 RID: 5217
		private string _uri;

		// Token: 0x04001462 RID: 5218
		private string _typeName;

		// Token: 0x04001463 RID: 5219
		private string _methodName;

		// Token: 0x04001464 RID: 5220
		private object[] _args;

		// Token: 0x04001465 RID: 5221
		private Type[] _methodSignature;

		// Token: 0x04001466 RID: 5222
		private MethodBase _methodBase;

		// Token: 0x04001467 RID: 5223
		private LogicalCallContext _callContext;

		// Token: 0x04001468 RID: 5224
		private ArgInfo _inArgInfo;

		// Token: 0x04001469 RID: 5225
		private Identity _targetIdentity;

		// Token: 0x0400146A RID: 5226
		private Type[] _genericArguments;

		/// <summary>An <see cref="T:System.Collections.IDictionary" /> interface that represents a collection of the remoting message's properties. </summary>
		// Token: 0x0400146B RID: 5227
		protected IDictionary ExternalProperties;

		/// <summary>An <see cref="T:System.Collections.IDictionary" /> interface that represents a collection of the remoting message's properties. </summary>
		// Token: 0x0400146C RID: 5228
		protected IDictionary InternalProperties;
	}
}
