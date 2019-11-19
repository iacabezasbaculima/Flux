using OpenTK;

namespace Flux.src.Flux.Renderer
{
	public interface IShape
	{
		void Draw(Shader sh, Matrix4 model, Matrix4 view, Matrix4 projection);
	}
}
