using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompressAudioFiles.Models
{
    class CompressionResult
    {
        public string CompressedFilePath { get; set; }
        public long OriginalFileSize { get; set; }
        public long CompressedFileSize { get; set; }
        public double CompressionRatio { get; set; }
        public TimeSpan CompressionTime { get; set; }
        public string AlgorithmName { get; set; }
        public CompressionSettings UsedSettings { get; set; }
    }
}
