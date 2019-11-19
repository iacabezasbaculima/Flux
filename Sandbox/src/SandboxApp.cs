using Flux.src;

namespace Sandbox.src
{
	class SandboxApp
	{
		public static void Main(string[] args)
		{
			using (SandBox mygame = new SandBox(800, 600))
			{
				mygame.Run(60.0, 60.0);
			}
		}
	}
}
