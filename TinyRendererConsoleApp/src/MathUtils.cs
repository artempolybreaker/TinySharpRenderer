using System;

namespace TinyRendererConsoleApp.math
{
    public struct float2
    {
        public float x, y;

        public float2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }


        public static explicit operator int2(float2 value)
        {
            return new int2((int) value.x, (int) value.y);
        }
    }
    public struct float3
    {
        public float x, y, z;

        public float3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public static float3 operator *(float3 a, float3 b)
        {
            return new float3(a.x * b.x, a.y * b.y, a.z * b.z);
        }

        public static float3 operator -(float3 a, float3 b)
        {
            return new float3(a.x - b.x, a.y - b.y, a.z - b.z);
        }
    }
    public struct int3
    {
        public int x, y, z;
    }
    public struct int2
    {
        public int x, y;

        public int2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static int2 operator +(int2 a, int2 b)
        {
            return new int2(a.x + b.x, a.y + b.y);
        }
        
        public static int2 operator -(int2 a, int2 b)
        {
            return new int2(a.x - b.x, a.y - b.y);
        }

        public static int2 operator *(int2 a, int b)
        {
            return new int2(a.x * b, a.y * b);
        }
        
        public static float2 operator *(int2 a, float b)
        {
            return new float2(a.x * b, a.y * b);
        }
        
    }

    public static class MathUtils
    {
        public static (int2 min, int2 max) BoundingBox(int2[] values)
        {
            int2 min = new int2(int.MaxValue, int.MaxValue);
            int2 max = new int2(int.MinValue, int.MinValue);
            
            for (int i = 0; i < values.Length; i++)
            {
                var point = values[i];
                min.x = point.x < min.x ? point.x : min.x;
                min.y = point.y < min.y ? point.y : min.y;
                max.x = point.x > max.x ? point.x : max.x;
                max.y = point.y > max.y ? point.y : max.y;
            }

            return (min, max);
        }

        public static int2 Clamp(int2 value, int2 min, int2 max)
        {
            value.x = Math.Clamp(value.x, min.x, max.x);
            value.y = Math.Clamp(value.y, min.y, max.y);

            return value;
        }

        public static float Magnitude(float3 vec)
        {
            var sum = vec.x * vec.x + vec.y * vec.y + vec.z * vec.z;
            return (float) Math.Sqrt(sum);
        }

        public static float3 Normalize(float3 vec)
        {
            var length = Magnitude(vec);
            return new float3(vec.x / length, vec.y / length, vec.z / length);
        }

        public static float3 Cross(float3 a, float3 b)
        {
            var aYZX = new float3(a.y, a.z, a.x);
            var bYZX = new float3(b.y, b.z, b.x);
            var res = (a * bYZX - aYZX * b);
            return new float3(res.y, res.z, res.x);
        }

        public static float Dot(float3 a, float3 b)
        {
            return a.x * b.x + a.y * b.y + a.z * b.z;
        }
    }
}