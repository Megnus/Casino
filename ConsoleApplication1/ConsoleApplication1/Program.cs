using System;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

/*http://www.elitepvpers.com/forum/net-languages/118712-c-windows-api.html*/
/*http://www.jarloo.com/take-a-screenshot-in-c/*/

namespace CasinoTest
{
	internal class Program
	{
		[DllImport( "user32" )]
		public static extern int SetCursorPos( int x, int y );

		[DllImport( "user32.dll" )]
		private static extern void mouse_event( uint dwFlags, uint dx, uint dy, uint dwData, int dwExtraInfo );

		[Flags]
		public enum MouseEventFlags
		{
			LEFTDOWN = 0x00000002,
			LEFTUP = 0x00000004,
			MIDDLEDOWN = 0x00000020,
			MIDDLEUP = 0x00000040,
			MOVE = 0x00000001,
			ABSOLUTE = 0x00008000,
			RIGHTDOWN = 0x00000008,
			RIGHTUP = 0x00000010
		}

		private static Bitmap GetScreenShot()
		{
			Bitmap bitmap = new Bitmap(
				Screen.PrimaryScreen.Bounds.Width,
				Screen.PrimaryScreen.Bounds.Height,
				PixelFormat.Format32bppArgb );

			using ( Graphics graphics = Graphics.FromImage( bitmap ) )
			{
				graphics.CopyFromScreen(
					Screen.PrimaryScreen.Bounds.X,
					Screen.PrimaryScreen.Bounds.Y,
					0,
					0,
					Screen.PrimaryScreen.Bounds.Size,
					CopyPixelOperation.SourceCopy );
			}

			return bitmap;
		}

		private static void Main( string[] args )
		{
			SetCursorPos( 0, 0 );
			mouse_event( (uint) MouseEventFlags.RIGHTDOWN, 0, 0, 0, 0 );
			mouse_event( (uint) MouseEventFlags.RIGHTUP, 0, 0, 0, 0 );
			GetScreenShot().Save( "C://Users/magnus.PANAGORAROOM/ScreenCapture.jpg", ImageFormat.Jpeg );
			GetPixelExample();
			Console.Read();
		}

		private static void GetPixelExample()
		{
			// Create a Bitmap object from an image file.
			Bitmap myBitmap = new Bitmap( "C://Users/magnus.PANAGORAROOM/ScreenCapture.jpg" );

			// Get the color of a pixel within myBitmap.

			Color pixelColor = myBitmap.GetPixel( 50, 50 ); //GetScreenShot().GetPixel( 50, 50 ); 

			for ( var i = 1; i < 100; i++ )
			{
				string redHex = ColorTranslator.ToHtml( GetScreenShot().GetPixel( 5, 5 ) );
				Console.WriteLine( redHex );
				Thread.Sleep( 1000 );
				
			}
				
			Console.WriteLine( pixelColor.ToString() );
			// Fill a rectangle with pixelColor.
			//SolidBrush pixelBrush = new SolidBrush( pixelColor );
			//e.Graphics.FillRectangle( pixelBrush, 0, 0, 100, 100 );
		}
	}
}