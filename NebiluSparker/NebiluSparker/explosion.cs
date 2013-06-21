using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;
/* Copied form Projectile. 
 * */
namespace NebiluSparker
{
	public class explosion
	{
		private GraphicsContext graphics;
		private Sprite expl;
		private float Speed;
		private long frameExplosion;
		private int activeFrame;
		// Read only const.
		private const int FRAME_DURA = 45;
		private const int FRAME_MAX = 15;
		private const int FRAME_CELLSIZE = 137;
		private const int FRAME_CELLSIZE2 = 300;
		
		public bool isLive;
		
			
		public float X{
			// get syntax use return to variable.
			get{return expl.Position.X;}
		}
		
		public float Y{
			// get syntax use return to variable.
			get{return expl.Position.Y;}
		}
		
		public explosion (GraphicsContext gs, int x, int y)
		{
			graphics = gs;
			
			Texture2D t = new Texture2D("Application/Resources/Flame.png", false);
			expl = new Sprite(gs, t);
			// Set position center.
			expl.Position.X = x;
			expl.Position.Y = y;
			Speed = 20.7f;
			
		    /*// missile.SetColor(1,0,0,2);
			expl.Scale.X = 1.6f;
			expl.Scale.Y = .7f;*/
			
			isLive = true;
			// Frame initialization.
			activeFrame = 0;
			// Frame cell animation.
			expl.Height = FRAME_CELLSIZE;
			expl.Width = FRAME_CELLSIZE2;
		}
		
		public void Update(long elapsedTime){
			
			expl.Position.X -= Speed;
			// This line show how to update active sprite sheet.
			frameExplosion += elapsedTime;
			if(frameExplosion > FRAME_DURA){
				activeFrame++;
				frameExplosion = 0;
				if(activeFrame > FRAME_MAX)
					isLive = false;
			}
		}
		
		public void Render(){
			// This adding the frame animation. 
			expl.SetTextureCoord(FRAME_CELLSIZE * activeFrame,
			                     0,
			                     FRAME_CELLSIZE * (activeFrame+1)-1,
			                     expl.Height-1);
			// Hero rendering itself.
			expl.Render();
		}
	}
}

