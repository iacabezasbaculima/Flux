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
		protected static void GLEnableDepthTest(bool isEnabled)
		{
			if (isEnabled)
				GL.Enable(EnableCap.DepthTest);
			else
				GL.Disable(EnableCap.DepthTest);
		}
		protected static void GLEnableMSAA()
		{
			GL.GetInteger(GetPName.SampleBuffers, out int buffers);
			//Console.WriteLine("sample buffers: " + buffers);
			if (buffers == 1)
				GL.Enable(EnableCap.Multisample);
		}
		protected static void GLEnableBlend(bool isEnabled)
		{
			if (isEnabled)
			{
				GL.Enable(EnableCap.Blend);
				GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
			}
			else
				GL.Disable(EnableCap.Blend);
		}
		protected static void GLCullFace()
		{
			GL.CullFace(CullFaceMode.Back);
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
		protected static void GLActiveTexture(TextureUnit unit)
		{
			GL.ActiveTexture(unit);
		}
		protected static void GLBindTexture(int handle)
		{
			GL.BindTexture(TextureTarget.Texture2D, handle);
		}
	}
}
