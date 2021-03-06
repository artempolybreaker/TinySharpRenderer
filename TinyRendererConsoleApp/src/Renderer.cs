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
        
        public static void SetLine6(Image<Rgba32> image, int2 p0, int2 p1, Rgba32 color)
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

        public static void Triangle(int2 t0, int2 t1, int2 t2, Image<Rgba32> image, Rgba32 color) 
        {
            if (t0.y == t1.y && t0.y == t2.y) return;
            
            // sort
            if (t0.y > t1.y) (t0, t1) = (t1, t0);
            if (t0.y > t2.y) (t0, t2) = (t2, t0);
            if (t1.y > t2.y) (t1, t2) = (t2, t1);

            int totalHeight = t2.y - t0.y;

            for (var i = 0; i < totalHeight; i++)
            {
                bool secondHalf = i > t1.y - t0.y || t1.y == t0.y;
                int segmentHeight = secondHalf ? t2.y - t1.y : t1.y - t0.y;

                float totalStep = (float)i / totalHeight;
                float segmentStep = (float)(i - (secondHalf ? t1.y - t0.y : 0)) / segmentHeight;

                int2 a = t0 + (int2)((t2 - t0) * totalStep);
                int2 b = secondHalf ? t1 + (int2) ((t2 - t1) * segmentStep) : t0 + (int2) ((t1 - t0) * segmentStep);

                if (a.x > b.x) (a, b) = (b, a);

                for (int j = a.x; j <= b.x; j++)
                {
                    image[j, t0.y + i] = Rgba32.Blue;
                }
            }

            SetLine6(image, t0, t1, Rgba32.Yellow);
            SetLine6(image, t1, t2, Rgba32.Yellow);
            SetLine6(image, t2, t0, Rgba32.Red);
        }

        public static void Triangle(int2[] points, Image<Rgba32> image, Rgba32 color)
        {
            // bounding box
            var (min, max) = MathUtils.BoundingBox(points);
            
            min.x = Math.Clamp(min.x, 0, image.Width);
            min.y = Math.Clamp(min.y, 0, image.Height);
            min.x = Math.Clamp(min.x, 0, image.Width);
            min.x = Math.Clamp(min.x, 0, image.Width);

            for (int x = min.x; x < max.x; x++)
            {
                for (int y = min.y; y < max.y; y++)
                {
                    var point = new int2(x, y);
                    
                    float3 bcScreen = Barycentric(points, point);
                    if (bcScreen.x < 0 || bcScreen.y < 0 || bcScreen.z < 0) continue;
                    image[x, y] = color;
                }
            }

        }

        private static float3 Barycentric(int2[] points, int2 point)
        {
            if (points.Length != 3)
            {
                return new float3(-1f, -1f, -1f);
            }

            var vX = new float3(points[2].x - points[0].x, points[1].x - points[0].x, points[0].x - point.x);
            var vY = new float3(points[2].y - points[0].y, points[1].y - points[0].y, points[0].y - point.y);

            float3 orth = MathUtils.Cross(vX, vY);

            /* `pts` and `P` has integer value as coordinates
            so `abs(u[2])` < 1 means `u[2]` is 0, that means
            triangle is degenerate, in this case return something with negative coordinates */
            if (Math.Abs(orth.z) < 1) return new float3(-1, 1, 1);
            return new float3(1f - (orth.x+orth.y)/orth.z, orth.y/orth.z, orth.x/orth.z);
        }
    }
}