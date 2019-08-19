    //we need to find out where we are at a moment and it will be easyiesr if we can do it with ints, thats why we are creating the following struct
    [System.Serializable]
    public struct IntVector2
    {
        public int r, c;

    //its a simple constructor
    public IntVector2(int r, int c)
    {
        this.r = r;
        this.c = c;
    }

    //since we are going to add both vectors together in order to define out coordinates we can use the operator +
    public static IntVector2 operator +(IntVector2 a, IntVector2 b)
        {
            a.r += b.r;
            a.c += b.c;
            return a;
        }
    }

