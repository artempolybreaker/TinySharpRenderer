using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Processors.Transforms;
using SixLabors.Primitives;
using TinyRendererConsoleApp.math;

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

            RenderObjFile();
            
            // int2 p0 = new int2(10, 70);
            // int2 p1 = new int2(50, 160);
            // int2 p2 = new int2(70, 80);
            // int2 p3 = new int2(180, 50);
            // int2 p4 = new int2(150, 1);
            // int2 p5 = new int2(70, 180);
            // int2 p6 = new int2(180, 150);
            // int2 p7 = new int2(120, 160);
            // int2 p8 = new int2(130, 180);
            // Renderer.Triangle(p0, p1, p2, image, Rgba32.Red);
            // Renderer.Triangle(p3, p4, p5, image, Rgba32.White);
            // Renderer.Triangle(p6, p7, p8, image, Rgba32.Gold);

            // int2[] points = new int2[] {new int2(10, 10), new int2(100, 30), new int2(190, 160)};
            // Renderer.Triangle(points, image, Rgba32.Blue);
            
            // SAVE TO DISK
            var encoder = new PngEncoder()
            {
                CompressionLevel = 1,
            };

            if (!Directory.Exists("output/"))
            {
                Directory.CreateDirectory("output/");
            }
            image.Save("output/image.png", encoder);
        }

        private static void ParseObjFile(List<float3> vertices, List<int> faces)
        {
            using var reader =
                new StreamReader(
                    File.OpenRead(
                        "D:/PROJECTS/C#/TinyRenderer/TinyRenderer/TinyRendererConsoleApp/data/objModels/african_head.obj"));

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
                    line = line.Trim();
                    var bar = line.Split(' ');
                    vertices.Add(new float3()
                    {
                        x = float.Parse(bar[0]),
                        y = float.Parse(bar[1]),
                        z = float.Parse(bar[2]),
                    });
                }
                else if (line.Contains("f "))
                {
                    line = line.Trim();
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

        private static void RenderObjFile()
        {
            var width = 512;
            var height = 512;
            using var image = new Image<Rgba32>(width, height);
            List<float3> vertices = new List<float3>();
            List<int> faces = new List<int>();

            //PARSE FILE
            ParseObjFile(vertices, faces);

            
            var random = new Random(1);
            //RENDER FILLED COLORED
            for (int i = 0; i < faces.Count; i+=3)
            {
                int2[] screenCoords = new int2[3];
                float3[] worldCoords = new float3[3];
                for (int j = 0; j < 3; j++)
                {
                    var v0 = vertices[faces[j + i]];
            
                    int x = (int) ((v0.x + 1f) * (width - 1) / 2f);
                    int y = (int) ((v0.y + 1f) * (height - 1) / 2f);
                    
                    screenCoords[j] = new int2(x, y);
                    worldCoords[j] = v0;
                }

                var normal = MathUtils.Cross(worldCoords[2] - worldCoords[0], worldCoords[1] - worldCoords[0]);
                normal = MathUtils.Normalize(normal);
                var lightDir = new float3(0,0,-1);
                var intensity = MathUtils.Dot(normal,lightDir);
                intensity = Math.Clamp(intensity, 0, 1);
                if (intensity > 0)
                {
                    Renderer.Triangle(screenCoords, image, new Rgba32(intensity * (float)random.NextDouble(), intensity * (float)random.NextDouble(), intensity * (float)random.NextDouble()));
                }
            }
            
            
            // //RENDER WIRE
            // for (int i = 0; i < faces.Count; i+=3)
            // {
            //     int2[] screenCoords = new int2[3];
            //     for (int j = 0; j < 3; j++)
            //     {
            //         var v0 = vertices[faces[j + i]];
            //         var v1 = vertices[faces[(j + 1) % 3 + i]];
            //
            //         int x0 = (int) ((v0.x + 1f) * (width - 1) / 2f);
            //         int y0 = (int) ((v0.y + 1f) * (height - 1) / 2f);
            //         int x1 = (int) ((v1.x + 1f) * (width - 1) / 2f);
            //         int y1 = (int) ((v1.y + 1f) * (height - 1) / 2f);
            //
            //         screenCoords[j] = new int2(x0, y0);
            //         try
            //         {
            //             Renderer.SetLine5(image, x0, y0, x1, y1, Rgba32.White);
            //         }
            //         catch (IndexOutOfRangeException e)
            //         {
            //             Console.Write($"Out of bounds exception, coords were: v0:({x0},{y0}), v1:({x1},{y1})");
            //             return;
            //         }
            //     }
            // }
            
            image.Mutate(new FlipProcessor(FlipMode.Vertical));
            
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
                Renderer.SetLine6(image, new int2(13, 20), new int2(80, 40), Rgba32.Yellow);
                Renderer.SetLine6(image, new int2(20, 13), new int2(40, 80), Rgba32.White);
                Renderer.SetLine6(image, new int2(80, 40), new int2(13, 20), Rgba32.Red);
            }

            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);
        }
    }
}