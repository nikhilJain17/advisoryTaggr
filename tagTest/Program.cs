using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagLib;

namespace tagTest
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                if (args[0].Equals("-h"))
                {
                    Console.WriteLine();
                    Console.WriteLine("AdvisoryTaggr");
                    Console.WriteLine("Version 1.0.0");
                    Console.WriteLine("Rishi Masand (2016)");
                    Console.WriteLine();
                    Console.WriteLine("Add iTunes Advisory tags to m4a files.");
                    Console.WriteLine();
                    Console.WriteLine("usage: advisorytaggr path tag");
                    Console.WriteLine("examples:");
                    Console.WriteLine();
                    Console.WriteLine("  advisorytaggr /path/to.m4a explicit");
                    Console.WriteLine("  advisorytaggr /path/to.m4a clean");
                    Console.WriteLine("  advisorytaggr /path/to.m4a none");
                    Console.WriteLine();
                }
                else if (args.Length == 2)
                {
                    string path = args[0];
                    string advisory = args[1];
                    switch (advisory)
                    {
                        case "explicit":
                            //make explicit (1)
                            List<string> ratingSeq = new List<string>(new string[] { "72", "74", "6E", "67", "00", "00", "00", "11", "64", "61", "74", "61", "00", "00", "00", "15", "00", "00", "00", "00" });
                            List<string> currentSeq = new List<string>();

                            FileStream fs = new FileStream(path, FileMode.Open);
                            int hexIn;
                            string hex;

                            int pos = 0;
                            bool getNext = false;

                            for (int i = 0; (hexIn = fs.ReadByte()) != -1; i++)
                            {
                                hex = string.Format("{0:X2}", hexIn);
                                //Console.WriteLine(hex);
                                currentSeq.Add(hex);
                                if (getNext)
                                {
                                    //Console.WriteLine(hex);
                                    break;
                                }
                                if (currentSeq.Count > ratingSeq.Count)
                                {
                                    currentSeq.RemoveAt(0);
                                }
                                if (Enumerable.SequenceEqual(ratingSeq, currentSeq))
                                {
                                    //Console.WriteLine("FOUND IT");
                                    //Console.WriteLine(currentSeq[currentSeq.Count - 1]);
                                    //Console.WriteLine(fs.Position);
                                    pos = (int)fs.Position;
                                    getNext = true;
                                }
                            }
                            fs.Position = pos;
                            fs.Write(new byte[] { 0x01 }, 0, 1);
                            Console.Write(path + " advisory changed to " + advisory);
                            break;
                        case "clean":
                            //make clean (2)
                            List<string> ratingSeqC = new List<string>(new string[] { "72", "74", "6E", "67", "00", "00", "00", "11", "64", "61", "74", "61", "00", "00", "00", "15", "00", "00", "00", "00" });
                            List<string> currentSeqC = new List<string>();

                            FileStream fsC = new FileStream(path, FileMode.Open);
                            int hexInC;
                            string hexC;

                            int posC = 0;
                            bool getNextC = false;

                            for (int i = 0; (hexInC = fsC.ReadByte()) != -1; i++)
                            {
                                hexC = string.Format("{0:X2}", hexInC);
                                //Console.WriteLine(hex);
                                currentSeqC.Add(hexC);
                                if (getNextC)
                                {
                                    //Console.WriteLine(hex);
                                    break;
                                }
                                if (currentSeqC.Count > ratingSeqC.Count)
                                {
                                    currentSeqC.RemoveAt(0);
                                }
                                if (Enumerable.SequenceEqual(ratingSeqC, currentSeqC))
                                {
                                    //Console.WriteLine("FOUND IT");
                                    //Console.WriteLine(currentSeq[currentSeq.Count - 1]);
                                    //Console.WriteLine(fs.Position);
                                    posC = (int)fsC.Position;
                                    getNextC = true;
                                }
                            }
                            fsC.Position = posC;
                            fsC.Write(new byte[] { 0x02 }, 0, 1);
                            Console.Write(path + " advisory changed to " + advisory);
                            break;
                        case "none":
                            //make none (0)
                            List<string> ratingSeqN = new List<string>(new string[] { "72", "74", "6E", "67", "00", "00", "00", "11", "64", "61", "74", "61", "00", "00", "00", "15", "00", "00", "00", "00" });
                            List<string> currentSeqN = new List<string>();

                            FileStream fsN = new FileStream(path, FileMode.Open);
                            int hexInN;
                            string hexN;

                            int posN = 0;
                            bool getNextN = false;

                            for (int i = 0; (hexInN = fsN.ReadByte()) != -1; i++)
                            {
                                hexN = string.Format("{0:X2}", hexInN);
                                //Console.WriteLine(hex);
                                currentSeqN.Add(hexN);
                                if (getNextN)
                                {
                                    //Console.WriteLine(hex);
                                    break;
                                }
                                if (currentSeqN.Count > ratingSeqN.Count)
                                {
                                    currentSeqN.RemoveAt(0);
                                }
                                if (Enumerable.SequenceEqual(ratingSeqN, currentSeqN))
                                {
                                    //Console.WriteLine("FOUND IT");
                                    //Console.WriteLine(currentSeq[currentSeq.Count - 1]);
                                    //Console.WriteLine(fs.Position);
                                    posN = (int)fsN.Position;
                                    getNextN = true;
                                }
                            }
                            fsN.Position = posN;
                            fsN.Write(new byte[] { 0x00 }, 0, 1);
                            Console.Write(path + " advisory changed to " + advisory);
                            break;
                        default:
                            Console.Write("Invalid Advisory Value. Please use \"explicit\", \"clean\", or \"none\"");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid command entered. Please use -h for help.");
                }
            }
            else
            {
                Console.WriteLine("No command entered. Please use -h for help.");
            }
        }
    }
}
