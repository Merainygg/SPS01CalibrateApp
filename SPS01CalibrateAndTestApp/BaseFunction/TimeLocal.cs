using System.Runtime.InteropServices;

namespace TPLoopTestSystem
{
    public sealed class TimeLocal
    {

        [DllImport("winmm")]
        private static extern uint timeGetTime();

        public static void DelayMs(uint delay)
        {
            var start = timeGetTime();
            uint stamp;
            do
            {
                stamp = timeGetTime() - start;
            }
            while (stamp < delay);
        }
    }
}
