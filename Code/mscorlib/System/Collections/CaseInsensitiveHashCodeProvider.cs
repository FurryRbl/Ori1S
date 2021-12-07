using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Collections
{
	/// <summary>Supplies a hash code for an object, using a hashing algorithm that ignores the case of strings.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001B4 RID: 436
	[ComVisible(true)]
	[Obsolete("Please use StringComparer instead.")]
	[Serializable]
	public class CaseInsensitiveHashCodeProvider : IHashCodeProvider
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.CaseInsensitiveHashCodeProvider" /> class using the <see cref="P:System.Threading.Thread.CurrentCulture" /> of the current thread.</summary>
		// Token: 0x060016B0 RID: 5808 RVA: 0x000585CC File Offset: 0x000567CC
		public CaseInsensitiveHashCodeProvider()
		{
			CultureInfo currentCulture = CultureInfo.CurrentCulture;
			if (!CaseInsensitiveHashCodeProvider.AreEqual(currentCulture, CultureInfo.InvariantCulture))
			{
				this.m_text = CultureInfo.CurrentCulture.TextInfo;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.CaseInsensitiveHashCodeProvider" /> class using the specified <see cref="T:System.Globalization.CultureInfo" />.</summary>
		/// <param name="culture">The <see cref="T:System.Globalization.CultureInfo" /> to use for the new <see cref="T:System.Collections.CaseInsensitiveHashCodeProvider" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="culture" /> is null. </exception>
		// Token: 0x060016B1 RID: 5809 RVA: 0x00058608 File Offset: 0x00056808
		public CaseInsensitiveHashCodeProvider(CultureInfo culture)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			if (!CaseInsensitiveHashCodeProvider.AreEqual(culture, CultureInfo.InvariantCulture))
			{
				this.m_text = culture.TextInfo;
			}
		}

		/// <summary>Gets an instance of <see cref="T:System.Collections.CaseInsensitiveHashCodeProvider" /> that is associated with the <see cref="P:System.Threading.Thread.CurrentCulture" /> of the current thread and that is always available.</summary>
		/// <returns>An instance of <see cref="T:System.Collections.CaseInsensitiveHashCodeProvider" /> that is associated with the <see cref="P:System.Threading.Thread.CurrentCulture" /> of the current thread.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x17000364 RID: 868
		// (get) Token: 0x060016B3 RID: 5811 RVA: 0x0005865C File Offset: 0x0005685C
		public static CaseInsensitiveHashCodeProvider Default
		{
			get
			{
				object obj = CaseInsensitiveHashCodeProvider.sync;
				CaseInsensitiveHashCodeProvider result;
				lock (obj)
				{
					if (CaseInsensitiveHashCodeProvider.singleton == null)
					{
						CaseInsensitiveHashCodeProvider.singleton = new CaseInsensitiveHashCodeProvider();
					}
					else if (CaseInsensitiveHashCodeProvider.singleton.m_text == null)
					{
						if (!CaseInsensitiveHashCodeProvider.AreEqual(CultureInfo.CurrentCulture, CultureInfo.InvariantCulture))
						{
							CaseInsensitiveHashCodeProvider.singleton = new CaseInsensitiveHashCodeProvider();
						}
					}
					else if (!CaseInsensitiveHashCodeProvider.AreEqual(CaseInsensitiveHashCodeProvider.singleton.m_text, CultureInfo.CurrentCulture))
					{
						CaseInsensitiveHashCodeProvider.singleton = new CaseInsensitiveHashCodeProvider();
					}
					result = CaseInsensitiveHashCodeProvider.singleton;
				}
				return result;
			}
		}

		// Token: 0x060016B4 RID: 5812 RVA: 0x00058718 File Offset: 0x00056918
		private static bool AreEqual(CultureInfo a, CultureInfo b)
		{
			return a.LCID == b.LCID;
		}

		// Token: 0x060016B5 RID: 5813 RVA: 0x00058728 File Offset: 0x00056928
		private static bool AreEqual(TextInfo info, CultureInfo culture)
		{
			return info.LCID == culture.LCID;
		}

		/// <summary>Gets an instance of <see cref="T:System.Collections.CaseInsensitiveHashCodeProvider" /> that is associated with <see cref="P:System.Globalization.CultureInfo.InvariantCulture" /> and that is always available.</summary>
		/// <returns>An instance of <see cref="T:System.Collections.CaseInsensitiveHashCodeProvider" /> that is associated with <see cref="P:System.Globalization.CultureInfo.InvariantCulture" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x17000365 RID: 869
		// (get) Token: 0x060016B6 RID: 5814 RVA: 0x00058738 File Offset: 0x00056938
		public static CaseInsensitiveHashCodeProvider DefaultInvariant
		{
			get
			{
				return CaseInsensitiveHashCodeProvider.singletonInvariant;
			}
		}

		/// <summary>Returns a hash code for the given object, using a hashing algorithm that ignores the case of strings.</summary>
		/// <returns>A hash code for the given object, using a hashing algorithm that ignores the case of strings.</returns>
		/// <param name="obj">The <see cref="T:System.Object" /> for which a hash code is to be returned. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="obj" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060016B7 RID: 5815 RVA: 0x00058740 File Offset: 0x00056940
		public int GetHashCode(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			string text = obj as string;
			if (text == null)
			{
				return obj.GetHashCode();
			}
			int num = 0;
			if (this.m_text != null && !CaseInsensitiveHashCodeProvider.AreEqual(this.m_text, CultureInfo.InvariantCulture))
			{
				foreach (char c in this.m_text.ToLower(text))
				{
					num = num * 31 + (int)c;
				}
			}
			else
			{
				for (int j = 0; j < text.Length; j++)
				{
					char c = char.ToLower(text[j], CultureInfo.InvariantCulture);
					num = num * 31 + (int)c;
				}
			}
			return num;
		}

		// Token: 0x0400086F RID: 2159
		private static readonly CaseInsensitiveHashCodeProvider singletonInvariant = new CaseInsensitiveHashCodeProvider(CultureInfo.InvariantCulture);

		// Token: 0x04000870 RID: 2160
		private static CaseInsensitiveHashCodeProvider singleton;

		// Token: 0x04000871 RID: 2161
		private static readonly object sync = new object();

		// Token: 0x04000872 RID: 2162
		private TextInfo m_text;
	}
}
