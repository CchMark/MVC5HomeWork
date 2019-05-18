namespace MVC5HomeWork.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    [MetadataType(typeof(CustomerContactMetaData))]
    public partial class CustomerContact : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var db = new customerEntities();
            
            if (this.Id ==0)
            {
                if (db.CustomerContact.Where(p =>p.客戶Id==this.客戶Id
                &&p.Email == this.Email).Any())
                {
                    yield return new ValidationResult("Email 已存在", new string[] { "Email" });
                }
            }
            else
            {
                if (db.CustomerContact.Where(p => p.客戶Id == this.客戶Id
                && p.Id != this.Id &&p.Email == this.Email).Any())
                {
                    yield return new ValidationResult("Email 已存在",new string[] { "Email" });
                }
            }
            yield return ValidationResult.Success;
        }
    }

    public partial class CustomerContactMetaData
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int 客戶Id { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 職稱 { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 姓名 { get; set; }
        
        [StringLength(250, ErrorMessage="欄位長度不得大於 250 個字元")]
        [Required]
        public string Email { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [RegularExpression(@"\d{4}-\d{6}",ErrorMessage ="手機號碼格式必須為09XX-XXXX-XXXX")]
        public string 手機 { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        public string 電話 { get; set; }
    
        public virtual CustomerData CustomerData { get; set; }
    }
}
