using Flux.src.Flux.Renderer;
using OpenTK.Graphics.OpenGL4;

namespace Flux.src.Platform.OpenGL
{
	public class OpenGLRendererAPI : RendererAPI
	{
		public override void Init()
		{
			//TODO: Enable GL debug messages with DebugProc
			GL.Enable(EnableCap.Blend);
			GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

			GL.Enable(EnableCap.DepthTest);
		}
		public override void SetViewport(int x, int y, int width, int height)
		{
			GL.Viewport(x, y, width, height);
		}

		public override void SetClearColor(OpenTK.Graphics.Color4 color)
		{
			GL.ClearColor(color);
		}

		public override void Clear()
		{
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
		}

		public override void SetBlend(bool isEnabled)
		{
			if (isEnabled)
			{
				GL.Enable(EnableCap.Blend);
				GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
			}
			else GL.Disable(EnableCap.Blend);
		}
		public override void SetCullFace(bool isEnabled)
		{
			if (isEnabled)
			{
				GL.Enable(EnableCap.CullFace);
				GL.CullFace(CullFaceMode.Back);
			}
			else GL.Disable(EnableCap.CullFace);
		}
		public override void SetDepthTest(bool isEnabled)
		{
			if (isEnabled) GL.Enable(EnableCap.DepthTest);
			else GL.Disable(EnableCap.DepthTest);
		}
		public override void SetMSAA(bool isEnabled)
		{
			if (isEnabled)
			{
				GL.GetInteger(GetPName.SampleBuffers, out int buffers);
				if (buffers == 1) GL.Enable(EnableCap.Multisample);
			}
			else GL.Disable(EnableCap.Multisample);
		}

		public override void DrawIndexed(VertexArray vao)
		{
			GL.DrawElements(PrimitiveType.Triangles, vao.GetIndexBuffer().GetCount(), DrawElementsType.UnsignedInt, 0);
		}
	}
}
