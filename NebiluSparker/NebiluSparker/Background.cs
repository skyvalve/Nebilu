using System;
using System.Threading;
using System.Collections.Generic;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;

namespace NebiluSparker
{
	public class Background
	{
		private GraphicsContext graphics;
		private Sprite Starry, starries;
		private Sprite nebula, Nebula;
		
		public Background (GraphicsContext gs)
		{
			graphics = gs;
			Texture2D t = new Texture2D("/Application/Resources/starsBackground.png", false);
			Starry = new Sprite(graphics, t);
			
			Starry.Position.X = 0;
			Starry.Position.Y = 0;
			// Second connected back ground.
			starries = new Sprite(graphics, t);
			
			starries.Position.X = Starry.Width;
			starries.Position.Y = 0;
			
			t = new Texture2D("/Application/Resources/nebula_01.png", true);
			
			nebula = new Sprite(graphics, t);
			nebula.Position.X = 0;
			nebula.Position.Y = 0;	
			
			Nebula = new Sprite(graphics, t);
			Nebula.Position.X = nebula.Width;
			Nebula.Position.Y = 0;			
		}
		
		public void Update(){
			// Have to set update method in AppMain.cs#
			Starry.Position.X --;
			// Nebula back ground.
			nebula.Position.X -= 2;
			Nebula.Position.X -= 2;
						
			starries.Position.X --;
			
			if(Starry.Position.X < -Starry.Width){
				
				Starry.Position.X = 0;
				starries.Position.X = starries.Width;
			}
			
			if(nebula.Position.X < -nebula.Width){
				
				nebula.Position.X = 0;
				Nebula.Position.X = nebula.Width;
			} 
		}
		
		public void Render(){
			
			Starry.Render();
			starries.Render();
			
			nebula.Render();
			Nebula.Render();
			
		}
	}
}

