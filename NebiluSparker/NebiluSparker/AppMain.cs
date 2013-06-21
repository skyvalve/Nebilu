using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;
// Audio is manually input.
using Sce.PlayStation.Core.Audio;

using System.Diagnostics;
// Handling by HUD.cs
// using Sce.PlayStation.HighLevel.UI;

namespace NebiluSparker
{
	public class AppMain
	{
		// Enumeration for game stage changing.
		private enum GameStates {Menu, Playing, Paused, GameOver};
		private static GameStates currentState;
		
		private static GraphicsContext graphics;
		private static HeroShip Hero;
		private static HUD hud;
		
		// Stopwatch is a native class.
		private static Stopwatch clock;
		private static long TimeStart;
		private static long TimeStop;
		private static long TimeDelta;
		///private static Label Q;
		
		// Background.cs
		private static Background background;
		// Foreground.cs
		private static Foreground foreground;		
		// Alien listing.
		private static List<AlienShip> Alienator;
		// Projectile missile.
		private static List<Projectile> missile;
		// Random variable.
		private static Random rand;
		// Time counter for generating new asailants.
		private static long alienTimeCounter;
		// Shooting threshold.
		private static long ShotTimer;		
		// Bgm music variable.
		//private static Bgm bgm;
    	private static BgmPlayer bgmPlayer;
		// Sound.
		private static SoundPlayer FireShots;
		private static SoundPlayer Explosion;
		// New instance of menus.
		private static MenuDisplays Menus;
		// List to explosions.
		private static List<explosion> Explosions;
		
		
		public static void Main (string[] args)
		{
			Initialize ();

			while (true) {
				
				// Custom time stamp.
				TimeStart = clock.ElapsedMilliseconds;
				// Begining strings.
				SystemEvents.CheckEvents ();
				Update ();
				Render ();
				// Custom time stamp.
				TimeStop = clock.ElapsedMilliseconds;
				TimeDelta = TimeStop - TimeStart;
			}
		}

		public static void Initialize ()
		{ 			
			// Set up the graphics system
			graphics = new GraphicsContext ();		 		
			// Background.cs initialization.
			background = new Background(graphics);
			// Fore ground
			foreground = new Foreground (graphics);			
			// Create clock from System.Dianogstic
			clock = new Stopwatch();
			clock.Start();			
			
			rand = new Random(); // rand.Next(-100, 230); this will perform random calculation.							
						
			// Music.
			Bgm bgm = new Bgm("/Application/Resources/bgm.mp3"); 
			// This will create a music based on the music file installed.
			bgmPlayer = bgm.CreatePlayer();	
			/* For this lines I experiencing problems is because
			 * the sound file I manually import is not working well
			 * with PSM sdk. It is there with some standard that will
			 * that more time in configuring it. 
			 * **********************************************************/			
			Sound noise=new Sound("/Application/Resources/missile_shot.wav");
			FireShots = noise.CreatePlayer();
			Sound Explode=new Sound("/Application/Resources/explosion.wav");
			Explosion = Explode.CreatePlayer();
			/* Game start with game enumaration transition to states.
			 * These lines below must be written after all of the initialization.
			 * */
			currentState = GameStates.Menu;
			// New games starts ^^
			NewGame();
			// Menu displays. Only once in initialize.
			Menus = new MenuDisplays(graphics);
		}

		public static void Update ()
		{
			/* Switch the enumeration.			 * 
			 * */
			switch(currentState){
				
			case GameStates.Menu : UpdateMenu(); break;
			case GameStates.GameOver : UpdateGameOver(); break;				
			case GameStates.Paused : UpdatePause(); break;
			case GameStates.Playing : UpdatePlays(); break;
			}
		}
		/* Enumeration game pad controllers. 
		 * */
		public static void UpdateMenu(){
			
			var gamePadData = GamePad.GetData (0);
			if((gamePadData.Buttons & GamePadButtons.Triangle)!= 0){
				
				NewGame();
				currentState = GameStates.Playing;
			}
				
		}
		
		public static void NewGame(){
			// HUD.cs initializing.
			hud = new HUD(graphics);
			// Hero ship initializer.
			Hero = new HeroShip(graphics);
			// List Alien ships.
			Alienator = new List<AlienShip>();
			// A connected variables for each alien ships.
			AlienShip m;
			// Here add enemy ships.
			m = new AlienShip(graphics, graphics.Screen.Rectangle.Width - 100, 
			              graphics.Screen.Rectangle.Height / 2, 4);
			Alienator.Add(m);
			m = new AlienShip(graphics, graphics.Screen.Rectangle.Width - 150, 
			              graphics.Screen.Rectangle.Height / 2 + 150, 2);
			Alienator.Add(m);
			m = new AlienShip(graphics, graphics.Screen.Rectangle.Width - 50, 
			              graphics.Screen.Rectangle.Height / 2 - 100, 6);
			Alienator.Add(m);
			m = new AlienShip(graphics, graphics.Screen.Rectangle.Width - 50, 
			              graphics.Screen.Rectangle.Height / 2 - 200, 5);
			Alienator.Add(m);
			// Alien generating.
			alienTimeCounter = 0;
			// Missile generating.
			missile = new List<Projectile>();	
			// Shot timer.
			ShotTimer = 0;	
			// Explosion here.
			Explosions = new List<explosion>();
		}
		
