using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using OpenTK.Graphics.OpenGL;

namespace Flux.src.Platform.OpenGL
{
	class OpenGLTexture
	{
		protected int textureId;

		protected OpenGLTexture(string filePath)
		{
			textureId = GL.GenTexture();
			GL.BindTexture(TextureTarget.Texture2D, textureId);
			try
			{
				CreateTexture(filePath);
			}
			catch (FileNotFoundException e)
			{
				Console.WriteLine(e);
			}
		}
		protected void BindTexture()
		{
			GL.BindTexture(TextureTarget.Texture2D, textureId);
		}
		protected void CreateTexture(string path)
		{
			Bitmap bmp = new Bitmap(path);

			BitmapData data = bmp.LockBits(new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height),
			ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

			GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
			OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

			bmp.UnlockBits(data);

			GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

			bmp.Dispose();
		}

	}
}
