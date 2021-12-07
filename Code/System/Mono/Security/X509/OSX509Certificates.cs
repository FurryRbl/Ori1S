using System;
using System.Runtime.InteropServices;

namespace Mono.Security.X509
{
	// Token: 0x02000438 RID: 1080
	internal class OSX509Certificates
	{
		// Token: 0x060026CF RID: 9935
		[DllImport("/System/Library/Frameworks/Security.framework/Security")]
		private static extern IntPtr SecCertificateCreateWithData(IntPtr allocator, IntPtr nsdataRef);

		// Token: 0x060026D0 RID: 9936
		[DllImport("/System/Library/Frameworks/Security.framework/Security")]
		private static extern int SecTrustCreateWithCertificates(IntPtr certOrCertArray, IntPtr policies, out IntPtr sectrustref);

		// Token: 0x060026D1 RID: 9937
		[DllImport("/System/Library/Frameworks/Security.framework/Security")]
		private static extern IntPtr SecPolicyCreateSSL(int server, IntPtr cfStringHostname);

		// Token: 0x060026D2 RID: 9938
		[DllImport("/System/Library/Frameworks/Security.framework/Security")]
		private static extern int SecTrustEvaluate(IntPtr secTrustRef, out OSX509Certificates.SecTrustResult secTrustResultTime);

		// Token: 0x060026D3 RID: 9939
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private unsafe static extern IntPtr CFDataCreate(IntPtr allocator, byte* bytes, IntPtr length);

		// Token: 0x060026D4 RID: 9940
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern void CFRelease(IntPtr handle);

		// Token: 0x060026D5 RID: 9941
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern IntPtr CFArrayCreate(IntPtr allocator, IntPtr values, IntPtr numValues, IntPtr callbacks);

		// Token: 0x060026D6 RID: 9942 RVA: 0x00078534 File Offset: 0x00076734
		private unsafe static IntPtr MakeCFData(byte[] data)
		{
			int num = 0;
			return OSX509Certificates.CFDataCreate(IntPtr.Zero, &data[num], (IntPtr)data.Length);
		}

		// Token: 0x060026D7 RID: 9943 RVA: 0x0007855C File Offset: 0x0007675C
		private unsafe static IntPtr FromIntPtrs(IntPtr[] values)
		{
			fixed (IntPtr* value = ref (values != null && values.Length != 0) ? ref values[0] : ref *null)
			{
				return OSX509Certificates.CFArrayCreate(IntPtr.Zero, (IntPtr)((void*)value), (IntPtr)values.Length, IntPtr.Zero);
			}
		}

		// Token: 0x060026D8 RID: 9944 RVA: 0x000785A4 File Offset: 0x000767A4
		public static OSX509Certificates.SecTrustResult TrustEvaluateSsl(X509CertificateCollection certificates)
		{
			OSX509Certificates.SecTrustResult result;
			try
			{
				result = OSX509Certificates._TrustEvaluateSsl(certificates);
			}
			catch
			{
				result = OSX509Certificates.SecTrustResult.Deny;
			}
			return result;
		}

		// Token: 0x060026D9 RID: 9945 RVA: 0x000785EC File Offset: 0x000767EC
		private static OSX509Certificates.SecTrustResult _TrustEvaluateSsl(X509CertificateCollection certificates)
		{
			if (certificates == null)
			{
				throw new ArgumentNullException("certificates");
			}
			int count = certificates.Count;
			IntPtr[] array = new IntPtr[count];
			IntPtr[] array2 = new IntPtr[count];
			IntPtr intPtr = IntPtr.Zero;
			OSX509Certificates.SecTrustResult result;
			try
			{
				for (int i = 0; i < count; i++)
				{
					array[i] = OSX509Certificates.MakeCFData(certificates[i].RawData);
				}
				for (int j = 0; j < count; j++)
				{
					array2[j] = OSX509Certificates.SecCertificateCreateWithData(IntPtr.Zero, array[j]);
					if (array2[j] == IntPtr.Zero)
					{
						return OSX509Certificates.SecTrustResult.Deny;
					}
				}
				intPtr = OSX509Certificates.FromIntPtrs(array2);
				IntPtr intPtr2;
				if (OSX509Certificates.SecTrustCreateWithCertificates(intPtr, OSX509Certificates.sslsecpolicy, out intPtr2) == 0)
				{
					OSX509Certificates.SecTrustResult secTrustResult;
					int num = OSX509Certificates.SecTrustEvaluate(intPtr2, out secTrustResult);
					if (num != 0)
					{
						result = OSX509Certificates.SecTrustResult.Deny;
					}
					else
					{
						OSX509Certificates.CFRelease(intPtr2);
						result = secTrustResult;
					}
				}
				else
				{
					result = OSX509Certificates.SecTrustResult.Deny;
				}
			}
			finally
			{
				for (int k = 0; k < count; k++)
				{
					if (array[k] != IntPtr.Zero)
					{
						OSX509Certificates.CFRelease(array[k]);
					}
				}
				if (intPtr != IntPtr.Zero)
				{
					OSX509Certificates.CFRelease(intPtr);
				}
				else
				{
					for (int l = 0; l < count; l++)
					{
						if (array2[l] != IntPtr.Zero)
						{
							OSX509Certificates.CFRelease(array2[l]);
						}
					}
				}
			}
			return result;
		}

		// Token: 0x040017E7 RID: 6119
		public const string SecurityLibrary = "/System/Library/Frameworks/Security.framework/Security";

		// Token: 0x040017E8 RID: 6120
		public const string CoreFoundationLibrary = "/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation";

		// Token: 0x040017E9 RID: 6121
		private static IntPtr sslsecpolicy = OSX509Certificates.SecPolicyCreateSSL(0, IntPtr.Zero);

		// Token: 0x02000439 RID: 1081
		public enum SecTrustResult
		{
			// Token: 0x040017EB RID: 6123
			Invalid,
			// Token: 0x040017EC RID: 6124
			Proceed,
			// Token: 0x040017ED RID: 6125
			Confirm,
			// Token: 0x040017EE RID: 6126
			Deny,
			// Token: 0x040017EF RID: 6127
			Unspecified,
			// Token: 0x040017F0 RID: 6128
			RecoverableTrustFailure,
			// Token: 0x040017F1 RID: 6129
			FatalTrustFailure,
			// Token: 0x040017F2 RID: 6130
			ResultOtherError
		}
	}
}
