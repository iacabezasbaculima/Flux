using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Flux.src.Flux.Core
{
	public class Window : GameWindow
	{
		public Window() : base() { PrintInfo(); }
		public Window(int width, int height) : base(width, height) { PrintInfo(); }
		public Window(int width, int height, GraphicsMode mode) : base(width, height, mode) { PrintInfo(); }
		public Window(int width, int height, GraphicsMode mode, string title) : base(width, height, mode, title) { PrintInfo(); }
		public Window(int width, int height, GraphicsMode mode, string title, GameWindowFlags flags) : base(width, height, mode, title, flags) { PrintInfo(); }
		public Window(int width, int height, GraphicsMode mode, string title, GameWindowFlags flags, DisplayDevice device) : base(width, height, mode, title, flags, device) { }

		private void PrintInfo()
		{
			Console.WriteLine("OpenGL Info:\n");
			Console.WriteLine("	Vendor: {0}", GL.GetString(StringName.Vendor));
			Console.WriteLine("	Renderer: {0}", GL.GetString(StringName.Renderer));
			Console.WriteLine("	Version: {0}", GL.GetString(StringName.Version));
			Console.WriteLine("	GLSL: {0}", GL.GetString(StringName.ShadingLanguageVersion));
		}
	}
}
