using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Defines the member of a type that is the default member used by <see cref="M:System.Type.InvokeMember(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Object,System.Object[],System.Reflection.ParameterModifier[],System.Globalization.CultureInfo,System.String[])" />. </summary>
	// Token: 0x02000056 RID: 86
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface)]
	[Serializable]
	public sealed class DefaultMemberAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.DefaultMemberAttribute" /> class.</summary>
		/// <param name="memberName">A String containing the name of the member to invoke. This may be a constructor, method, property, or field. A suitable invocation attribute must be specified when the member is invoked. The default member of a class can be specified by passing an empty String as the name of the member.The default member of a type is marked with the DefaultMemberAttribute custom attribute or marked in COM in the usual way. </param>
		// Token: 0x06000685 RID: 1669 RVA: 0x00014C60 File Offset: 0x00012E60
		public DefaultMemberAttribute(string memberName)
		{
			this.member_name = memberName;
		}

		/// <summary>Gets the name from the attribute.</summary>
		/// <returns>A string representing the member name.</returns>
		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000686 RID: 1670 RVA: 0x00014C70 File Offset: 0x00012E70
		public string MemberName
		{
			get
			{
				return this.member_name;
			}
		}

		// Token: 0x040000A8 RID: 168
		private string member_name;
	}
}
