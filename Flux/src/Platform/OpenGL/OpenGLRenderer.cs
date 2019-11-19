using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Flux.src.Platform.OpenGL
{
	public class OpenGLRenderer 
	{
		protected static void GLEnableDepthTest()
		{
			GL.Enable(EnableCap.DepthTest);
		}
		protected static void GLClear()
		{
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
		}

		protected static void GLDrawIndexed(OpenGLVertexArray vertexArrray)
		{
			GL.DrawElements(PrimitiveType.Triangles, vertexArrray.GetIndexBuffer().GetCount(), DrawElementsType.UnsignedInt, 0);
		}

		protected static void GLSetClearColor(Color4 color)
		{
			GL.ClearColor(color.R, color.G, color.B, color.A);
		}
		protected static void GLViewport(int x, int y, int width, int height)
		{
			GL.Viewport(x, y, width, height);
		}
	}
}
