using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapCreation
{
    class Environment
    {
        /// <summary>
        /// Точная карта
        /// </summary>
        private PixelMap preciseMap;

        /// <summary>
        /// Точная карта indoor-среды: с отступами от препятствий точной карты
        /// </summary>
        private PixelMap preciseIndoorMap;

        /// <summary>
        /// Составленная роботом карта
        /// </summary>
        private PixelMap predictedMap;


        public void getScan()
        {

        }

        public void canRobotStayOnThisPoint()
        {

        }
    }
}
