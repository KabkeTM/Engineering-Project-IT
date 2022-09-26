using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Cam.Effects
{
	[Serializable]
	public class PaletteGrading
	{
		public int size = 6;

		[Range(0f, .5f)]
		public float blending = 0.25f;
		public Color blendColor1 = Color.white;
		public Color blendColor2 = Color.black;

		[HideInInspector] public Color c1B, c2B;
	}

	public class RetroPalette_Green : MonoBehaviour
	{
		public List<PaletteGrading> gradings = new List<PaletteGrading>();
		public Color[] colors;

		private Material m_material;
		private Shader shader;

		private Material material
		{
			get
			{
				if (m_material == null)
				{
					shader = Shader.Find("Custom/RetroPalette");
					m_material = new Material(shader) { hideFlags = HideFlags.DontSave };
				}

				return m_material;
			}
		}


		public void BlendStops()
		{
			for (int i = 0; i < gradings.Count; i++)
			{
				PaletteGrading pgBefore = i == 0 ? null : gradings[i - 1];
				PaletteGrading pgNow = gradings[i];
				PaletteGrading pgAfter = i == gradings.Count - 1 ? null : gradings[i + 1];

				if (pgBefore != null)
					pgNow.c1B = Color.Lerp(pgBefore.blendColor2, pgNow.blendColor1, 1f - pgNow.blending);
				else
					pgNow.c1B = pgNow.blendColor1;

				if (pgAfter != null)
					pgNow.c2B = Color.Lerp(pgNow.blendColor2, pgAfter.blendColor1, pgNow.blending);
				else
					pgNow.c2B = pgNow.blendColor2;
			}
		}

		public void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			BlendStops();

			int s = 0;
			foreach (PaletteGrading pg in gradings)
				s += pg.size;

			colors = new Color[s];
			int i = 0;

			foreach (PaletteGrading pg in gradings)
			{
				for (int j = 0; j < pg.size; j++, i++)
				{
					colors[i] = Color.Lerp(pg.c1B, pg.c2B, (float)j / pg.size);
				}
			}

			if (material && colors.Length > 0)
			{
				material.SetInt("_ColorCount", colors.Length);
				material.SetColorArray("_Colors", colors);

				Graphics.Blit(src, dest, material);
			}
			else
			{
				Graphics.Blit(src, dest);
			}
		}
	}
}