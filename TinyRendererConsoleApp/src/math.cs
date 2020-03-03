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
}