using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models.EntityModels;

namespace Core.Models.EntityModels
{
    public class Upload
    {
        public int UploadId { get; set; }

        [Required]
        [Column(TypeName = "varchar")]
        [Display(Name = "Drive")]
        [StringLength(5, MinimumLength = 2)]
        public string DriveLetter { get; set; }

        [Required]
        [Column(TypeName = "varchar")]
        [StringLength(150, MinimumLength = 2)]
        public string Title { get; set; }

        [ForeignKey("Category")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        
        [ForeignKey("SubCategory")]
        [Display(Name = "Sub Category")]
        public int? SubCategoryId { get; set; }
        public virtual SubCategory SubCategory { get; set; }


        //Base path, that will be generated

        [Required]
        [Column(TypeName = "varchar")]
        public string UploadPath { get; set; }
        public ICollection<FileInfo> FileInfos { get; set; }

        public byte?[] Thumbnail { get; set; }

        [Display(Name = "Publish Date")]
        public DateTime PublishDate { get; set; }

        [Display(Name = "Last Update")]
        public DateTime LastUpdate { get; set; }

    }
}
