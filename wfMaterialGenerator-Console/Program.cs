using sdPBR_WFMaterialGenerator;
using System;

if (args.Length < 2)
    Console.WriteLine("usage: wfMaterialGenerator-Console [source material directory] [destination material directory]");
else
    new WFMaterialGenerator(args[0], args[1]).Generate();
