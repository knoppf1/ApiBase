
using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.IO;
using System.Drawing;
using CsvHelper;
using System.Linq;

namespace Tools
{
    public static class Coopertec
    {
        public static Nullable<DateTime> StrToDate(string value, string format = "dd/MM/yyyy", string cultureString = "pt-BR")
        {
            Nullable<DateTime> dt = null;
            if (value != null && value != "")
            {
                dt = DateTime.ParseExact(
                    s: value,
                    format: format,
                    provider: CultureInfo.GetCultureInfo(cultureString));
            }
            return dt;

        }

        public static Nullable<DateTime> StrToDateTime(string value, string format = "dd/MM/yyyy HH:mm:ss", string cultureString = "pt-BR")
        {
            Nullable<DateTime> dt = null;
            if (value != null && value != "")
            {
                dt = DateTime.ParseExact(
                    s: value,
                    format: format,
                    provider: CultureInfo.GetCultureInfo(cultureString));
            }
            return dt;

        }

        public static string DateToStr(DateTime? value)
        {
            try
            {
                return value == null ? "" : ((DateTime)value).ToString("dd/MM/yyyy");
            }
            catch (FormatException)
            {
                throw;
            }
        }

        public static string DateTimeToStr(DateTime? value)
        {
            try
            {
                return value == null ? "" : ((DateTime)value).ToString("dd/MM/yyyy HH:mm:ss");
            }
            catch (FormatException)
            {
                throw;
            }
        }

        public static string DatabaseToNumber(decimal value, int casasDecimal)
        {
            try
            {
                return value.ToString("N", CultureInfo.GetCultureInfo("pt-BR"));
            }
            catch (FormatException)
            {
                throw;
            }

        }


        public static decimal NumberToDatabase(string value)
        {
            try
            {
                decimal r = Convert.ToDecimal(value, CultureInfo.GetCultureInfo("pt-BR"));
                return r;
            }
            catch (FormatException)
            {
                throw;
            }

        }

        //public static image base64toimage(string base64string)
        //{
        //    // convert base 64 string to byte[]
        //    byte[] imagebytes = convert.frombase64string(base64string);
        //    // convert byte[] to image
        //    using (var ms = new memorystream(imagebytes, 0, imagebytes.length))
        //    {
        //        image image = image.fromstream(ms, true);
        //        return image;
        //    }
        //}

        public static string UploadImage(string ImageBase64, string ImageAtual, string PastaModel)
        {
            string file = "";

            if (ImageBase64 != "" && ImageBase64 != null)
            {
                //Gera uma hash aleatoria para o nome do arquivo
                string nomeImage = Guid.NewGuid().ToString() + ".jpg";
                string[] file64 = ImageBase64.Split(",");
                var bytes = Convert.FromBase64String(file64[1]);

                //Verifica se existe a pasta no servidor
                string filedir = Path.Combine("uploads", PastaModel);
                if (!Directory.Exists(filedir))
                {
                    //Se nao existe cria a pasta no servidor
                    Directory.CreateDirectory(filedir);
                }

                file = Path.Combine(filedir, nomeImage);


                if (bytes.Length > 0)
                {
                    using (var stream = new FileStream(file, FileMode.Create))
                    {
                        stream.Write(bytes, 0, bytes.Length);
                        stream.Flush();
                    }
                }

                //Deleta a Imagem anterior
                if (File.Exists(ImageAtual))
                {
                    File.Delete(ImageAtual);
                }
            }
            return file;
        }

        public static string ImageToBase64(string imageUrl)
        {
            string base64Image = "";
            if (imageUrl != null && imageUrl != "")
            {
                if (File.Exists(imageUrl))
                {
                    byte[] imageArray = System.IO.File.ReadAllBytes(@imageUrl);
                    base64Image = "data:image/jpg;base64," + Convert.ToBase64String(imageArray);
                }
            }
            return base64Image;
        }

        public static string UploadCSV(string FileBase64, string FileAtual, string PastaModel)
        {
            string file = "";

            if (FileBase64 != "" && FileBase64 != null)
            {
                //Gera uma hash aleatoria para o nome do arquivo
                string nomeImage = Guid.NewGuid().ToString() + ".csv";
                string[] file64 = FileBase64.Split(",");
                var bytes = Convert.FromBase64String(file64[1]);

                //Verifica se existe a pasta no servidor
                string filedir = Path.Combine(Directory.GetCurrentDirectory(), "uploads", PastaModel);
                if (!Directory.Exists(filedir))
                {
                    //Se nao existe cria a pasta no servidor
                    Directory.CreateDirectory(filedir);
                }

                file = Path.Combine(filedir, nomeImage);


                if (bytes.Length > 0)
                {
                    using (var stream = new FileStream(file, FileMode.Create))
                    {
                        stream.Write(bytes, 0, bytes.Length);
                        stream.Flush();
                    }
                }

                //Deleta a Imagem anterior
                if (FileAtual != null)
                {
                    if (File.Exists(FileAtual))
                    {
                        File.Delete(FileAtual);
                    }
                }
            }

            List<CsvLine> lines = new List<CsvLine>();

            try
            {
                using (var reader = new StreamReader(file))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {

                    while (csv.Read())
                    {
                        string row = csv.GetField(0);
                        string[] line = row.Split(";");
                        string nome = line[0];
                        string tel = line[1];

                    }
                    //var records = csv.GetRecords<dynamic>();
                    //records.ToList().ForEach(row =>
                    //{
                    //    string[] l = row.
                    //    string nome = l[0];
                    //    string telefone = l[1];
                    //}
                    //);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return file;
        }

        public static string ArchiveToBase64(string archiveUrl)
        {
            string base64Archive = "";
            if (archiveUrl != null && archiveUrl != "")
            {
                if (File.Exists(archiveUrl))
                {
                    byte[] archiveArray = System.IO.File.ReadAllBytes(archiveUrl);
                    base64Archive = Convert.ToBase64String(archiveArray);
                }
            }
            return base64Archive;
        }

        //Salva o Arquivo e retorna o caminho do arquivo
        public static string UploadArchive(string FileBase64, string FileAtual, string PastaModel)
        {
            string file = "";

            if (FileBase64 != "" && FileBase64 != null)
            {
                //Extrai o tipo de extensão do arquivo
                string[] file64 = FileBase64.Split(",");
                string[] filestamp = file64[0].Split(";");
                string[] extensao = filestamp[0].Split("/");

                //Gera uma hash aleatoria para o nome do arquivo
                string nomeArquivo = Guid.NewGuid().ToString() + "." + extensao[1];
                var bytes = Convert.FromBase64String(file64[1]);
               
                //Verifica se existe a pasta no servidor
                string filedir = Path.Combine("uploads", PastaModel);
                if (!Directory.Exists(filedir))
                {
                    //Se nao existe cria a pasta no servidor
                    Directory.CreateDirectory(filedir);
                }

                file = Path.Combine(filedir, nomeArquivo);

                if (bytes.Length > 0)
                {
                    using (var stream = new FileStream(file, FileMode.Create))
                    {
                        stream.Write(bytes, 0, bytes.Length);
                        stream.Flush();
                    }
                }

                //Deleta a Arquivo Anterior
                if (FileAtual != null)
                {
                    if (File.Exists(FileAtual))
                    {
                        File.Delete(FileAtual);
                    }
                }
            }

            return file;
        }

    }
}




public class CsvLine
{
    public string Nome { get; set; }
    public string Telefone { get; set; }

    public override string ToString()
    {
        return $"Cliente {Nome}: Tel: {Telefone}";
    }
}