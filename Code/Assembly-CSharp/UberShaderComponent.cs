using System;
using UnityEngine;

// Token: 0x020000ED RID: 237
[AddComponentMenu("Uber Shader/Uber Shader Component")]
[ExecuteInEditMode]
public class UberShaderComponent : MonoBehaviour, IStrippable
{
	// Token: 0x06000984 RID: 2436 RVA: 0x0002A07C File Offset: 0x0002827C
	[ContextMenu("Show hidden componets")]
	public void ShowHiddenComponents()
	{
		foreach (Component component in base.GetComponents<Component>())
		{
			component.hideFlags = HideFlags.None;
		}
	}

	// Token: 0x06000985 RID: 2437 RVA: 0x0002A0AF File Offset: 0x000282AF
	public bool DoStrip()
	{
		return true;
	}

	// Token: 0x17000207 RID: 519
	// (get) Token: 0x06000986 RID: 2438 RVA: 0x0002A0B4 File Offset: 0x000282B4
	public UberShaderBlock Block
	{
		get
		{
			if (this.m_block == null)
			{
				this.m_block = base.GetComponent<UberShaderBlock>();
				if (this.m_block == null)
				{
					this.m_block = base.gameObject.AddComponent<UberShaderBlockTextured>();
				}
			}
			return this.m_block;
		}
	}

	// Token: 0x17000208 RID: 520
	// (get) Token: 0x06000987 RID: 2439 RVA: 0x0002A106 File Offset: 0x00028306
	public UberShaderBlockTextured TexturedBlock
	{
		get
		{
			return this.Block as UberShaderBlockTextured;
		}
	}

	// Token: 0x17000209 RID: 521
	// (get) Token: 0x06000988 RID: 2440 RVA: 0x0002A113 File Offset: 0x00028313
	public UberShaderBlockGrabPass GrabPassBlock
	{
		get
		{
			return this.Block as UberShaderBlockGrabPass;
		}
	}

	// Token: 0x06000989 RID: 2441 RVA: 0x0002A120 File Offset: 0x00028320
	public static UberShaderBlockTextured CreateTextured(GameObject gameObject)
	{
		if (!gameObject.GetComponent<UberShaderComponent>())
		{
			UberShaderComponent uberShaderComponent = gameObject.AddComponent<UberShaderComponent>();
			return uberShaderComponent.Block as UberShaderBlockTextured;
		}
		Debug.Log("Already an uber shader on the object!");
		return null;
	}

	// Token: 0x0600098A RID: 2442 RVA: 0x0002A15C File Offset: 0x0002835C
	public UberShaderBlock SetBlock(Type type)
	{
		if (this.Block != null)
		{
			UnityEngine.Object.DestroyImmediate(this.Block, true);
			return base.gameObject.AddComponent(type) as UberShaderBlock;
		}
		return base.gameObject.AddComponent(type) as UberShaderBlock;
	}

	// Token: 0x0600098B RID: 2443 RVA: 0x0002A1A9 File Offset: 0x000283A9
	public UberShaderModifier GetModifier(Type type)
	{
		return base.GetComponent(type) as UberShaderModifier;
	}

	// Token: 0x0600098C RID: 2444 RVA: 0x0002A1B7 File Offset: 0x000283B7
	public T GetModifier<T>() where T : UberShaderModifier
	{
		return base.GetComponent(typeof(T)) as T;
	}

	// Token: 0x040007CF RID: 1999
	private UberShaderBlock m_block;
}
