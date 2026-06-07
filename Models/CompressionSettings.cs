using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompressAudioFiles.Models
{
    class CompressionSettings
    {
        public string AlgorithmName { get; set; }
        public int SampleRate { get; set; }
        public int QuantizationLevels { get; set; }
        public int BitRate { get; set; }
        //FARAH RAM
        public int DeltaStep { get; set; }

        //Aya
        public int PredictiveQuantizationStep { get; set; }

        public CompressionSettings()
        {
            AlgorithmName = null;
            SampleRate = 44100;
            QuantizationLevels = 2;
            BitRate = 1;
            DeltaStep = 1000;
            PredictiveQuantizationStep = 256;
        }






    }
}
