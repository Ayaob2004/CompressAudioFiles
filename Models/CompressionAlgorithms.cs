using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompressAudioFiles.Models
{
    class CompressionAlgorithms
    {
        public const string NonlinearQuantization = "Nonlinear Quantization";
        public const string DPCM = "DPCM";
        public const string PredictiveDifferentialCoding = "Predictive Differential Coding";
        public const string DeltaModulation = "Delta Modulation";
        public const string AdaptiveDeltaModulation = "Adaptive Delta Modulation";
    }
}
