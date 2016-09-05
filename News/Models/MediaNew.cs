using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace News.Models
{
    public class MediaNew
    {
        [Display(Name = "Заголовок")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Required]
        [Editable(true)]
        public virtual string Title { get; set; }
        [Display(Name = "Источник")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Required]
        [Editable(true)]
        public virtual string Source { get; set; }
        [Display(Name = "Наименование")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Required]
        [Editable(true)]
        public virtual string Name { get; set; }
        public HttpPostedFileBase attachment { get; set; }

    }
}