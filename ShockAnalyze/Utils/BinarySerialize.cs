using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ShockAnalyze
{
    /// <summary>
    /// BinarySerialize class,provider basic Serialize and Deserialize method.
    /// </summary>
    public class BinarySerialize<T>
    {
        private string _strFilePath = string.Empty;

        public void Serialize(T obj, string strFilePath)
        {
            _strFilePath = strFilePath;
            //FileInfo fi = new FileInfo(_strFilePath);
            //if (fi.Exists)
            //    throw new ArgumentException("File specified is exist already!");
            using (FileStream fs = new FileStream(_strFilePath, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, obj);
            }
        }

        /// <summary>
        /// Deserialize an instance of T.
        /// </summary>
        /// <typeparam name="T">Any type.</typeparam>
        /// <returns>The result of deserialized.</returns>
        public T DeSerialize(string filePath)
        {
            T t = default(T);

            FileInfo fi = new FileInfo(filePath);
            if (!fi.Exists) return t;
            //throw new ArgumentException("File specified is not exist!");

            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                try
                {
                    t = (T)formatter.Deserialize(fs);
                }
                catch (Exception ex)
                {
                    //throw ex;
                    Console.WriteLine(ex.ToString());
                }
            }

            return t;
        }

    }
}
