using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using OpenTK.Graphics.OpenGL4;
using PixelFormat = OpenTK.Graphics.OpenGL4.PixelFormat;

namespace Flux.src.Platform.OpenGL
{
	public class OpenGLTexture : Flux.Renderer.Texture2D
	{
		private readonly int TextureHandle;
		private readonly string Path;
		private readonly int Width, Height, Channels;
		private readonly SizedInternalFormat InternalFormat;
		private readonly PixelFormat DataFormat;
		private readonly System.Drawing.Imaging.PixelFormat BmpFormat;

		public OpenGLTexture(string fileName)
		{
			Path = $"assets/images/{fileName}";
			InternalFormat = SizedInternalFormat.Rgba8;
			DataFormat = PixelFormat.Bgra;

			using (Bitmap bmp = new Bitmap(Path))
			{
				Width = bmp.Width;
				Height = bmp.Height;
				Channels = bmp.PixelFormat == System.Drawing.Imaging.PixelFormat.Format32bppArgb ? 4 : 3;
				BmpFormat = bmp.PixelFormat;

				var data = bmp.LockBits(new Rectangle(0, 0, Width, Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

				// All further texture state calls are applied on texture unit zero
				GL.ActiveTexture(TextureUnit.Texture0);
				GL.CreateTextures(TextureTarget.Texture2D, 1, out TextureHandle);
				GL.BindTexture(TextureTarget.Texture2D, TextureHandle);
				GL.TextureStorage2D(TextureHandle, 1, InternalFormat, Width, Height);
				GL.TextureSubImage2D(TextureHandle, 0, 0, 0, Width, Height, DataFormat, PixelType.UnsignedByte, data.Scan0);
			}
			int[] texParams = new int[] { 
				(int)All.Nearest,
				(int)All.Linear,
				(int)All.Repeat
			};
			GL.TextureParameterI(TextureHandle, TextureParameterName.TextureMinFilter, ref texParams[1]);
			GL.TextureParameterI(TextureHandle, TextureParameterName.TextureMagFilter, ref texParams[0]);
			// S (x-axis), T (y-axis)
			GL.TextureParameterI(TextureHandle, TextureParameterName.TextureWrapS, ref texParams[2]);
			GL.TextureParameterI(TextureHandle, TextureParameterName.TextureWrapT, ref texParams[2]);
			GL.GenerateTextureMipmap(TextureHandle);
		}
		public override void Bind(int unit)
		{
			GL.BindTextureUnit(unit, TextureHandle);
		}
		public override int GetTextureID()
		{
			return TextureHandle;
		}
		public override int GetWidth()
		{
			return Width;
		}
		public override int GetHeight()
		{
			return Height;
		}
	}
}
