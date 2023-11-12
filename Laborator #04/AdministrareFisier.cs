using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// ======================
// Laborator #04
// Bîrsan Dorin-Alexandru
// grupa 3132a
// ======================

namespace Laborator__04
{
    internal class AdministrareFisier
    {
        private string numeFisier;

        public AdministrareFisier(string numeFisier)
        {
            this.numeFisier = numeFisier;
            Stream streamFisierText = File.Open(numeFisier, FileMode.OpenOrCreate);
            streamFisierText.Close();
        }

        public int[,] GetCoordonate()
        {
            int[,] coordonate = new int[3, 36];

            using (StreamReader streamReader = new StreamReader(numeFisier))
            {
                string linieFisier;
                int i = 0;

                while ((linieFisier = streamReader.ReadLine()) != null)
                {
                    string[] values = linieFisier.Split(',');
                    for (int j = 0; j < 3; j++)
                    {
                        coordonate[i / 12, (i % 12) * 3 + j] = int.Parse(values[j].Trim());
                    }

                    i++;

                }
            }

            return coordonate;
        }
    }
}
