using System;
using Core.Interfaces;
using System.IO;
using System.Xml.Serialization;

namespace iOS.Implementations
{


    public class FileSystem : iFileSystem
    {
        public bool exist(string fileName)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string filepath = Path.Combine(path, fileName);

            return System.IO.File.Exists(filepath);
        }

        public object deserilaze(XmlSerializer xmlSerializer, string fileName)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string filepath = Path.Combine(path, fileName);

            string text;
            try
            {
                text = System.IO.File.ReadAllText(filepath);

            }
            catch (Exception)
            {
                return null;
            }

            using (var stream = new StringReader(text))
            {

                var data = xmlSerializer.Deserialize(stream);
                return data;
            }

        }

        #region iFileSystem implementation
        public void serilaze(System.Xml.Serialization.XmlSerializer xml, string fileName, object list)
        {

            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string filepath = Path.Combine(path, fileName);

            Stream writer = new FileStream(filepath, FileMode.Create);//initialises the writer

            xml.Serialize(writer, list);//Writes to the file

            writer.Close();//Closes the writer

        }
        #endregion




    }

}

