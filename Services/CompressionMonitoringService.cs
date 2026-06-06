using CompressAudioFiles.Models;
using System;
using System.Collections.Generic;

namespace CompressAudioFiles.Services
{
    public class CompressionMonitoringService
    {
        public event EventHandler<OperationProgressEventArgs> MonitoringUpdated;

        public List<double> CompressionRatioPoints { get; private set; } = new List<double>();
        public List<double> ProcessingSpeedPoints { get; private set; } = new List<double>();
        public List<double> ProgressPoints { get; private set; } = new List<double>();

        public void Reset()
        {
            CompressionRatioPoints.Clear();
            ProcessingSpeedPoints.Clear();
            ProgressPoints.Clear();
        }

        public void HandleProgress(object sender, OperationProgressEventArgs e)
        {
            ProgressPoints.Add(e.ProgressPercentage);
            CompressionRatioPoints.Add(e.CompressionRatio);
            ProcessingSpeedPoints.Add(e.ProcessingSpeed);

            MonitoringUpdated?.Invoke(this, e);
        }
    }
}