		public static void UpdateGameOver(){
			// Game over back to menu.
			var gamePadData = GamePad.GetData (0);
			if((gamePadData.Buttons & GamePadButtons.Select)!= 0)
				currentState = GameStates.Menu;
		}
		
		public static void UpdatePause(){
			// Paused to plays.
			var gamePadData = GamePad.GetData (0);
			if((gamePadData.Buttons & GamePadButtons.Triangle)!= 0){
				
				hud = new HUD(graphics);
				currentState = GameStates.Playing;
			}
		}
		
		public static void UpdatePlays(){
			// Music plays from here.		
			bgmPlayer.Loop = true;	
			bgmPlayer.Volume = .62f;
			bgmPlayer.Play();	
			// Query gamepad for current state
			var gamePadData = GamePad.GetData (0);
			// Gamepad date invoke.
			Hero.Update(gamePadData);
			// Back ground update.
			background.Update();
			// Fore ground update.
			foreground.Update();
			// Alien handling.
			HandleAliens();
			// Projectile handling.
			HandleProjectile();
			// Dynamic label to tell Hero position, in connection 
			// to HeroShip.cs in *get* value.
			hud.UpdatePosition("Position = " +Hero.X+ ":" +Hero.Y);
			///Q.Text = "Position = " +Hero.X+ ":" +Hero.Y;
			
			// Time implementation.
			hud.UpdateFPS(TimeDelta);
			// Aliens counter in HUD.
			hud.UpdateAlienCount(Alienator.Count);
			// MIssiles counter from HUD.
			hud.UpdateMissileCount(missile.Count);
			// Explosion frame method.
			HandleExplosion();
			// Shot timer update.
			ShotTimer += TimeDelta;
			// User Game over management.
			if(HeroEnemiesCollision()== true){
				
				Menus = new MenuDisplays(graphics);
				currentState = GameStates.GameOver;
				
			}
			// Projectile button.
			if((gamePadData.Buttons & GamePadButtons.Cross)!= 0){
				
				HandlingShots();
			}
			
			if((gamePadData.Buttons & GamePadButtons.Circle)!= 0){
				// Menus with graphics pass in.
				Menus = new MenuDisplays(graphics);
				currentState = GameStates.Paused;
			}
			
		}
		 
		private static bool HeroEnemiesCollision(){
			/* A collision created for the Hero ship.
			 * */
			foreach(AlienShip o in Alienator){
				Rectangle AlienExtents = o.Extents;
				Rectangle HeroShipExtents = Hero.Extents;
				
				if(Overlaps(AlienExtents, HeroShipExtents)) return true;
			}
			return false;
		}
		
		private static void HandleExplosion(){
			/* Explosion handler. */
			for(int x = Explosions.Count-1; x >= 0; x--)
				if(Explosions[x].isLive == false )
					Explosions.RemoveAt(x);
				else 
					/* Time transpired. */
					Explosions[x].Update(TimeDelta);
					
		}
		
		private static void HandleAliens(){
			// Alien counter.
			alienTimeCounter += TimeDelta;
			// Generating if statement.
			if(alienTimeCounter > 2400){
				alienTimeCounter -= 2400;
				int offset = rand.Next(-200, 201);
				int speed;
				speed = rand.Next(2, 5);
				
				AlienShip m = new AlienShip(graphics, graphics.Screen.Rectangle.Width + 50,
				                  graphics.Screen.Rectangle.Height / 2 +offset, speed);
				Alienator.Add(m);			
			}
			// foreach statement.
			foreach(AlienShip m in Alienator){
				m.Update();			
			}
			
			for(int i = Alienator.Count-1; i >= 0; i--){
				// Loop to substract.
				if(Alienator[i].X < -100){
					Alienator.RemoveAt(i);
				}				
			}
		}
		
		private static void HandleProjectile(){
			// Projectile method.
			foreach(Projectile m in missile)
				m.Update();
			// Projectile count.
			for(int i = missile.Count-1; i >= 0; i--){
				if(missile[i].X > graphics.Screen.Rectangle.Width){
					missile.RemoveAt(i);		
				}
			}			
			// Add in method for collision.
			CheckForCollision();
		}
		
