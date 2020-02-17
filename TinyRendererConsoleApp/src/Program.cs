using System;
using System.Collections.Generic;
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
            int height = 512;
            int width = 512;
            using var image = new Image<Rgba32>(width, height);

            // Renderer.SetLine5(image, 13, 20, 80, 40, Rgba32.Yellow);
            // Renderer.SetLine5(image, 20, 13, 40, 80, Rgba32.White);
            // Renderer.SetLine5(image, 80, 40, 13, 20, Rgba32.Red);
            
            // Test(image);

            List<math.float3> vertices = new List<math.float3>();
            List<int> faces = new List<int>();
            // PARSE FILE
            ParseObjFile(vertices, faces);
            
            // RENDER WIRE
            for (int i = 0; i < faces.Count; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    var v0 = vertices[faces[j]];
                    var v1 = vertices[faces[(j + 1) % 3]];

                    int x0 = (int) ((v0.x + 1f) * width / 2f);
                    int y0 = (int) ((v0.y + 1f) * height / 2f);
                    int x1 = (int) ((v1.x + 1f) * width / 2f);
                    int y1 = (int) ((v1.y + 1f) * height / 2f);
                    
                    Renderer.SetLine5(image, x0, y0, x1, y1, Rgba32.White);
                }
            }
            
            // SAVE TO DISK
            var encoder = new PngEncoder()
            {
                CompressionLevel = 1,
            };
            
            if (!Directory.Exists("output/"))
            {
                Directory.CreateDirectory("output/");
            }
            
            image.Save("output/dude.png", encoder);
        }

        private static void ParseObjFile(List<math.float3> vertices, List<int> faces)
        {
            using var reader =
                new StreamReader(
                    File.OpenRead("D:/PROJECTS/C#/TinyRenderer/TinyRenderer/TinyRendererConsoleApp/data/objModels/african_head.obj"));

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (line == null)
                {
                    continue;
                }

                if (line.Contains("v "))
                {
                    line = line.Remove(0, 2);
                    var bar = line.Split(' ');
                    vertices.Add(new math.float3()
                    {
                        x = float.Parse(bar[0]),
                        y = float.Parse(bar[1]),
                        z = float.Parse(bar[2]),
                    });
                }
                else if (line.Contains("f "))
                {
                    line = line.Remove(0, 2);
                    var indices = line.Split(' ');
                    for (int i = 0; i < indices.Length; i++)
                    {
                        var foo = indices[i].Split('/');
                        faces.Add(Int32.Parse(foo[0]) - 1);
                    }
                }
            }

            Console.WriteLine(vertices.Count);
            Console.WriteLine(faces.Count);
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