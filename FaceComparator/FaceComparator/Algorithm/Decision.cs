using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FaceComparator.Algorithm
{
    [Serializable]
    class Decision
    {
        public BitmapImage Image { get; set; }
        public BitmapImage Thumbnail { get; set; }

        public Decision(BitmapImage img)
        {
            Image = img;
            Thumbnail = img;
        }
    }
}