		private static void CheckForCollision(){
			for(int i = 0; i < missile.Count;i++ ){
				/* A 2 dimensional loop with for key word. 
				 * any one can make 3 or 4 dimensional loops
				 * based on for keyword by adding another for
				 * loop inside a for loop. 
				 * */
				for(int c = 0; c < Alienator.Count;c++){
					// This will asking the missile what are their extents?
					Rectangle missileExtents = missile[i].Extents;
					/* This will asking the Alien's extents.
					 * This also polimorphism from AlienShip.cs
					 * Rectangle extents. */
					Rectangle AliensExtents = Alienator[c].Extents;
					// See these two methods overlapsed?
					if(Overlaps(missileExtents, AliensExtents)== true){
						// This line will remove Alien ships and missile collided.
						missile[i].isLive = false;
						Alienator[c].isLive = false;
						Explosion.Play();
						explosion bomb;
						bomb = new explosion(graphics, (int)Alienator[c].X, 
						                     (int)Alienator[c].Y);
						Explosions.Add(bomb); 
						break;
					}
				}
			}
			/* This formula don't need curly braces; This calculating
			 * when to obsolete the aliens and projectiles repeatable
			 * because it constructed in a for loop. 
			 * */
			for (int c = Alienator.Count-1; c >= 0; c--)
				if (Alienator[c].isLive == false)
					Alienator.RemoveAt(c);
			
			for(int i = missile.Count-1; i >= 0; i--)
				if (missile[i].isLive == false)
					missile.RemoveAt(i);
		}
		
		public static bool Overlaps(Rectangle h1, Rectangle h2){
			/* These lines comprised for overlaps. */
			if (h1.X + h1.Width < h2.X) return false;
			
			if (h1.X > h2.X + h2.Width ) return false;
			/* Above lines are for X and width, here below is 
			 * for Y and height. 
			 * */
			if (h1.Y + h1.Height < h2.Y) return false;
			
			if (h1.Y > h2.Y + h2.Height ) return false;
			
			return true;			
		}
		
		private static void HandlingShots(){
			// Time delta.
			if(ShotTimer >= 360){
				// Shot timer reset to 0.
				ShotTimer = 0;				
				// Missile timer.
				Projectile m = new Projectile(graphics, (int)Hero.X +50, (int)Hero.Y +20);
					missile.Add(m);		
				// Noise to shots.
				FireShots.Play();
			}
		}

		public static void Render ()
		{
			/* Game state rendering.
			 * */
			switch(currentState){
				
			case GameStates.Playing: RenderPlaying(); break;
			case GameStates.GameOver: RenderGameOver(); break;
			case GameStates.Menu: RenderMenu(); break;
			case GameStates.Paused: RenderPause(); break;
				
			}
		}
		
		public static void RenderGameOver(){
			// Clear the screen
			graphics.SetClearColor (1.6f, 0.0f, 0.0f, 0.5f);
			graphics.Clear ();
			// Menu.
			foreground.Render();
			Menus.Announcement = "GAME OVER - Press SELECT to return the TITLE.";
			Menus.Render();
			// Present the screen
			graphics.SwapBuffers ();
		}
		
		public static void RenderMenu(){
			// Clear the screen
			graphics.SetClearColor (0.0f, 0.0f, 1.0f, 0.0f);
			graphics.Clear ();
			// Back and fore grounds rendering.
			background.Render();
			//foreground.Render();
			/* Menu announcement.*/
			Menus.Announcement = "MAIN MENU - Press start to play.";
			Menus.Render();			
			// Present the screen
			graphics.SwapBuffers ();
		}
			
		public static void RenderPause(){
			graphics.SetClearColor (0.0f, 0.0f, 0.0f, 0.0f);
			graphics.Clear ();
			Menus.Announcement = "PAUSED - Press TRIANGLE to resume.";
			Menus.Render();
			graphics.SwapBuffers (); 
		}
		
		public static void RenderPlaying(){
			// Clear the screen
			graphics.SetClearColor (0.0f, 0.0f, 0.0f, 0.0f);
			graphics.Clear ();
			// Back ground rendering.
			background.Render();
			// Custom resource.
			Hero.Render();
			// Foreach statement. After in of foreach must be match.
			foreach(AlienShip mv in Alienator)
				mv.Render();
			// Projectile rendering.
			foreach(Projectile m in missile)
				m.Render();
			// Explosion frames
			foreach(explosion x in Explosions)
				x.Render(); 
			// Fore ground rendering.
			foreground.Render ();
			// HUD rendering.
			hud.Render();
			// Present the screen
			graphics.SwapBuffers ();
			
		}
	}
}
