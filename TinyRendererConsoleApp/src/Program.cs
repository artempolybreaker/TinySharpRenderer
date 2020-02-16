using System;
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

            using var image = new Image<Rgba32>(40, 40);
            image[0,0] = Rgba32.Red;
            image[10,10] = Rgba32.Red;
            image[20,20] = Rgba32.Red;
            image[30,30] = Rgba32.Red;

            using var fs = File.Create($"image.png");
            image.SaveAsPng(fs);
        }
    }
}