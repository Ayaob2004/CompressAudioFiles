using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompressAudioFiles.Models
{
    public class OperationProgressEventArgs : EventArgs
    {
        public string AlgorithmName { get; set; }
        public string OperationName { get; set; }
        public long ProcessedSamples { get; set; }
        public long TotalSamples { get; set; }
        public double ProgressPercentage { get; set; }
        public long OriginalSizeBytes { get; set; }
        public long CurrentOutputSizeBytes { get; set; }

        public double CompressionRatio { get; set; }
        public double ProcessingSpeed { get; set; }

        public TimeSpan ElapsedTime { get; set; }



    }
}
