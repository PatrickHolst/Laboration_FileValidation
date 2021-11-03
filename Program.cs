using System;
using System.IO;

namespace Lab_3
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName = "";
            int size = 54;
            byte[] data = new byte[size];
            var buffer = new byte[8];
            bool menu = true;

            while (menu)
            {
                Console.WriteLine("\nChoose which file to validate:\n");

                Console.WriteLine("   1: patrick.png");
                Console.WriteLine("   2: flower.jpg");
                Console.WriteLine("   3: bitmap.bmp");
                Console.WriteLine("   4: textfile.txt");
                Console.WriteLine("   5: Upload own file.");
                Console.WriteLine("   6: Exit");

                Console.Write("\nChoice: ");
                string userMenuInput = Console.ReadLine();

                switch (userMenuInput)
                {
                    case "1":
                        fileName = @"..\..\patrick.png";
                        break;
                    case "2":
                        fileName = @"..\..\flower.jpg";
                        break;
                    case "3":
                        fileName = @"..\..\bitmap.bmp";
                        break;
                    case "4":
                        fileName = @"..\..\textfile.txt";
                        break;
                    case "5":
                        Console.WriteLine("\nEnter the path to the file you want to validate:");
                        Console.Write("   ");
                        fileName = Console.ReadLine();
                        break;                        
                    case "6":
                        menu = false;
                        break;
                    default:
                        Console.WriteLine("Invalid input. Try again.");
                        break;
                }
                try
                {
                    using (FileStream fs = File.OpenRead(fileName))
                    {
                        var width = 0;
                        var height = 0;

                        fs.Read(data, 0, data.Length);
                        if (data[0] == 137 && data[1] == 80 && data[2] == 78 && data[3] == 71)
                        {
                            fs.Position = 16;
                            fs.Read(buffer, offset: 0, buffer.Length);
                            width = BitConverter.ToInt32(new byte[] { buffer[3], buffer[2], buffer[1], buffer[0] });
                            height = BitConverter.ToInt32(new byte[] { buffer[7], buffer[6], buffer[5], buffer[4] });
                            Console.WriteLine($"\n  {fileName} is a valid PNG-file. Width x Height (px): {width}x{height}");

                        }
                        else if (data[0] == 66 && data[1] == 77)
                        {
                            width = data[18] + (data[19] * 256) +
                                   (data[20] * 256 * 256) +
                                   (data[21] * 256 * 256 * 256);

                            height = data[22] + (data[23] * 256) +
                                    (data[24] * 256 * 256) +
                                    (data[25] * 256 * 256 * 256);

                            Console.WriteLine($"\n  {fileName} is valid BMP-file. Width x Height (px): {width}x{height}");
                        }
                        else
                        {
                            Console.WriteLine($"\n{fileName} is not a valid BMP or PNG-file");
                        }
                    }
                    data = new byte[54];
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }           
        }
    }
}
