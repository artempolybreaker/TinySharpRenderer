using System;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Math = System.Math;

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

            var dx = System.Math.Abs(x1 - x0);
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
        
        public static void SetLine6(Image<Rgba32> image, int x0, int y0, int x1, int y1, Rgba32 color)
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

            if (steep)
            {
                for (int x = x0; x <= x1; x++)
                {
                    image[y, x] = color;
                    error2 += derror2;
                    if (!(error2 > dx)) continue;
                    y += y1 > y0 ? 1 : -1;
                    error2 -= dx * 2;
                }
            }
            else
            {
                for (int x = x0; x <= x1; x++)
                {
                    image[x, y] = color;
                    error2 += derror2;
                    if (!(error2 > dx)) continue;
                    y += y1 > y0 ? 1 : -1;
                    error2 -= dx * 2;
                }
            }
        }
        
    }
}