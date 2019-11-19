using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;


namespace Flux.src.Flux.Renderer
{
	public class OrthographicCamera
	{
		public Matrix4 ProjectionMatrix { get; }
		public OrthographicCamera(float left = 0.0f, float right = 800.0f, float bottom = 0.0f, float top = 600.0f)
		{
			ProjectionMatrix = Matrix4.CreateOrthographicOffCenter(left, right, bottom, top, 0.1f, 100.0f);

		}

	}
}
