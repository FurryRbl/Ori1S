using System;
using System.Collections.Generic;
using Core;
using UnityEngine;

// Token: 0x020009E4 RID: 2532
public class ConceptScreen : MonoBehaviour, ISuspendable
{
	// Token: 0x17000882 RID: 2178
	// (get) Token: 0x0600370B RID: 14091 RVA: 0x000E71A7 File Offset: 0x000E53A7
	// (set) Token: 0x0600370C RID: 14092 RVA: 0x000E71AF File Offset: 0x000E53AF
	public bool IsSuspended { get; set; }

	// Token: 0x0600370D RID: 14093 RVA: 0x000E71B8 File Offset: 0x000E53B8
	public void Awake()
	{
		ConceptScreen.Instance = this;
		SuspensionManager.Register(this);
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600370E RID: 14094 RVA: 0x000E71D2 File Offset: 0x000E53D2
	public void OnDestroy()
	{
		if (ConceptScreen.Instance == this)
		{
			ConceptScreen.Instance = null;
		}
		SuspensionManager.Unregister(this);
	}

	// Token: 0x0600370F RID: 14095 RVA: 0x000E71F0 File Offset: 0x000E53F0
	public void ApplyImage()
	{
		ConceptScreen.ImageData imageData = this.Images[this.m_index];
		float num = imageData.Width / imageData.Height;
		float num2 = 8f * num;
		float y = 8f;
		if (num2 > 14f)
		{
			num2 = 14f;
			y = 14f / num;
		}
		this.Screen.transform.localScale = new Vector3(num2, y, 1f);
		this.Screen.material.mainTexture = imageData.Texture;
	}

	// Token: 0x06003710 RID: 14096 RVA: 0x000E7278 File Offset: 0x000E5478
	public void Activate(Texture texture)
	{
		SuspensionManager.SuspendExcluding(this.m_suspenables);
		if (this.OpenSound)
		{
			Sound.Play(this.OpenSound.GetSound(null), base.transform.position, null);
		}
		this.Parent.IsActive = false;
		base.gameObject.SetActive(true);
		this.FadeIn.Initialize();
		this.FadeIn.AnimatorDriver.ContinueForward();
		this.m_index = this.Images.FindIndex((ConceptScreen.ImageData a) => a.Texture == texture);
		this.ApplyImage();
	}

	// Token: 0x06003711 RID: 14097 RVA: 0x000E7324 File Offset: 0x000E5524
	public void Deactivate()
	{
		SuspensionManager.GetSuspendables(this.m_suspenables, base.gameObject);
		SuspensionManager.ResumeExcluding(this.m_suspenables);
		if (this.CloseSound)
		{
			Sound.Play(this.CloseSound.GetSound(null), base.transform.position, null);
		}
		this.Parent.IsActive = true;
		this.FadeIn.Initialize();
		this.FadeIn.AnimatorDriver.ContinueBackwards();
	}

	// Token: 0x06003712 RID: 14098 RVA: 0x000E73A4 File Offset: 0x000E55A4
	public void FixedUpdate()
	{
		if (Core.Input.Cancel.OnPressed || Core.Input.SpiritFlame.OnPressed || Core.Input.ActionButtonA.OnPressed)
		{
			this.Deactivate();
		}
		if (this.FadeIn.AnimatorDriver.CurrentTime <= 0f && this.FadeIn.AnimatorDriver.IsReversed)
		{
			base.gameObject.SetActive(false);
		}
		if (Core.Input.Right.OnPressed)
		{
			this.ChangeImage(1);
		}
		if (Core.Input.Left.OnPressed)
		{
			this.ChangeImage(-1);
		}
	}

	// Token: 0x06003713 RID: 14099 RVA: 0x000E744C File Offset: 0x000E564C
	private void ChangeImage(int e)
	{
		this.m_index = (this.m_index + e + this.Images.Count) % this.Images.Count;
		this.ApplyImage();
		if (this.SwitchSound)
		{
			Sound.Play(this.SwitchSound.GetSound(null), base.transform.position, null);
		}
	}

	// Token: 0x04003206 RID: 12806
	public static ConceptScreen Instance;

	// Token: 0x04003207 RID: 12807
	public CleverMenuItemSelectionManager Parent;

	// Token: 0x04003208 RID: 12808
	public Renderer Screen;

	// Token: 0x04003209 RID: 12809
	public TransparencyAnimator FadeIn;

	// Token: 0x0400320A RID: 12810
	private HashSet<ISuspendable> m_suspenables = new HashSet<ISuspendable>();

	// Token: 0x0400320B RID: 12811
	private int m_index;

	// Token: 0x0400320C RID: 12812
	public SoundProvider SwitchSound;

	// Token: 0x0400320D RID: 12813
	public SoundProvider OpenSound;

	// Token: 0x0400320E RID: 12814
	public SoundProvider CloseSound;

	// Token: 0x0400320F RID: 12815
	public List<ConceptScreen.ImageData> Images = new List<ConceptScreen.ImageData>();

	// Token: 0x020009E5 RID: 2533
	[Serializable]
	public class ImageData
	{
		// Token: 0x06003714 RID: 14100 RVA: 0x000E74B3 File Offset: 0x000E56B3
		public ImageData(Texture2D image, float width, float height)
		{
			this.Texture = image;
			this.Width = width;
			this.Height = height;
		}

		// Token: 0x04003211 RID: 12817
		public Texture2D Texture;

		// Token: 0x04003212 RID: 12818
		public float Width;

		// Token: 0x04003213 RID: 12819
		public float Height;
	}
}
