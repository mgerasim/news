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
        [Display(Name = "Ключевые слова")]
        [StringLength(200, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Required]
        [Editable(true)]
        public virtual string Keywords { get; set; }
        [Display(Name = "Автор")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Required]
        [Editable(true)]
        public virtual string Author { get; set; }
        [Display(Name = "Содержимое")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Required]
        [Editable(true)]
        public virtual string Content { get; set; }
        public virtual int Displayed_Days { get; set; }

        public virtual DateTime Published_At { get; set; }
    }
}