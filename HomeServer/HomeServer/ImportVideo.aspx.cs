using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Net.Mail;
using System.Net;
using System.Diagnostics;
using System.Text;

namespace HomeServer
{
    public partial class ImportVideo : System.Web.UI.Page
    {
        private string extension = ".mp4";
        //Begin in this directory
        private string toConvertFolder = @"D:\FilesToConvert\";
        //Output to this directoy
        private string outputFolder = @"D:\ConvertedFiles\";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Button1.Enabled = false;
            //Grab list of directories 
            string[] directories = Directory.GetDirectories(toConvertFolder);

            //For files in root folder
            string[] files = Directory.GetFiles(toConvertFolder);
            foreach(string file in files)
            {
                buildFileName(toConvertFolder, file);
            }
            //For files in subfolders
            foreach(string directory in directories)
            {
                files = Directory.GetFiles(directory);

                foreach (string file in files)
                {
                    buildFileName(directory, file);
                }
            }
        }

        private void buildFileName(string directory, string file)
        {
            string[] splitFile = file.Split('\\');

            string title = splitFile[splitFile.Length - 1];

            string[] dotSplit = title.Split('.');

            string ext = "." + dotSplit[dotSplit.Length - 1];

            //In case filename has multiple '.'
            title = dotSplit[0];
            for (int x = 1; x < dotSplit.Length - 1; x++)
            {
                //Don't forget the extra '.'s!
                title = title + '.' + dotSplit[x];
            }

            bool fileAlreadyConverted = false;
            string[] convertedFiles = Directory.GetFiles(outputFolder);
            foreach (string convFile in convertedFiles)
            {
                string[] convSplit = convFile.Split('\\');
                if (title + extension == convSplit[convSplit.Length - 1])
                {
                    fileAlreadyConverted = true;
                }
            }
            if (!fileAlreadyConverted)
            {
                //Build source file path
                string sourceFile = directory + "\\" + title + ext;
                //Split directory
                string[] dir = directory.Split('\\');
                string outputFile = "";
                //If last folder name is equal to the base output folder then don't add it to output file
                if (outputFolder == dir[dir.Length - 1])
                    outputFile = outputFolder + title + extension;
                else
                    outputFile = outputFolder + dir[dir.Length - 1] + '\\' + title + extension;

                ffmpegSpawn(sourceFile, outputFile);
            }
        }

        private void ffmpegSpawn(string sourceFile, string outputFile)
        {
            System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo();
            
            info.FileName = @"D:/Programs/ffmpeg/bin/ffmpeg.exe";

            if (extension == ".m4v")
                info.Arguments = "-i \"" + sourceFile + "\" \"" + outputFile + "\"";
            else
                info.Arguments = "-i \"" + sourceFile + "\" -vcodec h264 -acodec aac -threads 4 -strict -2 \"" + outputFile + "\"";

            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo = info;
            p.StartInfo.Verb = "runas";
            p.StartInfo.UseShellExecute = false;
            //p.StartInfo.CreateNoWindow = true;

            StringBuilder outputBuilder = new StringBuilder();
            p.StartInfo.RedirectStandardOutput = true;
            p.EnableRaisingEvents = true;
            p.OutputDataReceived += new DataReceivedEventHandler
            (
                delegate(object s, DataReceivedEventArgs drE)
                {
                    // append the new data to the data already read-in
                    outputBuilder.Append(drE.Data);
                }
            );
            p.Start();
            p.BeginOutputReadLine();
            p.WaitForExit();
            p.CancelOutputRead();
            while (!p.HasExited)
            {

            }
            SendEmail(sourceFile, outputFile);
        }

        private void SendEmail(string sourceFile, string outputFile)
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("rthsvr@gmail.com", "K4tIpgxrbi1"),
                EnableSsl = true
            };
            client.Send("rthsvr@gmail.com", "ryantimmons.cg@gmail.com", sourceFile + " Conversion Complete!", sourceFile + " was successfully converted to " + outputFile + " at " + DateTime.Now);
        }
    }
}
