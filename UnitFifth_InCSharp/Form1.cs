using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace UnitFifth_InCSharp
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            dgvTable.Rows.Add("Sequential", "Low", "Medium", "Low", "High");
            dgvTable.Rows.Add("Direct", "Medium", "Low", "Medium", "Low");
            dgvTable.Rows.Add("Indexed", "High", "High", "High", "Medium");
        }


        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Limpiar los gráficos antes de agregar nuevos datos
            chart1.Series.Clear();
            chart2.Series.Clear();
            chart3.Series.Clear();

            // Obtener los datos del DataGridView
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("File Organization", typeof(string));
            dataTable.Columns.Add("Initial Cost", typeof(string));
            dataTable.Columns.Add("Operating Cost", typeof(string));
            dataTable.Columns.Add("Security Level", typeof(string));
            dataTable.Columns.Add("Ease of Access", typeof(string));

            // Add example data
            dataTable.Rows.Add("Sequential", "Low", "Medium", "Low", "High");
            dataTable.Rows.Add("Direct", "Medium", "Low", "Medium", "Low");
            dataTable.Rows.Add("Indexed", "High", "High", "High", "Medium");

            // Definir los colores para cada estadística
            Color[] colors = { Color.Blue, Color.Red, Color.Green, Color.Orange };

            // Graficar cada organización en su respectivo chart
            GraficarOrganizacion(chart1, dataTable.Rows[0], colors);
            GraficarOrganizacion(chart2, dataTable.Rows[1], colors);
            GraficarOrganizacion(chart3, dataTable.Rows[2], colors);
        }

        private void GraficarOrganizacion(Chart chart, DataRow row, Color[] colors)
        {
            // Crear una nueva tabla para obtener el nombre de las columnas
            DataTable dataTable = row.Table;

            string organizacion = row.Field<string>("File Organization");

            // Verificar si hay al menos una serie en el gráfico
            if (chart.Series.Count > 0)
            {
                // Limpiar la serie existente si la hay
                chart.Series.Clear();
            }

            // Crear una serie para la organización actual
            Series serie = new Series(organizacion);
            serie.ChartType = SeriesChartType.Radar;

            // Agregar los puntos al gráfico (estadísticas y sus valores)
            for (int i = 1; i < row.ItemArray.Length; i++)
            {
                string estadistica = dataTable.Columns[i].ColumnName; // Obtener el nombre de la columna
                string valor = row.Field<string>(estadistica);
                int valorNumerico = ValorNumerico(valor); // Convertir la etiqueta a un valor numérico
                DataPoint point = serie.Points.Add(valorNumerico);
                point.AxisLabel = estadistica;
                point.Color = colors[i - 1];
            }

            // Agregar la serie al gráfico
            chart.Series.Add(serie);
        }

        // Método para convertir etiquetas "Low", "Medium", "High" a valores numéricos
        private int ValorNumerico(string etiqueta)
        {
            switch (etiqueta)
            {
                case "Low":
                    return 1;
                case "Medium":
                    return 2;
                case "High":
                    return 3;
                default:
                    return 0;
            }
        }




    }
}
