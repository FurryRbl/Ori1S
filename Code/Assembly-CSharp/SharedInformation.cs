using System;
using System.Collections.Generic;

// Token: 0x0200023E RID: 574
public class SharedInformation
{
	// Token: 0x17000356 RID: 854
	// (get) Token: 0x060012FC RID: 4860 RVA: 0x00058067 File Offset: 0x00056267
	public static string UserSourceAnimationsPath
	{
		get
		{
			return SharedInformation.UserHostNameToSourceAnimations[Environment.MachineName];
		}
	}

	// Token: 0x040010AE RID: 4270
	public const string ARIE_MACHINE_NAME = "MOONSTATION";

	// Token: 0x040010AF RID: 4271
	public const string GENNADIY_MACHINE_NAME = "HENRYKOROL-PC";

	// Token: 0x040010B0 RID: 4272
	public const string MAX_MACHINE_NAME = "UNIVERSALMAXHYP";

	// Token: 0x040010B1 RID: 4273
	public const string JAMES_MACHINE_NAME = "JAY-PC";

	// Token: 0x040010B2 RID: 4274
	public const string JAMES_SECOND_MACHINE_NAME = "JAMES-PC";

	// Token: 0x040010B3 RID: 4275
	public const string THOMAS_MACHINE_NAME = "THOMASMAHLER-PC";

	// Token: 0x040010B4 RID: 4276
	public const string DAVID_MACHINE_NAME = "DAVID-BEAST-PCC";

	// Token: 0x040010B5 RID: 4277
	public const string MICHAEL_MACHINE_NAME = "MIKE-PC";

	// Token: 0x040010B6 RID: 4278
	public const string ARTHUR_MACHINE_NAME = "ARTHURBRUSSEE";

	// Token: 0x040010B7 RID: 4279
	public const string JOHANNES_MACHINE_NAME = "JOHANNESPC";

	// Token: 0x040010B8 RID: 4280
	public const string ANNA_MACHINE_NAME = "remoevd-NATHAN";

	// Token: 0x040010B9 RID: 4281
	private const string RAHEL_MACHINE_NAME = "RAHELBRUNOLD-PC";

	// Token: 0x040010BA RID: 4282
	public const string WILLEM_MACHINE_NAME = "PHI";

	// Token: 0x040010BB RID: 4283
	public static string DATA_FOLDER_PATH = string.Empty;

	// Token: 0x040010BC RID: 4284
	public static Dictionary<string, string> UserDropboxPathMap = new Dictionary<string, string>
	{
		{
			"MOONSTATION",
			"D:\\Dropbox"
		},
		{
			"DAVID-BEAST-PCC",
			"E:\\Dropbox"
		},
		{
			"JAY-PC",
			"C:\\Windows.old\\Users\\Jay\\Dropbox"
		},
		{
			"JAMES-PC",
			"D:\\Dropbox"
		},
		{
			"HENRYKOROL-PC",
			"E:\\Dropbox (Moon Game Studios)"
		},
		{
			"THOMASMAHLER-PC",
			"E:\\Dropbox"
		},
		{
			"UNIVERSALMAXHYP",
			"C:\\Users\\universalmaxhyper\\Dropbox"
		},
		{
			"MIKE-PC",
			"C:\\Users\\Mike\\Dropbox"
		},
		{
			"JOHANNESPC",
			"I:\\WORK\\Johannes\\Dropbox\\Dropbox"
		}
	};

	// Token: 0x040010BD RID: 4285
	public static Dictionary<string, string> UserHostNameToName = new Dictionary<string, string>
	{
		{
			"MOONSTATION",
			"Arie"
		},
		{
			"DAVID-BEAST-PCC",
			"David"
		},
		{
			"JAY-PC",
			"James"
		},
		{
			"JAMES-PC",
			"James"
		},
		{
			"HENRYKOROL-PC",
			"Gennadiy"
		},
		{
			"THOMASMAHLER-PC",
			"Thomas"
		},
		{
			"UNIVERSALMAXHYP",
			"Max"
		},
		{
			"MIKE-PC",
			"Mike"
		},
		{
			"ARTHURBRUSSEE",
			"Arthur"
		},
		{
			"remoevd-NATHAN",
			"Anna"
		}
	};

	// Token: 0x040010BE RID: 4286
	public static Dictionary<string, string> UserHostNameToSourceAnimations = new Dictionary<string, string>
	{
		{
			"MOONSTATION",
			"c:\\dev\\sourceAnimations\\trunk"
		},
		{
			"DAVID-BEAST-PCC",
			"C:\\Sein\\sourceAnimations\\trunk"
		},
		{
			"JAY-PC",
			"D:\\sourceAnimations"
		},
		{
			"JAMES-PC",
			"D:\\sourceAnimations"
		},
		{
			"HENRYKOROL-PC",
			string.Empty
		},
		{
			"THOMASMAHLER-PC",
			string.Empty
		},
		{
			"UNIVERSALMAXHYP",
			string.Empty
		},
		{
			"MIKE-PC",
			string.Empty
		},
		{
			"ARTHURBRUSSEE",
			"C:\\Users\\Arthur Brussee\\Documents\\Unity\\SeinProjects\\Animations\\SeinProjects\\trunk"
		},
		{
			"JOHANNESPC",
			string.Empty
		},
		{
			"RAHELBRUNOLD-PC",
			"D:\\Work\\MoonStudios\\sourceAnimation"
		},
		{
			"PHI",
			"E:\\00Ori\\animations"
		}
	};
}
