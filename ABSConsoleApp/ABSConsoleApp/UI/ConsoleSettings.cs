namespace ABSConsoleApp.UI
{
    using System;
    public static class ConsoleSettings
    {
        public static int Width { get; set; }
        public static int Height { get; set; }

        public static string Title
        {
            get
            {
                return Console.Title;
            }
            set
            {
                Console.Title = value;
            }
        }
        public static void SetSize() => Console.SetWindowSize(Width, Height);
        public static void Clear() => Console.Clear();
        public static int ConsolePosstionRow() => Console.CursorTop;
        public static void SetPossition(int row, int colm)
        {
            var setPossitionColm = colm < 1 ? 1 : colm;
            var setPossitionRow = row < 0 ? 0 : row;
            Console.SetCursorPosition(setPossitionRow, setPossitionColm);
        }

        public static ConsoleKeyInfo Key => Console.ReadKey();

        public static void HighlightLine()
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;
        }

        public static void ResetColor() => Console.ResetColor();
    }
}
