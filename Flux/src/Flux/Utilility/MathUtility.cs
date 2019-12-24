using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Flux.src.Flux.Utility
{
	public static class MathUtility
	{
		public static Matrix4 CreateTransformationMatrix(Vector2 position, Vector2 scale)
		{
			Matrix4 t = Matrix4.Identity;
			t *= Matrix4.CreateTranslation(position.X, position.Y, 0f);
			t *= Matrix4.CreateScale(new Vector3(scale.X, scale.Y, 1f));
			//Console.WriteLine($"{t.ExtractTranslation()}, {t.ExtractScale()}");
			return t;
		}
		/// <summary>
		/// Create a transformation matrix from position and scale vectors.
		/// </summary>
		/// <param name="position"></param>
		/// <param name="scale"></param>
		/// <returns></returns>
		public static Matrix4 CreateTransformationMatrix(Vector3 position, Vector3 scale)
		{
			var result = Matrix4.Identity;
			result *= Matrix4.CreateScale(scale);
			result *= Matrix4.CreateTranslation(position);

			return result;
		}
	}
}
