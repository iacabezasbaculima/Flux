using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flux.src.Flux.Renderer
{
	public class GuiTexture
	{
		public Texture2D GuiTex;
		private Vector2 _position;
		private Vector2 _scale;

		public GuiTexture(Texture2D texture, Vector2 position, Vector2 scale)
		{
			GuiTex = texture;
			_position = position;
			_scale = scale;
		}

		public Vector2 Position { get => _position; set => _position = value; }
		public Vector2 Scale { get => _scale; set => _scale = value; }
	}
}
