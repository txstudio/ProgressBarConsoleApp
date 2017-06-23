using HttpSimulator;
using System;
using System.Threading;

namespace ConsoleApp
{
    class Program
    {
        static int _positionTop;
        static int _positionLeft;
        static int _presentLeft;
        static int _presentTotal = 100;


        static void Main(string[] args)
        {
            ImageDownloader _downloader;

            _downloader = new ImageDownloader();
            _downloader.DowloadValueChange += _downloader_DowloadValueChange;
            _downloader.DownloadStarting += _downloader_DownloadStarting;
            _downloader.DownloadFinish += _downloader_DownloadFinish;
            _downloader.Download(@"http://url/");

            Console.WriteLine("press any key to exit");
            Console.ReadKey();
        }


        //檔案下載開始事件
        private static void _downloader_DownloadStarting(object sender, ImageDownloadStartEventArgs e)
        {
            Console.WriteLine("start <\"{0}\"> downloading", e.Url);
            Console.Write("[");

            _positionTop = Console.CursorTop;
            _positionLeft = Console.CursorLeft;

            for (int i = 0; i <= 25; i++)
            {
                Console.Write(" ");
            }
            Console.Write("] ");

            _presentLeft = Console.CursorLeft;

            Console.Write("   /100 (%)");
        }

        //下載作業完成事件
        private static void _downloader_DownloadFinish(object sender, ImageDownloadFinishEventArgs e)
        {
            Console.WriteLine();
            Console.WriteLine("download finished");
        }

        //下載進度變更事件
        private static void _downloader_DowloadValueChange(object sender, ImageDownloadChangeEventArgs e)
        {
            var _currentValue = e.CurrentValue;
            var _tick = Convert.ToInt32(_currentValue * 25 / _presentTotal);

            Console.SetCursorPosition(_positionLeft + _tick, _positionTop);
            Console.Write("-");

            Console.SetCursorPosition(_presentLeft, _positionTop);
            Console.Write(_currentValue.ToString().PadLeft(3,' '));
        }
    }
}