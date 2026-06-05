using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CompressAudioFiles.Models
{
    public class AudioMetadata
    {
        public long FileSize { get; set; }
        public TimeSpan Duration { get; set; }
        public int SampleRate { get; set; }
        public int ChannelsCount { get; set; }
        public int BitRate { get; set; }
        public string EncodingType { get; set; }
    }
}


