using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace gui
{
    public partial class SineWaveGui : Form
    {
        public SineWaveGui()
        {
            // generate
            Generate generateSin = new Generate();
            generateSin.generate();

            // analysis
            var pointsCount = generateSin.buffer.Where(x => x > generateSin.lineValue).Count();
            double? minX = generateSin.interval.Min(); 
            double? maxX = generateSin.interval.Max();
            string outputLine = "Under " + generateSin.lineValue  + " are" +  pointsCount +  " points in interval("+ minX+ " meter, "+ maxX  +" meter)";

            // gui
            InitializeComponent();
            sineWave.Series.Clear();         

            string seriesPoints = "points under " + generateSin.lineValue;
            Series serPoints = sineWave.Series.Add(seriesPoints);
            serPoints.ChartArea = sineWave.ChartAreas[0].Name;
            serPoints.Name = seriesPoints;
            serPoints.ChartType = SeriesChartType.Point;
            serPoints.Points.Add(generateSin.buffer);
            for (int n = Convert.ToInt32(minX * 10); n < Convert.ToInt32(maxX*10); n++)
                serPoints.Points.AddXY(n, generateSin.buffer[n]);

            string seriesOtherPoints = "other points ";
            Series serOtherPoints = sineWave.Series.Add(seriesOtherPoints);
            serOtherPoints.ChartArea = sineWave.ChartAreas[0].Name;
            serOtherPoints.Name = seriesOtherPoints;
            serOtherPoints.ChartType = SeriesChartType.Point;
            for (int n = 0; n < Convert.ToInt32(minX * 10); n++)
                serOtherPoints.Points.AddXY(n, generateSin.buffer[n]);
            for (int n = Convert.ToInt32(maxX * 10); n< generateSin.buffer.Length; n++)
                serOtherPoints.Points.AddXY(n, generateSin.buffer[n]);

            string seriesNameLine = "line";
            Series serLine = sineWave.Series.Add(seriesNameLine);
            serLine.ChartArea = sineWave.ChartAreas[0].Name;
            serLine.Name = seriesNameLine;
            serLine.ChartType = SeriesChartType.Line;
            serLine.Points.AddXY(0, generateSin.lineValue);
            serLine.Points.AddXY(generateSin.sampleRate, generateSin.lineValue);

            lbSinusoid.Text = outputLine;
        }
    }


    interface IGenerate
    {
        void generate();
    }

    class Generate : IGenerate
    {
        public double[] buffer;
        public double lineValue = 0.6;
        public int sampleRate;
        public double?[] interval;

        public void generate()
        {
            sampleRate = (int)(500 / 0.1);
            interval = new double?[sampleRate];
            buffer = new double[sampleRate];
            double amplitude = 1;
            double frequency = 1;
            for (int n = 0; n < buffer.Length; n++)
            {
                buffer[n] = (double)(amplitude * Math.Sin((2 * Math.PI * n * frequency) / sampleRate));
                if (buffer[n] > lineValue)
                {
                    interval[n] = (double)n / 10;
                }
                else
                {
                    interval[n] = null;
                }
            }
        }
    }
}
