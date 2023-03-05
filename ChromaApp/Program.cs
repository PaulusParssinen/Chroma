﻿using Chroma;
using System;
using System.IO;

ChromaFurniture furni;

furni = new ChromaFurniture(@"C:\Users\Alex\Documents\GitHub\Havana\tools\www\r38\dcr\hof_furni\rare_icecream_campaign.swf", isSmallFurni: false, renderState: 0, renderDirection: 2, colourId: -1);
furni.Run();

File.WriteAllBytes(furni.GetFileName(), furni.CreateImage());

furni = new ChromaFurniture(@"C:\Users\Alex\Documents\GitHub\Havana\tools\www\r38\dcr\hof_furni\rare_icecream.swf", isSmallFurni: false, renderState: 2, renderDirection: 0, colourId: -1);
furni.Run();

File.WriteAllBytes(furni.GetFileName(), furni.CreateImage());

/*furni = new ChromaFurniture(@"C:\Users\Alex\Documents\Habbo Furni\gamedata-2020-14-12\swf_furni\doorB.swf", isSmallFurni: false, renderState: 2, renderDirection: 0, colourId: -1);
furni.Run();

File.WriteAllBytes(furni.GetFileName(), furni.CreateImage());*/

Console.WriteLine("Finished conversion");
Console.Read();