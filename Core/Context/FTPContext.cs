﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models.EntityModels;

namespace Core.Context
{
    public class FTPContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Upload> Uploads { get; set; }
        public DbSet<FileInfo> FileInfos { get; set; }
    }
}
