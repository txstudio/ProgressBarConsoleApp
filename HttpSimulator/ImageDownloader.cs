using System;
using System.Threading;

namespace HttpSimulator
{
    #region 事件狀態物件
    
    public class ImageDownloadStartEventArgs
    {
        private string _url;

        public ImageDownloadStartEventArgs(string url)
        {
            this._url = url;
        }

        public string Url
        {
            get
            {
                return this._url;
            }
        }
    }

    public class ImageDownloadFinishEventArgs { }

    public class ImageDownloadChangeEventArgs
    {
        private int _currentValue;

        public ImageDownloadChangeEventArgs(int currentValue)
        {
            this._currentValue = currentValue;
        }

        public int CurrentValue
        {
            get
            {
                return this._currentValue;
            }
        }
    }

    #endregion


    /// <summary>
    /// 模擬下載物件
    /// </summary>
    public class ImageDownloader
    {
        const int sleepTick = 250;
        const int presentTotal = 100;

        //註冊事件：方法-1
        //public delegate void DownloadChangeEventHandler(object sender, ImageDownloadChangeEventArgs e);
        //public event DownloadChangeEventHandler DowloadValueChange;

        //public delegate void DownloadStartEventHandler(object sender, ImageDownloadStartEventArgs e);
        //public event DownloadStartEventHandler DownloadStarting;

        //public delegate void DownloadFinishEventHandler(object sender, ImageDownloadFinishEventArgs e);
        //public event DownloadFinishEventHandler DownloadFinish;

        //註冊事件：方法-2
        public event EventHandler<ImageDownloadChangeEventArgs> DowloadValueChange;
        public event EventHandler<ImageDownloadStartEventArgs> DownloadStarting;
        public event EventHandler<ImageDownloadFinishEventArgs> DownloadFinish;
        


        public void Download(string url)
        {
            int _currentValue = 0;

            this.DownloadStarting(this, new ImageDownloadStartEventArgs(url));

            while(_currentValue < presentTotal)
            {
                _currentValue = _currentValue + 1;

                if(DowloadValueChange != null)
                    this.DowloadValueChange(this, new ImageDownloadChangeEventArgs(_currentValue));

                Thread.Sleep(sleepTick);
            }

            this.DownloadFinish(this, new ImageDownloadFinishEventArgs());
        }

    }
}
