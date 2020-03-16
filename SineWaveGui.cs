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
            InitializeComponent();

            int sampleRate = (int)(500 / 0.1);
            double lineValue = 0.6;
            sineWave.Series.Clear();         

            string seriesPoints = "points under " + lineValue;
            Series serPoints = sineWave.Series.Add(seriesPoints);

            serPoints.ChartArea = sineWave.ChartAreas[0].Name;
            serPoints.Name = seriesPoints;
            serPoints.ChartType = SeriesChartType.Point;

            string seriesOtherPoints = "other points ";
            Series serOtherPoints = sineWave.Series.Add(seriesOtherPoints);

            serOtherPoints.ChartArea = sineWave.ChartAreas[0].Name;
            serOtherPoints.Name = seriesOtherPoints;
            serOtherPoints.ChartType = SeriesChartType.Point;

            double?[] interval = new double?[sampleRate];
            double[] buffer = new double[sampleRate];
            double amplitude = 1;
            double frequency = 1;
            for (int n = 0; n < buffer.Length; n++)
            {
                buffer[n] = (double)(amplitude * Math.Sin((2 * Math.PI * n * frequency) / sampleRate));
                if (buffer[n] > lineValue)
                {
                    serPoints.Points.AddXY(n, buffer[n]);
                    interval[n] = (double) n/10;
                }
                else
                {
                    interval[n] = null;
                    serOtherPoints.Points.AddXY(n, buffer[n]);
                }    
            }

            string seriesNameLine = "line";
            Series serLine = sineWave.Series.Add(seriesNameLine);

            serLine.ChartArea = sineWave.ChartAreas[0].Name;
            serLine.Name = seriesNameLine;
            serLine.ChartType = SeriesChartType.Line;
            serLine.Points.AddXY(0, lineValue);
            serLine.Points.AddXY(sampleRate, lineValue);

            var pointsCount = buffer.Where(x => x > lineValue).Count();

            var minX = interval.Min(); 
            var maxX = interval.Max();
            lbSinusoid.Text = "Under " + lineValue  + " are" +  pointsCount +  " points in interval ("+ minX+ " meter,"+ maxX  +" meter)";
        }
    }
}
