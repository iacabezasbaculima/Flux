using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flux.src.Platform.OpenGL;

namespace Flux.src.Flux.Renderer
{
	public class GuiRenderer
	{
		private Shader _shader;
		private OpenGLVertexArray _vao;
		public GuiRenderer()
		{
			_shader = Shader.Create("gui", "gui.vert", "gui.frag");

			_vao = new OpenGLVertexArray();

			float[] positions = { 1, 1, 1, -1, -1, -1, -1, 1 };
			uint[] indices = { 0, 1, 3, 1, 2, 3 };

			OpenGLVertexBuffer vbo = new OpenGLVertexBuffer(positions, positions.Length, OpenTK.Graphics.OpenGL.BufferUsageHint.StaticDraw);
			
			BufferLayout bl = new BufferLayout {
				{ShaderDataType.Float2, "position" }
			};
			bl.CalculateOffsetsAndStride();
			vbo.SetLayout(bl);
			_vao.AddVertexBuffer(vbo);
			
			OpenGLIndexBuffer ibo = new OpenGLIndexBuffer(indices, OpenTK.Graphics.OpenGL.BufferUsageHint.StaticDraw);
			
			_vao.SetIndexBuffer(ibo);
		}

		public void Render(List<GuiTexture> guis)
		{
			_shader.Bind();
			_vao.Bind();
			// Enable alpha blending
			RenderCommand.EnableBlend(true);
			// Disable depth test
			RenderCommand.EnableDepthTest(false);
			// The last gui element added to list is rendered first
			foreach (var gui in guis)
			{
				// Unit Texture0 is the default
				gui.GuiTex.Bind();
				OpenTK.Matrix4 transformation = Utility.MathUtility.CreateTransformationMatrix(gui.Position, gui.Scale);
				_shader.SetMatrix4("transform", transformation, false);
				RenderCommand.DrawIndexed(_vao);
			}
			// enable depth test
			RenderCommand.EnableDepthTest(true);
			// disable alpha blending
			RenderCommand.EnableBlend(false);
			_vao.Unbind();
			// should unbind shader program below, we assume that a new shader will be used below to override the current shader
		}
	}
}
