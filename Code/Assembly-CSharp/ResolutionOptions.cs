using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x0200012D RID: 301
public class ResolutionOptions : CleverMenuOptionsList
{
	// Token: 0x06000C2A RID: 3114 RVA: 0x00036660 File Offset: 0x00034860
	public new void Awake()
	{
		base.Awake();
		if (Application.isEditor)
		{
			this.m_resolutions = new List<Resolution>
			{
				new Resolution
				{
					width = 1280,
					height = 720,
					refreshRate = 60
				},
				new Resolution
				{
					width = 1920,
					height = 1080,
					refreshRate = 60
				},
				new Resolution
				{
					width = 1920,
					height = 1081,
					refreshRate = 60
				},
				new Resolution
				{
					width = 1920,
					height = 720,
					refreshRate = 60
				},
				new Resolution
				{
					width = 640,
					height = 480,
					refreshRate = 60
				},
				new Resolution
				{
					width = 1920,
					height = 1080,
					refreshRate = 60
				},
				new Resolution
				{
					width = 3440,
					height = 1440,
					refreshRate = 60
				}
			};
			this.m_resolutions = this.m_resolutions.Distinct<Resolution>().ToList<Resolution>();
			ResolutionOptions.SortResolutions(this.m_resolutions);
		}
		else
		{
			this.m_resolutions = new List<Resolution>(Screen.resolutions);
			int num = 0;
			int num2 = 0;
			foreach (Resolution resolution in this.m_resolutions)
			{
				num = Math.Max(num, resolution.width);
				num2 = Math.Max(num2, resolution.height);
			}
			foreach (int[] array in ResolutionOptions.m_extraResolutions)
			{
				if (array[0] <= num && array[1] < num2)
				{
					this.m_resolutions.Add(new Resolution
					{
						width = array[0],
						height = array[1],
						refreshRate = 60
					});
				}
			}
			this.m_resolutions = this.m_resolutions.Distinct<Resolution>().ToList<Resolution>();
			ResolutionOptions.SortResolutions(this.m_resolutions);
		}
	}

	// Token: 0x06000C2B RID: 3115 RVA: 0x00036918 File Offset: 0x00034B18
	public new void OnEnable()
	{
		base.OnEnable();
		base.ClearItems();
		int num = 0;
		foreach (Resolution r2 in this.m_resolutions)
		{
			Resolution r = r2;
			string aspectRatio = ResolutionOptions.GetAspectRatio(new Vector2((float)r2.width, (float)r2.height), 0.07f);
			if (aspectRatio != string.Empty)
			{
				num++;
				base.AddItem(string.Concat(new object[]
				{
					r2.width,
					"x",
					r2.height,
					" ",
					aspectRatio
				}), delegate()
				{
					this.SetResolution(r);
				});
			}
		}
		base.SetSelection(0);
	}

	// Token: 0x06000C2C RID: 3116 RVA: 0x00036A1C File Offset: 0x00034C1C
	public void SetResolution(Resolution resolution)
	{
		GameSettings.Instance.Resolution = new Vector2((float)resolution.width, (float)resolution.height);
		SettingsScreen.Instance.SetDirty();
	}

	// Token: 0x06000C2D RID: 3117 RVA: 0x00036A48 File Offset: 0x00034C48
	public static string GetAspectRatio(Vector2 res, float precision)
	{
		foreach (Vector4 vector in ResolutionOptions.m_allowedAspectRatios)
		{
			if (ResolutionOptions.IsAspectRatio(res, (int)vector.x, (int)vector.y, precision))
			{
				return string.Concat(new object[]
				{
					"[",
					(int)vector.z,
					":",
					(int)vector.w,
					"]"
				});
			}
		}
		return string.Empty;
	}

	// Token: 0x06000C2E RID: 3118 RVA: 0x00036AE4 File Offset: 0x00034CE4
	private static bool IsAspectRatio(Vector2 res, int horizontal, int vertical, float precision)
	{
		return Math.Abs(res.x / res.y - (float)horizontal / (float)vertical) < precision;
	}

	// Token: 0x06000C2F RID: 3119 RVA: 0x00036B10 File Offset: 0x00034D10
	private static void SortResolutions(List<Resolution> resolutions)
	{
		resolutions.Sort(delegate(Resolution x, Resolution y)
		{
			if (x.width > y.width)
			{
				return -1;
			}
			if (x.width < y.width)
			{
				return 1;
			}
			if (x.height >= y.height)
			{
				return -1;
			}
			return 1;
		});
	}

	// Token: 0x040009FA RID: 2554
	private List<Resolution> m_resolutions = new List<Resolution>();

	// Token: 0x040009FB RID: 2555
	private static Vector4[] m_allowedAspectRatios = new Vector4[]
	{
		new Vector4(16f, 9f, 16f, 9f),
		new Vector4(16f, 10f, 16f, 10f),
		new Vector4(5f, 4f, 5f, 4f),
		new Vector4(4f, 3f, 4f, 3f),
		new Vector4(21f, 9f, 21f, 9f),
		new Vector4(43f, 18f, 21f, 9f),
		new Vector4(64f, 27f, 21f, 9f)
	};

	// Token: 0x040009FC RID: 2556
	private static int[][] m_extraResolutions = new int[][]
	{
		new int[]
		{
			1920,
			1080
		},
		new int[]
		{
			5120,
			2160
		},
		new int[]
		{
			2560,
			1080
		},
		new int[]
		{
			1920,
			810
		},
		new int[]
		{
			1600,
			675
		},
		new int[]
		{
			1280,
			540
		}
	};
}
