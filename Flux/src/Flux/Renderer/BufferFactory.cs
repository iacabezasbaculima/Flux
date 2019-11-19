using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flux.src.Platform.OpenGL;

namespace Flux.src.Flux.Renderer
{
	public static class BufferFactory
	{
		public static Buffer CreateBuffer(float[] vertices, int length, OpenTK.Graphics.OpenGL.BufferUsageHint hint)
		{
			return new OpenGLVertexBuffer(vertices, length, hint);
		}
		public static Buffer CreateBuffer(uint[] indices, OpenTK.Graphics.OpenGL.BufferUsageHint hint)
		{
			return new OpenGLIndexBuffer(indices, hint);
		}
	}
}
