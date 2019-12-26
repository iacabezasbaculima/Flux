using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Flux.src.Flux.Renderer
{
	public abstract class RendererAPI
	{
		public abstract void Init();
		public abstract void SetViewport(int x, int y, int width, int height);
		public abstract void SetClearColor(OpenTK.Graphics.Color4 color);
		public abstract void Clear();
		public abstract void SetBlend(bool isEnabled);
		public abstract void SetCullFace(bool isEnabled);
		public abstract void SetDepthTest(bool isEnabled);
		public abstract void SetMSAA(bool isEnabled);


		public abstract void DrawIndexed(VertexArray vao);

		public static RendererAPI Create() { return new Platform.OpenGL.OpenGLRendererAPI(); }
	}
}
