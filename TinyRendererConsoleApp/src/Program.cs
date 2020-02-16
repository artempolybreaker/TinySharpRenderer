using System;
using System.Diagnostics;
using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;

namespace TinyRendererConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            using var image = new Image<Rgba32>(100, 100);

            // Renderer.SetLine5(image, 13, 20, 80, 40, Rgba32.Yellow);
            // Renderer.SetLine5(image, 20, 13, 40, 80, Rgba32.White);
            // Renderer.SetLine5(image, 80, 40, 13, 20, Rgba32.Red);
            
            Test(image);

            // SAVE TO DISK
            var encoder = new PngEncoder()
            {
                CompressionLevel = 1,
            };

            if (!Directory.Exists("output/"))
            {
                Directory.CreateDirectory("output/");
            }

            image.Save("output/image2.png", encoder);
        }

        private static void Test(Image<Rgba32> image)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            for (int i = 0; i < 1000000; i++)
            {
                Renderer.SetLine3(image, 13, 20, 80, 40, Rgba32.Yellow);
                Renderer.SetLine3(image, 20, 13, 40, 80, Rgba32.White);
                Renderer.SetLine3(image, 80, 40, 13, 20, Rgba32.Red);
            }

            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);
            sw.Restart();

            for (int i = 0; i < 1000000; i++)
            {
                Renderer.SetLine4(image, 13, 20, 80, 40, Rgba32.Yellow);
                Renderer.SetLine4(image, 20, 13, 40, 80, Rgba32.White);
                Renderer.SetLine4(image, 80, 40, 13, 20, Rgba32.Red);
            }

            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);

            sw.Restart();

            for (int i = 0; i < 1000000; i++)
            {
                Renderer.SetLine5(image, 13, 20, 80, 40, Rgba32.Yellow);
                Renderer.SetLine5(image, 20, 13, 40, 80, Rgba32.White);
                Renderer.SetLine5(image, 80, 40, 13, 20, Rgba32.Red);
            }

            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);
            
            sw.Restart();

            for (int i = 0; i < 1000000; i++)
            {
                Renderer.SetLine6(image, 13, 20, 80, 40, Rgba32.Yellow);
                Renderer.SetLine6(image, 20, 13, 40, 80, Rgba32.White);
                Renderer.SetLine6(image, 80, 40, 13, 20, Rgba32.Red);
            }

            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);       
        }
    }
}