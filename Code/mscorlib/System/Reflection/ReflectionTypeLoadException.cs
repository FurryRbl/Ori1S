using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Reflection
{
	/// <summary>The exception that is thrown by the <see cref="M:System.Reflection.Module.GetTypes" /> method if any of the classes in a module cannot be loaded. This class cannot be inherited.</summary>
	// Token: 0x020002B7 RID: 695
	[ComVisible(true)]
	[Serializable]
	public sealed class ReflectionTypeLoadException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.ReflectionTypeLoadException" /> class with the given classes and their associated exceptions.</summary>
		/// <param name="classes">An array of type Type containing the classes that were defined in the module and loaded. This array can contain null reference (Nothing in Visual Basic) values. </param>
		/// <param name="exceptions">An array of type Exception containing the exceptions that were thrown by the class loader. The null reference (Nothing in Visual Basic) values in the <paramref name="classes" /> array line up with the exceptions in this <paramref name="exceptions" /> array. </param>
		// Token: 0x06002336 RID: 9014 RVA: 0x0007E0D0 File Offset: 0x0007C2D0
		public ReflectionTypeLoadException(Type[] classes, Exception[] exceptions) : base(Locale.GetText("The classes in the module cannot be loaded."))
		{
			this.loaderExceptions = exceptions;
			this.types = classes;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.ReflectionTypeLoadException" /> class with the given classes, their associated exceptions, and exception descriptions.</summary>
		/// <param name="classes">An array of type Type containing the classes that were defined in the module and loaded. This array can contain null reference (Nothing in Visual Basic) values. </param>
		/// <param name="exceptions">An array of type Exception containing the exceptions that were thrown by the class loader. The null reference (Nothing in Visual Basic) values in the <paramref name="classes" /> array line up with the exceptions in this <paramref name="exceptions" /> array. </param>
		/// <param name="message">A String describing the reason the exception was thrown. </param>
		// Token: 0x06002337 RID: 9015 RVA: 0x0007E0F0 File Offset: 0x0007C2F0
		public ReflectionTypeLoadException(Type[] classes, Exception[] exceptions, string message) : base(message)
		{
			this.loaderExceptions = exceptions;
			this.types = classes;
		}

		// Token: 0x06002338 RID: 9016 RVA: 0x0007E108 File Offset: 0x0007C308
		private ReflectionTypeLoadException(SerializationInfo info, StreamingContext sc) : base(info, sc)
		{
			this.types = (Type[])info.GetValue("Types", typeof(Type[]));
			this.loaderExceptions = (Exception[])info.GetValue("Exceptions", typeof(Exception[]));
		}

		/// <summary>Gets the array of classes that were defined in the module and loaded.</summary>
		/// <returns>An array of type Type containing the classes that were defined in the module and loaded. This array can contain some null values.</returns>
		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x06002339 RID: 9017 RVA: 0x0007E160 File Offset: 0x0007C360
		public Type[] Types
		{
			get
			{
				return this.types;
			}
		}

		/// <summary>Gets the array of exceptions thrown by the class loader.</summary>
		/// <returns>An array of type Exception containing the exceptions thrown by the class loader. The null values in the <paramref name="classes" /> array of this instance line up with the exceptions in this array.</returns>
		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x0600233A RID: 9018 RVA: 0x0007E168 File Offset: 0x0007C368
		public Exception[] LoaderExceptions
		{
			get
			{
				return this.loaderExceptions;
			}
		}

		/// <summary>Provides an <see cref="T:System.Runtime.Serialization.ISerializable" /> implementation for serialized objects.</summary>
		/// <param name="info">The information and data needed to serialize or deserialize an object. </param>
		/// <param name="context">The context for the serialization. </param>
		/// <exception cref="T:System.ArgumentNullException">info is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600233B RID: 9019 RVA: 0x0007E170 File Offset: 0x0007C370
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("Types", this.types);
			info.AddValue("Exceptions", this.loaderExceptions);
		}

		// Token: 0x04000D35 RID: 3381
		private Exception[] loaderExceptions;

		// Token: 0x04000D36 RID: 3382
		private Type[] types;
	}
}
