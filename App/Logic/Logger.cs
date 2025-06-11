using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace Logic
{
    public class Logger
    {
        private string colorName;
        private readonly Random random = new();
        private string filePath = "C:\\Users\\tymon\\source\\repos\\TPW2025\\App\\Logic\\bin\\Debug\\net8.0-windows\\ball_log_game.txt";
        private readonly Queue<string> buffer = new();
        private bool running = false;
        private Thread writerThread;

        public void Start()
        {
            filePath = "C:\\Users\\tymon\\source\\repos\\TPW2025\\App\\Logic\\bin\\Debug\\net8.0-windows\\ball_log_game_" + random.Next(999) + ".txt";
            running = true;
            writerThread = new Thread(WriteLoop);
            writerThread.Start();
        }

        public void Stop()
        {
            running = false;
            writerThread?.Join();
            Environment.Exit(0);
        }

        public void BallConstructor(Ball ball)
        {
            var timestamp = DateTime.Now.ToString("HH:mm:ss");
            string log = $"[{timestamp}]:New Ball created:\n\t\tX: {ball.X:F2}\n\t\tY: {ball.Y:F2}\n\t\tRadius: {ball.Radius:F2}\n\t\tVelocity X: {ball.VelocityX:F2}\n\t\tVelocity Y: {ball.VelocityY:F2}\n\t\tColor: {ball.Color}";
            lock (buffer)
            {
                buffer.Enqueue(log);
            }
        }

        public void BallCollisionWithWall(Ball ball, String side)
        {
            var timestamp = DateTime.Now.ToString("HH:mm:ss::ff");
            //Debug.Print("Ten kolor to: " + FindColour(Color.FromArgb(255, 255, 0, 0), out colorName));
            string log = "Error";
            switch (side)
            {
                case "up":
                    log = $"[{timestamp}]:Collision with upper wall detected:\n\t\tX: {ball.X:F2}\n\t\tY: {ball.Y:F2}\n\t\tRadius: {ball.Radius:F2}\n\t\tVelocity X: {ball.VelocityX:F2}\n\t\tVelocity Y: {ball.VelocityY:F2}\n\t\tColor: {ball.Color}";
                    break;
                case "down":
                    log = $"[{timestamp}]:Collision with lower wall detected:\n\t\tX: {ball.X:F2}\n\t\tY: {ball.Y:F2}\n\t\tRadius: {ball.Radius:F2}\n\t\tVelocity X: {ball.VelocityX:F2}\n\t\tVelocity Y: {ball.VelocityY:F2}\n\t\tColor: {ball.Color}";
                    break;
                case "left":
                    log = $"[{timestamp}]:Collision with left wall detected:\n\t\tX: {ball.X:F2}\n\t\tY: {ball.Y:F2}\n\t\tRadius: {ball.Radius:F2}\n\t\tVelocity X: {ball.VelocityX:F2}\n\t\tVelocity Y: {ball.VelocityY:F2}\n\t\tColor: {ball.Color}";
                    break;
                case "right":
                    log = $"[{timestamp}]:Collision with right wall detected:\n\t\tX: {ball.X:F2}\n\t\tY: {ball.Y:F2}\n\t\tRadius: {ball.Radius:F2}\n\t\tVelocity X: {ball.VelocityX:F2}\n\t\tVelocity Y: {ball.VelocityY:F2}\n\t\tColor: {ball.Color}";
                    break;
            }
            lock (buffer)
            {
                buffer.Enqueue(log);
            }
        }

        public void BallCollisionWithBall(Ball ballA, Ball ballB)
        {
            var timestamp = DateTime.Now.ToString("HH:mm:ss");
            string log = $"[{timestamp}]:Collision with ball detected:\n" +
                         $"\n\t\tBall A\n\t\tX: {ballA.X:F2}\n\t\tY: {ballA.Y:F2}\n\t\tRadius: {ballA.Radius:F2}\n\t\tVelocity X: {ballA.VelocityX:F2}\n\t\tVelocity Y: {ballA.VelocityY:F2}\n\t\tColor: {ballA.Color}" +
                         $"\n\n\t\tBall B\n\t\tX: {ballB.X:F2}\n\t\tY: {ballB.Y:F2}\n\t\tRadius: {ballB.Radius:F2}\n\t\tVelocity X: {ballB.VelocityX:F2}\n\t\tVelocity Y: {ballB.VelocityY:F2}\n\t\tColor: {ballB.Color}";
            lock (buffer)
            {
                buffer.Enqueue(log);
            }
        }


        private void WriteLoop()
        {
            while (running)
            {
                try
                {
                    string? line = null;
                    lock (buffer)
                    {
                        if (buffer.Count > 0)
                            line = buffer.Dequeue();
                    }

                    if (line != null)
                    {
                        File.AppendAllText(filePath, line + Environment.NewLine);
                    }
                    else
                    {
                        Thread.Sleep(50);
                    }
                }
                catch (IOException)
                {
                    Thread.Sleep(200);
                }
            }
        }




        /*
        enum MatchType
        {
            NoMatch,
            ExactMatch,
            ClosestMatch
        };
        
        static MatchType FindColour(Color colour, out string name)
        {
            MatchType
              result = MatchType.NoMatch;

            int least_difference = int.MaxValue;

            name = "";

            foreach (PropertyInfo system_colour in typeof(Color).GetProperties(BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy))
            {
                Color
                  system_colour_value = (Color)system_colour.GetValue(null, null);

                if (system_colour_value == colour)
                {
                    name = system_colour.Name;
                    result = MatchType.ExactMatch;
                    break;
                }

                int
                  a = colour.A - system_colour_value.A,
                  r = colour.R - system_colour_value.R,
                  g = colour.G - system_colour_value.G,
                  b = colour.B - system_colour_value.B,
                  difference = a * a + r * r + g * g + b * b;

                if (result == MatchType.NoMatch || difference < least_difference)
                {
                    result = MatchType.ClosestMatch;
                    name = system_colour.Name;
                    least_difference = difference;
                }
            }

            return result;
        }
        */
    }
}
