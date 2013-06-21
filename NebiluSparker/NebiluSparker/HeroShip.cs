using System;
using System.Threading;
using System.Collections.Generic;
using System.IO;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;

/* Staring battle craft; Code written from youtube
 * tutorial very inspired to me learning more about
 * C# rather than not because less exploitation 
 * against those monopolitics organization. This 
 * game is written to port to PS vita or PSM 
 * comercializable any time when it is completed.
 * I as author of Nebilu Sparker I found it is 
 * interesting although it could seems another revamp
 * retro to other person but what inspired me is because
 * a retro game on most advanced and sophiticated gaming 
 * devices.
 * Thanks for reading my comment.
 * */

namespace NebiluSparker
{
	public class HeroShip
	{
		private GraphicsContext graphics;
		private Sprite Hero;
		private int Speed;
		
		public Rectangle Extents{
			/* This create a collision between the Hero and Aliens. 
			 * */
			get {return new Rectangle(X, Y, Hero.Width, Hero.Height);} 
		}
		
		public float X{
			// get syntax use return to variable.
			get{return Hero.Position.X;}
		}
		
		public float Y{
			// get syntax use return to variable.
			get{return Hero.Position.Y;}
		}
		
		public HeroShip (GraphicsContext gs)
		{
			graphics = gs;
			Texture2D t = new Texture2D("Application/Resources/Hero.png", false);
			Hero = new Sprite(gs, t);
			// Set position center.
			Hero.Position.X = graphics.Screen.Rectangle.Width / 2 - Hero.Width / 2;
			Hero.Position.Y = graphics.Screen.Rectangle.Height / 2 - Hero.Height /2;
			Speed = 3;
			
			//Hero.SetColor(1,0,0,2);
			Hero.Scale.X = 0.3f;
			Hero.Scale.Y = .3f;
		}
		
		public void Update(GamePadData gamePadData){
			
			if((gamePadData.Buttons& GamePadButtons.Left )!=0 ){				
				// Boundary left limit.
				if(Hero.Position.X -Speed >= 0) Hero.Position.X -= Speed;
				
			}
			if((gamePadData.Buttons& GamePadButtons.Right )!=0 ){				
				// Boundary right limit.
				if(Hero.Position.X + Hero.Width +Speed < graphics.Screen.Rectangle.Width * .85f)
					Hero.Position.X += Speed;
			}
			if((gamePadData.Buttons& GamePadButtons.Up )!=0 ){				
				// Boundary up limit.
				if(Hero.Position.Y -Speed >= 0) Hero.Position.Y -= Speed;
				
			}
			if((gamePadData.Buttons& GamePadButtons.Down )!=0 ){
				// Boundary down limit.
				if(Hero.Position.Y + Hero.Height +Speed < graphics.Screen.Rectangle.Height)
					Hero.Position.Y += Speed;
			}
		}
		
		public void Render(){
			
			// Hero rendering itself.
			Hero.Render();
		}
	}
}

