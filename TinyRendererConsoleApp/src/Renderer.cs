using System;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Math = System.Math;
using TinyRendererConsoleApp.math;

namespace TinyRendererConsoleApp
{
    public class Renderer
    {
        public static void SetLine1(Image<Rgba32> image, int x0,int y0, int x1, int y1, Rgba32 color)
        {
            for (float t = 0; t < 1; t += 0.1f)
            {
                int x = (int)(x0 + (x1 - x0) * t);
                int y = (int)(y0 + (y1 - y0) * t);
                
                image[x,y] = color;
            }
        }
        
        public static void SetLine2(Image<Rgba32> image, int x0, int y0, int x1, int y1, Rgba32 color)
        {
            for (var x = x0; x <= x1; x++)
            {
                float t = (x - x0) / (float) (x1 - x0);
                int y = (int) (y0 * (1 - t) + y1 * t);
                
                image[x, y] = color;
            }
        }
        
        public static void SetLine3(Image<Rgba32> image, int x0, int y0, int x1, int y1, Rgba32 color)
        {
            var steep = System.Math.Abs(x0 - x1) < System.Math.Abs(y0 - y1);
            if (steep)
            {
                var tmp = x0;
                x0 = y0;
                y0 = tmp;
                
                tmp = x1;
                x1 = y1;
                y1 = tmp;
            }

            if (x0 > x1)
            {
                var tmp = x0;
                x0 = x1;
                x1 = tmp;

                tmp = y0;
                y0 = y1;
                y1 = tmp;
            }

            var dx = x1 - x0;
            var dy = y1 - y0;
            for (int x = x0; x <= x1; x++)
            {
                var t = (x - x0) / (float)(x1 - x0);
                int y = (int)(y0 * (1 - t) + y1 * t);
                if (steep)
                {
                    image[y, x] = color;
                }
                else
                {
                    image[x, y] = color;
                }
            }
        }
        
        public static void SetLine4(Image<Rgba32> image, int x0, int y0, int x1, int y1, Rgba32 color)
        {
            var steep = Math.Abs(x0 - x1) < Math.Abs(y0 - y1);
            if (steep)
            {
                var tmp = x0;
                x0 = y0;
                y0 = tmp;
                
                tmp = x1;
                x1 = y1;
                y1 = tmp;
            }

            if (x0 > x1)
            {
                var tmp = x0;
                x0 = x1;
                x1 = tmp;

                tmp = y0;
                y0 = y1;
                y1 = tmp;
            }

            var dx = Math.Abs(x1 - x0);
            var dy = Math.Abs(y1 - y0);
            float derror = dy / (float)dx;
            float error = 0;

            int y = y0;
            for (int x = x0; x <= x1; x++)
            {
                if (steep)
                {
                    image[y, x] = color;
                }
                else
                {
                    image[x, y] = color;
                }

                error += derror;

                if (!(error > 0.5f)) continue;
                y += y1 > y0 ? 1 : -1;
                error -= 1f;
            }

        }
        
        public static void SetLine5(Image<Rgba32> image, int x0, int y0, int x1, int y1, Rgba32 color)
        {
            var steep = Math.Abs(x0 - x1) < Math.Abs(y0 - y1);
            if (steep)
            {
                var tmp = x0;
                x0 = y0;
                y0 = tmp;
                
                tmp = x1;
                x1 = y1;
                y1 = tmp;
            }

            if (x0 > x1)
            {
                var tmp = x0;
                x0 = x1;
                x1 = tmp;

                tmp = y0;
                y0 = y1;
                y1 = tmp;
            }

            var dx = Math.Abs(x1 - x0);
            var dy = Math.Abs(y1 - y0);
            int derror2 = dy * 2;
            int error2 = 0;

            int y = y0;
            for (int x = x0; x <= x1; x++)
            {
                if (steep)
                {
                    image[y, x] = color;
                }
                else
                {
                    image[x, y] = color;
                }

                error2 += derror2;

                if (!(error2 > dx)) continue;
                y += y1 > y0 ? 1 : -1;
                error2 -= dx * 2;
            }

        }
        
        public static void SetLine6(Image<Rgba32> image, math.int2 p0, math.int2 p1, Rgba32 color)
        {
            var steep = Math.Abs(p0.x - p1.x) < Math.Abs(p0.y - p1.y);
            if (steep)
            {
                var tmp = p0.x;
                p0.x = p0.y;
                p0.y = tmp;
                
                tmp = p1.x;
                p1.x = p1.y;
                p1.y = tmp;
            }

            if (p0.x > p1.x)
            {
                var tmp = p0.x;
                p0.x = p1.x;
                p1.x = tmp;

                tmp = p0.y;
                p0.y = p1.y;
                p1.y = tmp;
            }

            var dx = Math.Abs(p1.x - p0.x);
            var dy = Math.Abs(p1.y - p0.y);
            int derror2 = dy * 2;
            int error2 = 0;

            int y = p0.y;

            if (steep)
            {
                for (int x = p0.x; x <= p1.x; x++)
                {
                    image[y, x] = color;
                    error2 += derror2;
                    if (!(error2 > dx)) continue;
                    y += p1.y > p0.y ? 1 : -1;
                    error2 -= dx * 2;
                }
            }
            else
            {
                for (int x = p0.x; x <= p1.x; x++)
                {
                    image[x, y] = color;
                    error2 += derror2;
                    if (!(error2 > dx)) continue;
                    y += p1.y > p0.y ? 1 : -1;
                    error2 -= dx * 2;
                }
            }
        }

        public static void Triangle(math.int2 t0, math.int2 t1, math.int2 t2, Image<Rgba32> image, Rgba32 color) 
        {
            // sort
            if (t0.y > t1.y) (t0, t1) = (t1, t0);
            if (t0.y > t2.y) (t0, t2) = (t2, t0);
            if (t1.y > t2.y) (t1, t2) = (t2, t1);

            int totalHeight = t2.y - t0.y;
            int segmentHeight = t1.y - t0.y ;

            for (var y = t0.y; y <= t1.y; y++)
            {
                float stepTotal = (float) (y - t0.y) / totalHeight;
                int2 a = t0 + (int2)((t2 - t0) * stepTotal);
                
                float stepSegment = (float) (y - t0.y) / segmentHeight;
                int2 b = t0 + (int2)((t1 - t0) * stepSegment);
                
                image[a.x, y] = Rgba32.Red;
                image[b.x, y] = Rgba32.Yellow;
                
                SetLine6(image, new int2(a.x, y), new int2(b.x, y), Rgba32.Blue);
            }

            segmentHeight = t2.y - t1.y ;
            for (var y = t2.y; y > t1.y; y--)
            {
                float stepTotal = (float) (t2.y - y) / totalHeight;
                int2 a = t2 + (int2)((t0 - t2) * stepTotal);
                
                float stepSegment = (float) (t2.y - y) / segmentHeight;
                int2 b = t2 + (int2)((t1 - t2) * stepSegment);
                
                image[a.x, y] = Rgba32.Red;
                image[b.x, y] = Rgba32.Yellow;

                SetLine6(image, new int2(a.x, y), new int2(b.x, y), Rgba32.Blue);
            }
            SetLine6(image, t0, t1, Rgba32.Yellow);
            SetLine6(image, t1, t2, Rgba32.Yellow);
            SetLine6(image, t2, t0, Rgba32.Red);
        }
    }
}