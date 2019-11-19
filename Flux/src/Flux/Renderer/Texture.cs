using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Flux.src.Flux.Renderer
{
	public class Texture : IDisposable
	{
		public int Handle;

		public Texture(string filePath)
		{
			Handle = GL.GenTexture();
			Use();
			try
			{
				CreateTexture(filePath);
			}
			catch (FileNotFoundException e)
			{
				Console.WriteLine(e);
			}
		}
		public void Use()
		{
			GL.BindTexture(TextureTarget.Texture2D, Handle);
		}
		public void CreateTexture(string path)
		{
			Bitmap bmp = new Bitmap(path);

			BitmapData data = bmp.LockBits(new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height),
			ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

			GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
			OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

			bmp.UnlockBits(data);

			GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
		}

		//public int LoadImage(string path)
		//{
		//	try
		//	{
		//		Bitmap bmp = new Bitmap(path);
		//		return CreateTexture(path);
		//	}
		//	catch (FileNotFoundException e)
		//	{
		//		Console.WriteLine(e);
		//		return -1;
		//	}
		//}

		//protected virtual void Dispose(bool disposing)
		//{
		//	if(!disposedValue)
		//	{
		//		disposedValue = true;
		//	}
		//}
		public void Dispose()
		{
			//Dispose(true);
			GC.SuppressFinalize(this);
		}

	}
}
