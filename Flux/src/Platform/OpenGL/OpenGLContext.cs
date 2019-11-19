using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;

namespace Flux.src.Platform.OpenGL
{
	class OpenGLContext : GameWindow
	{
		protected OpenGLContext(int width, int height) : base(width, height) { }
		protected OpenGLContext(int width, int height, GraphicsMode mode) : base(width, height, mode) { }
		protected OpenGLContext(int width, int height, GraphicsMode mode,string title) : base(width, height, mode, title) { }
		protected OpenGLContext(int width, int height, GraphicsMode mode, string title, GameWindowFlags flags) : base(width, height, mode, title, flags) { }
		protected OpenGLContext(int width, int height, GraphicsMode mode, string title, GameWindowFlags flags, DisplayDevice device) : base(width, height, mode, title, flags, device) { }
		protected virtual void Init()
		{
			
		}
	}
}
