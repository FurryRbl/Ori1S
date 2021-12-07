using System;
using System.Diagnostics;

namespace System.Runtime.Versioning
{
	/// <summary>Specifies the resource exposure for a member of a class. This class cannot be inherited.</summary>
	// Token: 0x0200052E RID: 1326
	[Conditional("RESOURCE_ANNOTATION_WORK")]
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field, Inherited = false)]
	public sealed class ResourceExposureAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Versioning.ResourceExposureAttribute" /> class with the specified exposure level.</summary>
		/// <param name="exposureLevel">The scope of the resource.</param>
		// Token: 0x06003452 RID: 13394 RVA: 0x000ABD00 File Offset: 0x000A9F00
		public ResourceExposureAttribute(ResourceScope exposureLevel)
		{
			this.exposure = exposureLevel;
		}

		/// <summary>Gets the resource exposure scope.</summary>
		/// <returns>A <see cref="T:System.Runtime.Versioning.ResourceScope" /> object.</returns>
		// Token: 0x170009D0 RID: 2512
		// (get) Token: 0x06003453 RID: 13395 RVA: 0x000ABD10 File Offset: 0x000A9F10
		public ResourceScope ResourceExposureLevel
		{
			get
			{
				return this.exposure;
			}
		}

		// Token: 0x0400160C RID: 5644
		private ResourceScope exposure;
	}
}
