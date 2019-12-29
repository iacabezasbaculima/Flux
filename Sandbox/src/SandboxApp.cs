using Flux.src;

namespace Sandbox.src
{
	class SandboxApp
	{
		public static void Main(string[] args)
		{
			using (var game = new LoadObjFileWithVectorLists())
			{
				game.Run(60.0, 60.0);
			}
		}
	}
}
