using UnityEngine;


	public class Dither : MonoBehaviour
	{
		public Texture2D pattern;
		
		public float threshold;		
		public float strength;

		private Material m_material;
		private Shader shader;

		private Material material
		{
			get
			{
				if (m_material == null)
				{
					shader = Shader.Find("Custom/Dither");
					m_material = new Material(shader) { hideFlags = HideFlags.DontSave };
				}

				return m_material;
			}
		}

		public void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			if (material)
			{
				threshold = GraphicVariables.thresholdVal;
				strength = GraphicVariables.strengthVal;
				material.SetTexture("_Dither", pattern);
				material.SetInt("_Width", pattern.width);
				material.SetInt("_Height", pattern.height);
				material.SetFloat("_Threshold", threshold);
				material.SetFloat("_Strength", strength);

				Graphics.Blit(src, dest, material);
			}
		}

		private void OnDisable()
		{
			if (m_material)
				DestroyImmediate(m_material);
		}
	}
