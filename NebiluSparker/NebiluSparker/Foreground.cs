using System;
using System.Threading;
using System.Collections.Generic;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;

namespace NebiluSparker
{
	public class Foreground
	{
		private GraphicsContext graphics;
		
		private Sprite Nebula, neebula;
		
		public Foreground (GraphicsContext gs)
		{
			graphics = gs;
					
			Texture2D t = new Texture2D("/Application/Resources/nebula_02.png", false);
			
			Nebula = new Sprite(graphics, t);
			Nebula.Position.X = 0; 
			Nebula.Position.Y = 0;
			
			neebula = new Sprite(graphics, t);
			neebula.Position.X = Nebula.Width;
			neebula.Position.Y = 0;
		}
		
		public void Update(){			
			
			// Nebula back ground.
			Nebula.Position.X -= 4;
			neebula.Position.X -= 4;
					
			if(Nebula.Position.X < -Nebula.Width){
				
				Nebula.Position.X = 0;
				neebula.Position.X = Nebula.Width;
			} 
		}
		
		public void Render(){
			
			Nebula.Render();
			neebula.Render();
		}
	}
}

