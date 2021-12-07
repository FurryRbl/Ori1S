using System;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Represents type declarations: class types, interface types, array types, value types, enumeration types, type parameters, generic type definitions, and open or closed constructed generic types.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000033 RID: 51
	[ComVisible(true)]
	[ComDefaultInterface(typeof(_Type))]
	[ClassInterface(ClassInterfaceType.None)]
	[Serializable]
	public abstract class Type : MemberInfo, IReflect, _Type
	{
		/// <summary>Maps a set of names to a corresponding set of dispatch identifiers.</summary>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="rgszNames">Passed-in array of names to be mapped.</param>
		/// <param name="cNames">Count of the names to be mapped.</param>
		/// <param name="lcid">The locale context in which to interpret the names.</param>
		/// <param name="rgDispId">Caller-allocated array which receives the IDs corresponding to the names.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x060004BF RID: 1215 RVA: 0x0001296C File Offset: 0x00010B6C
		void _Type.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the type information for an object, which can then be used to get the type information for an interface.</summary>
		/// <param name="iTInfo">The type information to return.</param>
		/// <param name="lcid">The locale identifier for the type information.</param>
		/// <param name="ppTInfo">Receives a pointer to the requested type information object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x060004C0 RID: 1216 RVA: 0x00012974 File Offset: 0x00010B74
		void _Type.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the number of type information interfaces that an object provides (either 0 or 1).</summary>
		/// <param name="pcTInfo">Points to a location that receives the number of type information interfaces provided by the object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x060004C1 RID: 1217 RVA: 0x0001297C File Offset: 0x00010B7C
		void _Type.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Provides access to properties and methods exposed by an object.</summary>
		/// <param name="dispIdMember">Identifies the member.</param>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="lcid">The locale context in which to interpret arguments.</param>
		/// <param name="wFlags">Flags describing the context of the call.</param>
		/// <param name="pDispParams">Pointer to a structure containing an array of arguments, an array of argument DISPIDs for named arguments, and counts for the number of elements in the arrays.</param>
		/// <param name="pVarResult">Pointer to the location where the result is to be stored.</param>
		/// <param name="pExcepInfo">Pointer to a structure that contains exception information.</param>
		/// <param name="puArgErr">The index of the first argument that has an error.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x060004C2 RID: 1218 RVA: 0x00012984 File Offset: 0x00010B84
		void _Type.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x0001298C File Offset: 0x00010B8C
		private static bool FilterName_impl(MemberInfo m, object filterCriteria)
		{
			string text = (string)filterCriteria;
			if (text == null || text.Length == 0)
			{
				return false;
			}
			if (text[text.Length - 1] == '*')
			{
				return string.Compare(text, 0, m.Name, 0, text.Length - 1, false, CultureInfo.InvariantCulture) == 0;
			}
			return text.Equals(m.Name);
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x000129F4 File Offset: 0x00010BF4
		private static bool FilterNameIgnoreCase_impl(MemberInfo m, object filterCriteria)
		{
			string text = (string)filterCriteria;
			if (text == null || text.Length == 0)
			{
				return false;
			}
			if (text[text.Length - 1] == '*')
			{
				return string.Compare(text, 0, m.Name, 0, text.Length - 1, true, CultureInfo.InvariantCulture) == 0;
			}
			return string.Compare(text, m.Name, true, CultureInfo.InvariantCulture) == 0;
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x00012A68 File Offset: 0x00010C68
		private static bool FilterAttribute_impl(MemberInfo m, object filterCriteria)
		{
			int num = ((IConvertible)filterCriteria).ToInt32(null);
			if (m is MethodInfo)
			{
				return (((MethodInfo)m).Attributes & (MethodAttributes)num) != MethodAttributes.PrivateScope;
			}
			if (m is FieldInfo)
			{
				return (((FieldInfo)m).Attributes & (FieldAttributes)num) != FieldAttributes.PrivateScope;
			}
			if (m is PropertyInfo)
			{
				return (((PropertyInfo)m).Attributes & (PropertyAttributes)num) != PropertyAttributes.None;
			}
			return m is EventInfo && (((EventInfo)m).Attributes & (EventAttributes)num) != EventAttributes.None;
		}

		/// <summary>Gets the <see cref="T:System.Reflection.Assembly" /> in which the type is declared. For generic types, gets the <see cref="T:System.Reflection.Assembly" /> in which the generic type is defined.</summary>
		/// <returns>An <see cref="T:System.Reflection.Assembly" /> instance that describes the assembly containing the current type. For generic types, the instance describes the assembly that contains the generic type definition, not the assembly that creates and uses a particular constructed type.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060004C6 RID: 1222
		public abstract Assembly Assembly { get; }

		/// <summary>Gets the assembly-qualified name of the <see cref="T:System.Type" />, which includes the name of the assembly from which the <see cref="T:System.Type" /> was loaded.</summary>
		/// <returns>The assembly-qualified name of the <see cref="T:System.Type" />, which includes the name of the assembly from which the <see cref="T:System.Type" /> was loaded, or null if the current instance represents a generic type parameter.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060004C7 RID: 1223
		public abstract string AssemblyQualifiedName { get; }

		/// <summary>Gets the attributes associated with the <see cref="T:System.Type" />.</summary>
		/// <returns>A <see cref="T:System.Reflection.TypeAttributes" /> object representing the attribute set of the <see cref="T:System.Type" />, unless the <see cref="T:System.Type" /> represents a generic type parameter, in which case the value is unspecified. </returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060004C8 RID: 1224 RVA: 0x00012B00 File Offset: 0x00010D00
		public TypeAttributes Attributes
		{
			get
			{
				return this.GetAttributeFlagsImpl();
			}
		}

		/// <summary>Gets the type from which the current <see cref="T:System.Type" /> directly inherits.</summary>
		/// <returns>The <see cref="T:System.Type" /> from which the current <see cref="T:System.Type" /> directly inherits, or null if the current Type represents the <see cref="T:System.Object" /> class or an interface.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060004C9 RID: 1225
		public abstract Type BaseType { get; }

		/// <summary>Gets the type that declares the current nested type or generic type parameter.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing the enclosing type, if the current type is a nested type; or the generic type definition, if the current type is a type parameter of a generic type; or the type that declares the generic method, if the current type is a type parameter of a generic method; otherwise, null.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060004CA RID: 1226 RVA: 0x00012B08 File Offset: 0x00010D08
		public override Type DeclaringType
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets a reference to the default binder, which implements internal rules for selecting the appropriate members to be called by <see cref="M:System.Type.InvokeMember(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Object,System.Object[],System.Reflection.ParameterModifier[],System.Globalization.CultureInfo,System.String[])" />.</summary>
		/// <returns>A reference to the default binder used by the system.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060004CB RID: 1227 RVA: 0x00012B0C File Offset: 0x00010D0C
		public static Binder DefaultBinder
		{
			get
			{
				return Binder.DefaultBinder;
			}
		}

		/// <summary>Gets the fully qualified name of the <see cref="T:System.Type" />, including the namespace of the <see cref="T:System.Type" /> but not the assembly.</summary>
		/// <returns>The fully qualified name of the <see cref="T:System.Type" />, including the namespace of the <see cref="T:System.Type" /> but not the assembly; or null if the current instance represents a generic type parameter, an array type, pointer type, or byref type based on a type parameter, or a generic type that is not a generic type definition but contains unresolved type parameters.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060004CC RID: 1228
		public abstract string FullName { get; }

		/// <summary>Gets the GUID associated with the <see cref="T:System.Type" />.</summary>
		/// <returns>The GUID associated with the <see cref="T:System.Type" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060004CD RID: 1229
		public abstract Guid GUID { get; }

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Type" /> encompasses or refers to another type; that is, whether the current <see cref="T:System.Type" /> is an array, a pointer, or is passed by reference.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is an array, a pointer, or is passed by reference; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060004CE RID: 1230 RVA: 0x00012B14 File Offset: 0x00010D14
		public bool HasElementType
		{
			get
			{
				return this.HasElementTypeImpl();
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> is abstract and must be overridden.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is abstract; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060004CF RID: 1231 RVA: 0x00012B1C File Offset: 0x00010D1C
		public bool IsAbstract
		{
			get
			{
				return (this.Attributes & TypeAttributes.Abstract) != TypeAttributes.NotPublic;
			}
		}

		/// <summary>Gets a value indicating whether the string format attribute AnsiClass is selected for the <see cref="T:System.Type" />.</summary>
		/// <returns>true if the string format attribute AnsiClass is selected for the <see cref="T:System.Type" />; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060004D0 RID: 1232 RVA: 0x00012B30 File Offset: 0x00010D30
		public bool IsAnsiClass
		{
			get
			{
				return (this.Attributes & TypeAttributes.StringFormatMask) == TypeAttributes.NotPublic;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> is an array.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is an array; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060004D1 RID: 1233 RVA: 0x00012B44 File Offset: 0x00010D44
		public bool IsArray
		{
			get
			{
				return this.IsArrayImpl();
			}
		}

		/// <summary>Gets a value indicating whether the string format attribute AutoClass is selected for the <see cref="T:System.Type" />.</summary>
		/// <returns>true if the string format attribute AutoClass is selected for the <see cref="T:System.Type" />; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060004D2 RID: 1234 RVA: 0x00012B4C File Offset: 0x00010D4C
		public bool IsAutoClass
		{
			get
			{
				return (this.Attributes & TypeAttributes.StringFormatMask) == TypeAttributes.AutoClass;
			}
		}

		/// <summary>Gets a value indicating whether the class layout attribute AutoLayout is selected for the <see cref="T:System.Type" />.</summary>
		/// <returns>true if the class layout attribute AutoLayout is selected for the <see cref="T:System.Type" />; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060004D3 RID: 1235 RVA: 0x00012B64 File Offset: 0x00010D64
		public bool IsAutoLayout
		{
			get
			{
				return (this.Attributes & TypeAttributes.LayoutMask) == TypeAttributes.NotPublic;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> is passed by reference.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is passed by reference; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060004D4 RID: 1236 RVA: 0x00012B74 File Offset: 0x00010D74
		public bool IsByRef
		{
			get
			{
				return this.IsByRefImpl();
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> is a class; that is, not a value type or interface.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is a class; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060004D5 RID: 1237 RVA: 0x00012B7C File Offset: 0x00010D7C
		public bool IsClass
		{
			get
			{
				return !this.IsInterface && !this.IsValueType;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> is a COM object.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is a COM object; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060004D6 RID: 1238 RVA: 0x00012B94 File Offset: 0x00010D94
		public bool IsCOMObject
		{
			get
			{
				return this.IsCOMObjectImpl();
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> can be hosted in a context.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> can be hosted in a context; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060004D7 RID: 1239 RVA: 0x00012B9C File Offset: 0x00010D9C
		public bool IsContextful
		{
			get
			{
				return this.IsContextfulImpl();
			}
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Type" /> represents an enumeration.</summary>
		/// <returns>true if the current <see cref="T:System.Type" /> represents an enumeration; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060004D8 RID: 1240 RVA: 0x00012BA4 File Offset: 0x00010DA4
		public bool IsEnum
		{
			get
			{
				return this.IsSubclassOf(typeof(Enum));
			}
		}

		/// <summary>Gets a value indicating whether the class layout attribute ExplicitLayout is selected for the <see cref="T:System.Type" />.</summary>
		/// <returns>true if the class layout attribute ExplicitLayout is selected for the <see cref="T:System.Type" />; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060004D9 RID: 1241 RVA: 0x00012BB8 File Offset: 0x00010DB8
		public bool IsExplicitLayout
		{
			get
			{
				return (this.Attributes & TypeAttributes.LayoutMask) == TypeAttributes.ExplicitLayout;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> has a <see cref="T:System.Runtime.InteropServices.ComImportAttribute" /> attribute applied, indicating that it was imported from a COM type library.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> has a <see cref="T:System.Runtime.InteropServices.ComImportAttribute" />; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060004DA RID: 1242 RVA: 0x00012BC8 File Offset: 0x00010DC8
		public bool IsImport
		{
			get
			{
				return (this.Attributes & TypeAttributes.Import) != TypeAttributes.NotPublic;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> is an interface; that is, not a class or a value type.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is an interface; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060004DB RID: 1243 RVA: 0x00012BDC File Offset: 0x00010DDC
		public bool IsInterface
		{
			get
			{
				return (this.Attributes & TypeAttributes.ClassSemanticsMask) == TypeAttributes.ClassSemanticsMask;
			}
		}

		/// <summary>Gets a value indicating whether the class layout attribute SequentialLayout is selected for the <see cref="T:System.Type" />.</summary>
		/// <returns>true if the class layout attribute SequentialLayout is selected for the <see cref="T:System.Type" />; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060004DC RID: 1244 RVA: 0x00012BEC File Offset: 0x00010DEC
		public bool IsLayoutSequential
		{
			get
			{
				return (this.Attributes & TypeAttributes.LayoutMask) == TypeAttributes.SequentialLayout;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> is marshaled by reference.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is marshaled by reference; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060004DD RID: 1245 RVA: 0x00012BFC File Offset: 0x00010DFC
		public bool IsMarshalByRef
		{
			get
			{
				return this.IsMarshalByRefImpl();
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> is nested and visible only within its own assembly.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is nested and visible only within its own assembly; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060004DE RID: 1246 RVA: 0x00012C04 File Offset: 0x00010E04
		public bool IsNestedAssembly
		{
			get
			{
				return (this.Attributes & TypeAttributes.VisibilityMask) == TypeAttributes.NestedAssembly;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> is nested and visible only to classes that belong to both its own family and its own assembly.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is nested and visible only to classes that belong to both its own family and its own assembly; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060004DF RID: 1247 RVA: 0x00012C14 File Offset: 0x00010E14
		public bool IsNestedFamANDAssem
		{
			get
			{
				return (this.Attributes & TypeAttributes.VisibilityMask) == TypeAttributes.NestedFamANDAssem;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> is nested and visible only within its own family.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is nested and visible only within its own family; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060004E0 RID: 1248 RVA: 0x00012C24 File Offset: 0x00010E24
		public bool IsNestedFamily
		{
			get
			{
				return (this.Attributes & TypeAttributes.VisibilityMask) == TypeAttributes.NestedFamily;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> is nested and visible only to classes that belong to either its own family or to its own assembly.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is nested and visible only to classes that belong to its own family or to its own assembly; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060004E1 RID: 1249 RVA: 0x00012C34 File Offset: 0x00010E34
		public bool IsNestedFamORAssem
		{
			get
			{
				return (this.Attributes & TypeAttributes.VisibilityMask) == TypeAttributes.VisibilityMask;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> is nested and declared private.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is nested and declared private; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060004E2 RID: 1250 RVA: 0x00012C44 File Offset: 0x00010E44
		public bool IsNestedPrivate
		{
			get
			{
				return (this.Attributes & TypeAttributes.VisibilityMask) == TypeAttributes.NestedPrivate;
			}
		}

		/// <summary>Gets a value indicating whether a class is nested and declared public.</summary>
		/// <returns>true if the class is nested and declared public; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060004E3 RID: 1251 RVA: 0x00012C54 File Offset: 0x00010E54
		public bool IsNestedPublic
		{
			get
			{
				return (this.Attributes & TypeAttributes.VisibilityMask) == TypeAttributes.NestedPublic;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> is not declared public.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is not declared public and is not a nested type; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060004E4 RID: 1252 RVA: 0x00012C64 File Offset: 0x00010E64
		public bool IsNotPublic
		{
			get
			{
				return (this.Attributes & TypeAttributes.VisibilityMask) == TypeAttributes.NotPublic;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> is a pointer.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is a pointer; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060004E5 RID: 1253 RVA: 0x00012C74 File Offset: 0x00010E74
		public bool IsPointer
		{
			get
			{
				return this.IsPointerImpl();
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> is one of the primitive types.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is one of the primitive types; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060004E6 RID: 1254 RVA: 0x00012C7C File Offset: 0x00010E7C
		public bool IsPrimitive
		{
			get
			{
				return this.IsPrimitiveImpl();
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> is declared public.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is declared public and is not a nested type; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060004E7 RID: 1255 RVA: 0x00012C84 File Offset: 0x00010E84
		public bool IsPublic
		{
			get
			{
				return (this.Attributes & TypeAttributes.VisibilityMask) == TypeAttributes.Public;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> is declared sealed.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is declared sealed; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060004E8 RID: 1256 RVA: 0x00012C94 File Offset: 0x00010E94
		public bool IsSealed
		{
			get
			{
				return (this.Attributes & TypeAttributes.Sealed) != TypeAttributes.NotPublic;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> is serializable.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is serializable; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060004E9 RID: 1257 RVA: 0x00012CA8 File Offset: 0x00010EA8
		public bool IsSerializable
		{
			get
			{
				if ((this.Attributes & TypeAttributes.Serializable) != TypeAttributes.NotPublic)
				{
					return true;
				}
				Type type = this.UnderlyingSystemType;
				if (type == null)
				{
					return false;
				}
				if (type.IsSystemType)
				{
					return Type.type_is_subtype_of(type, typeof(Enum), false) || Type.type_is_subtype_of(type, typeof(Delegate), false);
				}
				while (type != typeof(Enum) && type != typeof(Delegate))
				{
					type = type.BaseType;
					if (type == null)
					{
						return false;
					}
				}
				return true;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> has a name that requires special handling.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> has a name that requires special handling; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060004EA RID: 1258 RVA: 0x00012D40 File Offset: 0x00010F40
		public bool IsSpecialName
		{
			get
			{
				return (this.Attributes & TypeAttributes.SpecialName) != TypeAttributes.NotPublic;
			}
		}

		/// <summary>Gets a value indicating whether the string format attribute UnicodeClass is selected for the <see cref="T:System.Type" />.</summary>
		/// <returns>true if the string format attribute UnicodeClass is selected for the <see cref="T:System.Type" />; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060004EB RID: 1259 RVA: 0x00012D54 File Offset: 0x00010F54
		public bool IsUnicodeClass
		{
			get
			{
				return (this.Attributes & TypeAttributes.StringFormatMask) == TypeAttributes.UnicodeClass;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> is a value type.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is a value type; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060004EC RID: 1260 RVA: 0x00012D6C File Offset: 0x00010F6C
		public bool IsValueType
		{
			get
			{
				return this.IsValueTypeImpl();
			}
		}

		/// <summary>Gets a <see cref="T:System.Reflection.MemberTypes" /> value indicating that this member is a type or a nested type.</summary>
		/// <returns>A <see cref="T:System.Reflection.MemberTypes" /> value indicating that this member is a type or a nested type.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060004ED RID: 1261 RVA: 0x00012D74 File Offset: 0x00010F74
		public override MemberTypes MemberType
		{
			get
			{
				return MemberTypes.TypeInfo;
			}
		}

		/// <summary>Gets the module (the DLL) in which the current <see cref="T:System.Type" /> is defined.</summary>
		/// <returns>The module in which the current <see cref="T:System.Type" /> is defined.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060004EE RID: 1262
		public abstract override Module Module { get; }

		/// <summary>Gets the namespace of the <see cref="T:System.Type" />.</summary>
		/// <returns>The namespace of the <see cref="T:System.Type" />; null if the current instance has no namespace, or represents a generic parameter.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060004EF RID: 1263
		public abstract string Namespace { get; }

		/// <summary>Gets the class object that was used to obtain this member. </summary>
		/// <returns>The Type object through which this MemberInfo object was obtained. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060004F0 RID: 1264 RVA: 0x00012D78 File Offset: 0x00010F78
		public override Type ReflectedType
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets the handle for the current <see cref="T:System.Type" />.</summary>
		/// <returns>The handle for the current <see cref="T:System.Type" />.</returns>
		/// <exception cref="T:System.NotSupportedException">The .NET Compact Framework does not currently support this property.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060004F1 RID: 1265 RVA: 0x00012D7C File Offset: 0x00010F7C
		public virtual RuntimeTypeHandle TypeHandle
		{
			get
			{
				return default(RuntimeTypeHandle);
			}
		}

		/// <summary>Gets the initializer for the <see cref="T:System.Type" />.</summary>
		/// <returns>A <see cref="T:System.Reflection.ConstructorInfo" /> containing the name of the class constructor for the <see cref="T:System.Type" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060004F2 RID: 1266 RVA: 0x00012D94 File Offset: 0x00010F94
		[ComVisible(true)]
		public ConstructorInfo TypeInitializer
		{
			get
			{
				return this.GetConstructorImpl(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, CallingConventions.Any, Type.EmptyTypes, null);
			}
		}

		/// <summary>Indicates the type provided by the common language runtime that represents this type.</summary>
		/// <returns>The underlying system type for the <see cref="T:System.Type" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060004F3 RID: 1267
		public abstract Type UnderlyingSystemType { get; }

		/// <summary>Determines if the underlying system type of the current <see cref="T:System.Type" /> is the same as the underlying system type of the specified <see cref="T:System.Object" />.</summary>
		/// <returns>true if the underlying system type of <paramref name="o" /> is the same as the underlying system type of the current <see cref="T:System.Type" />; otherwise, false. This method also returns false if the object specified by the <paramref name="o" /> parameter is not a Type.</returns>
		/// <param name="o">The <see cref="T:System.Object" /> whose underlying system type is to be compared with the underlying system type of the current <see cref="T:System.Type" />. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060004F4 RID: 1268 RVA: 0x00012DA8 File Offset: 0x00010FA8
		public override bool Equals(object o)
		{
			if (o == null)
			{
				return false;
			}
			Type type = o as Type;
			return type != null && this.Equals(type);
		}

		/// <summary>Determines if the underlying system type of the current <see cref="T:System.Type" /> is the same as the underlying system type of the specified <see cref="T:System.Type" />.</summary>
		/// <returns>true if the underlying system type of <paramref name="o" /> is the same as the underlying system type of the current <see cref="T:System.Type" />; otherwise, false.</returns>
		/// <param name="o">The <see cref="T:System.Type" /> whose underlying system type is to be compared with the underlying system type of the current <see cref="T:System.Type" />. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060004F5 RID: 1269 RVA: 0x00012DD4 File Offset: 0x00010FD4
		public bool Equals(Type o)
		{
			return o != null && this.UnderlyingSystemType.EqualsInternal(o.UnderlyingSystemType);
		}

		// Token: 0x060004F6 RID: 1270
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern bool EqualsInternal(Type type);

		// Token: 0x060004F7 RID: 1271
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Type internal_from_handle(IntPtr handle);

		// Token: 0x060004F8 RID: 1272
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Type internal_from_name(string name, bool throwOnError, bool ignoreCase);

		/// <summary>Gets the <see cref="T:System.Type" /> with the specified name, performing a case-sensitive search.</summary>
		/// <returns>The type with the specified name, if found; otherwise, null.</returns>
		/// <param name="typeName">The assembly-qualified name of the type to get. See <see cref="P:System.Type.AssemblyQualifiedName" />. If the type is in the currently executing assembly or in Mscorlib.dll, it is sufficient to supply the type name qualified by its namespace.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeName" /> is null. </exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">A class initializer is invoked and throws an exception. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="typeName" /> represents a generic type that has a pointer type, a ByRef type, or <see cref="T:System.Void" /> as one of its type arguments. -or-<paramref name="typeName" /> represents a generic type that has an incorrect number of type arguments.-or-<paramref name="typeName" /> represents a generic type, and one of its type arguments does not satisfy the constraints for the corresponding type parameter</exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="typeName" /> represents an array of <see cref="T:System.TypedReference" />. This is a change from the behavior in the .NET Framework versions 1.0 and 1.1, which was to return a null reference.</exception>
		/// <exception cref="T:System.IO.FileLoadException">The assembly or one of its dependencies was found, but could not be loaded. </exception>
		/// <exception cref="T:System.BadImageFormatException">The assembly or one of its dependencies is not valid. -or-Version 2.0 or later of the common language runtime is currently loaded, and the assembly was compiled with a later version.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060004F9 RID: 1273 RVA: 0x00012DF0 File Offset: 0x00010FF0
		public static Type GetType(string typeName)
		{
			if (typeName == null)
			{
				throw new ArgumentNullException("TypeName");
			}
			return Type.internal_from_name(typeName, false, false);
		}

		/// <summary>Gets the <see cref="T:System.Type" /> with the specified name, performing a case-sensitive search and specifying whether to throw an exception if the type is not found.</summary>
		/// <returns>The type with the specified name. If the type is not found, the <paramref name="throwOnError" /> parameter specifies whether null is returned or an exception is thrown. In some cases, an exception is thrown regardless of the value of <paramref name="throwOnError" />. See the Exceptions section. </returns>
		/// <param name="typeName">The assembly-qualified name of the type to get. See <see cref="P:System.Type.AssemblyQualifiedName" />. If the type is in the currently executing assembly or in Mscorlib.dll, it is sufficient to supply the type name qualified by its namespace.</param>
		/// <param name="throwOnError">true to throw an exception if the type cannot be found; false to return null. Specifying false also suppresses some other exception conditions, but not all of them. See the Exceptions section. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeName" /> is null. </exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">A class initializer is invoked and throws an exception. </exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="throwOnError" /> is true and the type is not found. -or-<paramref name="throwOnError" /> is true and <paramref name="typeName" /> contains invalid characters, such as an embedded tab.-or-<paramref name="throwOnError" /> is true and <paramref name="typeName" /> is an empty string.-or-<paramref name="throwOnError" /> is true and <paramref name="typeName" /> represents an array type with an invalid size. -or-<paramref name="typeName" /> represents an array of <see cref="T:System.TypedReference" />. This is a change from the behavior in the .NET Framework versions 1.0 and 1.1, which was to return a null reference.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="throwOnError" /> is true and <paramref name="typeName" /> contains invalid syntax. For example, "MyType[,*,]".-or- <paramref name="typeName" /> represents a generic type that has a pointer type, a ByRef type, or <see cref="T:System.Void" /> as one of its type arguments.-or-<paramref name="typeName" /> represents a generic type that has an incorrect number of type arguments.-or-<paramref name="typeName" /> represents a generic type, and one of its type arguments does not satisfy the constraints for the corresponding type parameter.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="throwOnError" /> is true and the assembly or one of its dependencies was not found.</exception>
		/// <exception cref="T:System.IO.FileLoadException">The assembly or one of its dependencies was found, but could not be loaded. </exception>
		/// <exception cref="T:System.BadImageFormatException">The assembly or one of its dependencies is not valid. -or-Version 2.0 or later of the common language runtime is currently loaded, and the assembly was compiled with a later version.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060004FA RID: 1274 RVA: 0x00012E0C File Offset: 0x0001100C
		public static Type GetType(string typeName, bool throwOnError)
		{
			if (typeName == null)
			{
				throw new ArgumentNullException("TypeName");
			}
			Type type = Type.internal_from_name(typeName, throwOnError, false);
			if (throwOnError && type == null)
			{
				throw new TypeLoadException("Error loading '" + typeName + "'");
			}
			return type;
		}

		/// <summary>Gets the <see cref="T:System.Type" /> with the specified name, specifying whether to perform a case-sensitive search and whether to throw an exception if the type is not found.</summary>
		/// <returns>The type with the specified name. If the type is not found, the <paramref name="throwOnError" /> parameter specifies whether null is returned or an exception is thrown. In some cases, an exception is thrown regardless of the value of <paramref name="throwOnError" />. See the Exceptions section. </returns>
		/// <param name="typeName">The assembly-qualified name of the type to get. See <see cref="P:System.Type.AssemblyQualifiedName" />. If the type is in the currently executing assembly or in Mscorlib.dll, it is sufficient to supply the type name qualified by its namespace.</param>
		/// <param name="throwOnError">true to throw an exception if the type cannot be found; false to return null. Specifying false also suppresses some other exception conditions, but not all of them. See the Exceptions section.</param>
		/// <param name="ignoreCase">true to perform a case-insensitive search for <paramref name="typeName" />, false to perform a case-sensitive search for <paramref name="typeName" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeName" /> is null. </exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">A class initializer is invoked and throws an exception. </exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="throwOnError" /> is true and the type is not found. -or-<paramref name="throwOnError" /> is true and <paramref name="typeName" /> contains invalid characters, such as an embedded tab.-or-<paramref name="throwOnError" /> is true and <paramref name="typeName" /> is an empty string.-or-<paramref name="throwOnError" /> is true and <paramref name="typeName" /> represents an array type with an invalid size. -or-<paramref name="typeName" /> represents an array of <see cref="T:System.TypedReference" />. This is a change from the behavior in the .NET Framework versions 1.0 and 1.1, which was to return a null reference.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="throwOnError" /> is true and <paramref name="typeName" /> contains invalid syntax. For example, "MyType[,*,]".-or- <paramref name="typeName" /> represents a generic type that has a pointer type, a ByRef type, or <see cref="T:System.Void" /> as one of its type arguments.-or-<paramref name="typeName" /> represents a generic type that has an incorrect number of type arguments.-or-<paramref name="typeName" /> represents a generic type, and one of its type arguments does not satisfy the constraints for the corresponding type parameter.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="throwOnError" /> is true and the assembly or one of its dependencies was not found. </exception>
		/// <exception cref="T:System.IO.FileLoadException">The assembly or one of its dependencies was found, but could not be loaded. </exception>
		/// <exception cref="T:System.BadImageFormatException">The assembly or one of its dependencies is not valid. -or-Version 2.0 or later of the common language runtime is currently loaded, and the assembly was compiled with a later version.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060004FB RID: 1275 RVA: 0x00012E58 File Offset: 0x00011058
		public static Type GetType(string typeName, bool throwOnError, bool ignoreCase)
		{
			if (typeName == null)
			{
				throw new ArgumentNullException("TypeName");
			}
			Type type = Type.internal_from_name(typeName, throwOnError, ignoreCase);
			if (throwOnError && type == null)
			{
				throw new TypeLoadException("Error loading '" + typeName + "'");
			}
			return type;
		}

		/// <summary>Gets the types of the objects in the specified array.</summary>
		/// <returns>An array of <see cref="T:System.Type" /> objects representing the types of the corresponding elements in <paramref name="args" />.</returns>
		/// <param name="args">An array of objects whose types to determine. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="args" /> is null. </exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The class initializers are invoked and at least one throws an exception. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060004FC RID: 1276 RVA: 0x00012EA4 File Offset: 0x000110A4
		public static Type[] GetTypeArray(object[] args)
		{
			if (args == null)
			{
				throw new ArgumentNullException("args");
			}
			Type[] array = new Type[args.Length];
			for (int i = 0; i < args.Length; i++)
			{
				array[i] = args[i].GetType();
			}
			return array;
		}

		// Token: 0x060004FD RID: 1277
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern TypeCode GetTypeCodeInternal(Type type);

		/// <summary>Gets the underlying type code of the specified <see cref="T:System.Type" />.</summary>
		/// <returns>The <see cref="T:System.TypeCode" /> value of the underlying type.</returns>
		/// <param name="type">The <see cref="T:System.Type" /> whose underlying type code to get. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060004FE RID: 1278 RVA: 0x00012EEC File Offset: 0x000110EC
		public static TypeCode GetTypeCode(Type type)
		{
			if (type is MonoType)
			{
				return Type.GetTypeCodeInternal(type);
			}
			if (type == null)
			{
				return TypeCode.Empty;
			}
			type = type.UnderlyingSystemType;
			if (!type.IsSystemType)
			{
				return TypeCode.Object;
			}
			return Type.GetTypeCodeInternal(type);
		}

		/// <summary>Gets the type associated with the specified class identifier (CLSID).</summary>
		/// <returns>System.__ComObject regardless of whether the CLSID is valid.</returns>
		/// <param name="clsid">The CLSID of the type to get. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060004FF RID: 1279 RVA: 0x00012F30 File Offset: 0x00011130
		[MonoTODO("This operation is currently not supported by Mono")]
		public static Type GetTypeFromCLSID(Guid clsid)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the type associated with the specified class identifier (CLSID), specifying whether to throw an exception if an error occurs while loading the type.</summary>
		/// <returns>System.__ComObject regardless of whether the CLSID is valid.</returns>
		/// <param name="clsid">The CLSID of the type to get. </param>
		/// <param name="throwOnError">true to throw any exception that occurs.-or- false to ignore any exception that occurs. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000500 RID: 1280 RVA: 0x00012F38 File Offset: 0x00011138
		[MonoTODO("This operation is currently not supported by Mono")]
		public static Type GetTypeFromCLSID(Guid clsid, bool throwOnError)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the type associated with the specified class identifier (CLSID) from the specified server.</summary>
		/// <returns>System.__ComObject regardless of whether the CLSID is valid.</returns>
		/// <param name="clsid">The CLSID of the type to get. </param>
		/// <param name="server">The server from which to load the type. If the server name is null, this method automatically reverts to the local machine. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000501 RID: 1281 RVA: 0x00012F40 File Offset: 0x00011140
		[MonoTODO("This operation is currently not supported by Mono")]
		public static Type GetTypeFromCLSID(Guid clsid, string server)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the type associated with the specified class identifier (CLSID) from the specified server, specifying whether to throw an exception if an error occurs while loading the type.</summary>
		/// <returns>System.__ComObject regardless of whether the CLSID is valid.</returns>
		/// <param name="clsid">The CLSID of the type to get. </param>
		/// <param name="server">The server from which to load the type. If the server name is null, this method automatically reverts to the local machine. </param>
		/// <param name="throwOnError">true to throw any exception that occurs.-or- false to ignore any exception that occurs. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000502 RID: 1282 RVA: 0x00012F48 File Offset: 0x00011148
		[MonoTODO("This operation is currently not supported by Mono")]
		public static Type GetTypeFromCLSID(Guid clsid, string server, bool throwOnError)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the type referenced by the specified type handle.</summary>
		/// <returns>The type referenced by the specified <see cref="T:System.RuntimeTypeHandle" />, or null if the <see cref="P:System.RuntimeTypeHandle.Value" /> property of <paramref name="handle" /> is null.</returns>
		/// <param name="handle">The <see cref="T:System.RuntimeTypeHandle" /> object that refers to the type. </param>
		/// <exception cref="T:System.Reflection.TargetInvocationException">A class initializer is invoked and throws an exception. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000503 RID: 1283 RVA: 0x00012F50 File Offset: 0x00011150
		public static Type GetTypeFromHandle(RuntimeTypeHandle handle)
		{
			if (handle.Value == IntPtr.Zero)
			{
				return null;
			}
			return Type.internal_from_handle(handle.Value);
		}

		/// <summary>Gets the type associated with the specified program identifier (ProgID), returning null if an error is encountered while loading the <see cref="T:System.Type" />.</summary>
		/// <returns>The type associated with the specified ProgID, if <paramref name="progID" /> is a valid entry in the registry and a type is associated with it; otherwise, null.</returns>
		/// <param name="progID">The ProgID of the type to get. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="progID" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000504 RID: 1284 RVA: 0x00012F84 File Offset: 0x00011184
		[MonoTODO("Mono does not support COM")]
		public static Type GetTypeFromProgID(string progID)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the type associated with the specified program identifier (ProgID), specifying whether to throw an exception if an error occurs while loading the type.</summary>
		/// <returns>The type associated with the specified program identifier (ProgID), if <paramref name="progID" /> is a valid entry in the registry and a type is associated with it; otherwise, null.</returns>
		/// <param name="progID">The ProgID of the type to get. </param>
		/// <param name="throwOnError">true to throw any exception that occurs.-or- false to ignore any exception that occurs. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="progID" /> is null. </exception>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">The specified ProgID is not registered. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000505 RID: 1285 RVA: 0x00012F8C File Offset: 0x0001118C
		[MonoTODO("Mono does not support COM")]
		public static Type GetTypeFromProgID(string progID, bool throwOnError)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the type associated with the specified program identifier (progID) from the specified server, returning null if an error is encountered while loading the type.</summary>
		/// <returns>The type associated with the specified program identifier (progID), if <paramref name="progID" /> is a valid entry in the registry and a type is associated with it; otherwise, null.</returns>
		/// <param name="progID">The progID of the type to get. </param>
		/// <param name="server">The server from which to load the type. If the server name is null, this method automatically reverts to the local machine. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="prodID" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000506 RID: 1286 RVA: 0x00012F94 File Offset: 0x00011194
		[MonoTODO("Mono does not support COM")]
		public static Type GetTypeFromProgID(string progID, string server)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the type associated with the specified program identifier (progID) from the specified server, specifying whether to throw an exception if an error occurs while loading the type.</summary>
		/// <returns>The <see cref="T:System.Type" /> associated with the specified program identifier (progID), if <paramref name="progID" /> is a valid entry in the registry and a type is associated with it; otherwise, null.</returns>
		/// <param name="progID">The progID of the <see cref="T:System.Type" /> to get. </param>
		/// <param name="server">The server from which to load the type. If the server name is null, this method automatically reverts to the local machine. </param>
		/// <param name="throwOnError">true to throw any exception that occurs.-or- false to ignore any exception that occurs. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="progID" /> is null. </exception>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">The specified progID is not registered. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000507 RID: 1287 RVA: 0x00012F9C File Offset: 0x0001119C
		[MonoTODO("Mono does not support COM")]
		public static Type GetTypeFromProgID(string progID, string server, bool throwOnError)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the handle for the <see cref="T:System.Type" /> of a specified object.</summary>
		/// <returns>The handle for the <see cref="T:System.Type" /> of the specified <see cref="T:System.Object" />.</returns>
		/// <param name="o">The <see cref="T:System.Object" /> for which to get the Type handle. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000508 RID: 1288 RVA: 0x00012FA4 File Offset: 0x000111A4
		public static RuntimeTypeHandle GetTypeHandle(object o)
		{
			if (o == null)
			{
				throw new ArgumentNullException();
			}
			return o.GetType().TypeHandle;
		}

		// Token: 0x06000509 RID: 1289
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool type_is_subtype_of(Type a, Type b, bool check_interfaces);

		// Token: 0x0600050A RID: 1290
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool type_is_assignable_from(Type a, Type b);

		/// <summary>Gets the current <see cref="T:System.Type" />.</summary>
		/// <returns>The current <see cref="T:System.Type" />.</returns>
		/// <exception cref="T:System.Reflection.TargetInvocationException">A class initializer is invoked and throws an exception. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600050B RID: 1291 RVA: 0x00012FC0 File Offset: 0x000111C0
		public new Type GetType()
		{
			return base.GetType();
		}

		/// <summary>Determines whether the class represented by the current <see cref="T:System.Type" /> derives from the class represented by the specified <see cref="T:System.Type" />.</summary>
		/// <returns>true if the Type represented by the <paramref name="c" /> parameter and the current Type represent classes, and the class represented by the current Type derives from the class represented by <paramref name="c" />; otherwise, false. This method also returns false if <paramref name="c" /> and the current Type represent the same class.</returns>
		/// <param name="c">The Type to compare with the current Type. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="c" /> parameter is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600050C RID: 1292 RVA: 0x00012FC8 File Offset: 0x000111C8
		[ComVisible(true)]
		public virtual bool IsSubclassOf(Type c)
		{
			if (c == null || c == this)
			{
				return false;
			}
			if (this.IsSystemType)
			{
				return c.IsSystemType && Type.type_is_subtype_of(this, c, false);
			}
			for (Type baseType = this.BaseType; baseType != null; baseType = baseType.BaseType)
			{
				if (baseType == c)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Returns an array of <see cref="T:System.Type" /> objects representing a filtered list of interfaces implemented or inherited by the current <see cref="T:System.Type" />.</summary>
		/// <returns>An array of <see cref="T:System.Type" /> objects representing a filtered list of the interfaces implemented or inherited by the current <see cref="T:System.Type" />, or an empty array of type <see cref="T:System.Type" /> if no interfaces matching the filter are implemented or inherited by the current <see cref="T:System.Type" />.</returns>
		/// <param name="filter">The <see cref="T:System.Reflection.TypeFilter" /> delegate that compares the interfaces against <paramref name="filterCriteria" />. </param>
		/// <param name="filterCriteria">The search criteria that determines whether an interface should be included in the returned array. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="filter" /> is null. </exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">A static initializer is invoked and throws an exception. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600050D RID: 1293 RVA: 0x0001302C File Offset: 0x0001122C
		public virtual Type[] FindInterfaces(TypeFilter filter, object filterCriteria)
		{
			if (filter == null)
			{
				throw new ArgumentNullException("filter");
			}
			ArrayList arrayList = new ArrayList();
			foreach (Type type in this.GetInterfaces())
			{
				if (filter(type, filterCriteria))
				{
					arrayList.Add(type);
				}
			}
			return (Type[])arrayList.ToArray(typeof(Type));
		}

		/// <summary>Searches for the interface with the specified name.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing the interface with the specified name, implemented or inherited by the current <see cref="T:System.Type" />, if found; otherwise, null.</returns>
		/// <param name="name">The <see cref="T:System.String" /> containing the name of the interface to get. For generic interfaces, this is the mangled name.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">The current <see cref="T:System.Type" /> represents a type that implements the same generic interface with different type arguments. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600050E RID: 1294 RVA: 0x0001309C File Offset: 0x0001129C
		public Type GetInterface(string name)
		{
			return this.GetInterface(name, false);
		}

		/// <summary>When overridden in a derived class, searches for the specified interface, specifying whether to do a case-insensitive search for the interface name.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing the interface with the specified name, implemented or inherited by the current <see cref="T:System.Type" />, if found; otherwise, null.</returns>
		/// <param name="name">The <see cref="T:System.String" /> containing the name of the interface to get. For generic interfaces, this is the mangled name.</param>
		/// <param name="ignoreCase">true to ignore the case of that part of <paramref name="name" /> that specifies the simple interface name (the part that specifies the namespace must be correctly cased).-or- false to perform a case-sensitive search for all parts of <paramref name="name" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">The current <see cref="T:System.Type" /> represents a type that implements the same generic interface with different type arguments. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600050F RID: 1295
		public abstract Type GetInterface(string name, bool ignoreCase);

		// Token: 0x06000510 RID: 1296
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void GetInterfaceMapData(Type t, Type iface, out MethodInfo[] targets, out MethodInfo[] methods);

		/// <summary>Returns an interface mapping for the specified interface type.</summary>
		/// <returns>An <see cref="T:System.Reflection.InterfaceMapping" /> object representing the interface mapping for <paramref name="interfaceType" />.</returns>
		/// <param name="interfaceType">The <see cref="T:System.Type" /> of the interface of which to retrieve a mapping. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="interfaceType" /> parameter does not refer to an interface. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="interfaceType" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Type" /> represents a generic type parameter; that is, <see cref="P:System.Type.IsGenericParameter" /> is true.</exception>
		/// <exception cref="T:System.NotSupportedException">The invoked method is not supported in the base class. Derived classes must provide an implementation.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000511 RID: 1297 RVA: 0x000130A8 File Offset: 0x000112A8
		[ComVisible(true)]
		public virtual InterfaceMapping GetInterfaceMap(Type interfaceType)
		{
			if (interfaceType == null)
			{
				throw new ArgumentNullException("interfaceType");
			}
			if (!interfaceType.IsInterface)
			{
				throw new ArgumentException(Locale.GetText("Argument must be an interface."), "interfaceType");
			}
			if (this.IsInterface)
			{
				throw new ArgumentException("'this' type cannot be an interface itself");
			}
			InterfaceMapping result;
			result.TargetType = this;
			result.InterfaceType = interfaceType;
			Type.GetInterfaceMapData(this, interfaceType, out result.TargetMethods, out result.InterfaceMethods);
			if (result.TargetMethods == null)
			{
				throw new ArgumentException(Locale.GetText("Interface not found"), "interfaceType");
			}
			return result;
		}

		/// <summary>When overridden in a derived class, gets all the interfaces implemented or inherited by the current <see cref="T:System.Type" />.</summary>
		/// <returns>An array of <see cref="T:System.Type" /> objects representing all the interfaces implemented or inherited by the current <see cref="T:System.Type" />.-or- An empty array of type <see cref="T:System.Type" />, if no interfaces are implemented or inherited by the current <see cref="T:System.Type" />.</returns>
		/// <exception cref="T:System.Reflection.TargetInvocationException">A static initializer is invoked and throws an exception. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000512 RID: 1298
		public abstract Type[] GetInterfaces();

		/// <summary>Determines whether an instance of the current <see cref="T:System.Type" /> can be assigned from an instance of the specified Type.</summary>
		/// <returns>true if <paramref name="c" /> and the current Type represent the same type, or if the current Type is in the inheritance hierarchy of <paramref name="c" />, or if the current Type is an interface that <paramref name="c" /> implements, or if <paramref name="c" /> is a generic type parameter and the current Type represents one of the constraints of <paramref name="c" />. false if none of these conditions are true, or if <paramref name="c" /> is null.</returns>
		/// <param name="c">The Type to compare with the current Type. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000513 RID: 1299 RVA: 0x00013144 File Offset: 0x00011344
		public virtual bool IsAssignableFrom(Type c)
		{
			if (c == null)
			{
				return false;
			}
			if (this.Equals(c))
			{
				return true;
			}
			if (c is TypeBuilder)
			{
				return ((TypeBuilder)c).IsAssignableTo(this);
			}
			if (!this.IsSystemType)
			{
				Type underlyingSystemType = this.UnderlyingSystemType;
				return underlyingSystemType.IsSystemType && underlyingSystemType.IsAssignableFrom(c);
			}
			if (!c.IsSystemType)
			{
				Type underlyingSystemType2 = c.UnderlyingSystemType;
				return underlyingSystemType2.IsSystemType && this.IsAssignableFrom(underlyingSystemType2);
			}
			return Type.type_is_assignable_from(this, c);
		}

		/// <summary>Determines whether the specified object is an instance of the current <see cref="T:System.Type" />.</summary>
		/// <returns>true if the current Type is in the inheritance hierarchy of the object represented by <paramref name="o" />, or if the current Type is an interface that <paramref name="o" /> supports. false if neither of these conditions is the case, or if <paramref name="o" /> is null, or if the current Type is an open generic type (that is, <see cref="P:System.Type.ContainsGenericParameters" /> returns true).</returns>
		/// <param name="o">The object to compare with the current Type. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000514 RID: 1300
		[MethodImpl(MethodImplOptions.InternalCall)]
		public virtual extern bool IsInstanceOfType(object o);

		/// <summary>Gets the number of dimensions in an <see cref="T:System.Array" />.</summary>
		/// <returns>An <see cref="T:System.Int32" /> containing the number of dimensions in the current Type.</returns>
		/// <exception cref="T:System.NotSupportedException">The functionality of this method is unsupported in the base class and must be implemented in a derived class instead. </exception>
		/// <exception cref="T:System.ArgumentException">The current Type is not an array. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000515 RID: 1301 RVA: 0x000131D4 File Offset: 0x000113D4
		public virtual int GetArrayRank()
		{
			throw new NotSupportedException();
		}

		/// <summary>When overridden in a derived class, returns the <see cref="T:System.Type" /> of the object encompassed or referred to by the current array, pointer or reference type.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the object encompassed or referred to by the current array, pointer, or reference type, or null if the current <see cref="T:System.Type" /> is not an array or a pointer, or is not passed by reference, or represents a generic type or a type parameter in the definition of a generic type or generic method.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000516 RID: 1302
		public abstract Type GetElementType();

		/// <summary>Returns the <see cref="T:System.Reflection.EventInfo" /> object representing the specified public event.</summary>
		/// <returns>The <see cref="T:System.Reflection.EventInfo" /> object representing the specified public event which is declared or inherited by the current <see cref="T:System.Type" />, if found; otherwise, null.</returns>
		/// <param name="name">The <see cref="T:System.String" /> containing the name of an event which is declared or inherited by the current <see cref="T:System.Type" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000517 RID: 1303 RVA: 0x000131DC File Offset: 0x000113DC
		public EventInfo GetEvent(string name)
		{
			return this.GetEvent(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		/// <summary>When overridden in a derived class, returns the <see cref="T:System.Reflection.EventInfo" /> object representing the specified event, using the specified binding constraints.</summary>
		/// <returns>The <see cref="T:System.Reflection.EventInfo" /> object representing the specified event which is declared or inherited by the current <see cref="T:System.Type" />, if found; otherwise, null.</returns>
		/// <param name="name">The <see cref="T:System.String" /> containing the name of an event which is declared or inherited by the current <see cref="T:System.Type" />. </param>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.-or- Zero, to return null. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000518 RID: 1304
		public abstract EventInfo GetEvent(string name, BindingFlags bindingAttr);

		/// <summary>Returns all the public events that are declared or inherited by the current <see cref="T:System.Type" />.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.EventInfo" /> objects representing all the public events which are declared or inherited by the current <see cref="T:System.Type" />.-or- An empty array of type <see cref="T:System.Reflection.EventInfo" />, if the current <see cref="T:System.Type" /> does not have public events.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000519 RID: 1305 RVA: 0x000131E8 File Offset: 0x000113E8
		public virtual EventInfo[] GetEvents()
		{
			return this.GetEvents(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		/// <summary>When overridden in a derived class, searches for events that are declared or inherited by the current <see cref="T:System.Type" />, using the specified binding constraints.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.EventInfo" /> objects representing all events which are declared or inherited by the current <see cref="T:System.Type" /> that match the specified binding constraints.-or- An empty array of type <see cref="T:System.Reflection.EventInfo" />, if the current <see cref="T:System.Type" /> does not have events, or if none of the events match the binding constraints.</returns>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.-or- Zero, to return null. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600051A RID: 1306
		public abstract EventInfo[] GetEvents(BindingFlags bindingAttr);

		/// <summary>Searches for the public field with the specified name.</summary>
		/// <returns>A <see cref="T:System.Reflection.FieldInfo" /> object representing the public field with the specified name, if found; otherwise, null.</returns>
		/// <param name="name">The <see cref="T:System.String" /> containing the name of the data field to get. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">This <see cref="T:System.Type" /> object is a <see cref="T:System.Reflection.Emit.TypeBuilder" /> whose <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> method has not yet been called. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600051B RID: 1307 RVA: 0x000131F4 File Offset: 0x000113F4
		public FieldInfo GetField(string name)
		{
			return this.GetField(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		/// <summary>Searches for the specified field, using the specified binding constraints.</summary>
		/// <returns>A <see cref="T:System.Reflection.FieldInfo" /> object representing the field that matches the specified requirements, if found; otherwise, null.</returns>
		/// <param name="name">The <see cref="T:System.String" /> containing the name of the data field to get. </param>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.-or- Zero, to return null. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600051C RID: 1308
		public abstract FieldInfo GetField(string name, BindingFlags bindingAttr);

		/// <summary>Returns all the public fields of the current <see cref="T:System.Type" />.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.FieldInfo" /> objects representing all the public fields defined for the current <see cref="T:System.Type" />.-or- An empty array of type <see cref="T:System.Reflection.FieldInfo" />, if no public fields are defined for the current <see cref="T:System.Type" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600051D RID: 1309 RVA: 0x00013200 File Offset: 0x00011400
		public FieldInfo[] GetFields()
		{
			return this.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		/// <summary>When overridden in a derived class, searches for the fields defined for the current <see cref="T:System.Type" />, using the specified binding constraints.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.FieldInfo" /> objects representing all fields defined for the current <see cref="T:System.Type" /> that match the specified binding constraints.-or- An empty array of type <see cref="T:System.Reflection.FieldInfo" />, if no fields are defined for the current <see cref="T:System.Type" />, or if none of the defined fields match the binding constraints.</returns>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.-or- Zero, to return null. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600051E RID: 1310
		public abstract FieldInfo[] GetFields(BindingFlags bindingAttr);

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>An <see cref="T:System.Int32" /> containing the hash code for this instance.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600051F RID: 1311 RVA: 0x0001320C File Offset: 0x0001140C
		public override int GetHashCode()
		{
			Type underlyingSystemType = this.UnderlyingSystemType;
			if (underlyingSystemType != null && underlyingSystemType != this)
			{
				return underlyingSystemType.GetHashCode();
			}
			return (int)this._impl.Value;
		}

		/// <summary>Searches for the public members with the specified name.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.MemberInfo" /> objects representing the public members with the specified name, if found; otherwise, an empty array.</returns>
		/// <param name="name">The <see cref="T:System.String" /> containing the name of the public members to get. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000520 RID: 1312 RVA: 0x00013244 File Offset: 0x00011444
		public MemberInfo[] GetMember(string name)
		{
			return this.GetMember(name, MemberTypes.All, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		/// <summary>Searches for the specified members, using the specified binding constraints.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.MemberInfo" /> objects representing the public members with the specified name, if found; otherwise, an empty array.</returns>
		/// <param name="name">The <see cref="T:System.String" /> containing the name of the members to get. </param>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.-or- Zero, to return an empty array. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000521 RID: 1313 RVA: 0x00013254 File Offset: 0x00011454
		public virtual MemberInfo[] GetMember(string name, BindingFlags bindingAttr)
		{
			return this.GetMember(name, MemberTypes.All, bindingAttr);
		}

		/// <summary>Searches for the specified members of the specified member type, using the specified binding constraints.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.MemberInfo" /> objects representing the public members with the specified name, if found; otherwise, an empty array.</returns>
		/// <param name="name">The <see cref="T:System.String" /> containing the name of the members to get. </param>
		/// <param name="type">The <see cref="T:System.Reflection.MemberTypes" /> value to search for. </param>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.-or- Zero, to return an empty array. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">A derived class must provide an implementation. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000522 RID: 1314 RVA: 0x00013264 File Offset: 0x00011464
		public virtual MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if ((bindingAttr & BindingFlags.IgnoreCase) != BindingFlags.Default)
			{
				return this.FindMembers(type, bindingAttr, Type.FilterNameIgnoreCase, name);
			}
			return this.FindMembers(type, bindingAttr, Type.FilterName, name);
		}

		/// <summary>Returns all the public members of the current <see cref="T:System.Type" />.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.MemberInfo" /> objects representing all the public members of the current <see cref="T:System.Type" />.-or- An empty array of type <see cref="T:System.Reflection.MemberInfo" />, if the current <see cref="T:System.Type" /> does not have public members.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000523 RID: 1315 RVA: 0x000132A8 File Offset: 0x000114A8
		public MemberInfo[] GetMembers()
		{
			return this.GetMembers(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		/// <summary>When overridden in a derived class, searches for the members defined for the current <see cref="T:System.Type" />, using the specified binding constraints.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.MemberInfo" /> objects representing all members defined for the current <see cref="T:System.Type" /> that match the specified binding constraints.-or- An empty array of type <see cref="T:System.Reflection.MemberInfo" />, if no members are defined for the current <see cref="T:System.Type" />, or if none of the defined members match the binding constraints.</returns>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.-or- Zero, to return null. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000524 RID: 1316
		public abstract MemberInfo[] GetMembers(BindingFlags bindingAttr);

		/// <summary>Searches for the public method with the specified name.</summary>
		/// <returns>A <see cref="T:System.Reflection.MethodInfo" /> object representing the public method with the specified name, if found; otherwise, null.</returns>
		/// <param name="name">The <see cref="T:System.String" /> containing the name of the public method to get. </param>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one method is found with the specified name. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000525 RID: 1317 RVA: 0x000132B4 File Offset: 0x000114B4
		public MethodInfo GetMethod(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			return this.GetMethodImpl(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, null, CallingConventions.Any, null, null);
		}

		/// <summary>Searches for the specified method, using the specified binding constraints.</summary>
		/// <returns>A <see cref="T:System.Reflection.MethodInfo" /> object representing the method that matches the specified requirements, if found; otherwise, null.</returns>
		/// <param name="name">The <see cref="T:System.String" /> containing the name of the method to get. </param>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.-or- Zero, to return null. </param>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one method is found with the specified name and matching the specified binding constraints. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000526 RID: 1318 RVA: 0x000132D4 File Offset: 0x000114D4
		public MethodInfo GetMethod(string name, BindingFlags bindingAttr)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			return this.GetMethodImpl(name, bindingAttr, null, CallingConventions.Any, null, null);
		}

		/// <summary>Searches for the specified public method whose parameters match the specified argument types.</summary>
		/// <returns>A <see cref="T:System.Reflection.MethodInfo" /> object representing the public method whose parameters match the specified argument types, if found; otherwise, null.</returns>
		/// <param name="name">The <see cref="T:System.String" /> containing the name of the public method to get. </param>
		/// <param name="types">An array of <see cref="T:System.Type" /> objects representing the number, order, and type of the parameters for the method to get.-or- An empty array of <see cref="T:System.Type" /> objects (as provided by the <see cref="F:System.Type.EmptyTypes" /> field) to get a method that takes no parameters. </param>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one method is found with the specified name and specified parameters. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null.-or- <paramref name="types" /> is null.-or- One of the elements in <paramref name="types" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="types" /> is multidimensional. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000527 RID: 1319 RVA: 0x000132F4 File Offset: 0x000114F4
		public MethodInfo GetMethod(string name, Type[] types)
		{
			return this.GetMethod(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, null, CallingConventions.Any, types, null);
		}

		/// <summary>Searches for the specified public method whose parameters match the specified argument types and modifiers.</summary>
		/// <returns>A <see cref="T:System.Reflection.MethodInfo" /> object representing the public method that matches the specified requirements, if found; otherwise, null.</returns>
		/// <param name="name">The <see cref="T:System.String" /> containing the name of the public method to get. </param>
		/// <param name="types">An array of <see cref="T:System.Type" /> objects representing the number, order, and type of the parameters for the method to get.-or- An empty array of <see cref="T:System.Type" /> objects (as provided by the <see cref="F:System.Type.EmptyTypes" /> field) to get a method that takes no parameters. </param>
		/// <param name="modifiers">An array of <see cref="T:System.Reflection.ParameterModifier" /> objects representing the attributes associated with the corresponding element in the <paramref name="types" /> array. To be only used when calling through COM interop, and only parameters that are passed by reference are handled. The default binder does not process this parameter.  </param>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one method is found with the specified name and specified parameters. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null.-or- <paramref name="types" /> is null.-or- One of the elements in <paramref name="types" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="types" /> is multidimensional.-or- <paramref name="modifiers" /> is multidimensional. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000528 RID: 1320 RVA: 0x00013304 File Offset: 0x00011504
		public MethodInfo GetMethod(string name, Type[] types, ParameterModifier[] modifiers)
		{
			return this.GetMethod(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, null, CallingConventions.Any, types, modifiers);
		}

		/// <summary>Searches for the specified method whose parameters match the specified argument types and modifiers, using the specified binding constraints.</summary>
		/// <returns>A <see cref="T:System.Reflection.MethodInfo" /> object representing the method that matches the specified requirements, if found; otherwise, null.</returns>
		/// <param name="name">The <see cref="T:System.String" /> containing the name of the method to get. </param>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.-or- Zero, to return null. </param>
		/// <param name="binder">A <see cref="T:System.Reflection.Binder" /> object that defines a set of properties and enables binding, which can involve selection of an overloaded method, coercion of argument types, and invocation of a member through reflection.-or- A null reference (Nothing in Visual Basic), to use the <see cref="P:System.Type.DefaultBinder" />. </param>
		/// <param name="types">An array of <see cref="T:System.Type" /> objects representing the number, order, and type of the parameters for the method to get.-or- An empty array of <see cref="T:System.Type" /> objects (as provided by the <see cref="F:System.Type.EmptyTypes" /> field) to get a method that takes no parameters. </param>
		/// <param name="modifiers">An array of <see cref="T:System.Reflection.ParameterModifier" /> objects representing the attributes associated with the corresponding element in the <paramref name="types" /> array. To be only used when calling through COM interop, and only parameters that are passed by reference are handled. The default binder does not process this parameter.</param>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one method is found with the specified name and matching the specified binding constraints. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null.-or- <paramref name="types" /> is null.-or- One of the elements in <paramref name="types" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="types" /> is multidimensional.-or- <paramref name="modifiers" /> is multidimensional. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000529 RID: 1321 RVA: 0x00013314 File Offset: 0x00011514
		public MethodInfo GetMethod(string name, BindingFlags bindingAttr, Binder binder, Type[] types, ParameterModifier[] modifiers)
		{
			return this.GetMethod(name, bindingAttr, binder, CallingConventions.Any, types, modifiers);
		}

		/// <summary>Searches for the specified method whose parameters match the specified argument types and modifiers, using the specified binding constraints and the specified calling convention.</summary>
		/// <returns>A <see cref="T:System.Reflection.MethodInfo" /> object representing the method that matches the specified requirements, if found; otherwise, null.</returns>
		/// <param name="name">The <see cref="T:System.String" /> containing the name of the method to get. </param>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.-or- Zero, to return null. </param>
		/// <param name="binder">A <see cref="T:System.Reflection.Binder" /> object that defines a set of properties and enables binding, which can involve selection of an overloaded method, coercion of argument types, and invocation of a member through reflection.-or- A null reference (Nothing in Visual Basic), to use the <see cref="P:System.Type.DefaultBinder" />. </param>
		/// <param name="callConvention">The <see cref="T:System.Reflection.CallingConventions" /> object that specifies the set of rules to use regarding the order and layout of arguments, how the return value is passed, what registers are used for arguments, and how the stack is cleaned up. </param>
		/// <param name="types">An array of <see cref="T:System.Type" /> objects representing the number, order, and type of the parameters for the method to get.-or- An empty array of <see cref="T:System.Type" /> objects (as provided by the <see cref="F:System.Type.EmptyTypes" /> field) to get a method that takes no parameters. </param>
		/// <param name="modifiers">An array of <see cref="T:System.Reflection.ParameterModifier" /> objects representing the attributes associated with the corresponding element in the <paramref name="types" /> array. To be only used when calling through COM interop, and only parameters that are passed by reference are handled. The default binder does not process this parameter. </param>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one method is found with the specified name and matching the specified binding constraints. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null.-or- <paramref name="types" /> is null.-or- One of the elements in <paramref name="types" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="types" /> is multidimensional.-or- <paramref name="modifiers" /> is multidimensional. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600052A RID: 1322 RVA: 0x00013324 File Offset: 0x00011524
		public MethodInfo GetMethod(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (types == null)
			{
				throw new ArgumentNullException("types");
			}
			for (int i = 0; i < types.Length; i++)
			{
				if (types[i] == null)
				{
					throw new ArgumentNullException("types");
				}
			}
			return this.GetMethodImpl(name, bindingAttr, binder, callConvention, types, modifiers);
		}

		/// <summary>When overridden in a derived class, searches for the specified method whose parameters match the specified argument types and modifiers, using the specified binding constraints and the specified calling convention.</summary>
		/// <returns>A <see cref="T:System.Reflection.MethodInfo" /> object representing the method that matches the specified requirements, if found; otherwise, null.</returns>
		/// <param name="name">The <see cref="T:System.String" /> containing the name of the method to get. </param>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.-or- Zero, to return null. </param>
		/// <param name="binder">A <see cref="T:System.Reflection.Binder" /> object that defines a set of properties and enables binding, which can involve selection of an overloaded method, coercion of argument types, and invocation of a member through reflection.-or- A null reference (Nothing in Visual Basic), to use the <see cref="P:System.Type.DefaultBinder" />. </param>
		/// <param name="callConvention">The <see cref="T:System.Reflection.CallingConventions" /> object that specifies the set of rules to use regarding the order and layout of arguments, how the return value is passed, what registers are used for arguments, and what process cleans up the stack. </param>
		/// <param name="types">An array of <see cref="T:System.Type" /> objects representing the number, order, and type of the parameters for the method to get.-or- An empty array of the type <see cref="T:System.Type" /> (that is, Type[] types = new Type[0]) to get a method that takes no parameters.-or- A null reference (Nothing in Visual Basic). If <paramref name="types" /> is null, arguments are not matched. </param>
		/// <param name="modifiers">An array of <see cref="T:System.Reflection.ParameterModifier" /> objects representing the attributes associated with the corresponding element in the <paramref name="types" /> array. The default binder does not process this parameter. </param>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one method is found with the specified name and matching the specified binding constraints. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="types" /> is multidimensional.-or- <paramref name="modifiers" /> is multidimensional.-or- <paramref name="types" /> and <paramref name="modifiers" /> do not have the same length. </exception>
		// Token: 0x0600052B RID: 1323
		protected abstract MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers);

		// Token: 0x0600052C RID: 1324 RVA: 0x0001338C File Offset: 0x0001158C
		internal MethodInfo GetMethodImplInternal(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			return this.GetMethodImpl(name, bindingAttr, binder, callConvention, types, modifiers);
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x000133A0 File Offset: 0x000115A0
		internal virtual MethodInfo GetMethod(MethodInfo fromNoninstanciated)
		{
			throw new InvalidOperationException("can only be called in generic type");
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x000133AC File Offset: 0x000115AC
		internal virtual ConstructorInfo GetConstructor(ConstructorInfo fromNoninstanciated)
		{
			throw new InvalidOperationException("can only be called in generic type");
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x000133B8 File Offset: 0x000115B8
		internal virtual FieldInfo GetField(FieldInfo fromNoninstanciated)
		{
			throw new InvalidOperationException("can only be called in generic type");
		}

		/// <summary>Returns all the public methods of the current <see cref="T:System.Type" />.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.MethodInfo" /> objects representing all the public methods defined for the current <see cref="T:System.Type" />.-or- An empty array of type <see cref="T:System.Reflection.MethodInfo" />, if no public methods are defined for the current <see cref="T:System.Type" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000530 RID: 1328 RVA: 0x000133C4 File Offset: 0x000115C4
		public MethodInfo[] GetMethods()
		{
			return this.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		/// <summary>When overridden in a derived class, searches for the methods defined for the current <see cref="T:System.Type" />, using the specified binding constraints.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.MethodInfo" /> objects representing all methods defined for the current <see cref="T:System.Type" /> that match the specified binding constraints.-or- An empty array of type <see cref="T:System.Reflection.MethodInfo" />, if no methods are defined for the current <see cref="T:System.Type" />, or if none of the defined methods match the binding constraints.</returns>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.-or- Zero, to return null. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000531 RID: 1329
		public abstract MethodInfo[] GetMethods(BindingFlags bindingAttr);

		/// <summary>Searches for the public nested type with the specified name.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing the public nested type with the specified name, if found; otherwise, null.</returns>
		/// <param name="name">The string containing the name of the nested type to get. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000532 RID: 1330 RVA: 0x000133D0 File Offset: 0x000115D0
		public Type GetNestedType(string name)
		{
			return this.GetNestedType(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		/// <summary>When overridden in a derived class, searches for the specified nested type, using the specified binding constraints.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing the nested type that matches the specified requirements, if found; otherwise, null.</returns>
		/// <param name="name">The string containing the name of the nested type to get. </param>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.-or- Zero, to return null. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000533 RID: 1331
		public abstract Type GetNestedType(string name, BindingFlags bindingAttr);

		/// <summary>Returns the public types nested in the current <see cref="T:System.Type" />.</summary>
		/// <returns>An array of <see cref="T:System.Type" /> objects representing the public types nested in the current <see cref="T:System.Type" /> (the search is not recursive), or an empty array of type <see cref="T:System.Type" /> if no public types are nested in the current <see cref="T:System.Type" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000534 RID: 1332 RVA: 0x000133DC File Offset: 0x000115DC
		public Type[] GetNestedTypes()
		{
			return this.GetNestedTypes(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		/// <summary>When overridden in a derived class, searches for the types nested in the current <see cref="T:System.Type" />, using the specified binding constraints.</summary>
		/// <returns>An array of <see cref="T:System.Type" /> objects representing all the types nested in the current <see cref="T:System.Type" /> that match the specified binding constraints (the search is not recursive), or an empty array of type <see cref="T:System.Type" />, if no nested types are found that match the binding constraints.</returns>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.-or- Zero, to return null. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000535 RID: 1333
		public abstract Type[] GetNestedTypes(BindingFlags bindingAttr);

		/// <summary>Returns all the public properties of the current <see cref="T:System.Type" />.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.PropertyInfo" /> objects representing all public properties of the current <see cref="T:System.Type" />.-or- An empty array of type <see cref="T:System.Reflection.PropertyInfo" />, if the current <see cref="T:System.Type" /> does not have public properties.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000536 RID: 1334 RVA: 0x000133E8 File Offset: 0x000115E8
		public PropertyInfo[] GetProperties()
		{
			return this.GetProperties(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		/// <summary>When overridden in a derived class, searches for the properties of the current <see cref="T:System.Type" />, using the specified binding constraints.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.PropertyInfo" /> objects representing all properties of the current <see cref="T:System.Type" /> that match the specified binding constraints.-or- An empty array of type <see cref="T:System.Reflection.PropertyInfo" />, if the current <see cref="T:System.Type" /> does not have properties, or if none of the properties match the binding constraints.</returns>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.-or- Zero, to return null. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000537 RID: 1335
		public abstract PropertyInfo[] GetProperties(BindingFlags bindingAttr);

		/// <summary>Searches for the public property with the specified name.</summary>
		/// <returns>A <see cref="T:System.Reflection.PropertyInfo" /> object representing the public property with the specified name, if found; otherwise, null.</returns>
		/// <param name="name">The <see cref="T:System.String" /> containing the name of the public property to get. </param>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one property is found with the specified name. See Remarks.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000538 RID: 1336 RVA: 0x000133F4 File Offset: 0x000115F4
		public PropertyInfo GetProperty(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			return this.GetPropertyImpl(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, null, null, null, null);
		}

		/// <summary>Searches for the specified property, using the specified binding constraints.</summary>
		/// <returns>A <see cref="T:System.Reflection.PropertyInfo" /> object representing the property that matches the specified requirements, if found; otherwise, null.</returns>
		/// <param name="name">The <see cref="T:System.String" /> containing the name of the property to get. </param>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.-or- Zero, to return null. </param>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one property is found with the specified name and matching the specified binding constraints. See Remarks.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000539 RID: 1337 RVA: 0x00013414 File Offset: 0x00011614
		public PropertyInfo GetProperty(string name, BindingFlags bindingAttr)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			return this.GetPropertyImpl(name, bindingAttr, null, null, null, null);
		}

		/// <summary>Searches for the public property with the specified name and return type.</summary>
		/// <returns>A <see cref="T:System.Reflection.PropertyInfo" /> object representing the public property with the specified name, if found; otherwise, null.</returns>
		/// <param name="name">The <see cref="T:System.String" /> containing the name of the public property to get. </param>
		/// <param name="returnType">The return type of the property. </param>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one property is found with the specified name. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null, or <paramref name="returnType" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600053A RID: 1338 RVA: 0x00013434 File Offset: 0x00011634
		public PropertyInfo GetProperty(string name, Type returnType)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			return this.GetPropertyImpl(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, null, returnType, null, null);
		}

		/// <summary>Searches for the specified public property whose parameters match the specified argument types.</summary>
		/// <returns>A <see cref="T:System.Reflection.PropertyInfo" /> object representing the public property whose parameters match the specified argument types, if found; otherwise, null.</returns>
		/// <param name="name">The <see cref="T:System.String" /> containing the name of the public property to get. </param>
		/// <param name="types">An array of <see cref="T:System.Type" /> objects representing the number, order, and type of the parameters for the indexed property to get.-or- An empty array of the type <see cref="T:System.Type" /> (that is, Type[] types = new Type[0]) to get a property that is not indexed. </param>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one property is found with the specified name and matching the specified argument types. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null.-or- <paramref name="types" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="types" /> is multidimensional. </exception>
		/// <exception cref="T:System.NullReferenceException">An element of <paramref name="types" /> is null.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600053B RID: 1339 RVA: 0x00013454 File Offset: 0x00011654
		public PropertyInfo GetProperty(string name, Type[] types)
		{
			return this.GetProperty(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, null, null, types, null);
		}

		/// <summary>Searches for the specified public property whose parameters match the specified argument types.</summary>
		/// <returns>A <see cref="T:System.Reflection.PropertyInfo" /> object representing the public property whose parameters match the specified argument types, if found; otherwise, null.</returns>
		/// <param name="name">The <see cref="T:System.String" /> containing the name of the public property to get. </param>
		/// <param name="returnType">The return type of the property. </param>
		/// <param name="types">An array of <see cref="T:System.Type" /> objects representing the number, order, and type of the parameters for the indexed property to get.-or- An empty array of the type <see cref="T:System.Type" /> (that is, Type[] types = new Type[0]) to get a property that is not indexed. </param>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one property is found with the specified name and matching the specified argument types. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null.-or- <paramref name="types" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="types" /> is multidimensional. </exception>
		/// <exception cref="T:System.NullReferenceException">An element of <paramref name="types" /> is null.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600053C RID: 1340 RVA: 0x00013464 File Offset: 0x00011664
		public PropertyInfo GetProperty(string name, Type returnType, Type[] types)
		{
			return this.GetProperty(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, null, returnType, types, null);
		}

		/// <summary>Searches for the specified public property whose parameters match the specified argument types and modifiers.</summary>
		/// <returns>A <see cref="T:System.Reflection.PropertyInfo" /> object representing the public property that matches the specified requirements, if found; otherwise, null.</returns>
		/// <param name="name">The <see cref="T:System.String" /> containing the name of the public property to get. </param>
		/// <param name="returnType">The return type of the property. </param>
		/// <param name="types">An array of <see cref="T:System.Type" /> objects representing the number, order, and type of the parameters for the indexed property to get.-or- An empty array of the type <see cref="T:System.Type" /> (that is, Type[] types = new Type[0]) to get a property that is not indexed. </param>
		/// <param name="modifiers">An array of <see cref="T:System.Reflection.ParameterModifier" /> objects representing the attributes associated with the corresponding element in the <paramref name="types" /> array. The default binder does not process this parameter. </param>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one property is found with the specified name and matching the specified argument types and modifiers. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null.-or- <paramref name="types" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="types" /> is multidimensional.-or- <paramref name="modifiers" /> is multidimensional.-or- <paramref name="types" /> and <paramref name="modifiers" /> do not have the same length. </exception>
		/// <exception cref="T:System.NullReferenceException">An element of <paramref name="types" /> is null.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600053D RID: 1341 RVA: 0x00013474 File Offset: 0x00011674
		public PropertyInfo GetProperty(string name, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			return this.GetProperty(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, null, returnType, types, modifiers);
		}

		/// <summary>Searches for the specified property whose parameters match the specified argument types and modifiers, using the specified binding constraints.</summary>
		/// <returns>A <see cref="T:System.Reflection.PropertyInfo" /> object representing the property that matches the specified requirements, if found; otherwise, null.</returns>
		/// <param name="name">The <see cref="T:System.String" /> containing the name of the property to get. </param>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.-or- Zero, to return null. </param>
		/// <param name="binder">A <see cref="T:System.Reflection.Binder" /> object that defines a set of properties and enables binding, which can involve selection of an overloaded method, coercion of argument types, and invocation of a member through reflection.-or- A null reference (Nothing in Visual Basic), to use the <see cref="P:System.Type.DefaultBinder" />. </param>
		/// <param name="returnType">The return type of the property. </param>
		/// <param name="types">An array of <see cref="T:System.Type" /> objects representing the number, order, and type of the parameters for the indexed property to get.-or- An empty array of the type <see cref="T:System.Type" /> (that is, Type[] types = new Type[0]) to get a property that is not indexed. </param>
		/// <param name="modifiers">An array of <see cref="T:System.Reflection.ParameterModifier" /> objects representing the attributes associated with the corresponding element in the <paramref name="types" /> array. The default binder does not process this parameter. </param>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one property is found with the specified name and matching the specified binding constraints. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null.-or- <paramref name="types" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="types" /> is multidimensional.-or- <paramref name="modifiers" /> is multidimensional.-or- <paramref name="types" /> and <paramref name="modifiers" /> do not have the same length. </exception>
		/// <exception cref="T:System.NullReferenceException">An element of <paramref name="types" /> is null.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600053E RID: 1342 RVA: 0x00013484 File Offset: 0x00011684
		public PropertyInfo GetProperty(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (types == null)
			{
				throw new ArgumentNullException("types");
			}
			for (int i = 0; i < types.Length; i++)
			{
				if (types[i] == null)
				{
					throw new ArgumentNullException("types");
				}
			}
			return this.GetPropertyImpl(name, bindingAttr, binder, returnType, types, modifiers);
		}

		/// <summary>When overridden in a derived class, searches for the specified property whose parameters match the specified argument types and modifiers, using the specified binding constraints.</summary>
		/// <returns>A <see cref="T:System.Reflection.PropertyInfo" /> object representing the property that matches the specified requirements, if found; otherwise, null.</returns>
		/// <param name="name">The <see cref="T:System.String" /> containing the name of the property to get. </param>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.-or- Zero, to return null. </param>
		/// <param name="binder">A <see cref="T:System.Reflection.Binder" /> object that defines a set of properties and enables binding, which can involve selection of an overloaded member, coercion of argument types, and invocation of a member through reflection.-or- A null reference (Nothing in Visual Basic), to use the <see cref="P:System.Type.DefaultBinder" />. </param>
		/// <param name="returnType">The return type of the property. </param>
		/// <param name="types">An array of <see cref="T:System.Type" /> objects representing the number, order, and type of the parameters for the indexed property to get.-or- An empty array of the type <see cref="T:System.Type" /> (that is, Type[] types = new Type[0]) to get a property that is not indexed. </param>
		/// <param name="modifiers">An array of <see cref="T:System.Reflection.ParameterModifier" /> objects representing the attributes associated with the corresponding element in the <paramref name="types" /> array. The default binder does not process this parameter. </param>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one property is found with the specified name and matching the specified binding constraints. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null.-or- <paramref name="types" /> is null.-or- One of the elements in <paramref name="types" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="types" /> is multidimensional.-or- <paramref name="modifiers" /> is multidimensional.-or- <paramref name="types" /> and <paramref name="modifiers" /> do not have the same length. </exception>
		// Token: 0x0600053F RID: 1343
		protected abstract PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers);

		// Token: 0x06000540 RID: 1344 RVA: 0x000134F0 File Offset: 0x000116F0
		internal PropertyInfo GetPropertyImplInternal(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			return this.GetPropertyImpl(name, bindingAttr, binder, returnType, types, modifiers);
		}

		/// <summary>When overridden in a derived class, searches for a constructor whose parameters match the specified argument types and modifiers, using the specified binding constraints and the specified calling convention.</summary>
		/// <returns>A <see cref="T:System.Reflection.ConstructorInfo" /> object representing the constructor that matches the specified requirements, if found; otherwise, null.</returns>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.-or- Zero, to return null. </param>
		/// <param name="binder">A <see cref="T:System.Reflection.Binder" /> object that defines a set of properties and enables binding, which can involve selection of an overloaded method, coercion of argument types, and invocation of a member through reflection.-or- A null reference (Nothing in Visual Basic), to use the <see cref="P:System.Type.DefaultBinder" />. </param>
		/// <param name="callConvention">The <see cref="T:System.Reflection.CallingConventions" /> object that specifies the set of rules to use regarding the order and layout of arguments, how the return value is passed, what registers are used for arguments, and the stack is cleaned up. </param>
		/// <param name="types">An array of <see cref="T:System.Type" /> objects representing the number, order, and type of the parameters for the constructor to get.-or- An empty array of the type <see cref="T:System.Type" /> (that is, Type[] types = new Type[0]) to get a constructor that takes no parameters. </param>
		/// <param name="modifiers">An array of <see cref="T:System.Reflection.ParameterModifier" /> objects representing the attributes associated with the corresponding element in the <paramref name="types" /> array. The default binder does not process this parameter. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="types" /> is null.-or- One of the elements in <paramref name="types" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="types" /> is multidimensional.-or- <paramref name="modifiers" /> is multidimensional.-or- <paramref name="types" /> and <paramref name="modifiers" /> do not have the same length. </exception>
		// Token: 0x06000541 RID: 1345
		protected abstract ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers);

		/// <summary>When overridden in a derived class, implements the <see cref="P:System.Type.Attributes" /> property and gets a bitmask indicating the attributes associated with the <see cref="T:System.Type" />.</summary>
		/// <returns>A <see cref="T:System.Reflection.TypeAttributes" /> object representing the attribute set of the <see cref="T:System.Type" />.</returns>
		// Token: 0x06000542 RID: 1346
		protected abstract TypeAttributes GetAttributeFlagsImpl();

		/// <summary>When overridden in a derived class, implements the <see cref="P:System.Type.HasElementType" /> property and determines whether the current <see cref="T:System.Type" /> encompasses or refers to another type; that is, whether the current <see cref="T:System.Type" /> is an array, a pointer, or is passed by reference.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is an array, a pointer, or is passed by reference; otherwise, false.</returns>
		// Token: 0x06000543 RID: 1347
		protected abstract bool HasElementTypeImpl();

		/// <summary>When overridden in a derived class, implements the <see cref="P:System.Type.IsArray" /> property and determines whether the <see cref="T:System.Type" /> is an array.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is an array; otherwise, false.</returns>
		// Token: 0x06000544 RID: 1348
		protected abstract bool IsArrayImpl();

		/// <summary>When overridden in a derived class, implements the <see cref="P:System.Type.IsByRef" /> property and determines whether the <see cref="T:System.Type" /> is passed by reference.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is passed by reference; otherwise, false.</returns>
		// Token: 0x06000545 RID: 1349
		protected abstract bool IsByRefImpl();

		/// <summary>When overridden in a derived class, implements the <see cref="P:System.Type.IsCOMObject" /> property and determines whether the <see cref="T:System.Type" /> is a COM object.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is a COM object; otherwise, false.</returns>
		// Token: 0x06000546 RID: 1350
		protected abstract bool IsCOMObjectImpl();

		/// <summary>When overridden in a derived class, implements the <see cref="P:System.Type.IsPointer" /> property and determines whether the <see cref="T:System.Type" /> is a pointer.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is a pointer; otherwise, false.</returns>
		// Token: 0x06000547 RID: 1351
		protected abstract bool IsPointerImpl();

		/// <summary>When overridden in a derived class, implements the <see cref="P:System.Type.IsPrimitive" /> property and determines whether the <see cref="T:System.Type" /> is one of the primitive types.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is one of the primitive types; otherwise, false.</returns>
		// Token: 0x06000548 RID: 1352
		protected abstract bool IsPrimitiveImpl();

		// Token: 0x06000549 RID: 1353
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsArrayImpl(Type type);

		/// <summary>Implements the <see cref="P:System.Type.IsValueType" /> property and determines whether the <see cref="T:System.Type" /> is a value type; that is, not a class or an interface.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is a value type; otherwise, false.</returns>
		// Token: 0x0600054A RID: 1354 RVA: 0x00013504 File Offset: 0x00011704
		protected virtual bool IsValueTypeImpl()
		{
			return this != typeof(ValueType) && this != typeof(Enum) && this.IsSubclassOf(typeof(ValueType));
		}

		/// <summary>Implements the <see cref="P:System.Type.IsContextful" /> property and determines whether the <see cref="T:System.Type" /> can be hosted in a context.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> can be hosted in a context; otherwise, false.</returns>
		// Token: 0x0600054B RID: 1355 RVA: 0x00013544 File Offset: 0x00011744
		protected virtual bool IsContextfulImpl()
		{
			return typeof(ContextBoundObject).IsAssignableFrom(this);
		}

		/// <summary>Implements the <see cref="P:System.Type.IsMarshalByRef" /> property and determines whether the <see cref="T:System.Type" /> is marshaled by reference.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is marshaled by reference; otherwise, false.</returns>
		// Token: 0x0600054C RID: 1356 RVA: 0x00013558 File Offset: 0x00011758
		protected virtual bool IsMarshalByRefImpl()
		{
			return typeof(MarshalByRefObject).IsAssignableFrom(this);
		}

		/// <summary>Searches for a public instance constructor whose parameters match the types in the specified array.</summary>
		/// <returns>A <see cref="T:System.Reflection.ConstructorInfo" /> object representing the public instance constructor whose parameters match the types in the parameter type array, if found; otherwise, null.</returns>
		/// <param name="types">An array of <see cref="T:System.Type" /> objects representing the number, order, and type of the parameters for the desired constructor.-or- An empty array of <see cref="T:System.Type" /> objects, to get a constructor that takes no parameters. Such an empty array is provided by the static field <see cref="F:System.Type.EmptyTypes" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="types" /> is null.-or- One of the elements in <paramref name="types" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="types" /> is multidimensional. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600054D RID: 1357 RVA: 0x0001356C File Offset: 0x0001176C
		[ComVisible(true)]
		public ConstructorInfo GetConstructor(Type[] types)
		{
			return this.GetConstructor(BindingFlags.Instance | BindingFlags.Public, null, CallingConventions.Any, types, null);
		}

		/// <summary>Searches for a constructor whose parameters match the specified argument types and modifiers, using the specified binding constraints.</summary>
		/// <returns>A <see cref="T:System.Reflection.ConstructorInfo" /> object representing the constructor that matches the specified requirements, if found; otherwise, null.</returns>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.-or- Zero, to return null. </param>
		/// <param name="binder">A <see cref="T:System.Reflection.Binder" /> object that defines a set of properties and enables binding, which can involve selection of an overloaded method, coercion of argument types, and invocation of a member through reflection.-or- A null reference (Nothing in Visual Basic), to use the <see cref="P:System.Type.DefaultBinder" />. </param>
		/// <param name="types">An array of <see cref="T:System.Type" /> objects representing the number, order, and type of the parameters for the constructor to get.-or- An empty array of the type <see cref="T:System.Type" /> (that is, Type[] types = new Type[0]) to get a constructor that takes no parameters.-or- <see cref="F:System.Type.EmptyTypes" />. </param>
		/// <param name="modifiers">An array of <see cref="T:System.Reflection.ParameterModifier" /> objects representing the attributes associated with the corresponding element in the parameter type array. The default binder does not process this parameter. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="types" /> is null.-or- One of the elements in <paramref name="types" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="types" /> is multidimensional.-or- <paramref name="modifiers" /> is multidimensional.-or- <paramref name="types" /> and <paramref name="modifiers" /> do not have the same length. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600054E RID: 1358 RVA: 0x0001357C File Offset: 0x0001177C
		[ComVisible(true)]
		public ConstructorInfo GetConstructor(BindingFlags bindingAttr, Binder binder, Type[] types, ParameterModifier[] modifiers)
		{
			return this.GetConstructor(bindingAttr, binder, CallingConventions.Any, types, modifiers);
		}

		/// <summary>Searches for a constructor whose parameters match the specified argument types and modifiers, using the specified binding constraints and the specified calling convention.</summary>
		/// <returns>A <see cref="T:System.Reflection.ConstructorInfo" /> object representing the constructor that matches the specified requirements, if found; otherwise, null.</returns>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.-or- Zero, to return null. </param>
		/// <param name="binder">A <see cref="T:System.Reflection.Binder" /> object that defines a set of properties and enables binding, which can involve selection of an overloaded method, coercion of argument types, and invocation of a member through reflection.-or- A null reference (Nothing in Visual Basic), to use the <see cref="P:System.Type.DefaultBinder" />. </param>
		/// <param name="callConvention">The <see cref="T:System.Reflection.CallingConventions" /> object that specifies the set of rules to use regarding the order and layout of arguments, how the return value is passed, what registers are used for arguments, and the stack is cleaned up. </param>
		/// <param name="types">An array of <see cref="T:System.Type" /> objects representing the number, order, and type of the parameters for the constructor to get.-or- An empty array of the type <see cref="T:System.Type" /> (that is, Type[] types = new Type[0]) to get a constructor that takes no parameters. </param>
		/// <param name="modifiers">An array of <see cref="T:System.Reflection.ParameterModifier" /> objects representing the attributes associated with the corresponding element in the <paramref name="types" /> array. The default binder does not process this parameter. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="types" /> is null.-or- One of the elements in <paramref name="types" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="types" /> is multidimensional.-or- <paramref name="modifiers" /> is multidimensional.-or- <paramref name="types" /> and <paramref name="modifiers" /> do not have the same length. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600054F RID: 1359 RVA: 0x0001358C File Offset: 0x0001178C
		[ComVisible(true)]
		public ConstructorInfo GetConstructor(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			if (types == null)
			{
				throw new ArgumentNullException("types");
			}
			for (int i = 0; i < types.Length; i++)
			{
				if (types[i] == null)
				{
					throw new ArgumentNullException("types");
				}
			}
			return this.GetConstructorImpl(bindingAttr, binder, callConvention, types, modifiers);
		}

		/// <summary>Returns all the public constructors defined for the current <see cref="T:System.Type" />.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.ConstructorInfo" /> objects representing all the public instance constructors defined for the current <see cref="T:System.Type" />, but not including the type initializer (static constructor). If no public instance constructors are defined for the current <see cref="T:System.Type" />, or if the current <see cref="T:System.Type" /> represents a type parameter in the definition of a generic type or generic method, an empty array of type <see cref="T:System.Reflection.ConstructorInfo" /> is returned.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000550 RID: 1360 RVA: 0x000135E4 File Offset: 0x000117E4
		[ComVisible(true)]
		public ConstructorInfo[] GetConstructors()
		{
			return this.GetConstructors(BindingFlags.Instance | BindingFlags.Public);
		}

		/// <summary>When overridden in a derived class, searches for the constructors defined for the current <see cref="T:System.Type" />, using the specified BindingFlags.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.ConstructorInfo" /> objects representing all constructors defined for the current <see cref="T:System.Type" /> that match the specified binding constraints, including the type initializer if it is defined. Returns an empty array of type <see cref="T:System.Reflection.ConstructorInfo" /> if no constructors are defined for the current <see cref="T:System.Type" />, if none of the defined constructors match the binding constraints, or if the current <see cref="T:System.Type" /> represents a type parameter in the definition of a generic type or generic method.</returns>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.-or- Zero, to return null. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000551 RID: 1361
		[ComVisible(true)]
		public abstract ConstructorInfo[] GetConstructors(BindingFlags bindingAttr);

		/// <summary>Searches for the members defined for the current <see cref="T:System.Type" /> whose <see cref="T:System.Reflection.DefaultMemberAttribute" /> is set.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.MemberInfo" /> objects representing all default members of the current <see cref="T:System.Type" />.-or- An empty array of type <see cref="T:System.Reflection.MemberInfo" />, if the current <see cref="T:System.Type" /> does not have default members.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000552 RID: 1362 RVA: 0x000135F0 File Offset: 0x000117F0
		public virtual MemberInfo[] GetDefaultMembers()
		{
			object[] customAttributes = this.GetCustomAttributes(typeof(DefaultMemberAttribute), true);
			if (customAttributes.Length == 0)
			{
				return new MemberInfo[0];
			}
			MemberInfo[] member = this.GetMember(((DefaultMemberAttribute)customAttributes[0]).MemberName);
			return (member == null) ? new MemberInfo[0] : member;
		}

		/// <summary>Returns a filtered array of <see cref="T:System.Reflection.MemberInfo" /> objects of the specified member type.</summary>
		/// <returns>A filtered array of <see cref="T:System.Reflection.MemberInfo" /> objects of the specified member type.-or- An empty array of type <see cref="T:System.Reflection.MemberInfo" />, if the current <see cref="T:System.Type" /> does not have members of type <paramref name="memberType" /> that match the filter criteria.</returns>
		/// <param name="memberType">A MemberTypes object indicating the type of member to search for. </param>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.-or- Zero, to return null. </param>
		/// <param name="filter">The delegate that does the comparisons, returning true if the member currently being inspected matches the <paramref name="filterCriteria" /> and false otherwise. You can use the FilterAttribute, FilterName, and FilterNameIgnoreCase delegates supplied by this class. The first uses the fields of FieldAttributes, MethodAttributes, and MethodImplAttributes as search criteria, and the other two delegates use String objects as the search criteria. </param>
		/// <param name="filterCriteria">The search criteria that determines whether a member is returned in the array of MemberInfo objects.The fields of FieldAttributes, MethodAttributes, and MethodImplAttributes can be used in conjunction with the FilterAttribute delegate supplied by this class. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="filter" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000553 RID: 1363 RVA: 0x00013644 File Offset: 0x00011844
		public virtual MemberInfo[] FindMembers(MemberTypes memberType, BindingFlags bindingAttr, MemberFilter filter, object filterCriteria)
		{
			ArrayList arrayList = new ArrayList();
			if ((memberType & MemberTypes.Method) != (MemberTypes)0)
			{
				MethodInfo[] methods = this.GetMethods(bindingAttr);
				if (filter != null)
				{
					foreach (MethodInfo memberInfo in methods)
					{
						if (filter(memberInfo, filterCriteria))
						{
							arrayList.Add(memberInfo);
						}
					}
				}
				else
				{
					arrayList.AddRange(methods);
				}
			}
			if ((memberType & MemberTypes.Constructor) != (MemberTypes)0)
			{
				ConstructorInfo[] constructors = this.GetConstructors(bindingAttr);
				if (filter != null)
				{
					foreach (ConstructorInfo memberInfo2 in constructors)
					{
						if (filter(memberInfo2, filterCriteria))
						{
							arrayList.Add(memberInfo2);
						}
					}
				}
				else
				{
					arrayList.AddRange(constructors);
				}
			}
			if ((memberType & MemberTypes.Property) != (MemberTypes)0)
			{
				int count = arrayList.Count;
				if (filter != null)
				{
					Type type = this;
					while (arrayList.Count == count && type != null)
					{
						PropertyInfo[] properties = type.GetProperties(bindingAttr);
						foreach (PropertyInfo memberInfo3 in properties)
						{
							if (filter(memberInfo3, filterCriteria))
							{
								arrayList.Add(memberInfo3);
							}
						}
						type = type.BaseType;
					}
				}
				else
				{
					PropertyInfo[] properties = this.GetProperties(bindingAttr);
					arrayList.AddRange(properties);
				}
			}
			if ((memberType & MemberTypes.Event) != (MemberTypes)0)
			{
				EventInfo[] events = this.GetEvents(bindingAttr);
				if (filter != null)
				{
					foreach (EventInfo memberInfo4 in events)
					{
						if (filter(memberInfo4, filterCriteria))
						{
							arrayList.Add(memberInfo4);
						}
					}
				}
				else
				{
					arrayList.AddRange(events);
				}
			}
			if ((memberType & MemberTypes.Field) != (MemberTypes)0)
			{
				FieldInfo[] fields = this.GetFields(bindingAttr);
				if (filter != null)
				{
					foreach (FieldInfo memberInfo5 in fields)
					{
						if (filter(memberInfo5, filterCriteria))
						{
							arrayList.Add(memberInfo5);
						}
					}
				}
				else
				{
					arrayList.AddRange(fields);
				}
			}
			if ((memberType & MemberTypes.NestedType) != (MemberTypes)0)
			{
				Type[] nestedTypes = this.GetNestedTypes(bindingAttr);
				if (filter != null)
				{
					foreach (Type memberInfo6 in nestedTypes)
					{
						if (filter(memberInfo6, filterCriteria))
						{
							arrayList.Add(memberInfo6);
						}
					}
				}
				else
				{
					arrayList.AddRange(nestedTypes);
				}
			}
			MemberInfo[] array7;
			switch (memberType)
			{
			case MemberTypes.Constructor:
				array7 = new ConstructorInfo[arrayList.Count];
				break;
			case MemberTypes.Event:
				array7 = new EventInfo[arrayList.Count];
				break;
			default:
				if (memberType != MemberTypes.Property)
				{
					if (memberType != MemberTypes.TypeInfo && memberType != MemberTypes.NestedType)
					{
						array7 = new MemberInfo[arrayList.Count];
					}
					else
					{
						array7 = new Type[arrayList.Count];
					}
				}
				else
				{
					array7 = new PropertyInfo[arrayList.Count];
				}
				break;
			case MemberTypes.Field:
				array7 = new FieldInfo[arrayList.Count];
				break;
			case MemberTypes.Method:
				array7 = new MethodInfo[arrayList.Count];
				break;
			}
			arrayList.CopyTo(array7);
			return array7;
		}

		/// <summary>Invokes the specified member, using the specified binding constraints and matching the specified argument list.</summary>
		/// <returns>An <see cref="T:System.Object" /> representing the return value of the invoked member.</returns>
		/// <param name="name">The <see cref="T:System.String" /> containing the name of the constructor, method, property, or field member to invoke.-or- An empty string ("") to invoke the default member. -or-For IDispatch members, a string representing the DispID, for example "[DispID=3]".</param>
		/// <param name="invokeAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted. The access can be one of the BindingFlags such as Public, NonPublic, Private, InvokeMethod, GetField, and so on. The type of lookup need not be specified. If the type of lookup is omitted, BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static are used. </param>
		/// <param name="binder">A <see cref="T:System.Reflection.Binder" /> object that defines a set of properties and enables binding, which can involve selection of an overloaded method, coercion of argument types, and invocation of a member through reflection.-or- A null reference (Nothing in Visual Basic), to use the <see cref="P:System.Type.DefaultBinder" />. Note that explicitly defining a <see cref="T:System.Reflection.Binder" /> object may be required for successfully invoking method overloads with variable arguments.</param>
		/// <param name="target">The <see cref="T:System.Object" /> on which to invoke the specified member. </param>
		/// <param name="args">An array containing the arguments to pass to the member to invoke. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="invokeAttr" /> does not contain CreateInstance and <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="invokeAttr" /> is not a valid <see cref="T:System.Reflection.BindingFlags" /> attribute. -or- <paramref name="invokeAttr" /> does not contain one of the following binding flags: InvokeMethod, CreateInstance, GetField, SetField, GetProperty, or SetProperty. -or- <paramref name="invokeAttr" /> contains CreateInstance combined with InvokeMethod, GetField, SetField, GetProperty, or SetProperty.-or- <paramref name="invokeAttr" /> contains both GetField and SetField.-or- <paramref name="invokeAttr" /> contains both GetProperty and SetProperty.-or- <paramref name="invokeAttr" /> contains InvokeMethod combined with SetField or SetProperty.-or- <paramref name="invokeAttr" /> contains SetField and <paramref name="args" /> has more than one element.-or- This method is called on a COM object and one of the following binding flags was not passed in: BindingFlags.InvokeMethod, BindingFlags.GetProperty, BindingFlags.SetProperty, BindingFlags.PutDispProperty, or BindingFlags.PutRefDispProperty.-or- One of the named parameter arrays contains a string that is null. </exception>
		/// <exception cref="T:System.MethodAccessException">The specified member is a class initializer. </exception>
		/// <exception cref="T:System.MissingFieldException">The field or property cannot be found. </exception>
		/// <exception cref="T:System.MissingMethodException">No method can be found that matches the arguments in <paramref name="args" />.-or- The current <see cref="T:System.Type" /> object represents a type that contains open type parameters, that is, <see cref="P:System.Type.ContainsGenericParameters" /> returns true. </exception>
		/// <exception cref="T:System.Reflection.TargetException">The specified member cannot be invoked on <paramref name="target" />. </exception>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one method matches the binding criteria. </exception>
		/// <exception cref="T:System.NotSupportedException">The .NET Compact Framework does not currently support this method.</exception>
		/// <exception cref="T:System.InvalidOperationException">The method represented by <paramref name="name" /> has one or more unspecified generic type parameters. That is, the method's <see cref="P:System.Reflection.MethodInfo.ContainsGenericParameters" /> property returns true.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000554 RID: 1364 RVA: 0x0001399C File Offset: 0x00011B9C
		[DebuggerStepThrough]
		[DebuggerHidden]
		public object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args)
		{
			return this.InvokeMember(name, invokeAttr, binder, target, args, null, null, null);
		}

		/// <summary>Invokes the specified member, using the specified binding constraints and matching the specified argument list and culture.</summary>
		/// <returns>An <see cref="T:System.Object" /> representing the return value of the invoked member.</returns>
		/// <param name="name">The <see cref="T:System.String" /> containing the name of the constructor, method, property, or field member to invoke.-or- An empty string ("") to invoke the default member. -or-For IDispatch members, a string representing the DispID, for example "[DispID=3]".</param>
		/// <param name="invokeAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted. The access can be one of the BindingFlags such as Public, NonPublic, Private, InvokeMethod, GetField, and so on. The type of lookup need not be specified. If the type of lookup is omitted, BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static are used. </param>
		/// <param name="binder">A <see cref="T:System.Reflection.Binder" /> object that defines a set of properties and enables binding, which can involve selection of an overloaded method, coercion of argument types, and invocation of a member through reflection.-or- A null reference (Nothing in Visual Basic), to use the <see cref="P:System.Type.DefaultBinder" />. Note that explicitly defining a <see cref="T:System.Reflection.Binder" /> object may be required for successfully invoking method overloads with variable arguments.</param>
		/// <param name="target">The <see cref="T:System.Object" /> on which to invoke the specified member. </param>
		/// <param name="args">An array containing the arguments to pass to the member to invoke. </param>
		/// <param name="culture">The <see cref="T:System.Globalization.CultureInfo" /> object representing the globalization locale to use, which may be necessary for locale-specific conversions, such as converting a numeric String to a Double.-or- A null reference (Nothing in Visual Basic) to use the current thread's <see cref="T:System.Globalization.CultureInfo" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="invokeAttr" /> does not contain CreateInstance and <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="invokeAttr" /> is not a valid <see cref="T:System.Reflection.BindingFlags" /> attribute. -or- <paramref name="invokeAttr" /> does not contain one of the following binding flags: InvokeMethod, CreateInstance, GetField, SetField, GetProperty, or SetProperty.-or- <paramref name="invokeAttr" /> contains CreateInstance combined with InvokeMethod, GetField, SetField, GetProperty, or SetProperty.-or- <paramref name="invokeAttr" /> contains both GetField and SetField.-or- <paramref name="invokeAttr" /> contains both GetProperty and SetProperty.-or- <paramref name="invokeAttr" /> contains InvokeMethod combined with SetField or SetProperty.-or- <paramref name="invokeAttr" /> contains SetField and <paramref name="args" /> has more than one element.-or- This method is called on a COM object and one of the following binding flags was not passed in: BindingFlags.InvokeMethod, BindingFlags.GetProperty, BindingFlags.SetProperty, BindingFlags.PutDispProperty, or BindingFlags.PutRefDispProperty.-or- One of the named parameter arrays contains a string that is null. </exception>
		/// <exception cref="T:System.MethodAccessException">The specified member is a class initializer. </exception>
		/// <exception cref="T:System.MissingFieldException">The field or property cannot be found. </exception>
		/// <exception cref="T:System.MissingMethodException">No method can be found that matches the arguments in <paramref name="args" />.-or- The current <see cref="T:System.Type" /> object represents a type that contains open type parameters, that is, <see cref="P:System.Type.ContainsGenericParameters" /> returns true. </exception>
		/// <exception cref="T:System.Reflection.TargetException">The specified member cannot be invoked on <paramref name="target" />. </exception>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one method matches the binding criteria. </exception>
		/// <exception cref="T:System.InvalidOperationException">The method represented by <paramref name="name" /> has one or more unspecified generic type parameters. That is, the method's <see cref="P:System.Reflection.MethodInfo.ContainsGenericParameters" /> property returns true.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000555 RID: 1365 RVA: 0x000139BC File Offset: 0x00011BBC
		[DebuggerStepThrough]
		[DebuggerHidden]
		public object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, CultureInfo culture)
		{
			return this.InvokeMember(name, invokeAttr, binder, target, args, null, culture, null);
		}

		/// <summary>When overridden in a derived class, invokes the specified member, using the specified binding constraints and matching the specified argument list, modifiers and culture.</summary>
		/// <returns>An <see cref="T:System.Object" /> representing the return value of the invoked member.</returns>
		/// <param name="name">The <see cref="T:System.String" /> containing the name of the constructor, method, property, or field member to invoke.-or- An empty string ("") to invoke the default member. -or-For IDispatch members, a string representing the DispID, for example "[DispID=3]".</param>
		/// <param name="invokeAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted. The access can be one of the BindingFlags such as Public, NonPublic, Private, InvokeMethod, GetField, and so on. The type of lookup need not be specified. If the type of lookup is omitted, BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static are used. </param>
		/// <param name="binder">A <see cref="T:System.Reflection.Binder" /> object that defines a set of properties and enables binding, which can involve selection of an overloaded method, coercion of argument types, and invocation of a member through reflection.-or- A null reference (Nothing in Visual Basic), to use the <see cref="P:System.Type.DefaultBinder" />. Note that explicitly defining a <see cref="T:System.Reflection.Binder" /> object may be required for successfully invoking method overloads with variable arguments.</param>
		/// <param name="target">The <see cref="T:System.Object" /> on which to invoke the specified member. </param>
		/// <param name="args">An array containing the arguments to pass to the member to invoke. </param>
		/// <param name="modifiers">An array of <see cref="T:System.Reflection.ParameterModifier" /> objects representing the attributes associated with the corresponding element in the <paramref name="args" /> array. A parameter's associated attributes are stored in the member's signature. The default binder processes this parameter only when calling a COM component. </param>
		/// <param name="culture">The <see cref="T:System.Globalization.CultureInfo" /> object representing the globalization locale to use, which may be necessary for locale-specific conversions, such as converting a numeric String to a Double.-or- A null reference (Nothing in Visual Basic) to use the current thread's <see cref="T:System.Globalization.CultureInfo" />. </param>
		/// <param name="namedParameters">An array containing the names of the parameters to which the values in the <paramref name="args" /> array are passed. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="invokeAttr" /> does not contain CreateInstance and <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="args" /> and <paramref name="modifiers" /> do not have the same length.-or- <paramref name="invokeAttr" /> is not a valid <see cref="T:System.Reflection.BindingFlags" /> attribute.-or- <paramref name="invokeAttr" /> does not contain one of the following binding flags: InvokeMethod, CreateInstance, GetField, SetField, GetProperty, or SetProperty.-or- <paramref name="invokeAttr" /> contains CreateInstance combined with InvokeMethod, GetField, SetField, GetProperty, or SetProperty.-or- <paramref name="invokeAttr" /> contains both GetField and SetField.-or- <paramref name="invokeAttr" /> contains both GetProperty and SetProperty.-or- <paramref name="invokeAttr" /> contains InvokeMethod combined with SetField or SetProperty.-or- <paramref name="invokeAttr" /> contains SetField and <paramref name="args" /> has more than one element.-or- The named parameter array is larger than the argument array.-or- This method is called on a COM object and one of the following binding flags was not passed in: BindingFlags.InvokeMethod, BindingFlags.GetProperty, BindingFlags.SetProperty, BindingFlags.PutDispProperty, or BindingFlags.PutRefDispProperty.-or- One of the named parameter arrays contains a string that is null. </exception>
		/// <exception cref="T:System.MethodAccessException">The specified member is a class initializer. </exception>
		/// <exception cref="T:System.MissingFieldException">The field or property cannot be found. </exception>
		/// <exception cref="T:System.MissingMethodException">No method can be found that matches the arguments in <paramref name="args" />.-or- No member can be found that has the argument names supplied in <paramref name="namedParameters" />.-or- The current <see cref="T:System.Type" /> object represents a type that contains open type parameters, that is, <see cref="P:System.Type.ContainsGenericParameters" /> returns true. </exception>
		/// <exception cref="T:System.Reflection.TargetException">The specified member cannot be invoked on <paramref name="target" />. </exception>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one method matches the binding criteria. </exception>
		/// <exception cref="T:System.InvalidOperationException">The method represented by <paramref name="name" /> has one or more unspecified generic type parameters. That is, the method's <see cref="P:System.Reflection.MethodInfo.ContainsGenericParameters" /> property returns true.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000556 RID: 1366
		public abstract object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters);

		/// <summary>Returns a String representing the name of the current Type.</summary>
		/// <returns>A <see cref="T:System.String" /> representing the name of the current <see cref="T:System.Type" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000557 RID: 1367 RVA: 0x000139DC File Offset: 0x00011BDC
		public override string ToString()
		{
			return this.FullName;
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000558 RID: 1368 RVA: 0x000139E4 File Offset: 0x00011BE4
		internal bool IsSystemType
		{
			get
			{
				return this._impl.Value != IntPtr.Zero;
			}
		}

		/// <summary>Returns an array of <see cref="T:System.Type" /> objects that represent the type arguments of a generic type or the type parameters of a generic type definition.</summary>
		/// <returns>An array of <see cref="T:System.Type" /> objects that represent the type arguments of a generic type. Returns an empty array if the current type is not a generic type.</returns>
		/// <exception cref="T:System.NotSupportedException">The invoked method is not supported in the base class. Derived classes must provide an implementation.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000559 RID: 1369 RVA: 0x000139FC File Offset: 0x00011BFC
		public virtual Type[] GetGenericArguments()
		{
			throw new NotSupportedException();
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Type" /> object has type parameters that have not been replaced by specific types.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> object is itself a generic type parameter or has type parameters for which specific types have not been supplied; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600055A RID: 1370 RVA: 0x00013A04 File Offset: 0x00011C04
		public virtual bool ContainsGenericParameters
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Type" /> represents a generic type definition, from which other generic types can be constructed.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> object represents a generic type definition; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600055B RID: 1371
		public virtual extern bool IsGenericTypeDefinition { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x0600055C RID: 1372
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern Type GetGenericTypeDefinition_impl();

		/// <summary>Returns a <see cref="T:System.Type" /> object that represents a generic type definition from which the current generic type can be constructed.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing a generic type from which the current type can be constructed.</returns>
		/// <exception cref="T:System.InvalidOperationException">The current type is not a generic type.  That is, <see cref="P:System.Type.IsGenericType" /> returns false. </exception>
		/// <exception cref="T:System.NotSupportedException">The invoked method is not supported in the base class. Derived classes must provide an implementation.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600055D RID: 1373 RVA: 0x00013A08 File Offset: 0x00011C08
		public virtual Type GetGenericTypeDefinition()
		{
			throw new NotSupportedException("Derived classes must provide an implementation.");
		}

		/// <summary>Gets a value indicating whether the current type is a generic type.</summary>
		/// <returns>true if the current type is a generic type; otherwise, false.</returns>
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600055E RID: 1374
		public virtual extern bool IsGenericType { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x0600055F RID: 1375
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Type MakeGenericType(Type gt, Type[] types);

		/// <summary>Substitutes the elements of an array of types for the type parameters of the current generic type definition and returns a <see cref="T:System.Type" /> object representing the resulting constructed type.</summary>
		/// <returns>A <see cref="T:System.Type" /> representing the constructed type formed by substituting the elements of <paramref name="typeArguments" /> for the type parameters of the current generic type.</returns>
		/// <param name="typeArguments">An array of types to be substituted for the type parameters of the current generic type.</param>
		/// <exception cref="T:System.InvalidOperationException">The current type does not represent a generic type definition. That is, <see cref="P:System.Type.IsGenericTypeDefinition" /> returns false. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeArguments" /> is null.-or- Any element of <paramref name="typeArguments" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The number of elements in <paramref name="typeArguments" /> is not the same as the number of type parameters in the current generic type definition.-or- Any element of <paramref name="typeArguments" /> does not satisfy the constraints specified for the corresponding type parameter of the current generic type. -or- <paramref name="typeArguments" /> contains an element that is a pointer type (<see cref="P:System.Type.IsPointer" /> returns true), a by-ref type (<see cref="P:System.Type.IsByRef" /> returns true), or <see cref="T:System.Void" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The invoked method is not supported in the base class. Derived classes must provide an implementation.</exception>
		// Token: 0x06000560 RID: 1376 RVA: 0x00013A14 File Offset: 0x00011C14
		public virtual Type MakeGenericType(params Type[] typeArguments)
		{
			if (!this.IsGenericTypeDefinition)
			{
				throw new InvalidOperationException("not a generic type definition");
			}
			if (typeArguments == null)
			{
				throw new ArgumentNullException("typeArguments");
			}
			if (this.GetGenericArguments().Length != typeArguments.Length)
			{
				throw new ArgumentException(string.Format("The type or method has {0} generic parameter(s) but {1} generic argument(s) where provided. A generic argument must be provided for each generic parameter.", this.GetGenericArguments().Length, typeArguments.Length), "typeArguments");
			}
			Type[] array = new Type[typeArguments.Length];
			for (int i = 0; i < typeArguments.Length; i++)
			{
				Type type = typeArguments[i];
				if (type == null)
				{
					throw new ArgumentNullException("typeArguments");
				}
				if (!(type is EnumBuilder) && !(type is TypeBuilder))
				{
					type = type.UnderlyingSystemType;
				}
				if (type == null || !type.IsSystemType)
				{
					throw new ArgumentNullException("typeArguments");
				}
				array[i] = type;
			}
			Type type2 = Type.MakeGenericType(this, array);
			if (type2 == null)
			{
				throw new TypeLoadException();
			}
			return type2;
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Type" /> represents a type parameter in the definition of a generic type or method.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> object represents a type parameter of a generic type definition or generic method definition; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000561 RID: 1377 RVA: 0x00013B08 File Offset: 0x00011D08
		public virtual bool IsGenericParameter
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Type" /> object represents a type whose definition is nested inside the definition of another type.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is nested inside another type; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000562 RID: 1378 RVA: 0x00013B0C File Offset: 0x00011D0C
		public bool IsNested
		{
			get
			{
				return this.DeclaringType != null;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> can be accessed by code outside the assembly.</summary>
		/// <returns>true if the current <see cref="T:System.Type" /> is a public type or a public nested type such that all the enclosing types are public; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000563 RID: 1379 RVA: 0x00013B1C File Offset: 0x00011D1C
		public bool IsVisible
		{
			get
			{
				if (this.IsNestedPublic)
				{
					return this.DeclaringType.IsVisible;
				}
				return this.IsPublic;
			}
		}

		// Token: 0x06000564 RID: 1380
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetGenericParameterPosition();

		/// <summary>Gets the position of the type parameter in the type parameter list of the generic type or method that declared the parameter, when the <see cref="T:System.Type" /> object represents a type parameter of a generic type or a generic method.</summary>
		/// <returns>The position of a type parameter in the type parameter list of the generic type or method that defines the parameter. Position numbers begin at 0.</returns>
		/// <exception cref="T:System.InvalidOperationException">The current type does not represent a type parameter. That is, <see cref="P:System.Type.IsGenericParameter" /> returns false. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000565 RID: 1381 RVA: 0x00013B48 File Offset: 0x00011D48
		public virtual int GenericParameterPosition
		{
			get
			{
				int genericParameterPosition = this.GetGenericParameterPosition();
				if (genericParameterPosition < 0)
				{
					throw new InvalidOperationException();
				}
				return genericParameterPosition;
			}
		}

		// Token: 0x06000566 RID: 1382
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern GenericParameterAttributes GetGenericParameterAttributes();

		/// <summary>Gets a combination of <see cref="T:System.Reflection.GenericParameterAttributes" /> flags that describe the covariance and special constraints of the current generic type parameter. </summary>
		/// <returns>A bitwise combination of <see cref="T:System.Reflection.GenericParameterAttributes" /> values that describes the covariance and special constraints of the current generic type parameter.</returns>
		/// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Type" /> object is not a generic type parameter. That is, the <see cref="P:System.Type.IsGenericParameter" /> property returns false.</exception>
		/// <exception cref="T:System.NotSupportedException">The invoked method is not supported in the base class.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000567 RID: 1383 RVA: 0x00013B6C File Offset: 0x00011D6C
		public virtual GenericParameterAttributes GenericParameterAttributes
		{
			get
			{
				if (!this.IsGenericParameter)
				{
					throw new InvalidOperationException();
				}
				return this.GetGenericParameterAttributes();
			}
		}

		// Token: 0x06000568 RID: 1384
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Type[] GetGenericParameterConstraints_impl();

		/// <summary>Returns an array of <see cref="T:System.Type" /> objects that represent the constraints on the current generic type parameter. </summary>
		/// <returns>An array of <see cref="T:System.Type" /> objects that represent the constraints on the current generic type parameter.</returns>
		/// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Type" /> object is not a generic type parameter. That is, the <see cref="P:System.Type.IsGenericParameter" /> property returns false.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000569 RID: 1385 RVA: 0x00013B88 File Offset: 0x00011D88
		public virtual Type[] GetGenericParameterConstraints()
		{
			if (!this.IsGenericParameter)
			{
				throw new InvalidOperationException();
			}
			return this.GetGenericParameterConstraints_impl();
		}

		/// <summary>Gets a <see cref="T:System.Reflection.MethodBase" /> that represents the declaring method, if the current <see cref="T:System.Type" /> represents a type parameter of a generic method.</summary>
		/// <returns>If the current <see cref="T:System.Type" /> represents a type parameter of a generic method, a <see cref="T:System.Reflection.MethodBase" /> that represents declaring method; otherwise, null.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600056A RID: 1386 RVA: 0x00013BA4 File Offset: 0x00011DA4
		public virtual MethodBase DeclaringMethod
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600056B RID: 1387
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Type make_array_type(int rank);

		/// <summary>Returns a <see cref="T:System.Type" /> object representing a one-dimensional array of the current type, with a lower bound of zero.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing a one-dimensional array of the current type, with a lower bound of zero.</returns>
		/// <exception cref="T:System.NotSupportedException">The invoked method is not supported in the base class. Derived classes must provide an implementation.</exception>
		/// <exception cref="T:System.TypeLoadException">The current type is <see cref="T:System.TypedReference" />.-or-The current type is a ByRef type. That is, <see cref="P:System.Type.IsByRef" /> returns true. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600056C RID: 1388 RVA: 0x00013BA8 File Offset: 0x00011DA8
		public virtual Type MakeArrayType()
		{
			return this.make_array_type(0);
		}

		/// <summary>Returns a <see cref="T:System.Type" /> object representing an array of the current type, with the specified number of dimensions.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing an array of the current type, with the specified number of dimensions.</returns>
		/// <param name="rank">The number of dimensions for the array. The number must be less than or equal to 32.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">
		///   <paramref name="rank" /> is invalid. For example, 0 or negative.</exception>
		/// <exception cref="T:System.NotSupportedException">The invoked method is not supported in the base class.</exception>
		/// <exception cref="T:System.TypeLoadException">The current type is <see cref="T:System.TypedReference" />.-or-The current type is a ByRef type. That is, <see cref="P:System.Type.IsByRef" /> returns true. -or-<paramref name="rank" /> is greater than 32.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600056D RID: 1389 RVA: 0x00013BB4 File Offset: 0x00011DB4
		public virtual Type MakeArrayType(int rank)
		{
			if (rank < 1)
			{
				throw new IndexOutOfRangeException();
			}
			return this.make_array_type(rank);
		}

		// Token: 0x0600056E RID: 1390
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Type make_byref_type();

		/// <summary>Returns a <see cref="T:System.Type" /> object that represents the current type when passed as a ref parameter (ByRef parameter in Visual Basic).</summary>
		/// <returns>A <see cref="T:System.Type" /> object that represents the current type when passed as a ref parameter (ByRef parameter in Visual Basic).</returns>
		/// <exception cref="T:System.NotSupportedException">The invoked method is not supported in the base class.</exception>
		/// <exception cref="T:System.TypeLoadException">The current type is <see cref="T:System.TypedReference" />.-or-The current type is a ByRef type. That is, <see cref="P:System.Type.IsByRef" /> returns true. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600056F RID: 1391 RVA: 0x00013BCC File Offset: 0x00011DCC
		public virtual Type MakeByRefType()
		{
			return this.make_byref_type();
		}

		/// <summary>Returns a <see cref="T:System.Type" /> object that represents a pointer to the current type.</summary>
		/// <returns>A <see cref="T:System.Type" /> object that represents a pointer to the current type.</returns>
		/// <exception cref="T:System.NotSupportedException">The invoked method is not supported in the base class.</exception>
		/// <exception cref="T:System.TypeLoadException">The current type is <see cref="T:System.TypedReference" />.-or-The current type is a ByRef type. That is, <see cref="P:System.Type.IsByRef" /> returns true. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000570 RID: 1392
		[MethodImpl(MethodImplOptions.InternalCall)]
		public virtual extern Type MakePointerType();

		/// <summary>Gets the <see cref="T:System.Type" /> with the specified name, specifying whether to perform a case-sensitive search and whether to throw an exception if the type is not found. The type is loaded for reflection only, not for execution.</summary>
		/// <returns>The type with the specified name, if found; otherwise, null. If the type is not found, the <paramref name="throwIfNotFound" /> parameter specifies whether null is returned or an exception is thrown. In some cases, an exception is thrown regardless of the value of <paramref name="throwIfNotFound" />. See the Exceptions section.</returns>
		/// <param name="typeName">The assembly-qualified name of the <see cref="T:System.Type" /> to get.</param>
		/// <param name="throwIfNotFound">true to throw a <see cref="T:System.TypeLoadException" /> if the type cannot be found; false to return null if the type cannot be found. Specifying false also suppresses some other exception conditions, but not all of them. See the Exceptions section.</param>
		/// <param name="ignoreCase">true to perform a case-insensitive search for <paramref name="typeName" />; false to perform a case-sensitive search for <paramref name="typeName" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeName" /> is null. </exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">A class initializer is invoked and throws an exception. </exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="throwIfNotFound" /> is true and the type is not found. -or-<paramref name="throwIfNotFound" /> is true and <paramref name="typeName" /> contains invalid characters, such as an embedded tab.-or-<paramref name="throwIfNotFound" /> is true and <paramref name="typeName" /> is an empty string.-or-<paramref name="throwIfNotFound" /> is true and <paramref name="typeName" /> represents an array type with an invalid size. -or-<paramref name="typeName" /> represents an array of <see cref="T:System.TypedReference" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="typeName" /> does not include the assembly name.-or-<paramref name="throwIfNotFound" /> is true and <paramref name="typeName" /> contains invalid syntax. For example, "MyType[,*,]".-or-<paramref name="typeName" /> represents a generic type that has a pointer type, a ByRef type, or <see cref="T:System.Void" /> as one of its type arguments.-or-<paramref name="typeName" /> represents a generic type that has an incorrect number of type arguments.-or-<paramref name="typeName" /> represents a generic type, and one of its type arguments does not satisfy the constraints for the corresponding type parameter.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="throwIfNotFound" /> is true and the assembly or one of its dependencies was not found. </exception>
		/// <exception cref="T:System.IO.FileLoadException">The assembly or one of its dependencies was found, but could not be loaded. </exception>
		/// <exception cref="T:System.BadImageFormatException">The assembly or one of its dependencies is not valid. -or-The assembly was compiled with a later version of the common language runtime than the version that is currently loaded.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000571 RID: 1393 RVA: 0x00013BD4 File Offset: 0x00011DD4
		public static Type ReflectionOnlyGetType(string typeName, bool throwIfNotFound, bool ignoreCase)
		{
			if (typeName == null)
			{
				throw new ArgumentNullException("typeName");
			}
			int num = typeName.IndexOf(',');
			if (num < 0 || num == 0 || num == typeName.Length - 1)
			{
				throw new ArgumentException("Assembly qualifed type name is required", "typeName");
			}
			string assemblyString = typeName.Substring(num + 1);
			Assembly assembly;
			try
			{
				assembly = Assembly.ReflectionOnlyLoad(assemblyString);
			}
			catch
			{
				if (throwIfNotFound)
				{
					throw;
				}
				return null;
			}
			return assembly.GetType(typeName.Substring(0, num), throwIfNotFound, ignoreCase);
		}

		// Token: 0x06000572 RID: 1394
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetPacking(out int packing, out int size);

		/// <summary>Gets a <see cref="T:System.Runtime.InteropServices.StructLayoutAttribute" /> that describes the layout of the current type.</summary>
		/// <returns>Gets a <see cref="T:System.Runtime.InteropServices.StructLayoutAttribute" /> that describes the gross layout features of the current type.</returns>
		/// <exception cref="T:System.NotSupportedException">The invoked method is not supported in the base class.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000573 RID: 1395 RVA: 0x00013C80 File Offset: 0x00011E80
		public virtual StructLayoutAttribute StructLayoutAttribute
		{
			get
			{
				LayoutKind layoutKind;
				if (this.IsLayoutSequential)
				{
					layoutKind = LayoutKind.Sequential;
				}
				else if (this.IsExplicitLayout)
				{
					layoutKind = LayoutKind.Explicit;
				}
				else
				{
					layoutKind = LayoutKind.Auto;
				}
				StructLayoutAttribute structLayoutAttribute = new StructLayoutAttribute(layoutKind);
				if (this.IsUnicodeClass)
				{
					structLayoutAttribute.CharSet = CharSet.Unicode;
				}
				else if (this.IsAnsiClass)
				{
					structLayoutAttribute.CharSet = CharSet.Ansi;
				}
				else
				{
					structLayoutAttribute.CharSet = CharSet.Auto;
				}
				if (layoutKind != LayoutKind.Auto)
				{
					this.GetPacking(out structLayoutAttribute.Pack, out structLayoutAttribute.Size);
				}
				return structLayoutAttribute;
			}
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x00013D0C File Offset: 0x00011F0C
		internal object[] GetPseudoCustomAttributes()
		{
			int num = 0;
			if ((this.Attributes & TypeAttributes.Serializable) != TypeAttributes.NotPublic)
			{
				num++;
			}
			if ((this.Attributes & TypeAttributes.Import) != TypeAttributes.NotPublic)
			{
				num++;
			}
			if (num == 0)
			{
				return null;
			}
			object[] array = new object[num];
			num = 0;
			if ((this.Attributes & TypeAttributes.Serializable) != TypeAttributes.NotPublic)
			{
				array[num++] = new SerializableAttribute();
			}
			if ((this.Attributes & TypeAttributes.Import) != TypeAttributes.NotPublic)
			{
				array[num++] = new ComImportAttribute();
			}
			return array;
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000575 RID: 1397 RVA: 0x00013D94 File Offset: 0x00011F94
		internal bool IsUserType
		{
			get
			{
				return this._impl.Value == IntPtr.Zero && (this.GetType().Assembly != typeof(Type).Assembly || this.GetType() == typeof(TypeDelegator));
			}
		}

		// Token: 0x0400006E RID: 110
		internal const BindingFlags DefaultBindingFlags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public;

		// Token: 0x0400006F RID: 111
		internal RuntimeTypeHandle _impl;

		/// <summary>Separates names in the namespace of the <see cref="T:System.Type" />. This field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000070 RID: 112
		public static readonly char Delimiter = '.';

		/// <summary>Represents an empty array of type <see cref="T:System.Type" />. This field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000071 RID: 113
		public static readonly Type[] EmptyTypes = new Type[0];

		/// <summary>Represents the member filter used on attributes. This field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000072 RID: 114
		public static readonly MemberFilter FilterAttribute = new MemberFilter(Type.FilterAttribute_impl);

		/// <summary>Represents the case-sensitive member filter used on names. This field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000073 RID: 115
		public static readonly MemberFilter FilterName = new MemberFilter(Type.FilterName_impl);

		/// <summary>Represents the case-insensitive member filter used on names. This field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000074 RID: 116
		public static readonly MemberFilter FilterNameIgnoreCase = new MemberFilter(Type.FilterNameIgnoreCase_impl);

		/// <summary>Represents a missing value in the <see cref="T:System.Type" /> information. This field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000075 RID: 117
		public static readonly object Missing = System.Reflection.Missing.Value;
	}
}
