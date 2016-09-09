using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace News.Models
{
    public class ArticleNew
    {
        [Display(Name = "Заголовок")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Required]
        [Editable(true)]
        public virtual string Title { get; set; }
        [Display(Name = "Анонс")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Required]
        [Editable(true)]
        public virtual string Anons { get; set; }
        [Display(Name = "Содержимое")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Required]
        [Editable(true)]
        public virtual string Content { get; set; }
    }
}