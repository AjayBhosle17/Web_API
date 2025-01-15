 namespace Demo
{
    public class Program
    {
        static void Main(string[] args)
        {
            int rows = 15;
            int mid = rows /2  + 1;
            int count = 1;
            
            for(int i = 1; i <= rows; i++)

            {
                int spaces = (i <= mid) ? mid - i : i - mid;
                for (int sp = 0; sp<spaces; sp++)
                {
                    Console.Write("  ");
                }
                for(int j=1;j<=count; j++)
                {
                        Console.Write("* ");
                }

                if (mid > i)
                {
                    count+=2;
                }
                else
                {
                    count-=2;
                }
                Console.WriteLine();
            }
        }
    }
}
          