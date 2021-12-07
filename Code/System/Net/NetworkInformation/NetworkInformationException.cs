using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace System.Net.NetworkInformation
{
	/// <summary>The exception that is thrown when an error occurs while retrieving network information.</summary>
	// Token: 0x020003A5 RID: 933
	[Serializable]
	public class NetworkInformationException : System.ComponentModel.Win32Exception
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.NetworkInformationException" /> class.</summary>
		// Token: 0x0600208B RID: 8331 RVA: 0x0005FE14 File Offset: 0x0005E014
		public NetworkInformationException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.NetworkInformationException" /> class with the specified error code.</summary>
		/// <param name="errorCode">A Win32 error code. </param>
		// Token: 0x0600208C RID: 8332 RVA: 0x0005FE1C File Offset: 0x0005E01C
		public NetworkInformationException(int errorCode) : base(errorCode)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.NetworkInformationException" /> class with serialized data.</summary>
		/// <param name="serializationInfo">A SerializationInfo object that contains the serialized exception data. </param>
		/// <param name="streamingContext">A StreamingContext that contains contextual information about the serialized exception. </param>
		// Token: 0x0600208D RID: 8333 RVA: 0x0005FE28 File Offset: 0x0005E028
		protected NetworkInformationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.error_code = info.GetInt32("ErrorCode");
		}

		/// <summary>Gets the Win32 error code for this exception.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value that contains the Win32 error code.</returns>
		// Token: 0x1700091A RID: 2330
		// (get) Token: 0x0600208E RID: 8334 RVA: 0x0005FE44 File Offset: 0x0005E044
		public override int ErrorCode
		{
			get
			{
				return this.error_code;
			}
		}

		// Token: 0x040013D6 RID: 5078
		private int error_code;
	}
}
