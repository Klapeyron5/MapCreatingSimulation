using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapCreation
{
    /// <summary>
    /// Карта помещения - общая для всех режимов приложения
    /// </summary>
    public class Environment
    {
        public Environment(String s)
        {
            if (s != null)
            {
                try
                {
                    preciseMap = new PixelMap(s);
                    loaded = 1;
                }
                catch (System.IO.FileNotFoundException e)
                {
                    loaded = 0;
                }
                catch (FormatException e)
                {
                    loaded = 2;
                }
            }
            else
                loaded = 0;
        }

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

        /// <summary>
        /// Если файл оригинальной карты существует по указанному в конструкторе пути, то true.
        /// Иначе карта не загружена и false.
        /// </summary>
        private byte loaded;

        public void getScan()
        {

        }

        public void canRobotStayOnThisPoint()
        {

        }

        /// <summary>
        /// Если файл оригинальной карты существует по указанному в конструкторе пути, то true.
        /// Иначе карта не загружена и false.
        /// </summary>
        public byte isMapLoaded()
        {
            return loaded;
        }
    }
}
