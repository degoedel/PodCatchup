﻿using LiteDB;
using PodCatchup.Infrastructure;
using PodCatchup.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodCatchup.Services
{
  public class LiteDBLibraryLoader : ILibraryLoader
  {
    public PodcastLibrary LoadLibrary()
    {
      String savefilename = "PodCatchup.db";
      if (!File.Exists(".portable"))
      {
        String mydocs = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        String appdir = Path.Combine(mydocs, "PodCatchup");
        savefilename = Path.Combine(appdir, "PodCatchup.db");
      }
      if (!File.Exists(savefilename))
      {
        return new PodcastLibrary();
      }
      using (var db = new LiteDatabase(savefilename))
      {
        var col = db.GetCollection<PodcastLibrary>("library");
        if (col.Count() > 0)
        {
          return col.FindAll().First();
        }
        else
        { 
          return new PodcastLibrary(); 
        }
      }
    }
  }
}
