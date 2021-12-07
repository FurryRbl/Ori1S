using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Resources
{
	/// <summary>The exception that is thrown when the satellite assembly for the resources of the neutral culture is missing.</summary>
	// Token: 0x02000305 RID: 773
	[ComVisible(true)]
	[Serializable]
	public class MissingSatelliteAssemblyException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.MissingSatelliteAssemblyException" /> class with default properties.</summary>
		// Token: 0x060027E0 RID: 10208 RVA: 0x0008DEE4 File Offset: 0x0008C0E4
		public MissingSatelliteAssemblyException() : base(Locale.GetText("The satellite assembly was not found for the required culture."))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.MissingSatelliteAssemblyException" /> class with the specified error message. </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		// Token: 0x060027E1 RID: 10209 RVA: 0x0008DEF8 File Offset: 0x0008C0F8
		public MissingSatelliteAssemblyException(string message) : base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.MissingSatelliteAssemblyException" /> class with a specified error message and the name of a neutral culture. </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="cultureName">The name of the neutral culture.</param>
		// Token: 0x060027E2 RID: 10210 RVA: 0x0008DF04 File Offset: 0x0008C104
		public MissingSatelliteAssemblyException(string message, string cultureName) : base(message)
		{
			this.culture = cultureName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.MissingSatelliteAssemblyException" /> class from serialized data. </summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination of the exception.</param>
		// Token: 0x060027E3 RID: 10211 RVA: 0x0008DF14 File Offset: 0x0008C114
		protected MissingSatelliteAssemblyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.MissingSatelliteAssemblyException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception. </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception.</param>
		// Token: 0x060027E4 RID: 10212 RVA: 0x0008DF20 File Offset: 0x0008C120
		public MissingSatelliteAssemblyException(string message, Exception inner) : base(message, inner)
		{
		}

		/// <summary>Gets the name of a neutral culture. </summary>
		/// <returns>A <see cref="T:System.String" /> object with the name of the neutral culture.</returns>
		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x060027E5 RID: 10213 RVA: 0x0008DF2C File Offset: 0x0008C12C
		public string CultureName
		{
			get
			{
				return this.culture;
			}
		}

		// Token: 0x04001003 RID: 4099
		private string culture;
	}
}
